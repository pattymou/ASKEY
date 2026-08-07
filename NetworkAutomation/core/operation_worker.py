from __future__ import annotations

import json
import os
import subprocess
import sys
import threading
import time
import traceback
from pathlib import Path
from typing import Any

ROOT = Path(__file__).resolve().parents[1]
if str(ROOT) not in sys.path:
    sys.path.insert(0, str(ROOT))

from core.operation_store import FINAL_STATES, OperationStore, utc_now
from core.summary_formatter import format_summary
from core.telegram_reply_formatter import format_telegram_reply
from core.user_settings import workflow_setting

try:
    from core.batch_notifier import BatchNotifier
except Exception:  # Progress notification must never block the actual operation.
    BatchNotifier = None  # type: ignore[assignment]


def _creationflags() -> int:
    if os.name == "nt":
        return subprocess.CREATE_NO_WINDOW
    return 0


def _stage_for(intent: str, elapsed: int, wait_ue_timeout_sec: int = 180) -> tuple[str, str, int]:
    if intent in {"set_band", "set_nr_band"}:
        radio_name = "NR/ENDC" if intent == "set_nr_band" else "LTE"
        if elapsed < 10:
            return "apply_config", f"正在產生並套用 {radio_name} Config", 15
        if elapsed < 25:
            return "restart_lte", "正在重新啟動 LTE Service", 35
        wait_elapsed = max(0, elapsed - 25)
        wait_progress = min(90, 35 + int(55 * wait_elapsed / max(1, wait_ue_timeout_sec)))
        return "wait_phy", (
            f"正在等待 UE 與 PHY Rate（上限 {wait_ue_timeout_sec} 秒）"
        ), wait_progress

    if intent == "set_band_then_iperf":
        if elapsed < 10:
            return "apply_config", "正在產生並套用 LTE Config", 10
        if elapsed < 25:
            return "restart_lte", "正在重新啟動 LTE Service", 25
        wait_end = 25 + max(10, wait_ue_timeout_sec)
        if elapsed < wait_end:
            wait_elapsed = max(0, elapsed - 25)
            wait_progress = min(65, 25 + int(40 * wait_elapsed / max(1, wait_ue_timeout_sec)))
            return "wait_ue", (
                f"正在等待 PHY 與可用資料 APN IP（上限 {wait_ue_timeout_sec} 秒）"
            ), wait_progress
        return "iperf", "正在檢查 iPerf Server 並執行測試", min(95, 65 + (elapsed - wait_end) // 2)

    if intent == "iperf_run":
        if elapsed < 10:
            return "precheck", "正在確認 UE 與 iPerf Server", 25
        return "iperf", "正在執行 iPerf 測試", min(95, 25 + elapsed // 2)

    if intent == "connection_status":
        return "connection", "正在檢查 UE 與 PHY 狀態", min(95, 10 + elapsed)

    return "status", "正在讀取 Callbox 狀態", min(95, 20 + elapsed)


def _heartbeat(
    store: OperationStore,
    operation_id: str,
    intent: str,
    stop_event: threading.Event,
) -> None:
    started = time.monotonic()
    wait_ue_timeout_sec = workflow_setting(ROOT, "wait_ue_timeout_sec")
    while not stop_event.wait(5):
        state = store.read(operation_id)
        if state.get("status") in {"completed", "failed", "cancelled"}:
            return
        elapsed = int(time.monotonic() - started)
        stage, message, progress = _stage_for(intent, elapsed, wait_ue_timeout_sec)
        store.update(
            operation_id,
            status="running",
            stage=stage,
            message=message,
            progress_percent=progress,
            elapsed_sec=elapsed,
            worker_heartbeat_at=utc_now(),
        )



def _single_progress_title(intent: str) -> str:
    return {
        "set_band": "單筆 LTE Band 設定",
        "set_nr_band": "單筆 NR／ENDC Band 設定",
        "set_band_then_iperf": "單筆 Band＋iPerf 測試",
        "iperf_run": "單筆 iPerf 測試",
        "connection_status": "單筆連線狀態檢查",
        "status": "單筆 Callbox 狀態檢查",
    }.get(intent, "單筆 NetworkAutomation 工作")


def _format_single_band_summary(intent: str, parameters: dict[str, Any]) -> list[str]:
    band_config = str(parameters.get("band_config") or "").strip()
    bandwidth_config = str(parameters.get("bandwidth_config") or "").strip()

    if not band_config:
        band = parameters.get("band")
        if band not in (None, ""):
            band_config = f"n{band}" if intent == "set_nr_band" else f"B{band}"

    if not bandwidth_config:
        bw = parameters.get("bandwidth_mhz")
        if bw not in (None, ""):
            try:
                numeric = float(bw)
                bandwidth_config = str(int(numeric)) if numeric.is_integer() else str(numeric)
            except (TypeError, ValueError):
                bandwidth_config = str(bw)

    lines: list[str] = []
    if band_config:
        lines.append(f"• Band：{band_config}")
    if bandwidth_config:
        lines.append(f"• BW：{bandwidth_config}")

    direction = str(parameters.get("direction") or "").strip().lower()
    duration = parameters.get("duration_sec")
    if direction:
        direction_name = {
            "tx": "Upload",
            "ul": "Upload",
            "upload": "Upload",
            "rx": "Download",
            "dl": "Download",
            "download": "Download",
            "trx": "TRX",
            "bidirectional": "TRX",
        }.get(direction, direction)
        if duration not in (None, ""):
            lines.append(f"• iPerf：{direction_name}，{duration} 秒")
        else:
            lines.append(f"• iPerf：{direction_name}")
    return lines


def _format_single_progress_message(
    intent: str,
    parameters: dict[str, Any],
    state: dict[str, Any],
    elapsed_sec: int,
) -> str:
    lines = [f"⏳ {_single_progress_title(intent)}進行中", ""]
    lines.extend(_format_single_band_summary(intent, parameters))
    if lines[-1] != "":
        lines.append("")
    lines.extend([
        f"• 階段：{state.get('message') or state.get('stage') or '執行中'}",
        f"• 已耗時：{elapsed_sec} 秒",
    ])
    return "\n".join(lines)


def _single_operation_progress_notifier(
    store: OperationStore,
    operation_id: str,
    intent: str,
    parameters: dict[str, Any],
    stop_event: threading.Event,
) -> None:
    """Push periodic progress for non-batch operations directly to Telegram.

    MCP progress events are not consistently rendered as Telegram messages by
    every OpenClaw runtime.  Batch tests already use ``BatchNotifier`` for
    visible progress, so single operations use the same transport here.  The
    actual Callbox worker is independent of this thread: notifier failures are
    ignored and can never stop or delay the operation.
    """
    if BatchNotifier is None:
        return

    try:
        initial = store.read(operation_id)
    except Exception:
        return
    if initial.get("owner_batch_id"):
        # Batch worker already sends its own [x/y] progress notifications.
        return

    try:
        notifier = BatchNotifier(ROOT)
    except Exception:
        return
    if not getattr(notifier, "enabled", False):
        return

    started = time.monotonic()
    next_periodic_sec = 30
    last_stage = ""
    last_message = ""
    last_sent_at = 0.0

    # Match the batch experience: acknowledge the single operation once, then
    # publish a first live status at 30 seconds and every 60 seconds after it.
    if not stop_event.is_set():
        started_lines = [f"▶️ {_single_progress_title(intent)}開始", ""]
        started_lines.extend(_format_single_band_summary(intent, parameters))
        if started_lines[-1] != "":
            started_lines.append("")
        started_lines.append("• 狀態：正在執行")
        try:
            notifier.send("\n".join(started_lines))
            last_sent_at = time.monotonic()
        except Exception:
            pass

    while not stop_event.wait(2):
        try:
            state = store.read(operation_id)
        except Exception:
            return
        status = str(state.get("status") or "")
        if status in FINAL_STATES:
            return

        elapsed = int(time.monotonic() - started)
        stage = str(state.get("stage") or "")
        message = str(state.get("message") or "")
        now = time.monotonic()

        # Long waits are reported at 30, 90, 150... seconds.  An iPerf stage
        # transition is reported immediately because it is a meaningful change
        # after Band/PHY readiness.  Avoid emitting every short-lived Config
        # sub-stage, which would otherwise flood Telegram.
        meaningful_stage_change = (
            elapsed >= 5
            and (stage, message) != (last_stage, last_message)
            and stage in {"iperf"}
        )
        periodic_due = elapsed >= next_periodic_sec
        enough_spacing = now - last_sent_at >= 8

        if enough_spacing and (meaningful_stage_change or periodic_due):
            # The Callbox child may finish between the periodic state read and
            # the actual Telegram send. Re-check both the local stop signal and
            # the durable operation status immediately before sending, so an
            # already-completed operation never queues another progress message.
            if stop_event.is_set():
                return
            try:
                latest = store.read(operation_id)
            except Exception:
                return
            if str(latest.get("status") or "") in FINAL_STATES:
                return

            text = _format_single_progress_message(intent, parameters, latest, elapsed)
            try:
                notifier.send(text)
            except Exception:
                pass
            last_sent_at = now
            while next_periodic_sec <= elapsed:
                next_periodic_sec += 60

        last_stage = stage
        last_message = message

def _load_job(store: OperationStore, operation_id: str) -> dict[str, Any]:
    state = store.read(operation_id)
    if state.get("status") in {"not_found", "corrupt"}:
        raise RuntimeError(state.get("message", "Operation state 無效。"))
    return state


def run(operation_id: str) -> int:
    store = OperationStore(ROOT)
    state = _load_job(store, operation_id)
    intent = str(state["intent"])
    cli_args = [str(item) for item in state["cli_args"]]
    timeout_sec = int(state["worker_timeout_sec"])
    parameters = state.get("parameters") if isinstance(state.get("parameters"), dict) else {}

    heartbeat_stop_event = threading.Event()
    progress_stop_event = threading.Event()
    heartbeat = threading.Thread(
        target=_heartbeat,
        args=(store, operation_id, intent, heartbeat_stop_event),
        daemon=True,
    )
    progress_notifier = threading.Thread(
        target=_single_operation_progress_notifier,
        args=(store, operation_id, intent, parameters, progress_stop_event),
        daemon=True,
    )

    stdout_path = store.worker_stdout_path(operation_id)
    stderr_path = store.worker_stderr_path(operation_id)

    store.update(
        operation_id,
        status="running",
        stage="starting",
        message="Worker 已啟動",
        progress_percent=1,
        worker_pid=os.getpid(),
        worker_started_at=utc_now(),
    )
    heartbeat.start()
    progress_notifier.start()

    command = [sys.executable, "-u", str(ROOT / "callbox_agent.py"), *cli_args]
    started = time.monotonic()

    try:
        with stdout_path.open("wb") as stdout_file, stderr_path.open("wb") as stderr_file:
            child_env = os.environ.copy()
            child_env["PYTHONIOENCODING"] = "utf-8"
            child_env["PYTHONUTF8"] = "1"
            process = subprocess.Popen(
                command,
                cwd=str(ROOT),
                stdin=subprocess.DEVNULL,
                stdout=stdout_file,
                stderr=stderr_file,
                creationflags=_creationflags(),
                env=child_env,
            )
            store.update(
                operation_id,
                child_pid=process.pid,
                command=command,
            )

            try:
                returncode = process.wait(timeout=timeout_sec)
            except subprocess.TimeoutExpired:
                process.kill()
                process.wait()
                raise TimeoutError(
                    f"Worker 超過安全上限 {timeout_sec} 秒，已停止子程序。"
                )

        # The child has finished. Stop the Telegram progress thread *before*
        # publishing completed/failed state. Waiting for an in-flight send to
        # return guarantees the final PASS/FAIL reply cannot be followed by a
        # stale "still running" message.
        progress_stop_event.set()
        progress_notifier.join()

        stdout = stdout_path.read_bytes().decode("utf-8", errors="replace").strip()
        stderr = stderr_path.read_bytes().decode("utf-8", errors="replace").strip()

        try:
            result = json.loads(stdout) if stdout else {}
        except json.JSONDecodeError:
            result = {
                "success": False,
                "error": "INVALID_JSON_OUTPUT",
                "message": "callbox_agent.py 沒有回傳有效 JSON。",
                "stdout_tail": stdout[-4000:],
            }

        result.setdefault("returncode", returncode)
        if stderr:
            result.setdefault("stderr_tail", stderr[-4000:])

        total_elapsed = round(time.monotonic() - started, 1)
        machine_result = result.get("machine_result")
        if isinstance(machine_result, dict):
            machine_result["operation_metrics"] = {
                "operation_id": operation_id,
                "total_elapsed_sec": total_elapsed,
            }
            tool_name = str(result.get("tool") or "")
            if tool_name:
                result["human_summary"] = format_summary(
                    tool_name,
                    machine_result,
                )
                result["telegram_reply"] = format_telegram_reply(
                    tool_name,
                    result,
                )

        result_path = store.write_result(operation_id, result)
        success = bool(result.get("success"))

        store.update(
            operation_id,
            status="completed" if success else "failed",
            stage="completed" if success else "failed",
            message=(
                result.get("human_summary")
                or result.get("message")
                or ("Operation 完成" if success else "Operation 失敗")
            ),
            progress_percent=100,
            elapsed_sec=total_elapsed,
            returncode=returncode,
            result_file=str(result_path),
            worker_finished_at=utc_now(),
        )
        return 0 if success else 1

    except Exception as exc:
        failure = {
            "success": False,
            "error": type(exc).__name__,
            "message": str(exc),
            "traceback": traceback.format_exc(),
        }
        result_path = store.write_result(operation_id, failure)
        store.update(
            operation_id,
            status="failed",
            stage="failed",
            message=str(exc),
            progress_percent=100,
            elapsed_sec=round(time.monotonic() - started, 1),
            result_file=str(result_path),
            worker_finished_at=utc_now(),
        )
        return 1

    finally:
        progress_stop_event.set()
        heartbeat_stop_event.set()
        # BatchNotifier sends have their own bounded subprocess timeout. A full
        # join here preserves message ordering even when Telegram/OpenClaw is a
        # little slow, while still remaining bounded by that transport timeout.
        progress_notifier.join()
        heartbeat.join(timeout=2)


def main() -> int:
    if len(sys.argv) != 2:
        print("Usage: python -m core.operation_worker <operation_id>")
        return 2
    return run(sys.argv[1])


if __name__ == "__main__":
    raise SystemExit(main())
