from __future__ import annotations

import json
import os
import subprocess
import sys
import time
import uuid
from datetime import datetime
from pathlib import Path
from typing import Any



FINAL_STATUSES = {"completed", "stopped", "failed"}


def _pid_alive(pid: Any) -> bool:
    try:
        value = int(pid)
        if value <= 0:
            return False
    except (TypeError, ValueError):
        return False
    if value == os.getpid():
        return True
    if os.name == "nt":
        result = subprocess.run(
            ["tasklist", "/FI", f"PID eq {value}", "/FO", "CSV", "/NH"],
            capture_output=True, text=True, encoding="utf-8", errors="ignore",
            creationflags=subprocess.CREATE_NO_WINDOW,
        )
        output = (result.stdout or "").strip()
        return bool(output and "No tasks" not in output and "INFO:" not in output)
    try:
        os.kill(value, 0)
        return True
    except (ProcessLookupError, PermissionError):
        return False


class BatchManager:
    def __init__(self, root: Path) -> None:
        self.root = root
        self.directory = root / "state" / "batches"
        self.directory.mkdir(parents=True, exist_ok=True)

    def path(self, batch_id: str) -> Path:
        return self.directory / f"{batch_id}.json"

    def read(self, batch_id: str) -> dict[str, Any]:
        path = self.path(batch_id)
        if not path.exists():
            return {"success": False, "status": "not_found", "batch_id": batch_id}
        return json.loads(path.read_text(encoding="utf-8"))

    def write(self, state: dict[str, Any]) -> None:
        state["updated_at"] = datetime.now().astimezone().isoformat(timespec="seconds")
        path = self.path(str(state["batch_id"]))
        temp = path.with_suffix(".tmp")
        temp.write_text(json.dumps(state, ensure_ascii=False, indent=2), encoding="utf-8")
        os.replace(temp, path)

    def update(self, batch_id: str, **changes: Any) -> dict[str, Any]:
        state = self.read(batch_id)
        if state.get("status") == "not_found":
            return state
        state.update(changes)
        self.write(state)
        return state

    def active(self) -> dict[str, Any] | None:
        for path in sorted(self.directory.glob("batch-*.json"), key=lambda p: p.stat().st_mtime, reverse=True):
            try:
                state = json.loads(path.read_text(encoding="utf-8"))
            except Exception:
                continue
            if state.get("status") in FINAL_STATUSES:
                continue

            # Recover the exact zombie produced by older versions: Stop killed
            # the batch process itself before it could write status=stopped.
            if state.get("stop_requested"):
                worker_alive = _pid_alive(state.get("worker_pid"))
                launcher_alive = _pid_alive(state.get("launcher_pid"))
                if not worker_alive and not launcher_alive:
                    try:
                        from core.batch_report import write_reports
                        reports = write_reports(
                            self.root / "batch_results",
                            str(state.get("batch_id")),
                            list(state.get("results") or []),
                        )
                    except Exception:
                        reports = dict(state.get("reports") or {})
                    state.update({
                        "status": "stopped",
                        "reports": reports,
                        "finished_at": datetime.now().astimezone().isoformat(timespec="seconds"),
                        "message": "已回收先前未正常釋放的停止批次。",
                        "current_operation_id": None,
                        "current_operation_status": None,
                        "current_operation_stage": None,
                    })
                    self.write(state)
                    continue
            return state
        return None

    def latest(self) -> dict[str, Any] | None:
        paths = sorted(self.directory.glob("batch-*.json"), key=lambda p: p.stat().st_mtime, reverse=True)
        return self.read(paths[0].stem) if paths else None

    def start(
        self,
        items: list[dict[str, Any]],
        continue_on_error: bool = True,
        name: str | None = None,
        notification_target: str | None = None,
        *,
        base_items: list[dict[str, Any]] | None = None,
        repeat_count: int = 1,
        expected_total: int | None = None,
        allow_restart: bool = False,
    ) -> dict[str, Any]:
        if not items:
            raise ValueError("批次測試至少需要一筆測項。")
        if repeat_count < 1:
            raise ValueError("repeat_count 必須大於或等於 1。")

        # Prevent an autonomous agent from stopping a just-started bad plan and
        # silently launching another batch without a new explicit user request.
        # A user-requested restart remains available through allow_restart=True.
        latest = self.latest()
        if latest and not allow_restart:
            latest_status = str(latest.get("status") or "")
            latest_completed = int(latest.get("completed") or 0)
            latest_total = int(latest.get("total") or 0)
            timestamp = (
                latest.get("finished_at")
                or latest.get("updated_at")
                or latest.get("created_at")
            )
            age_sec = None
            if timestamp:
                try:
                    age_sec = (
                        datetime.now().astimezone()
                        - datetime.fromisoformat(str(timestamp))
                    ).total_seconds()
                except (TypeError, ValueError):
                    age_sec = None
            if (
                latest_status in {"stopped", "failed"}
                and latest_total > 1
                and latest_completed <= 1
                and age_sec is not None
                and 0 <= age_sec <= 600
            ):
                raise RuntimeError(
                    "最近一個批次只執行了前 1 筆就停止。系統不會自動重開另一個批次；"
                    "請由使用者明確要求『重新開始／重跑』後，再以 allow_restart=true 啟動。"
                )

        active = self.active()
        if active:
            raise RuntimeError(f"已有批次測試執行中：{active.get('batch_id')}")
        # Do not start a batch while a normal durable operation is controlling
        # the same callbox.
        from core.operation_store import OperationStore
        active_operations = OperationStore(self.root).find_active()
        if active_operations:
            raise RuntimeError(
                f"目前已有單筆 Operation 執行中：{active_operations[0].get('operation_id')}"
            )
        batch_id = f"batch-{datetime.now().strftime('%Y%m%d-%H%M%S')}-{uuid.uuid4().hex[:6]}"
        # Do not run `openclaw sessions` synchronously inside the MCP tool call.
        # Doing so can wait on the currently active OpenClaw request and leave
        # the UI stuck at "Start Batch ... running" before the first Band runs.
        # The detached batch worker resolves the recent Telegram target after
        # this start call has already returned.
        resolved_target = str(notification_target).strip() if notification_target else None
        state = {
            "success": True,
            "batch_id": batch_id,
            "name": name or batch_id,
            "status": "queued",
            "pause_requested": False,
            "stop_requested": False,
            "continue_on_error": bool(continue_on_error),
            "base_item_count": len(base_items or items),
            "repeat_count": int(repeat_count),
            "expected_total": int(expected_total) if expected_total is not None else len(items),
            "total": len(items),
            "completed": 0,
            "next_index": 0,
            "base_items": list(base_items or items),
            "items": items,
            "results": [],
            "created_at": datetime.now().astimezone().isoformat(timespec="seconds"),
            "reports": {},
            "notification_target": resolved_target,
        }
        self.write(state)
        env = os.environ.copy(); env["PYTHONIOENCODING"] = "utf-8"; env["PYTHONUTF8"] = "1"
        flags = 0
        if os.name == "nt":
            flags = subprocess.CREATE_NO_WINDOW | subprocess.DETACHED_PROCESS | subprocess.CREATE_NEW_PROCESS_GROUP
        process = subprocess.Popen(
            [sys.executable, "-u", "-m", "core.batch_worker", batch_id],
            cwd=str(self.root), stdin=subprocess.DEVNULL,
            stdout=(self.directory / f"{batch_id}.stdout.log").open("ab"),
            stderr=(self.directory / f"{batch_id}.stderr.log").open("ab"),
            creationflags=flags, close_fds=True, env=env,
        )
        self.update(batch_id, launcher_pid=process.pid)
        return self.read(batch_id)

    def pause(self, batch_id: str | None = None) -> dict[str, Any]:
        state = self.read(batch_id) if batch_id else self.active()
        if not state: return {"success": False, "status": "not_found"}
        if state.get("status") in FINAL_STATUSES: return state
        return self.update(str(state["batch_id"]), pause_requested=True, message="已要求暫停；目前測項完成後暫停。")

    def resume(self, batch_id: str | None = None) -> dict[str, Any]:
        state = self.read(batch_id) if batch_id else self.latest()
        if not state: return {"success": False, "status": "not_found"}
        if state.get("status") != "paused": return state
        self.update(str(state["batch_id"]), pause_requested=False, stop_requested=False, status="queued", message="準備繼續批次測試。")
        env = os.environ.copy(); env["PYTHONIOENCODING"] = "utf-8"; env["PYTHONUTF8"] = "1"
        flags = 0
        if os.name == "nt": flags = subprocess.CREATE_NO_WINDOW | subprocess.DETACHED_PROCESS | subprocess.CREATE_NEW_PROCESS_GROUP
        subprocess.Popen([sys.executable, "-u", "-m", "core.batch_worker", str(state["batch_id"])], cwd=str(self.root), stdin=subprocess.DEVNULL,
                         stdout=(self.directory / f"{state['batch_id']}.stdout.log").open("ab"), stderr=(self.directory / f"{state['batch_id']}.stderr.log").open("ab"),
                         creationflags=flags, close_fds=True, env=env)
        time.sleep(0.2)
        return self.read(str(state["batch_id"]))

    def stop(self, batch_id: str | None = None) -> dict[str, Any]:
        state = self.read(batch_id) if batch_id else self.active()
        if not state:
            return {"success": False, "status": "not_found"}
        if state.get("status") in FINAL_STATUSES:
            return state

        batch_id_value = str(state["batch_id"])
        state = self.update(
            batch_id_value,
            stop_requested=True,
            pause_requested=False,
            status="stopping",
            message="正在取消目前測項；完成清理後立即停止並輸出報表。",
        )

        current_operation_id = state.get("current_operation_id")
        if current_operation_id:
            try:
                from core.operation_manager import OperationManager
                OperationManager(self.root).cancel(str(current_operation_id))
            except Exception as exc:
                self.update(batch_id_value, stop_cancel_error=str(exc))

        # Wait for the detached batch worker to finish cancelling the current
        # operation and write the final stopped state. A Band/iPerf subprocess
        # can need several seconds to unwind after cancellation.
        deadline = time.monotonic() + 30.0
        while time.monotonic() < deadline:
            current = self.read(batch_id_value)
            if current.get("status") in FINAL_STATUSES:
                return current
            time.sleep(0.25)

        # Last-resort finalization: never leave a user-requested stop in the
        # persistent "stopping" state. Preserve all completed rows and always
        # generate the matching XLSX/TXT reports.
        current = self.read(batch_id_value)
        try:
            from core.batch_report import write_reports
            reports = write_reports(
                self.root / "batch_results",
                batch_id_value,
                list(current.get("results") or []),
            )
        except Exception:
            reports = dict(current.get("reports") or {})
        return self.update(
            batch_id_value,
            status="stopped",
            reports=reports,
            finished_at=datetime.now().astimezone().isoformat(timespec="seconds"),
            message="批次測試已停止，最終報表已產生。",
            current_operation_id=None,
            current_operation_status=None,
            current_operation_stage=None,
        )
