from __future__ import annotations

import asyncio
import copy
import sys
import time
from pathlib import Path
from typing import Any, Literal

ROOT = Path(__file__).resolve().parents[1]
if str(ROOT) not in sys.path:
    sys.path.insert(0, str(ROOT))

from mcp.server.fastmcp import Context, FastMCP

from core.intent_validator import IntentValidationError, validate_intent
from core.followup_context import (
    extract_successful_ue_ip,
    load_followup_context,
    save_wait_download_context,
    save_wait_summary_context,
)
from core.mcp_result_compactor import compact_result
from core.operation_manager import OperationBusyError, OperationManager
from core.batch_manager import BatchManager
from core.batch_notifier import BatchNotifier, format_batch_final, send_report_files
from core.operation_store import FINAL_STATES
from core.report_link_server import ensure_report_server
from core.user_settings import (
    UserSettingError,
    format_settings_for_telegram,
    get_editable_settings,
    update_editable_setting,
    workflow_setting,
)

mcp = FastMCP("NetworkAutomation")
operations = OperationManager(ROOT)
batches = BatchManager(ROOT)

# Keep the localhost report links available whenever NetworkAutomation is running.
try:
    ensure_report_server(ROOT, ROOT / "batch_results")
except Exception:
    pass


async def _safe_progress(
    ctx: Context,
    progress: float,
    message: str,
) -> None:
    try:
        await ctx.report_progress(
            progress=progress,
            total=100,
            message=message,
        )
    except Exception:
        pass
    try:
        await ctx.info(message)
    except Exception:
        pass


async def _wait_for_operation(
    ctx: Context,
    operation_id: str,
    safety_timeout_sec: int,
    intent: str,
    parameters: dict[str, Any],
    followup_context: dict[str, Any] | None = None,
) -> Any:
    started = time.monotonic()
    last_message = ""

    while True:
        state = operations.read(operation_id)
        status = str(state.get("status"))
        message = str(state.get("message") or status)
        progress = float(state.get("progress_percent") or 0)

        if message != last_message:
            await _safe_progress(ctx, progress, message)
            last_message = message
        else:
            await _safe_progress(
                ctx,
                progress,
                f"{message}（Operation {operation_id}）",
            )

        if status in FINAL_STATES:
            result = operations.read_result(operation_id)
            if result is None:
                return {
                    "success": False,
                    "human_summary": "Operation 已結束，但找不到結果檔。",
                    "error": "RESULT_FILE_MISSING",
                    "operation": state,
                }
            if bool(result.get("success")):
                # Any successful Upload iPerf must refresh the follow-up window.
                # Do not limit this to LTE's set_band_then_iperf intent: NR SA,
                # ENDC and standalone Upload flows also need to support a later
                # "好 / 接著跑 download" reply.  Otherwise an older expired
                # context can be mistaken for the just-finished test.
                direction = str(parameters.get("direction") or "").strip().lower()
                if direction in {"upload", "tx", "ul"}:
                    ue_ip = extract_successful_ue_ip(result)
                    duration = parameters.get("duration_sec")
                    if ue_ip and duration:
                        save_wait_download_context(
                            ROOT,
                            ue_ip=ue_ip,
                            duration_sec=int(duration),
                            operation_id=operation_id,
                            result=result,
                        )
                elif direction in {"download", "rx", "dl"}:
                    if followup_context:
                        save_wait_summary_context(
                            ROOT,
                            previous=followup_context,
                            operation_id=operation_id,
                            result=result,
                        )

            telegram_reply = result.get("telegram_reply")
            if isinstance(telegram_reply, str) and telegram_reply.strip():
                return telegram_reply.strip()

            # Compatibility fallback for result files created before V12.2.
            human_summary = result.get("human_summary")
            if isinstance(human_summary, str) and human_summary.strip():
                return human_summary.strip()

            tool_name = str(result.get("tool") or "")
            if tool_name:
                compact = compact_result(ROOT, tool_name, result)
                fallback = compact.get("human_summary")
                if isinstance(fallback, str) and fallback.strip():
                    return fallback.strip()

            return "結果失敗。Operation 沒有產生 Telegram 回覆內容。"

        elapsed = time.monotonic() - started
        if elapsed >= safety_timeout_sec:
            # Worker continues independently. Do not kill a valid job merely
            # because the current OpenClaw request ended.
            return {
                "success": False,
                "pending": True,
                "operation_id": operation_id,
                "operation_status": status,
                "human_summary": (
                    "Operation 仍在背景執行，沒有被取消。"
                    f"請查詢 Operation {operation_id} 狀態。"
                ),
            }

        await asyncio.sleep(5)


@mcp.tool()
async def networkautomation_execute_intent(
    ctx: Context,
    intent: Literal[
        "set_band",
        "iperf_run",
        "set_band_then_iperf",
        "status",
        "connection_status",
        "set_nr_band",
    ],
    band: int | None = None,
    bandwidth_mhz: float | None = None,
    band_config: str | None = None,
    bandwidth_config: str | None = None,
    cell: int = 1,
    radio_mode: Literal["SA", "ENDC", "sa", "endc"] | None = None,
    dl_earfcn: int | None = None,
    nr_arfcn: int | None = None,
    time_slot: str | None = None,
    lte_band: int | None = None,
    lte_bandwidth_mhz: float | None = None,
    lte_earfcn: int | None = None,
    mimo_dl: str | None = None,
    mimo_ul: str | None = None,
    modulation_dl: str | None = None,
    modulation_ul: str | None = None,
    mcs_dl: str | None = None,
    mcs_ul: str | None = None,
    expected_imsi: str | None = None,
    expected_imei: str | None = None,
    apn: str | None = None,
    ue_ip: str | None = None,
    direction: Literal[
        "download",
        "upload",
        "bidirectional",
        "rx",
        "tx",
        "trx",
    ] | None = None,
    duration_sec: int | None = None,
    port: int | None = None,
    parallel_streams: int | None = None,
    interval_sec: int | None = None,
) -> Any:
    """
    Create or attach to one durable background operation, then wait for its
    file-backed result while reporting progress. Gateway restarts do not stop
    the worker.

    For NR/ENDC combinations, prefer the team's explicit notation:
      * n78A + 100
      * n78C + 100+100
      * n1A-n78A + 20+100
      * 1A_n78A + 20_100
      * 1A-3A_n78C + 20+20_100+100
    '_' separates LTE from NR; '-' separates bands inside one RAT; A/C/D/E/F
    mean 1/2/3/4/5 component carriers.
    """
    if duration_sec is None and intent in {"iperf_run", "set_band_then_iperf"}:
        duration_sec = workflow_setting(ROOT, "iperf_default_duration_sec")
    if port is None:
        port = workflow_setting(ROOT, "iperf_default_port")
    if parallel_streams is None:
        parallel_streams = workflow_setting(ROOT, "iperf_default_parallel_streams")
    if interval_sec is None:
        interval_sec = workflow_setting(ROOT, "iperf_default_interval_sec")

    parameters = {
        "band": band,
        "bandwidth_mhz": bandwidth_mhz,
        "band_config": band_config,
        "bandwidth_config": bandwidth_config,
        "cell": cell,
        "radio_mode": radio_mode,
        "dl_earfcn": dl_earfcn,
        "nr_arfcn": nr_arfcn,
        "time_slot": time_slot,
        "lte_band": lte_band,
        "lte_bandwidth_mhz": lte_bandwidth_mhz,
        "lte_earfcn": lte_earfcn,
        "mimo_dl": mimo_dl,
        "mimo_ul": mimo_ul,
        "modulation_dl": modulation_dl,
        "modulation_ul": modulation_ul,
        "mcs_dl": mcs_dl,
        "mcs_ul": mcs_ul,
        "expected_imsi": expected_imsi,
        "expected_imei": expected_imei,
        "apn": apn,
        "ue_ip": ue_ip,
        "direction": direction,
        "duration_sec": duration_sec,
        "port": port,
        "parallel_streams": parallel_streams,
        "interval_sec": interval_sec,
    }

    resolved_followup_context: dict[str, Any] | None = None
    if intent == "iperf_run" and direction in {"download", "rx"} and not ue_ip:
        context_status, context = load_followup_context(ROOT)
        if context_status == "expired":
            return (
                "這次的接續操作已超過 30 分鐘，前一次測試資料已清除。\n\n"
                "請重新輸入完整指令，例如：\n"
                "幫我跑 30 秒 download iPerf"
            )
        if context_status == "missing" or not context:
            return (
                "目前沒有可接續的 iPerf 操作。\n\n"
                "請輸入完整指令，例如：\n"
                "幫我跑 30 秒 download iPerf"
            )
        if context.get("stage") != "wait_download_confirm":
            return (
                "目前沒有等待執行 Download iPerf 的接續操作。\n\n"
                "請輸入完整指令，例如：\n"
                "幫我跑 30 秒 download iPerf"
            )
        stored_ip = str(context.get("ue_ip") or "").strip()
        if not stored_ip:
            return (
                "前一次測試沒有保留可用的 UE IP，無法接續執行。\n\n"
                "請重新輸入完整指令。"
            )
        parameters["ue_ip"] = stored_ip
        if not parameters.get("duration_sec"):
            parameters["duration_sec"] = int(context.get("duration_sec") or 30)
        resolved_followup_context = context

    try:
        validated = validate_intent(ROOT, intent, parameters)
    except IntentValidationError as exc:
        return {
            "success": False,
            "human_summary": str(exc),
            "error": "VALIDATION_ERROR",
        }
    except Exception as exc:
        return {
            "success": False,
            "human_summary": str(exc),
            "error": type(exc).__name__,
        }

    try:
        operation = operations.create_or_attach(
            validated.intent,
            validated.parameters,
            validated.cli_args,
        )
    except OperationBusyError as exc:
        active = exc.active
        return {
            "success": False,
            "busy": True,
            "operation_id": active.get("operation_id"),
            "operation_status": active.get("status"),
            "human_summary": (
                "目前已有 NetworkAutomation 工作執行中，"
                "為避免重複切 Band 或同時跑兩組 iPerf，本次沒有重複執行。"
            ),
        }
    except Exception as exc:
        return {
            "success": False,
            "human_summary": str(exc),
            "error": type(exc).__name__,
        }

    # The MCP request waits on tiny state files, not on the device subprocess.
    return await _wait_for_operation(
        ctx,
        str(operation["operation_id"]),
        safety_timeout_sec=780,
        intent=validated.intent,
        parameters=validated.parameters,
        followup_context=resolved_followup_context,
    )


@mcp.tool()
def networkautomation_get_settings() -> str:
    """Show all user-editable NetworkAutomation settings."""
    try:
        return format_settings_for_telegram(ROOT)
    except UserSettingError as exc:
        return f"讀取設定失敗：{exc}"


@mcp.tool()
def networkautomation_update_setting(
    setting_name: str,
    value: int,
) -> str:
    """Update one allow-listed setting and return a Telegram-ready reply."""
    try:
        result = update_editable_setting(ROOT, setting_name, value)
    except UserSettingError as exc:
        return f"設定修改失敗：{exc}"
    unit = f" {result['unit']}" if result.get("unit") else ""
    return "\n".join([
        "✅ 設定已更新",
        "",
        f"• 項目：{result['label']}",
        f"• 原設定：{result['old_value']}{unit}",
        f"• 新設定：{result['new_value']}{unit}",
        "",
        "新設定會套用到下一個工作，不需要重新啟動 OpenClaw。",
    ])


@mcp.tool()
def networkautomation_operation_status(
    operation_id: str,
) -> dict[str, Any]:
    state = operations.read(operation_id)
    result = operations.read_result(operation_id)
    response = {
        "success": state.get("status") != "not_found",
        "operation": state,
    }
    if result is not None and state.get("status") in FINAL_STATES:
        response["result"] = compact_result(
            ROOT,
            str(state.get("intent")),
            result,
        )
    return response


@mcp.tool()
def networkautomation_cancel_operation(
    operation_id: str,
) -> dict[str, Any]:
    return operations.cancel(operation_id)


@mcp.tool()
def networkautomation_start_batch(
    items: list[dict[str, Any]],
    repeat_count: int = 1,
    expected_total: int | None = None,
    continue_on_error: bool = True,
    name: str | None = None,
    allow_restart: bool = False,
) -> dict[str, Any]:
    """
    Start an unlimited-length sequential batch. Each base item accepts:
      band_config, bandwidth_config, action, duration_sec.
    action: phy, upload/tx, download/rx, bidirectional/trx.

    For repeated blocks, pass the base items only once and set repeat_count.
    Never manually expand a repeated block in the agent. Example:
      5 base items repeated 20 times -> items has 5 entries,
      repeat_count=20, expected_total=100.

    expected_total is a safety check. If it does not equal
    len(items) * repeat_count, no batch is started.
    There is no item-count limit.
    """
    try:
        repeat_value = int(repeat_count)
    except (TypeError, ValueError):
        return {
            "success": False,
            "error": "INVALID_REPEAT_COUNT",
            "human_summary": "repeat_count 必須是大於或等於 1 的整數；批次沒有啟動。",
        }
    if repeat_value < 1:
        return {
            "success": False,
            "error": "INVALID_REPEAT_COUNT",
            "human_summary": "repeat_count 必須大於或等於 1；批次沒有啟動。",
        }
    if not items:
        return {
            "success": False,
            "error": "EMPTY_BATCH",
            "human_summary": "批次測試至少需要一筆基礎測項；批次沒有啟動。",
        }

    base_count = len(items)
    calculated_total = base_count * repeat_value
    if expected_total is not None and int(expected_total) != calculated_total:
        return {
            "success": False,
            "error": "BATCH_TOTAL_MISMATCH",
            "base_item_count": base_count,
            "repeat_count": repeat_value,
            "calculated_total": calculated_total,
            "expected_total": int(expected_total),
            "human_summary": (
                "批次數量驗證失敗，沒有啟動任何測試。\n\n"
                f"• 基礎測項：{base_count} 筆\n"
                f"• 重複次數：{repeat_value} 次\n"
                f"• 正確總數：{calculated_total} 筆\n"
                f"• 傳入預期：{int(expected_total)} 筆\n\n"
                "請修正後重新送出；系統不會自行停止或重開批次。"
            ),
        }

    # Expand deterministically in Python so OpenClaw never has to count or
    # manufacture 50/100/1000 dictionary entries. The full base block is
    # repeated in order, e.g. A,B,C repeated twice -> A,B,C,A,B,C.
    expanded_items = [
        copy.deepcopy(item)
        for _ in range(repeat_value)
        for item in items
    ]

    try:
        state = batches.start(
            expanded_items,
            continue_on_error=continue_on_error,
            name=name,
            base_items=copy.deepcopy(items),
            repeat_count=repeat_value,
            expected_total=calculated_total,
            allow_restart=bool(allow_restart),
        )
    except Exception as exc:
        return {
            "success": False,
            "error": type(exc).__name__,
            "human_summary": str(exc),
        }
    return {
        "success": True,
        "batch_id": state.get("batch_id"),
        "base_item_count": base_count,
        "repeat_count": repeat_value,
        "expected_total": calculated_total,
        "total": state.get("total"),
        "human_summary": (
            "批次測試已啟動。\n\n"
            f"• 基礎測項：{base_count} 筆\n"
            f"• 重複次數：{repeat_value} 次\n"
            f"• 總測項：{state.get('total')} 筆\n"
            f"• Batch ID：{state.get('batch_id')}"
        ),
    }


@mcp.tool()
def networkautomation_pause_batch(batch_id: str | None = None) -> dict[str, Any]:
    """Pause after the currently running item finishes and generate XLSX/TXT reports."""
    state = batches.pause(batch_id)
    state["human_summary"] = state.get("message") or "已要求暫停批次測試。"
    return state


@mcp.tool()
def networkautomation_resume_batch(batch_id: str | None = None) -> dict[str, Any]:
    """Resume a paused batch from its next unfinished item."""
    state = batches.resume(batch_id)
    state["human_summary"] = state.get("message") or "批次測試已繼續。"
    return state


def _ensure_batch_report_delivery(state: dict[str, Any], caption: str) -> dict[str, Any]:
    """Guarantee stopped/completed reports are sent as Telegram attachments."""
    reports = state.get("reports") if isinstance(state.get("reports"), dict) else {}
    if not reports or not any(reports.get(kind) for kind in ("xlsx", "txt")):
        return state

    batch_id_value = str(state.get("batch_id") or "")
    # The detached worker may already be sending the attachments. Give it a
    # short window to finish before using this MCP call as the fallback sender.
    if state.get("report_delivery_status") == "sending" and batch_id_value:
        deadline = time.monotonic() + 8.0
        while time.monotonic() < deadline:
            current = batches.read(batch_id_value)
            if current.get("report_delivery_status") != "sending":
                state = current
                break
            time.sleep(0.25)

    delivery = state.get("report_delivery") if isinstance(state.get("report_delivery"), dict) else {}
    if state.get("report_delivery_status") == "sent" and all(
        isinstance(delivery.get(kind), dict) and delivery[kind].get("success")
        for kind in ("xlsx", "txt")
    ):
        return state

    target = str(state.get("notification_target") or "").strip()
    if not target:
        state["report_delivery_status"] = "unavailable"
        state["report_delivery_error"] = "缺少 Telegram notification_target。"
        return state

    notifier = BatchNotifier(ROOT, target)
    delivery = send_report_files(notifier, reports, caption)
    status = "sent" if all(item.get("success") for item in delivery.values()) else "partial"
    if batch_id_value:
        return batches.update(
            batch_id_value,
            report_delivery=delivery,
            report_delivery_status=status,
        )
    state["report_delivery"] = delivery
    state["report_delivery_status"] = status
    return state


@mcp.tool()
def networkautomation_stop_batch(batch_id: str | None = None) -> dict[str, Any]:
    """Stop the active batch and return the final detailed report immediately."""
    state = batches.stop(batch_id)
    status = str(state.get("status") or "")
    if status in {"stopped", "completed", "failed"}:
        state = _ensure_batch_report_delivery(state, "批次測試最終")

    # The stop command itself must return the final summary. Do not ask the
    # user whether they want to see it, and do not return only "已停止".
    if status == "stopped":
        state["human_summary"] = format_batch_final("stopped", state)
    elif status == "completed":
        state["human_summary"] = format_batch_final("completed", state)
    elif status == "failed":
        state["human_summary"] = format_batch_final("failed", state)
    else:
        # This is only a defensive fallback. BatchManager.stop now waits and
        # force-finalizes after cancellation, so normal calls return stopped.
        state["human_summary"] = (
            "正在停止批次測試並整理最終報表。完成後會自動顯示結果，"
            "不需要再次詢問或確認。"
        )
    return state


@mcp.tool()
def networkautomation_batch_status(batch_id: str | None = None) -> dict[str, Any]:
    """Return active/latest batch progress and report paths."""
    state = batches.read(batch_id) if batch_id else (batches.active() or batches.latest())
    if not state:
        return {"success": False, "human_summary": "目前沒有批次測試紀錄。"}
    return state


@mcp.tool()
def networkautomation_version() -> dict[str, Any]:
    return {
        "success": True,
        "version": "13.0-LTE-Full-Parameters",
        "architecture": (
            "OpenClaw MCP -> durable Operation Manager -> detached Worker "
            "-> callbox_agent -> atomic state/result files"
        ),
        "durable_operations": True,
        "gateway_restart_safe": True,
        "single_active_operation_guard": True,
        "idempotent_attach_for_same_request": True,
        "public_tools": [
            "networkautomation_execute_intent",
            "networkautomation_get_settings",
            "networkautomation_update_setting",
            "networkautomation_operation_status",
            "networkautomation_cancel_operation",
            "networkautomation_start_batch",
            "networkautomation_pause_batch",
            "networkautomation_resume_batch",
            "networkautomation_stop_batch",
            "networkautomation_batch_status",
            "networkautomation_version",
        ],
    }


if __name__ == "__main__":
    mcp.run()
