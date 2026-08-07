from __future__ import annotations

import json
import os
import subprocess
import sys
import time
import threading
import re
from datetime import datetime
from pathlib import Path
from typing import Any

ROOT = Path(__file__).resolve().parents[1]
if str(ROOT) not in sys.path: sys.path.insert(0, str(ROOT))

from core.batch_manager import BatchManager
from core.operation_manager import OperationManager
from core.operation_store import FINAL_STATES
from core.operation_worker import run as run_operation_worker
from core.batch_notifier import (
    BatchNotifier,
    format_batch_final,
    format_item_started,
    send_report_files,
)
from core.batch_report import write_reports
from core.intent_validator import validate_intent
from plugins.amarisoft.band_combo_parser import parse_radio_combination
from plugins.amarisoft.lte_config_modifier import LteBandDatabase
from plugins.amarisoft.nr_config_modifier import get_bandwidth_policy


def _now() -> str:
    return datetime.now().astimezone().isoformat(timespec="seconds")


def _run_operation(
    batch_id: str,
    intent: str,
    parameters: dict[str, Any],
    cli_args: list[str],
    manager: BatchManager,
    notifier: BatchNotifier,
    item_index: int,
    total: int,
) -> dict[str, Any]:
    """Run one batch item through the same durable Operation Worker as a single command.

    This prevents the batch path from treating a launcher acknowledgement or a
    stale summary as a completed device test. The function returns only after
    the operation state reaches completed/failed/cancelled and its result file
    has been written.
    """
    operations = OperationManager(ROOT)
    operation = operations.create_or_attach(
        intent,
        parameters,
        cli_args,
        owner_batch_id=batch_id,
        launch_worker=False,
    )
    operation_id = str(operation["operation_id"])

    # Execute the exact same OperationWorker used by a single command, but
    # inside this detached batch process. No extra console window and no nested
    # detached-worker handshake that can stall before Band switching starts.
    worker_error: list[str] = []
    def _worker_entry() -> None:
        try:
            run_operation_worker(operation_id)
        except BaseException as exc:
            worker_error.append(f"{type(exc).__name__}: {exc}")

    worker_thread = threading.Thread(
        target=_worker_entry,
        name=f"batch-operation-{operation_id}",
        daemon=True,
    )
    worker_thread.start()
    manager.update(
        batch_id,
        current_operation_id=operation_id,
        current_operation_status=operation.get("status"),
        current_operation_stage=operation.get("stage"),
    )

    last_notice_signature: tuple[str, str] | None = None
    last_progress_notice = 0.0
    while True:
        state = operations.read(operation_id)
        status = str(state.get("status") or "")
        stage = str(state.get("stage") or "")
        manager.update(
            batch_id,
            current_operation_status=status,
            current_operation_stage=stage,
            current_operation_message=state.get("message"),
            current_operation_elapsed_sec=state.get("elapsed_sec"),
        )

        # Send stage changes at a restrained cadence so Telegram shows that the
        # batch is really progressing without flooding the conversation.
        now = time.monotonic()
        message = str(state.get("message") or stage or "處理中")
        signature = (stage, message)
        # A terminal PASS/FAIL is not a progress message.  Never send the
        # one-line terminal state here; the caller sends the complete Band/BW/
        # ARFCN/PHY/iPerf result after the report row has been built.
        terminal_word = message.strip().upper() in {"PASS", "FAIL", "COMPLETED", "FAILED", "CANCELLED"}
        if status not in FINAL_STATES and not terminal_word and stage and (
            signature != last_notice_signature or now - last_progress_notice >= 60
        ):
            notifier.send(
                f"⏳ 批次測試 [{item_index}/{total}] 進行中\n\n"
                f"• 階段：{message}\n"
                f"• 已耗時：{state.get('elapsed_sec', 0)} 秒"
            )
            last_notice_signature = signature
            last_progress_notice = now

        if status in FINAL_STATES:
            result = operations.read_result(operation_id) or {
                "success": False,
                "error": "MISSING_OPERATION_RESULT",
                "message": "Operation 已結束，但找不到結果檔。",
            }
            result.setdefault("operation_id", operation_id)
            result.setdefault("operation_state", state)
            return result

        batch_state = manager.read(batch_id)
        if batch_state.get("stop_requested"):
            operations.cancel(operation_id)

        if not worker_thread.is_alive() and status not in FINAL_STATES:
            message = worker_error[-1] if worker_error else "Operation Worker 已意外結束。"
            operations.store.update(
                operation_id,
                status="failed",
                stage="worker_crashed",
                message=message,
                progress_percent=100,
                error="BATCH_INLINE_WORKER_EXITED",
            )

        time.sleep(1)


def _machine(value: Any) -> dict[str, Any]:
    if not isinstance(value, dict): return {}
    nested = value.get("machine_result")
    return nested if isinstance(nested, dict) else value


def _recursive_find_dict(data: Any, key: str) -> dict[str, Any] | None:
    if isinstance(data, dict):
        value = data.get(key)
        if isinstance(value, dict): return value
        for child in data.values():
            found = _recursive_find_dict(child, key)
            if found: return found
    elif isinstance(data, list):
        for child in data:
            found = _recursive_find_dict(child, key)
            if found: return found
    return None




def _recursive_find_value(data: Any, key: str) -> Any:
    if isinstance(data, dict):
        if key in data and data.get(key) not in (None, ""):
            return data.get(key)
        for child in data.values():
            found = _recursive_find_value(child, key)
            if found not in (None, ""):
                return found
    elif isinstance(data, list):
        for child in data:
            found = _recursive_find_value(child, key)
            if found not in (None, ""):
                return found
    return None


def _is_ue_timeout(data: dict[str, Any]) -> bool:
    error = _recursive_find_value(data, "error")
    stage = _recursive_find_value(data, "stage")
    return str(error or "").upper() == "UE_CONNECTION_TIMEOUT" or str(stage or "").lower() == "wait_ue_timeout"


def _ue_timeout_sec(data: dict[str, Any]) -> int:
    for key in ("wait_ue_timeout_sec", "ue_wait_timeout_sec"):
        value = _recursive_find_value(data, key)
        try:
            if value not in (None, ""):
                return int(float(value))
        except (TypeError, ValueError):
            pass
    connection = _recursive_find_dict(data, "connection") or {}
    try:
        elapsed = int(float(connection.get("elapsed_sec")))
        if elapsed > 0:
            return elapsed
    except (TypeError, ValueError):
        pass
    return 180

def _extract_phy(data: dict[str, Any]) -> tuple[Any, Any, str]:
    connection = _recursive_find_dict(data, "connection") or {}
    phy = connection.get("phy_rate") or _recursive_find_dict(data, "phy_rate") or {}
    return phy.get("total_dl_bitrate_mbps"), phy.get("total_ul_bitrate_mbps"), "已連線" if connection.get("connected") else "未連線"


def _extract_ip(data: dict[str, Any]) -> str | None:
    for key in ("ue_ip", "data_ue_ip"):
        def walk(v: Any) -> str | None:
            if isinstance(v, dict):
                if v.get(key): return str(v[key])
                for c in v.values():
                    x = walk(c)
                    if x: return x
            elif isinstance(v, list):
                for c in v:
                    x = walk(c)
                    if x: return x
            return None
        found = walk(data)
        if found: return found
    return None


def _extract_arfcn(data: dict[str, Any]) -> str:
    values: list[str] = []
    def walk(v: Any) -> None:
        if isinstance(v, dict):
            for key in ("dl_earfcn", "nr_arfcn"):
                if key in v and v[key] not in (None, "", "N/A"):
                    s = str(v[key])
                    if s not in values: values.append(s)
            for c in v.values(): walk(c)
        elif isinstance(v, list):
            for c in v: walk(c)
    walk(data)
    return "+".join(values)


def _summary_values(summary: dict[str, Any]) -> dict[str, Any]:
    return {
        "avg": summary.get("average_mbps"),
        "min": summary.get("minimum_mbps"),
        "max": summary.get("maximum_mbps"),
        "transfer": summary.get("total_transfer_mbytes"),
    }


def _direction_from_result(data: Any) -> str | None:
    """Return upload/download from any supported iPerf result envelope."""
    for key in ("direction", "iperf_direction"):
        value = _recursive_find_value(data, key)
        if value not in (None, ""):
            text = str(value).strip().lower()
            aliases = {"tx": "upload", "ul": "upload", "rx": "download", "dl": "download"}
            return aliases.get(text, text)
    return None


def _extract_iperf_details(data: dict[str, Any]) -> dict[str, dict[str, Any]]:
    """Extract one summary per direction, including bidirectional results.

    ``iperf.bidirectional`` stores two child envelopes under
    ``machine_result.results``.  The old implementation recursively selected
    only the first ``summary`` dictionary, so TRX batches lost the second
    direction and the stop report could show PHY only.  This function keeps
    Download and Upload separately while still supporting a normal one-way
    ``iperf.run`` envelope.
    """
    if not isinstance(data, dict) or not data:
        return {}

    machine = _machine(data)
    details: dict[str, dict[str, Any]] = {}
    child_results = machine.get("results") if isinstance(machine, dict) else None
    if isinstance(child_results, list):
        for child in child_results:
            if not isinstance(child, dict):
                continue
            direction = str(child.get("direction") or "").strip().lower()
            child_result = child.get("result")
            child_machine = _machine(child_result) if isinstance(child_result, dict) else {}
            if not direction:
                direction = _direction_from_result(child_machine or child_result) or "unknown"
            summary = _recursive_find_dict(child_machine or child_result, "summary") or {}
            if summary:
                values = _summary_values(summary)
                values["success"] = bool(
                    (child_result or {}).get("success", child_machine.get("success", True))
                    if isinstance(child_result, dict)
                    else child_machine.get("success", True)
                )
                details[direction] = values

    if details:
        return details

    summary = _recursive_find_dict(machine or data, "summary") or {}
    if not summary:
        return {}
    direction = _direction_from_result(machine or data) or "unknown"
    values = _summary_values(summary)
    values["success"] = bool(data.get("success", machine.get("success", True)))
    details[direction] = values
    return details


def _iperf_summary(data: dict[str, Any]) -> dict[str, Any]:
    """Backward-compatible single summary used by the legacy report columns."""
    details = _extract_iperf_details(data)
    if not details:
        return {"avg": None, "min": None, "max": None, "transfer": None}
    # One-way tests have one row. For TRX, keep a readable DL/UL value in the
    # old columns while the new direction-specific columns retain numeric data.
    if len(details) == 1:
        return next(iter(details.values()))

    def combined(key: str) -> str:
        dl = details.get("download", {}).get(key)
        ul = details.get("upload", {}).get(key)
        return f"DL {dl if dl is not None else 'N/A'} / UL {ul if ul is not None else 'N/A'}"

    return {
        "avg": combined("avg"),
        "min": combined("min"),
        "max": combined("max"),
        "transfer": combined("transfer"),
    }


_BAND_TOKEN_RE = re.compile(r"^(n?)(?:b)?(\d+)([acdef]?)$", re.IGNORECASE)
_CLASS_CARRIERS = {"A": 1, "C": 2, "D": 3, "E": 4, "F": 5}


def _fmt_bw(value: float) -> str:
    return str(int(value)) if float(value).is_integer() else f"{value:g}"


def _normalize_band_config(value: str) -> str:
    """Normalize user shorthand and apply Class-A when omitted.

    Examples: B5 -> 5A, 5 -> 5A, n78 -> n78A.  '_' remains the
    LTE/NR boundary and '-' remains the same-RAT band separator.
    """
    text = str(value or "").strip().replace(" ", "")
    text = text.replace("＿", "_").replace("－", "-")
    if not text:
        raise ValueError("缺少 band_config；Band 是每筆測項唯一不能省略的參數。")
    if text.count("_") > 1:
        raise ValueError("Band 組合最多只能有一個 '_'。")

    def normalize_side(side: str, expected_nr: bool) -> str:
        parts: list[str] = []
        for raw in side.split("-"):
            match = _BAND_TOKEN_RE.fullmatch(raw)
            if not match:
                raise ValueError(f"Band 格式不正確：{raw}")
            nr_prefix, band_no, ca_class = match.groups()
            is_nr = bool(nr_prefix)
            if is_nr != expected_nr:
                label = "NR" if expected_nr else "LTE"
                raise ValueError(f"{label} 區段的 Band 格式不正確：{raw}")
            ca = (ca_class or "A").upper()
            parts.append(f"{'n' if is_nr else ''}{int(band_no)}{ca}")
        return "-".join(parts)

    if "_" in text:
        lte_side, nr_side = text.split("_", 1)
        if not lte_side or not nr_side:
            raise ValueError("'_' 左邊必須是 LTE，右邊必須是 NR。")
        return f"{normalize_side(lte_side, False)}_{normalize_side(nr_side, True)}"
    if text.lower().startswith("n"):
        return normalize_side(text, True)
    return normalize_side(text, False)


def _default_bandwidth_config(band_config: str) -> str:
    """Resolve omitted BW using the same LTE/NR databases as single mode."""
    lte_side = ""
    nr_side = ""
    if "_" in band_config:
        lte_side, nr_side = band_config.split("_", 1)
    elif band_config.lower().startswith("n"):
        nr_side = band_config
    else:
        lte_side = band_config

    lte_values: list[float] = []
    if lte_side:
        db = LteBandDatabase(ROOT / "plugins" / "amarisoft" / "Earfcn_LTE.json")
        for token in lte_side.split("-"):
            match = _BAND_TOKEN_RE.fullmatch(token)
            if not match:
                raise ValueError(f"LTE Band 格式不正確：{token}")
            _, band_no, ca_class = match.groups()
            count = _CLASS_CARRIERS[(ca_class or "A").upper()]
            default_bw = db.get_default_bandwidth(int(band_no))
            lte_values.extend([default_bw] * count)

    nr_values: list[float] = []
    if nr_side:
        policy = get_bandwidth_policy(
            nr_side, ROOT / "plugins" / "amarisoft" / "nr_spec.json"
        )
        if policy.uses_bcs_allowed and policy.allowed_profiles_mhz:
            # Prefer the largest legal BCS profile. This preserves the team's
            # established defaults such as n78C -> 100+100 while n1A -> 20.
            nr_values = list(max(
                policy.allowed_profiles_mhz,
                key=lambda profile: (sum(profile), tuple(profile)),
            ))
        else:
            nr_values = [max(values) for values in policy.per_carrier_allowed_bandwidths_mhz]

    lte_text = "+".join(_fmt_bw(v) for v in lte_values)
    nr_text = "+".join(_fmt_bw(v) for v in nr_values)
    if lte_values and nr_values:
        return f"{lte_text}_{nr_text}"
    return lte_text or nr_text


def _normalize_item(item: dict[str, Any], index: int) -> dict[str, Any]:
    band = _normalize_band_config(str(item.get("band_config") or item.get("band") or ""))
    bw = str(item.get("bandwidth_config") or item.get("bw") or item.get("bandwidth") or "").strip()
    used_default_bw = not bool(bw)
    if used_default_bw:
        bw = _default_bandwidth_config(band)
    action = str(item.get("action") or "phy").strip().lower()
    aliases = {"phy":"phy", "iperf_upload":"upload", "upload":"upload", "tx":"upload",
               "iperf_download":"download", "download":"download", "rx":"download",
               "iperf_both":"bidirectional", "both":"bidirectional", "bidirectional":"bidirectional", "trx":"bidirectional"}
    if action not in aliases: raise ValueError(f"第 {index + 1} 筆 action 不支援：{action}")
    return {**item, "band_config": band, "bandwidth_config": bw, "action": aliases[action],
            "duration_sec": int(item.get("duration_sec") or item.get("duration") or 30),
            "_default_bandwidth_applied": used_default_bw}


def _band_cli(item: dict[str, Any]) -> tuple[str, dict[str, Any], list[str]]:
    combo = parse_radio_combination(item["band_config"], item["bandwidth_config"])
    if combo.mode == "LTE":
        intent = "set_band"
        params = {"band_config": item["band_config"], "bandwidth_config": item["bandwidth_config"]}
    else:
        intent = "set_nr_band"
        params = {"band_config": item["band_config"], "bandwidth_config": item["bandwidth_config"], "radio_mode": "SA" if combo.mode == "SA" else "ENDC"}
    validated = validate_intent(ROOT, intent, params)
    cli_args = list(validated.cli_args)
    if item.get("action") == "phy" and "--phy-only-ready" not in cli_args:
        cli_args.append("--phy-only-ready")
    return intent, validated.parameters, cli_args


def _build_row(
    index: int,
    item: dict[str, Any],
    started: str,
    finished: str,
    band_result: dict[str, Any],
    iperf_result: dict[str, Any] | None,
    error: str,
) -> dict[str, Any]:
    phy_dl, phy_ul, connected = _extract_phy(band_result)
    iperf_payload = iperf_result or {}
    summary = _iperf_summary(iperf_payload)
    details = _extract_iperf_details(iperf_payload)
    success = bool(band_result.get("success")) and (
        iperf_result is None or bool(iperf_result.get("success"))
    )
    action = item["action"]
    download = details.get("download", {})
    upload = details.get("upload", {})
    unknown = details.get("unknown", {})
    if action == "download" and not download:
        download = unknown
    if action == "upload" and not upload:
        upload = unknown

    return {
        "序號": index + 1,
        "Band": item["band_config"],
        "BW": item["bandwidth_config"],
        "ARFCN": _extract_arfcn(band_result),
        "測試類型": "PHY" if action == "phy" else "PHY+iPerf",
        "iPerf方向": "" if action == "phy" else action,
        "測試秒數": "" if action == "phy" else item["duration_sec"],
        "設定開始時間": started,
        "設定完成時間": finished,
        "UE連線狀態": connected,
        "PHY DL Mbps": phy_dl,
        "PHY UL Mbps": phy_ul,
        # Legacy/general columns are retained. For TRX they contain readable
        # DL/UL text; the direction-specific numeric columns below are the
        # authoritative values.
        "iPerf平均 Mbps": summary["avg"],
        "iPerf最低 Mbps": summary["min"],
        "iPerf最高 Mbps": summary["max"],
        "傳輸量 MB": summary["transfer"],
        "Download 平均 Mbps": download.get("avg"),
        "Download 最低 Mbps": download.get("min"),
        "Download 最高 Mbps": download.get("max"),
        "Download 傳輸量 MB": download.get("transfer"),
        "Upload 平均 Mbps": upload.get("avg"),
        "Upload 最低 Mbps": upload.get("min"),
        "Upload 最高 Mbps": upload.get("max"),
        "Upload 傳輸量 MB": upload.get("transfer"),
        "結果": "PASS" if success else "FAIL",
        "錯誤原因": error,
        # Internal notification metadata. batch_report.py writes only HEADERS.
        "_ue_timeout": _is_ue_timeout(band_result),
        "_wait_ue_timeout_sec": _ue_timeout_sec(band_result),
        "_iperf_skipped_due_to_ue": bool(_is_ue_timeout(band_result) and action != "phy"),
        "_band_success": bool(band_result.get("success")),
        "_iperf_success": None if iperf_result is None else bool(iperf_result.get("success")),
        "_iperf_details": details,
    }



def _display_metric(value: Any) -> str:
    return "N/A" if value in (None, "") else str(value)


def _iperf_direction_lines(row: dict[str, Any]) -> list[str]:
    """Format one-way or TRX iPerf data from the report row."""
    direction = str(row.get("iPerf方向") or "").lower()
    duration = row.get("測試秒數") or "N/A"
    details = row.get("_iperf_details") if isinstance(row.get("_iperf_details"), dict) else {}
    iperf_success = row.get("_iperf_success")
    status = "PASS" if iperf_success is True else "FAIL" if iperf_success is False else "未執行"

    label = {
        "download": "Download iPerf",
        "upload": "Upload iPerf",
        "bidirectional": "TRX iPerf",
    }.get(direction, f"{direction or 'iPerf'}")
    lines = ["", f"2. {label}（{duration}s）：{status}"]

    def add_direction(name: str, title: str, fallback_prefix: str | None = None) -> None:
        values = details.get(name, {}) if isinstance(details, dict) else {}
        if not values and fallback_prefix:
            values = {
                "avg": row.get(f"{fallback_prefix} 平均 Mbps"),
                "min": row.get(f"{fallback_prefix} 最低 Mbps"),
                "max": row.get(f"{fallback_prefix} 最高 Mbps"),
                "transfer": row.get(f"{fallback_prefix} 傳輸量 MB"),
            }
        lines.extend([
            "",
            f"【{title}】",
            f"• 平均速度：{_display_metric(values.get('avg'))} Mbps",
            f"• 最高／最低：{_display_metric(values.get('max'))} / {_display_metric(values.get('min'))} Mbps",
            f"• 總傳輸量：{_display_metric(values.get('transfer'))} MB",
        ])

    if direction == "bidirectional":
        add_direction("download", "Download", "Download")
        add_direction("upload", "Upload", "Upload")
    elif direction == "download":
        add_direction("download", "Download", "Download")
    elif direction == "upload":
        add_direction("upload", "Upload", "Upload")
    else:
        lines.extend([
            "",
            f"• 平均速度：{_display_metric(row.get('iPerf平均 Mbps'))} Mbps",
            f"• 最高／最低：{_display_metric(row.get('iPerf最高 Mbps'))} / {_display_metric(row.get('iPerf最低 Mbps'))} Mbps",
            f"• 總傳輸量：{_display_metric(row.get('傳輸量 MB'))} MB",
        ])
    return lines


def _format_detailed_item_result(index: int, total: int, row: dict[str, Any]) -> str:
    """Build the complete per-item Telegram result.

    The first line is only a heading. Every notification also includes Band,
    BW, ARFCN, UE/PHY and iPerf details, matching the single-operation reply.
    """
    result = str(row.get("結果") or "UNKNOWN")
    band = str(row.get("Band") or "N/A")
    bw = str(row.get("BW") or "N/A")
    arfcn = str(row.get("ARFCN") or "N/A")
    ue_state = str(row.get("UE連線狀態") or "未知")
    phy_dl = row.get("PHY DL Mbps")
    phy_ul = row.get("PHY UL Mbps")
    test_type = str(row.get("測試類型") or "PHY")
    iperf_dir = str(row.get("iPerf方向") or "")
    duration = row.get("測試秒數")
    error = str(row.get("錯誤原因") or "").strip()

    if result == "FAIL" and row.get("_ue_timeout"):
        upper_band = band.upper()
        if "_" in upper_band:
            title = f"❌ 批次測試 [{index}/{total}]：ENDC 設定完成，但 UE 未連線"
            advice = "請確認 UE 是否 Attach、SIM／RF、SCS、ARFCN 與 Time Slot 是否正確。"
        elif upper_band.startswith("N"):
            title = f"❌ 批次測試 [{index}/{total}]：SA NR 設定完成，但 UE 未連線"
            advice = "請確認 UE 是否 Attach、SIM／RF、SCS、ARFCN 與 Time Slot 是否正確。"
        else:
            title = f"❌ 批次測試 [{index}/{total}]：Band 設定完成，但 UE 未連線"
            advice = "請確認 UE 是否 Attach、SIM／RF 是否正常。"
        lines = [
            title,
            "",
            "1. Band 設定：FAIL",
            "",
            f"• Band：{band}（{bw}MHz，ARFCN：{arfcn}）",
            f"• 等待 UE／PHY 逾時：{row.get('_wait_ue_timeout_sec') or 180} 秒",
            f"• 連線：{ue_state}",
            f"• PHY DL／UL：{phy_dl if phy_dl is not None else 0} / {phy_ul if phy_ul is not None else 0} Mbps",
        ]
        if test_type == "PHY+iPerf":
            iperf_label = {
                "upload": "Upload iPerf",
                "download": "Download iPerf",
                "bidirectional": "TRX iPerf",
            }.get(iperf_dir.lower(), iperf_dir or "iPerf")
            lines += [
                "",
                f"2. {iperf_label}（{duration or 'N/A'}s）：未執行",
                "",
                "• 原因：UE 尚未連線",
            ]
        if error:
            lines += ["", f"• 錯誤原因：{error}"]
        lines += ["", advice]
    else:
        band_success = bool(row.get("_band_success", result == "PASS"))
        iperf_success = row.get("_iperf_success")
        icon = "✅" if result == "PASS" else "❌"
        lines = [
            f"{icon} 批次測試 [{index}/{total}] {'完成' if result == 'PASS' else '失敗'}",
            "",
            f"1. Band 設定：{'PASS' if band_success else 'FAIL'}",
            "",
            f"• Band：{band}（{bw}MHz，ARFCN：{arfcn}）",
            f"• 連線：{ue_state}",
            f"• PHY DL／UL：{phy_dl if phy_dl is not None else 'N/A'} / {phy_ul if phy_ul is not None else 'N/A'} Mbps",
        ]
        if test_type == "PHY+iPerf":
            lines += _iperf_direction_lines(row)
        if error:
            lines += ["", f"• 錯誤原因：{error}"]
        lines += ["", "• 本筆資料已寫入 Excel 與 TXT 報表"]

    if index < total:
        lines += ["", f"下一筆：[{index + 1}/{total}]"]
    return "\n".join(lines)


def run(batch_id: str) -> int:
    manager = BatchManager(ROOT)
    state = manager.read(batch_id)
    if state.get("status") == "not_found": return 2
    notifier = BatchNotifier(ROOT, state.get("notification_target"))
    manager.update(
        batch_id,
        status="running",
        worker_pid=os.getpid(),
        started_at=state.get("started_at") or _now(),
        notification_target=notifier.target,
        message="批次測試執行中",
    )
    output_dir = ROOT / "batch_results"

    while True:
        state = manager.read(batch_id)
        if state.get("stop_requested"):
            reports = write_reports(output_dir, batch_id, state.get("results", []))
            final_state = manager.update(
                batch_id,
                status="stopped",
                reports=reports,
                finished_at=_now(),
                message="批次測試已停止，報表已產生。",
                report_delivery_status="sending",
            )
            notifier.send(format_batch_final("stopped", final_state))
            delivery = send_report_files(notifier, reports, "批次測試已停止")
            manager.update(
                batch_id,
                report_delivery=delivery,
                report_delivery_status=(
                    "sent" if all(item.get("success") for item in delivery.values()) else "partial"
                ),
            )
            return 0
        if state.get("pause_requested"):
            reports = write_reports(output_dir, batch_id, state.get("results", []))
            final_state = manager.update(
                batch_id,
                status="paused",
                reports=reports,
                paused_at=_now(),
                message="批次測試已暫停，階段報表已產生。",
                report_delivery_status="sending",
            )
            notifier.send(format_batch_final("paused", final_state))
            delivery = send_report_files(notifier, reports, "批次測試已暫停")
            manager.update(
                batch_id,
                report_delivery=delivery,
                report_delivery_status=(
                    "sent" if all(item.get("success") for item in delivery.values()) else "partial"
                ),
            )
            return 0
        index = int(state.get("next_index") or 0)
        items = state.get("items") or []
        if index >= len(items):
            reports = write_reports(output_dir, batch_id, state.get("results", []))
            final_state = manager.update(
                batch_id,
                status="completed",
                reports=reports,
                finished_at=_now(),
                message="批次測試全部完成。",
                report_delivery_status="sending",
            )
            notifier.send(format_batch_final("completed", final_state))
            delivery = send_report_files(notifier, reports, "批次測試完成")
            manager.update(
                batch_id,
                report_delivery=delivery,
                report_delivery_status=(
                    "sent" if all(item.get("success") for item in delivery.values()) else "partial"
                ),
            )
            return 0

        raw = items[index]
        started = _now(); band_result: dict[str, Any] = {}; iperf_result: dict[str, Any] | None = None; error = ""
        try:
            item = _normalize_item(raw, index)
            manager.update(batch_id, current_index=index, current_item=item, message=f"正在執行第 {index + 1}/{len(items)} 筆：{item['band_config']}")
            notifier.send(format_item_started(index + 1, len(items), item))
            intent, parameters, cli = _band_cli(item)
            band_result = _run_operation(
                batch_id, intent, parameters, cli, manager, notifier,
                index + 1, len(items),
            )
            if not band_result.get("success"):
                error = str(band_result.get("human_summary") or band_result.get("message") or band_result.get("error") or "Band 設定失敗")
            elif item["action"] != "phy":
                phy_dl_now, phy_ul_now, _ = _extract_phy(band_result)
                notifier.send(
                    f"📶 批次測試 [{index + 1}/{len(items)}] 已取得 PHY\n\n"
                    f"• Band：{item['band_config']}\n"
                    f"• PHY DL／UL：{phy_dl_now if phy_dl_now is not None else 'N/A'} / "
                    f"{phy_ul_now if phy_ul_now is not None else 'N/A'} Mbps\n"
                    f"• 下一階段：準備執行 {item['action']} iPerf {item['duration_sec']} 秒"
                )
                ue_ip = _extract_ip(band_result)
                if not ue_ip:
                    error = "找不到可用的 UE IP，無法執行 iPerf。"
                else:
                    iperf_validated = validate_intent(
                        ROOT,
                        "iperf_run",
                        {
                            "ue_ip": ue_ip,
                            "direction": item["action"],
                            "duration_sec": item["duration_sec"],
                        },
                    )
                    iperf_result = _run_operation(
                        batch_id,
                        iperf_validated.intent,
                        iperf_validated.parameters,
                        iperf_validated.cli_args,
                        manager, notifier, index + 1, len(items),
                    )
                    if not iperf_result.get("success"):
                        error = str(iperf_result.get("human_summary") or iperf_result.get("message") or iperf_result.get("error") or "iPerf 失敗")
        except Exception as exc:
            item = {"band_config": str(raw.get("band_config") or raw.get("band") or ""), "bandwidth_config": str(raw.get("bandwidth_config") or raw.get("bw") or ""), "action": str(raw.get("action") or "phy"), "duration_sec": raw.get("duration_sec") or 30}
            error = str(exc)
            band_result = {"success": False, "error": type(exc).__name__, "message": error}

        row = _build_row(index, item, started, _now(), band_result, iperf_result, error)
        state = manager.read(batch_id)
        results = list(state.get("results") or []); results.append(row)
        reports = write_reports(output_dir, batch_id, results)
        manager.update(
            batch_id, results=results, completed=len(results), next_index=index + 1,
            reports=reports, last_result=row,
            current_operation_id=None, current_operation_status=None,
            current_operation_stage=None, current_operation_message=None,
            message=f"第 {index + 1}/{len(items)} 筆完成：{row['結果']}",
        )
        detail_message = _format_detailed_item_result(index + 1, len(items), row)
        # Safety net: per-item notifications must never be only a one-line
        # PASS/FAIL status.  This also makes mixed-version deployments obvious.
        if "\n" not in detail_message.strip():
            detail_message = (
                f"{'✅' if row.get('結果') == 'PASS' else '❌'} 批次測試 [{index + 1}/{len(items)}] {row.get('結果', 'UNKNOWN')}\n\n"
                f"• Band 組合：{row.get('Band') or 'N/A'}\n"
                f"• BW：{row.get('BW') or 'N/A'}\n"
                f"• ARFCN：{row.get('ARFCN') or 'N/A'}\n"
                f"• UE：{row.get('UE連線狀態') or '未知'}\n"
                f"• PHY DL／UL：{row.get('PHY DL Mbps') if row.get('PHY DL Mbps') is not None else 'N/A'} / "
                f"{row.get('PHY UL Mbps') if row.get('PHY UL Mbps') is not None else 'N/A'} Mbps\n"
                f"• 原因：{row.get('錯誤原因') or '無'}"
            )
        # The detailed result is the authoritative per-item notification.
        # Retry transient OpenClaw/Telegram failures so the operator never sees
        # only a bare PASS/FAIL status.
        ok = False
        notify_error = ""
        for attempt in range(3):
            ok, notify_error = notifier.send(detail_message)
            if ok:
                break
            time.sleep(2 * (attempt + 1))
        if not ok:
            manager.update(
                batch_id,
                notification_error=notify_error,
                message=f"第 {index + 1}/{len(items)} 筆完成，但詳細 Telegram 訊息傳送失敗：{notify_error}",
            )
        if row["結果"] == "FAIL" and not bool(state.get("continue_on_error", True)):
            manager.update(batch_id, pause_requested=True, message="測項失敗，已依設定準備暫停。")


if __name__ == "__main__":
    raise SystemExit(run(sys.argv[1]) if len(sys.argv) == 2 else 2)
