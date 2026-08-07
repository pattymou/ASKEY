from __future__ import annotations

import json
import os
import subprocess
import sys
import time
from datetime import datetime
from pathlib import Path
from typing import Any

from core.user_settings import workflow_setting


IPERF_READY_STABLE_SEC = 5


def _run_json(command: list[str], cwd: Path) -> dict[str, Any]:
    child_env = os.environ.copy()
    child_env["PYTHONIOENCODING"] = "utf-8"
    child_env["PYTHONUTF8"] = "1"
    process = subprocess.run(
        command,
        cwd=str(cwd),
        capture_output=True,
        text=True,
        encoding="utf-8",
        errors="strict",
        env=child_env,
    )
    stdout = (process.stdout or "").strip()
    stderr = (process.stderr or "").strip()
    try:
        result = json.loads(stdout) if stdout else {}
    except json.JSONDecodeError:
        result = {
            "success": False,
            "error": "INVALID_JSON_OUTPUT",
            "stdout": stdout,
        }
    result.setdefault("returncode", process.returncode)
    if stderr:
        result.setdefault("stderr", stderr)
    return result


def _machine(result: dict[str, Any]) -> dict[str, Any]:
    value = result.get("machine_result")
    return value if isinstance(value, dict) else result


def _semantic_success(result: dict[str, Any]) -> bool:
    machine = _machine(result)
    if "success" in machine:
        return bool(machine.get("success"))
    return bool(result.get("success"))


def select_iperf_ip(
    band_result: dict[str, Any],
    explicit_ip: str | None,
    preferred_apn: str | None,
) -> tuple[str | None, str]:
    if explicit_ip:
        return explicit_ip, "explicit"

    machine = _machine(band_result)
    # `amarisoft connection` returns connection fields at the top level, while
    # set-band results historically nested them under `connection`. Support
    # both shapes so a valid data APN IP is not mistaken for a timeout.
    nested_connection = machine.get("connection")
    connection = nested_connection if isinstance(nested_connection, dict) else machine

    # Prefer the normalized fields produced by ue_connection.py.  Depending on
    # the command/envelope version, the selected data IP may be exposed as
    # data_ue_ip, data_ue.ip, ue.ip, or inside ues[].
    direct_ip = connection.get("data_ue_ip")
    if isinstance(direct_ip, str) and direct_ip.strip():
        return direct_ip.strip(), str(connection.get("data_ip_source") or "data_ue_ip")

    for key in ("data_ue", "ue"):
        value = connection.get(key)
        if isinstance(value, dict):
            candidate_ip = value.get("ip")
            if isinstance(candidate_ip, str) and candidate_ip.strip():
                return candidate_ip.strip(), str(connection.get("data_ip_source") or key)

    ues = connection.get("ues") or []
    candidates = [ue for ue in ues if isinstance(ue, dict) and ue.get("ip")]

    if preferred_apn:
        for ue in candidates:
            if preferred_apn.lower() in str(ue.get("apn") or "").lower():
                return str(ue["ip"]), f"apn:{preferred_apn}"

    # Prefer the actual data APN used for iPerf.
    for ue in candidates:
        if str(ue.get("apn") or "").lower() == "internet":
            return str(ue["ip"]), "apn:internet"

    # Prefer a non-TestPLMN APN when several addresses are present, but do not
    # reject TestPLMN when it is the only UE IP actually assigned.  The iPerf
    # target must remain dynamic: use the address reported by Amarisoft rather
    # than waiting for a hard-coded 192.168.3.2-style address.
    for ue in candidates:
        apn_name = str(ue.get("apn") or "").strip().lower()
        if apn_name and apn_name != "testplmn":
            return str(ue["ip"]), "first_non_testplmn"

    unique_ips: list[str] = []
    seen_ips: set[str] = set()
    for ue in candidates:
        candidate_ip = str(ue.get("ip") or "").strip()
        if candidate_ip and candidate_ip not in seen_ips:
            seen_ips.add(candidate_ip)
            unique_ips.append(candidate_ip)

    if len(unique_ips) == 1:
        return unique_ips[0], "only_ue_ip_fallback"
    if len(unique_ips) > 1:
        return None, "multiple_ambiguous_ue_ips"
    return None, "no_ue_ip"


def wait_for_iperf_ip(
    root: Path,
    initial_result: dict[str, Any],
    explicit_ip: str | None,
    preferred_apn: str | None,
    max_wait_sec: int = 120,
    poll_interval_sec: float = 3.0,
    ready_stable_sec: float = 5.0,
) -> tuple[str | None, str, list[dict[str, Any]]]:
    """Wait until PHY, UE IP and reachability are all stable before iPerf.

    A PHY bitrate can appear slightly before the UE data path is ready.  Do not
    start iPerf on the first PHY sample.  The same selected IP must remain
    PHY-connected and Ping-reachable for ``ready_stable_sec`` first.
    """
    attempts: list[dict[str, Any]] = []
    started = time.monotonic()
    ready_since: float | None = None
    ready_ip: str | None = None
    ready_source = "not_ready"

    while time.monotonic() - started < max_wait_sec:
        command = [
            sys.executable,
            str(root / "callbox_agent.py"),
            "amarisoft",
            "connection",
        ]
        connection_result = _run_json(command, root)
        now = time.monotonic()
        elapsed = round(now - started, 1)
        ip, source = select_iperf_ip(
            connection_result,
            explicit_ip,
            preferred_apn,
        )
        attempt_machine = _machine(connection_result)
        attempt_connection = attempt_machine.get("connection")
        if not isinstance(attempt_connection, dict):
            attempt_connection = attempt_machine

        connected = bool(attempt_connection.get("connected"))
        ping = attempt_connection.get("ping") or {}
        # For auto-selected IPs, check_once Pings the same data_ue IP.  An
        # explicitly supplied IP is rechecked by the iPerf runner itself; here
        # PHY stability is still required before launching it.
        ping_ready = bool(ping.get("success")) if not explicit_ip else True
        same_ping_ip = (
            not ping.get("ip")
            or not ip
            or str(ping.get("ip")) == str(ip)
        )
        ready_now = bool(ip and connected and ping_ready and same_ping_ip)

        if ready_now:
            if ready_ip != ip or ready_since is None:
                ready_ip = ip
                ready_source = source
                ready_since = now
            stable_elapsed = round(now - ready_since, 1)
        else:
            ready_ip = None
            ready_since = None
            stable_elapsed = 0.0

        attempts.append({
            "elapsed_sec": elapsed,
            "success": _semantic_success(connection_result),
            "connected": connected,
            "connection_basis": attempt_connection.get("connection_basis"),
            "selected_ip": ip,
            "source": source,
            "data_ue_ip": attempt_connection.get("data_ue_ip"),
            "data_ip_source": attempt_connection.get("data_ip_source"),
            "phy_rate": attempt_connection.get("phy_rate") or {},
            "ping": ping,
            "ready_now": ready_now,
            "ready_stable_elapsed_sec": stable_elapsed,
            "ready_stable_required_sec": ready_stable_sec,
            "ues": attempt_connection.get("ues") or [],
            "error": attempt_connection.get("error") or connection_result.get("stderr"),
        })

        if ready_now and stable_elapsed >= ready_stable_sec:
            return ready_ip, ready_source, attempts

        time.sleep(poll_interval_sec)

    return None, "usable_ue_ip_or_ping_timeout", attempts


def _resolved_radio_context(
    band_result: dict[str, Any],
    *,
    band: int,
    bandwidth: float | None,
    cell: int,
    requested_dl_earfcn: int | None,
) -> dict[str, Any]:
    """Build radio context from the confirmed set-band result.

    When EARFCN is auto-selected by the band modifier, the original request has
    no --dl-earfcn value.  In that case we must persist the confirmed EARFCN
    returned by set-band rather than saving None.
    """
    machine = _machine(band_result)
    state = machine.get("state") or {}
    modifier = machine.get("modifier") or {}

    confirmed_earfcn = state.get("dl_earfcn")
    if confirmed_earfcn is None:
        confirmed_earfcn = modifier.get("dl_earfcn")
    if confirmed_earfcn is None:
        confirmed_earfcn = requested_dl_earfcn

    return {
        "band": state.get("band", modifier.get("band", band)),
        "bandwidth_mhz": state.get(
            "bandwidth_mhz",
            modifier.get("bandwidth_mhz", bandwidth),
        ),
        "cell": state.get("cell", modifier.get("cell", cell)),
        "dl_earfcn": confirmed_earfcn,
        "updated_at": datetime.now().astimezone().isoformat(timespec="seconds"),
    }


def run_band_then_iperf(
    root: Path,
    *,
    cell: int,
    band: int,
    bandwidth: float,
    direction: str,
    duration: int,
    ue_ip: str | None,
    port: int,
    parallel: int,
    interval: int,
    dl_earfcn: int | None = None,
    expected_imsi: str | None = None,
    expected_imei: str | None = None,
    apn: str | None = None,
    mimo_dl: str | None = None,
    mimo_ul: str | None = None,
    modulation_dl: str | None = None,
    modulation_ul: str | None = None,
    mcs_dl: str | None = None,
    mcs_ul: str | None = None,
    time_slot: str | None = None,
) -> dict[str, Any]:
    agent = str(root / "callbox_agent.py")
    # IMPORTANT: set-band must not perform its own UE wait in this workflow.
    # Otherwise the user-configurable wait_ue_timeout_sec only starts after a
    # second hidden wait has already finished.  Apply/verify the radio config
    # first, then use the single workflow-owned UE/IP deadline below.
    band_cmd = [
        sys.executable, agent,
        "amarisoft", "set-band",
        "--cell", str(cell),
        "--band", str(band),
        "--skip-ue-wait",
    ]
    if bandwidth is not None:
        band_cmd += ["--bandwidth", str(bandwidth)]
    for value, flag in (
        (mimo_dl, "--mimo-dl"), (mimo_ul, "--mimo-ul"),
        (modulation_dl, "--modulation-dl"), (modulation_ul, "--modulation-ul"),
        (mcs_dl, "--mcs-dl"), (mcs_ul, "--mcs-ul"), (time_slot, "--time-slot"),
    ):
        if value is not None:
            band_cmd += [flag, str(value)]
    for value, flag in (
        (dl_earfcn, "--dl-earfcn"),
        (expected_imsi, "--expected-imsi"),
        (expected_imei, "--expected-imei"),
        (apn, "--apn"),
    ):
        if value is not None:
            band_cmd += [flag, str(value)]

    band_result = _run_json(band_cmd, root)
    if not _semantic_success(band_result):
        return {
            "success": False,
            "workflow": "band_then_iperf",
            "stage": "set_band",
            "band_result": band_result,
            "message": "Band 切換未完成，因此沒有執行 iPerf。",
        }

    radio_context = _resolved_radio_context(
        band_result,
        band=band,
        bandwidth=bandwidth,
        cell=cell,
        requested_dl_earfcn=dl_earfcn,
    )
    context_file = root / "state" / "last_radio_context.json"
    context_file.parent.mkdir(parents=True, exist_ok=True)
    context_file.write_text(
        json.dumps(radio_context, ensure_ascii=False, indent=2),
        encoding="utf-8",
    )

    wait_timeout_sec = workflow_setting(root, "wait_ue_timeout_sec")
    wait_poll_interval_sec = workflow_setting(root, "wait_ue_poll_interval_sec")
    # Keep this internal instead of reading an unregistered editable setting.
    # This prevents KeyError before the iPerf stage starts.
    ready_stable_sec = IPERF_READY_STABLE_SEC
    selected_ip, ip_source, ip_wait_attempts = wait_for_iperf_ip(
        root,
        band_result,
        ue_ip,
        apn,
        max_wait_sec=wait_timeout_sec,
        poll_interval_sec=float(wait_poll_interval_sec),
        ready_stable_sec=float(ready_stable_sec),
    )
    if not selected_ip:
        return {
            "success": False,
            "workflow": "band_then_iperf",
            "stage": "wait_ue_timeout",
            "band_result": band_result,
            "ip_wait_attempts": ip_wait_attempts,
            "wait_ue_timeout_sec": wait_timeout_sec,
            "iperf_ready_stable_sec": ready_stable_sec,
            "message": (
                f"等待 UE 連線與可用資料 APN IP 超過 {wait_timeout_sec} 秒；"
                "iPerf 未執行。"
            ),
        }

    directions = ["download", "upload"] if direction == "bidirectional" else [direction]
    iperf_results: list[dict[str, Any]] = []
    for current_direction in directions:
        command = [
            sys.executable, agent,
            "iperf", "run",
            "--ue-ip", selected_ip,
            "--direction", current_direction,
            "--duration", str(duration),
            "--port", str(port),
            "--parallel", str(parallel),
            "--interval", str(interval),
            "--band", str(radio_context["band"]),
            "--bandwidth", str(radio_context["bandwidth_mhz"]),
            "--cell", str(radio_context["cell"]),
        ]
        confirmed_earfcn = radio_context.get("dl_earfcn")
        if confirmed_earfcn is not None:
            command += ["--dl-earfcn", str(confirmed_earfcn)]
        result = _run_json(command, root)
        iperf_results.append({
            "direction": current_direction,
            "result": result,
        })
        if not _semantic_success(result):
            return {
                "success": False,
                "workflow": "band_then_iperf",
                "stage": f"iperf_{current_direction}",
                "ue_ip": selected_ip,
                "ue_ip_source": ip_source,
                "ip_wait_attempts": ip_wait_attempts,
                "band_result": band_result,
                "iperf_results": iperf_results,
                "message": f"Band 已完成，但 {current_direction} iPerf 失敗。",
            }

    return {
        "success": True,
        "workflow": "band_then_iperf",
        "stage": "completed",
        "ue_ip": selected_ip,
        "ue_ip_source": ip_source,
        "ip_wait_attempts": ip_wait_attempts,
        "band_result": band_result,
        "iperf_results": iperf_results,
        "message": "Band 切換、PHY 連線確認與 iPerf 測試均完成。",
    }
