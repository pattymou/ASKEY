from __future__ import annotations

import json
import os
import re
import shutil
import subprocess
from pathlib import Path
from typing import Any, Callable


TRANSPORT_VERSION = "openclaw-direct-node-v4"


def _hidden_process_kwargs() -> dict[str, Any]:
    kwargs: dict[str, Any] = {}
    if os.name == "nt":
        startupinfo = subprocess.STARTUPINFO()
        startupinfo.dwFlags |= subprocess.STARTF_USESHOWWINDOW
        startupinfo.wShowWindow = subprocess.SW_HIDE
        kwargs["startupinfo"] = startupinfo
        kwargs["creationflags"] = subprocess.CREATE_NO_WINDOW
    return kwargs


def _base_env() -> dict[str, str]:
    env = os.environ.copy()
    env["PYTHONIOENCODING"] = "utf-8"
    env["PYTHONUTF8"] = "1"
    return env


def _run_command(args: list[str], timeout: int = 30) -> subprocess.CompletedProcess[str]:
    return subprocess.run(
        args,
        capture_output=True,
        text=True,
        encoding="utf-8",
        errors="replace",
        timeout=timeout,
        env=_base_env(),
        **_hidden_process_kwargs(),
    )


def _resolve_openclaw_direct_command(executable: str) -> list[str] | None:
    """Return ``[node.exe, openclaw.mjs]`` for a global npm installation.

    Calling the npm ``openclaw.cmd``/``openclaw.ps1`` shim for every Telegram
    notification can create a visible Windows Terminal window.  Directly
    starting Node with the OpenClaw entry point preserves multiline arguments
    and allows ``CREATE_NO_WINDOW`` to work reliably.
    """
    resolved = shutil.which(executable) or executable
    shim = Path(resolved)
    if not shim.is_absolute():
        try:
            shim = shim.resolve()
        except OSError:
            pass

    roots: list[Path] = []
    if shim.parent != Path("."):
        roots.append(shim.parent)

    # Standard npm global layout:
    #   %APPDATA%\\npm\\openclaw.cmd
    #   %APPDATA%\\npm\\node_modules\\openclaw\\openclaw.mjs
    script_candidates: list[Path] = []
    for root in roots:
        script_candidates.extend([
            root / "node_modules" / "openclaw" / "openclaw.mjs",
            root / "node_modules" / "openclaw" / "dist" / "cli.js",
            root / "node_modules" / "openclaw" / "dist" / "index.js",
        ])

    # Also read the npm .cmd shim when available so non-standard installs can
    # still be resolved without launching the wrapper.
    if shim.suffix.lower() == ".cmd" and shim.is_file():
        try:
            text = shim.read_text(encoding="utf-8", errors="ignore")
            for match in re.finditer(
                r'(?i)["\']?(?:%dp0%|%~dp0)[\\/]+([^"\'\r\n]*?openclaw(?:\\|/)openclaw\\.mjs)',
                text,
            ):
                relative = match.group(1).replace("\\", os.sep).replace("/", os.sep)
                script_candidates.append(shim.parent / relative)
        except OSError:
            pass

    script: Path | None = None
    seen: set[str] = set()
    for candidate in script_candidates:
        key = str(candidate).lower()
        if key in seen:
            continue
        seen.add(key)
        if candidate.is_file():
            script = candidate.resolve()
            break
    if script is None:
        return None

    node_candidates: list[str] = []
    for root in roots:
        local_node = root / "node.exe"
        if local_node.is_file():
            node_candidates.append(str(local_node.resolve()))
    system_node = shutil.which("node") or shutil.which("node.exe")
    if system_node:
        node_candidates.append(system_node)
    if not node_candidates:
        return None
    return [node_candidates[0], str(script)]


def _single_line_fallback(value: str) -> str:
    """Preserve line breaks without passing CR/LF through an npm .cmd shim."""
    return value.replace("\r\n", "\u2028").replace("\r", "\u2028").replace("\n", "\u2028")


def _run_openclaw_cli(
    executable: str,
    cli_args: list[str],
    timeout: int,
) -> subprocess.CompletedProcess[str]:
    """Run OpenClaw with no visible console and intact multiline messages.

    On Windows, invoke the OpenClaw JavaScript entry point with ``node.exe``
    directly.  This bypasses both npm shims and PowerShell, which prevents one
    Windows Terminal window from being opened for every batch notification.
    """
    if os.name != "nt":
        return _run_command([executable, *cli_args], timeout)

    direct = _resolve_openclaw_direct_command(executable)
    if direct is not None:
        return _run_command([*direct, *cli_args], timeout)

    # Last-resort compatibility path.  Keep the message as one argv line so
    # cmd.exe cannot split it; Telegram renders U+2028 as a visual line break.
    safe_args = [_single_line_fallback(value) for value in cli_args]
    return _run_command([executable, *safe_args], timeout)

def _session_rows(payload: Any) -> list[dict[str, Any]]:
    if isinstance(payload, dict):
        rows = payload.get("sessions")
        if isinstance(rows, list):
            return [row for row in rows if isinstance(row, dict)]
        for value in payload.values():
            found = _session_rows(value)
            if found:
                return found
    elif isinstance(payload, list):
        return [row for row in payload if isinstance(row, dict)]
    return []


def _target_from_session(row: dict[str, Any]) -> str | None:
    # Newer OpenClaw session rows may persist a deliveryContext.
    for container_key in ("deliveryContext", "delivery_context", "route", "active"):
        value = row.get(container_key)
        if isinstance(value, dict):
            channel = str(value.get("channel") or value.get("provider") or "").lower()
            target = value.get("target") or value.get("to") or value.get("chatId") or value.get("chat_id")
            if channel == "telegram" and target not in (None, ""):
                return str(target)

    # Common durable Telegram session key:
    # agent:main:telegram:direct:123456789
    key = str(row.get("key") or row.get("sessionKey") or "")
    match = re.search(r":telegram:(?:direct|group|channel):([^:]+)(?::thread:[^:]+)?$", key, re.IGNORECASE)
    if match:
        return match.group(1)
    return None


def resolve_recent_telegram_target(
    runner: Callable[[str, list[str], int], subprocess.CompletedProcess[str]] = _run_openclaw_cli,
) -> str | None:
    """Resolve the most recently active Telegram conversation target."""
    executable = shutil.which("openclaw") or "openclaw"
    try:
        proc = runner(executable, ["sessions", "--active", "180", "--limit", "25", "--json"], 30)
        if proc.returncode != 0 or not (proc.stdout or "").strip():
            return None
        payload = json.loads(proc.stdout)
    except Exception:
        return None

    rows = _session_rows(payload)
    # CLI returns newest rows first. If not, common timestamp keys keep the
    # most recently active row first without requiring a fixed schema.
    rows.sort(
        key=lambda row: str(
            row.get("updatedAt")
            or row.get("updated_at")
            or row.get("lastActivityAt")
            or row.get("last_activity_at")
            or ""
        ),
        reverse=True,
    )
    for row in rows:
        target = _target_from_session(row)
        if target:
            return target
    return None


class BatchNotifier:
    def __init__(self, root: Path, target: str | None = None) -> None:
        self.root = root
        self.target = str(target).strip() if target else None
        if not self.target:
            self.target = resolve_recent_telegram_target()
        self.executable = shutil.which("openclaw") or "openclaw"

    @property
    def enabled(self) -> bool:
        return bool(self.target)

    def send(self, message: str) -> tuple[bool, str]:
        if not self.target:
            return False, "找不到最近使用的 Telegram 對話 target。"
        try:
            proc = _run_openclaw_cli(
                self.executable,
                [
                    "message",
                    "send",
                    "--channel",
                    "telegram",
                    "--target",
                    self.target,
                    "--message",
                    message,
                ],
                45,
            )
        except Exception as exc:
            return False, str(exc)
        if proc.returncode != 0:
            return False, (proc.stderr or proc.stdout or "OpenClaw message send failed").strip()[-1000:]
        return True, ""

    def send_file(self, path: str | Path, caption: str) -> tuple[bool, str]:
        if not self.target:
            return False, "找不到最近使用的 Telegram 對話 target。"
        file_path = Path(path)
        if not file_path.exists():
            return False, f"報表不存在：{file_path}"
        try:
            proc = _run_openclaw_cli(
                self.executable,
                [
                    "message",
                    "send",
                    "--channel",
                    "telegram",
                    "--target",
                    self.target,
                    "--message",
                    caption,
                    "--media",
                    str(file_path),
                    "--force-document",
                ],
                90,
            )
        except Exception as exc:
            return False, str(exc)
        if proc.returncode != 0:
            return False, (proc.stderr or proc.stdout or "OpenClaw file send failed").strip()[-1000:]
        return True, ""



def send_report_files(
    notifier: "BatchNotifier",
    reports: dict[str, Any],
    caption_prefix: str,
) -> dict[str, dict[str, Any]]:
    """Send XLSX/TXT as clickable Telegram document attachments."""
    delivery: dict[str, dict[str, Any]] = {}
    for kind in ("xlsx", "txt"):
        path = reports.get(kind)
        if not path:
            delivery[kind] = {"success": False, "error": "REPORT_PATH_MISSING"}
            continue
        ok, error = notifier.send_file(path, f"{caption_prefix}：{kind.upper()} 報表")
        delivery[kind] = {"success": ok, "error": error, "path": str(path)}
    return delivery


def format_item_started(index: int, total: int, item: dict[str, Any]) -> str:
    action_map = {
        "phy": "只記錄 PHY",
        "upload": f"Upload iPerf {item.get('duration_sec', 30)} 秒",
        "download": f"Download iPerf {item.get('duration_sec', 30)} 秒",
        "bidirectional": f"雙向 iPerf，各 {item.get('duration_sec', 30)} 秒",
    }
    action = action_map.get(str(item.get("action") or "phy"), str(item.get("action") or "phy"))
    return (
        f"▶️ 批次測試 [{index}/{total}] 開始\n\n"
        f"• Band：{item.get('band_config')}\n"
        f"• BW：{item.get('bandwidth_config')}\n"
        f"• 測試：{action}\n"
        "• 狀態：正在切換 Band，等待 UE／PHY"
    )


def _metric(value: Any) -> str:
    return "N/A" if value in (None, "") else str(value)


def _row_iperf_details(row: dict[str, Any]) -> dict[str, dict[str, Any]]:
    details = row.get("_iperf_details")
    if isinstance(details, dict) and details:
        return {
            str(key).lower(): value
            for key, value in details.items()
            if isinstance(value, dict)
        }

    # Rows loaded from an XLSX/TXT-compatible state may only have public
    # direction-specific columns. Rebuild the same detail shape here.
    result: dict[str, dict[str, Any]] = {}
    for direction, prefix in (("download", "Download"), ("upload", "Upload")):
        values = {
            "avg": row.get(f"{prefix} 平均 Mbps"),
            "min": row.get(f"{prefix} 最低 Mbps"),
            "max": row.get(f"{prefix} 最高 Mbps"),
            "transfer": row.get(f"{prefix} 傳輸量 MB"),
        }
        if any(value not in (None, "") for value in values.values()):
            result[direction] = values
    return result


def _iperf_full_lines(row: dict[str, Any]) -> list[str]:
    direction = str(row.get("iPerf方向") or "").lower()
    duration = row.get("測試秒數") or "N/A"
    details = _row_iperf_details(row)
    success = row.get("_iperf_success")
    status = "PASS" if success is True else "FAIL" if success is False else "未執行"
    label = {
        "download": "Download iPerf",
        "upload": "Upload iPerf",
        "bidirectional": "TRX iPerf",
    }.get(direction, direction or "iPerf")
    lines = ["", f"2. {label}（{duration}s）：{status}"]

    def append_direction(key: str, title: str) -> None:
        values = details.get(key, {})
        lines.extend([
            "",
            f"【{title}】",
            f"• 平均速度：{_metric(values.get('avg'))} Mbps",
            f"• 最高／最低：{_metric(values.get('max'))} / {_metric(values.get('min'))} Mbps",
            f"• 總傳輸量：{_metric(values.get('transfer'))} MB",
        ])

    if direction == "bidirectional":
        append_direction("download", "Download")
        append_direction("upload", "Upload")
    elif direction == "download":
        append_direction("download", "Download")
    elif direction == "upload":
        append_direction("upload", "Upload")
    else:
        lines.extend([
            "",
            f"• 平均速度：{_metric(row.get('iPerf平均 Mbps'))} Mbps",
            f"• 最高／最低：{_metric(row.get('iPerf最高 Mbps'))} / {_metric(row.get('iPerf最低 Mbps'))} Mbps",
            f"• 總傳輸量：{_metric(row.get('傳輸量 MB'))} MB",
        ])
    return lines


def _iperf_compact_text(row: dict[str, Any]) -> str:
    direction = str(row.get("iPerf方向") or "").lower()
    duration = row.get("測試秒數") or "N/A"
    details = _row_iperf_details(row)

    def one(key: str, label: str) -> str:
        values = details.get(key, {})
        return (
            f"{label} avg {_metric(values.get('avg'))} Mbps "
            f"(min {_metric(values.get('min'))}, max {_metric(values.get('max'))}, "
            f"{_metric(values.get('transfer'))} MB)"
        )

    if direction == "bidirectional":
        return f"TRX {duration}s：{one('download', 'DL')}；{one('upload', 'UL')}"
    if direction == "download":
        return f"Download {duration}s：{one('download', 'DL')}"
    if direction == "upload":
        return f"Upload {duration}s：{one('upload', 'UL')}"
    return (
        f"iPerf {direction or ''} {duration}s：avg {_metric(row.get('iPerf平均 Mbps'))} Mbps, "
        f"min {_metric(row.get('iPerf最低 Mbps'))}, max {_metric(row.get('iPerf最高 Mbps'))}, "
        f"{_metric(row.get('傳輸量 MB'))} MB"
    )


def format_item_finished(index: int, total: int, row: dict[str, Any]) -> str:
    """Return a complete per-item result message, never bare PASS/FAIL."""
    result = str(row.get("結果") or "UNKNOWN")
    band = str(row.get("Band") or "N/A")
    bw = str(row.get("BW") or "N/A")
    arfcn = str(row.get("ARFCN") or "N/A")
    ue_state = str(row.get("UE連線狀態") or "未知")
    phy_dl = row.get("PHY DL Mbps")
    phy_ul = row.get("PHY UL Mbps")
    test_type = str(row.get("測試類型") or "PHY")
    error = str(row.get("錯誤原因") or "").strip()

    if result == "FAIL" and row.get("_ue_timeout"):
        upper_band = band.upper()
        if "_" in upper_band:
            heading = f"❌ 批次測試 [{index}/{total}]：ENDC 設定完成，但 UE 未連線"
            advice = "請確認 UE 是否 Attach、SIM／RF、SCS、ARFCN 與 Time Slot 是否正確。"
        elif upper_band.startswith("N"):
            heading = f"❌ 批次測試 [{index}/{total}]：SA NR 設定完成，但 UE 未連線"
            advice = "請確認 UE 是否 Attach、SIM／RF、SCS、ARFCN 與 Time Slot 是否正確。"
        else:
            heading = f"❌ 批次測試 [{index}/{total}]：Band 設定完成，但 UE 未連線"
            advice = "請確認 UE 是否 Attach、SIM／RF 是否正常。"
        lines = [
            heading,
            "",
            "1. Band 設定：FAIL",
            "",
            f"• Band：{band}（{bw}MHz，ARFCN：{arfcn}）",
            f"• 等待 UE／PHY 逾時：{row.get('_wait_ue_timeout_sec') or 180} 秒",
            f"• 連線：{ue_state}",
            f"• PHY DL／UL：{_metric(phy_dl if phy_dl is not None else 0)} / {_metric(phy_ul if phy_ul is not None else 0)} Mbps",
        ]
        if test_type == "PHY+iPerf":
            lines.extend(["", "2. iPerf：未執行", "", "• 原因：UE 尚未連線"])
        if error:
            lines.extend(["", f"• 錯誤原因：{error}"])
        lines.extend(["", advice])
    else:
        icon = "✅" if result == "PASS" else "❌"
        band_success = bool(row.get("_band_success", result == "PASS"))
        lines = [
            f"{icon} 批次測試 [{index}/{total}] {'完成' if result == 'PASS' else '失敗'}",
            "",
            f"1. Band 設定：{'PASS' if band_success else 'FAIL'}",
            "",
            f"• Band：{band}（{bw}MHz，ARFCN：{arfcn}）",
            f"• 連線：{ue_state}",
            f"• PHY DL／UL：{_metric(phy_dl)} / {_metric(phy_ul)} Mbps",
        ]
        if test_type == "PHY+iPerf":
            lines.extend(_iperf_full_lines(row))
        if error:
            lines.extend(["", f"• 錯誤原因：{error}"])
        lines.extend(["", "• 本筆資料已寫入 Excel 與 TXT 報表"])

    if index < total:
        lines.extend(["", f"下一筆：[{index + 1}/{total}]"])
    return "\n".join(lines)


def _has_metric(value: Any) -> bool:
    return value not in (None, "")


def _append_metric_lines(lines: list[str], values: dict[str, Any]) -> None:
    metric_rows = [
        ("平均", values.get("avg"), "Mbps"),
        ("最低", values.get("min"), "Mbps"),
        ("最高", values.get("max"), "Mbps"),
        ("傳輸量", values.get("transfer"), "MB"),
    ]
    for label, value, unit in metric_rows:
        if _has_metric(value):
            lines.append(f"• {label}：{value} {unit}")


def _final_result_block(row: dict[str, Any]) -> str:
    """Format one final result as readable sections without semicolons."""
    idx = row.get("序號") or "?"
    band = row.get("Band") or "N/A"
    bw = row.get("BW") or "N/A"
    arfcn = row.get("ARFCN") or "N/A"
    overall = str(row.get("結果") or "UNKNOWN")
    error = str(row.get("錯誤原因") or "").strip()
    ue_state = row.get("UE連線狀態") or "未知"
    phy_dl = row.get("PHY DL Mbps")
    phy_ul = row.get("PHY UL Mbps")
    band_success = bool(row.get("_band_success", overall == "PASS"))
    test_type = str(row.get("測試類型") or "PHY")

    lines = [
        f"【{idx}】{band} / {bw}",
        f"• 整體結果：{overall}",
        f"• ARFCN：{arfcn}",
        "",
        "Band 設定",
        f"• 結果：{'PASS' if band_success else 'FAIL'}",
        f"• UE：{ue_state}",
        f"• PHY DL：{_metric(phy_dl)} Mbps",
        f"• PHY UL：{_metric(phy_ul)} Mbps",
    ]

    if test_type == "PHY+iPerf":
        direction = str(row.get("iPerf方向") or "").lower()
        duration = row.get("測試秒數") or "N/A"
        iperf_success = row.get("_iperf_success")
        iperf_status = "PASS" if iperf_success is True else "FAIL" if iperf_success is False else "未執行"
        label = {
            "download": "Download",
            "upload": "Upload",
            "bidirectional": "TRX",
        }.get(direction, direction or "iPerf")
        details = _row_iperf_details(row)
        lines.extend(["", f"iPerf（{label}，{duration} 秒）", f"• 結果：{iperf_status}"])

        if direction == "bidirectional":
            for key, title in (("download", "Download"), ("upload", "Upload")):
                values = details.get(key, {})
                if values:
                    lines.extend(["", title])
                    _append_metric_lines(lines, values)
        elif direction in {"download", "upload"}:
            values = details.get(direction, {})
            if values:
                _append_metric_lines(lines, values)
        else:
            values = {
                "avg": row.get("iPerf平均 Mbps"),
                "min": row.get("iPerf最低 Mbps"),
                "max": row.get("iPerf最高 Mbps"),
                "transfer": row.get("傳輸量 MB"),
            }
            _append_metric_lines(lines, values)

    if error:
        lines.extend(["", "失敗原因", f"• {error}"])
    return "\n".join(lines)


def format_batch_final(status: str, state: dict[str, Any]) -> str:
    results = state.get("results") or []
    passed = sum(1 for row in results if row.get("結果") == "PASS")
    failed = sum(1 for row in results if row.get("結果") == "FAIL")
    status_text = {
        "completed": "全部完成",
        "paused": "已暫停",
        "stopped": "已停止",
        "failed": "執行失敗",
    }.get(status, status)
    lines = [
        f"🏁 批次測試{status_text}",
        "",
        f"• 總筆數：{state.get('total', 0)}",
        f"• 已完成：{len(results)}",
        f"• PASS：{passed}",
        f"• FAIL：{failed}",
    ]

    # Final Telegram summary intentionally omits per-item details.
    # Every completed item remains available in the XLSX and TXT reports.

    reports = state.get("reports") if isinstance(state.get("reports"), dict) else {}
    xlsx_path = str(reports.get("xlsx") or "").strip()
    txt_path = str(reports.get("txt") or "").strip()
    xlsx_url = str(reports.get("xlsx_url") or "").strip()
    txt_url = str(reports.get("txt_url") or "").strip()

    lines.extend(["", "報表"])
    if xlsx_path:
        lines.extend(["", "Excel", f"• 路徑：{xlsx_path}"])
        if xlsx_url:
            lines.append(f"• 開啟：{xlsx_url}")
    else:
        lines.extend(["", "Excel", "• 產生中"])

    if txt_path:
        lines.extend(["", "TXT", f"• 路徑：{txt_path}"])
        if txt_url:
            lines.append(f"• 開啟：{txt_url}")
    else:
        lines.extend(["", "TXT", "• 產生中"])

    if xlsx_url or txt_url:
        lines.extend([
            "",
            "• 上面的 http://127.0.0.1 連結可直接點擊。",
            "• 連結限執行 NetworkAutomation 的這台電腦使用。",
        ])
    else:
        lines.extend([
            "",
            "• Telegram 無法把 D:\\ 本機路徑直接變成可點擊連結。",
            "• 報表伺服器尚未啟動，請先依照路徑開啟檔案。",
        ])
    return "\n".join(lines)
