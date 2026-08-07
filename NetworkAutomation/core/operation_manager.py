from __future__ import annotations

import hashlib
import json
import os
import subprocess
import sys
import uuid
import time
from pathlib import Path
from typing import Any

from core.operation_store import FINAL_STATES, OperationStore, utc_now


class OperationBusyError(RuntimeError):
    def __init__(self, active: dict[str, Any]) -> None:
        self.active = active
        super().__init__(
            f"已有 Operation 執行中：{active.get('operation_id')} "
            f"({active.get('intent')}, {active.get('stage')})"
        )


class OperationManager:
    def __init__(self, root: Path) -> None:
        self.root = root
        self.store = OperationStore(root)

    @staticmethod
    def fingerprint(intent: str, parameters: dict[str, Any]) -> str:
        payload = json.dumps(
            {"intent": intent, "parameters": parameters},
            ensure_ascii=False,
            sort_keys=True,
            separators=(",", ":"),
        )
        return hashlib.sha256(payload.encode("utf-8")).hexdigest()

    @staticmethod
    def _worker_timeout(intent: str, duration_sec: int | None) -> int:
        if intent in {"set_band", "set_nr_band"}:
            # Config upload/restart can consume several minutes before UE waiting starts.
            # Keep this safely above the configurable UE/PHY timeout so the worker
            # always returns a success/timeout message instead of being killed first.
            return 900
        if intent == "set_band_then_iperf":
            return max(900, int(duration_sec or 0) * 2 + 600)
        if intent == "iperf_run":
            return max(420, int(duration_sec or 0) * 2 + 300)
        if intent == "connection_status":
            return 420
        return 240

    @staticmethod
    def _creationflags() -> int:
        if os.name == "nt":
            return (
                subprocess.CREATE_NO_WINDOW
                | subprocess.DETACHED_PROCESS
                | subprocess.CREATE_NEW_PROCESS_GROUP
            )
        return 0

    def recover_stale_operations(self) -> list[dict[str, Any]]:
        recovered: list[dict[str, Any]] = []
        now = time.time()

        for state in self.store.find_active():
            operation_id = str(state.get("operation_id"))
            status = str(state.get("status"))
            updated_at = str(state.get("updated_at") or "")
            worker_pid = state.get("worker_pid")
            launcher_pid = state.get("launcher_pid")

            stale = False
            reason = ""

            if status == "queued" and not worker_pid:
                path = self.store.path(operation_id)
                age = now - path.stat().st_mtime if path.exists() else 999
                if age > 15:
                    stale = True
                    reason = "Worker 未在 15 秒內完成啟動握手。"

            if stale:
                recovered.append(
                    self.store.update(
                        operation_id,
                        status="failed",
                        stage="launch_failed",
                        message=reason,
                        progress_percent=100,
                        error="STALE_OPERATION_RECOVERED",
                    )
                )

        return recovered

    def create_or_attach(
        self,
        intent: str,
        parameters: dict[str, Any],
        cli_args: list[str],
        owner_batch_id: str | None = None,
        launch_worker: bool = True,
    ) -> dict[str, Any]:
        self.recover_stale_operations()
        # A batch owns the callbox until it is paused/stopped/completed.
        try:
            from core.batch_manager import BatchManager
            active_batch = BatchManager(self.root).active()
        except Exception:
            active_batch = None
        if active_batch and str(active_batch.get("batch_id")) != str(owner_batch_id or ""):
            raise OperationBusyError({
                "operation_id": active_batch.get("batch_id"),
                "intent": "batch_test",
                "stage": active_batch.get("status"),
                "status": active_batch.get("status"),
            })
        fingerprint = self.fingerprint(intent, parameters)

        same = self.store.find_active_by_fingerprint(fingerprint)
        if same:
            same["attached_existing"] = True
            return same

        active = self.store.find_active()
        if active:
            raise OperationBusyError(active[0])

        operation_id = f"op-{uuid.uuid4().hex[:16]}"
        state = {
            "success": True,
            "operation_id": operation_id,
            "fingerprint": fingerprint,
            "intent": intent,
            "parameters": parameters,
            "cli_args": cli_args,
            "status": "queued",
            "stage": "queued",
            "message": "Operation 已排入執行",
            "progress_percent": 0,
            "created_at": utc_now(),
            "updated_at": utc_now(),
            "worker_timeout_sec": self._worker_timeout(
                intent,
                parameters.get("duration_sec"),
            ),
            "attached_existing": False,
            "owner_batch_id": owner_batch_id,
        }
        self.store.write(state)

        # Batch mode runs OperationWorker inline in a background thread inside
        # the already-detached batch process. This avoids a second detached
        # Windows Python launch, which could fail its handshake and leave the
        # batch queued forever before any Config was applied.
        if not launch_worker:
            return self.store.read(operation_id)

        worker_stdout = self.store.worker_stdout_path(operation_id)
        worker_stderr = self.store.worker_stderr_path(operation_id)
        command = [
            sys.executable,
            "-u",
            "-m",
            "core.operation_worker",
            operation_id,
        ]

        try:
            with worker_stdout.open("ab") as out, worker_stderr.open("ab") as err:
                worker_env = os.environ.copy()
                worker_env["PYTHONIOENCODING"] = "utf-8"
                worker_env["PYTHONUTF8"] = "1"
                process = subprocess.Popen(
                    command,
                    cwd=str(self.root),
                    stdin=subprocess.DEVNULL,
                    stdout=out,
                    stderr=err,
                    creationflags=self._creationflags(),
                    close_fds=True,
                    env=worker_env,
                )
            self.store.update(
                operation_id,
                launcher_pid=process.pid,
                worker_launch_command=command,
            )

            # Worker must write worker_pid/status within a short handshake
            # window. Otherwise mark launch_failed immediately instead of
            # leaving a permanent queued zombie.
            deadline = time.monotonic() + 10
            while time.monotonic() < deadline:
                current = self.store.read(operation_id)
                if current.get("worker_pid"):
                    break
                if current.get("status") in {"failed", "completed"}:
                    break
                time.sleep(0.2)
            else:
                self.store.update(
                    operation_id,
                    status="failed",
                    stage="launch_failed",
                    message="Worker 未在 10 秒內完成啟動握手。",
                    progress_percent=100,
                    error="WORKER_START_HANDSHAKE_TIMEOUT",
                )
        except Exception as exc:
            self.store.update(
                operation_id,
                status="failed",
                stage="launch_failed",
                message=str(exc),
                progress_percent=100,
                error=type(exc).__name__,
            )
            raise

        return self.store.read(operation_id)

    def read(self, operation_id: str) -> dict[str, Any]:
        return self.store.read(operation_id)

    def read_result(self, operation_id: str) -> dict[str, Any] | None:
        return self.store.read_result(operation_id)

    def cancel(self, operation_id: str) -> dict[str, Any]:
        state = self.store.read(operation_id)
        if state.get("status") in FINAL_STATES:
            return state

        child_pid = state.get("child_pid")
        worker_pid = state.get("worker_pid")
        owner_batch_id = state.get("owner_batch_id")

        # A batch operation runs OperationWorker in a thread of the batch
        # process. In that case worker_pid is the batch process PID itself.
        # Killing it here leaves the batch JSON permanently at "running" and
        # blocks every later batch. Cancel only the external child command and
        # let batch_worker observe the cancelled state and finish cleanly.
        pids_to_kill: list[int] = []
        if child_pid:
            pids_to_kill.append(int(child_pid))
        if worker_pid and not owner_batch_id and int(worker_pid) != os.getpid():
            pids_to_kill.append(int(worker_pid))

        if os.name == "nt":
            for pid in dict.fromkeys(pids_to_kill):
                subprocess.run(
                    ["taskkill", "/PID", str(pid), "/T", "/F"],
                    capture_output=True,
                    creationflags=subprocess.CREATE_NO_WINDOW,
                )
        else:
            import signal
            for pid in dict.fromkeys(pids_to_kill):
                try:
                    os.kill(pid, signal.SIGTERM)
                except (ProcessLookupError, PermissionError):
                    pass

        self.store.update(
            operation_id,
            status="cancelled",
            stage="cancelled",
            message="Operation 已取消",
            progress_percent=100,
            cancel_requested=True,
        )
        return self.store.read(operation_id)
