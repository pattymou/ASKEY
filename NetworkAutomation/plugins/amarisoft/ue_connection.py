from __future__ import annotations

import argparse
import ipaddress
import json
import shlex
import sys
import time
from dataclasses import dataclass
from pathlib import Path
from typing import Any

ROOT = Path(__file__).resolve().parents[2]
if str(ROOT) not in sys.path:
    sys.path.insert(0, str(ROOT))

from core.ssh import SSHClient


@dataclass(frozen=True)
class CallboxAccess:
    host: str
    port: int
    username: str
    password: str
    ssh_timeout_sec: int
    command_timeout_sec: int


@dataclass(frozen=True)
class ConnectionPolicy:
    ue_websocket_port: int
    stats_websocket_port: int
    poll_interval_sec: float
    max_wait_sec: int
    ping_count: int
    ping_timeout_sec: int
    require_phy_rate: bool
    min_phy_bitrate_bps: int
    ip_ping_optional: bool
    data_ip_grace_sec: int
    preferred_data_apns: tuple[str, ...]
    excluded_data_apns: tuple[str, ...]


def load_settings(path: Path) -> tuple[CallboxAccess, ConnectionPolicy]:
    raw = json.loads(path.read_text(encoding="utf-8"))
    c = raw["callbox"]
    p = c.get("connection_check", {})
    return (
        CallboxAccess(
            host=str(c["host"]),
            port=int(c.get("port", 22)),
            username=str(c["username"]),
            password=str(c.get("password", "")),
            ssh_timeout_sec=int(c.get("ssh_timeout_sec", 30)),
            command_timeout_sec=int(c.get("command_timeout_sec", 120)),
        ),
        ConnectionPolicy(
            ue_websocket_port=int(p.get("ue_websocket_port", p.get("websocket_port", 9000))),
            stats_websocket_port=int(p.get("stats_websocket_port", 9001)),
            poll_interval_sec=float(p.get("poll_interval_sec", 3)),
            max_wait_sec=int(p.get("max_wait_sec", 600)),
            ping_count=int(p.get("ping_count", 1)),
            ping_timeout_sec=int(p.get("ping_timeout_sec", 2)),
            require_phy_rate=bool(p.get("require_phy_rate", True)),
            min_phy_bitrate_bps=int(p.get("min_phy_bitrate_bps", 1)),
            ip_ping_optional=bool(p.get("ip_ping_optional", True)),
            data_ip_grace_sec=int(p.get("data_ip_grace_sec", 30)),
            preferred_data_apns=tuple(
                str(value).strip().lower()
                for value in p.get("preferred_data_apns", ["internet"])
                if str(value).strip()
            ),
            excluded_data_apns=tuple(
                str(value).strip().lower()
                for value in p.get("excluded_data_apns", ["testplmn"])
                if str(value).strip()
            ),
        ),
    )


def _decode_all_json(text: str) -> list[Any]:
    """
    Decode JSON payloads printed by Amarisoft ws.js.

    Amarisoft prefixes its response with log lines such as ``[0.003]``. Those
    brackets are not JSON arrays, so this parser only treats ``{`` as a JSON
    start for the current stats/ue_get responses. It also prefers the payload
    after ``### Message received`` when that marker is present.
    """
    decoder = json.JSONDecoder()
    values: list[Any] = []

    marker = "Message received"
    search_text = text[text.find(marker) + len(marker):] if marker in text else text
    index = 0

    while index < len(search_text):
        start = search_text.find("{", index)
        if start < 0:
            break
        try:
            value, consumed = decoder.raw_decode(search_text[start:])
        except json.JSONDecodeError:
            index = start + 1
            continue
        values.append(value)
        index = start + consumed

    return values


def _find_stats_payload(values: list[Any]) -> dict[str, Any] | None:
    """Find a dict whose cells contain dl_bitrate/ul_bitrate, even when nested."""
    for value in reversed(values):
        for record in _walk(value):
            cells = record.get("cells")
            if not isinstance(cells, dict):
                continue
            for cell in cells.values():
                if isinstance(cell, dict) and (
                    "dl_bitrate" in cell or "ul_bitrate" in cell
                ):
                    return record
    return None

def _walk(value: Any):
    if isinstance(value, dict):
        yield value
        for child in value.values():
            yield from _walk(child)
    elif isinstance(value, list):
        for child in value:
            yield from _walk(child)


def _candidate_ip(record: dict[str, Any]) -> str | None:
    for key in ("ip", "ipv4", "ipv4_addr", "pdn_ip", "address"):
        value = record.get(key)
        if isinstance(value, str):
            try:
                parsed = ipaddress.ip_address(value.strip())
                if parsed.version == 4:
                    return str(parsed)
            except ValueError:
                pass
    return None


def extract_ues(payload: Any) -> list[dict[str, Any]]:
    found: list[dict[str, Any]] = []
    seen: set[tuple[str, str, str]] = set()
    for record in _walk(payload):
        ip = _candidate_ip(record)
        imsi = str(record.get("imsi", "")).strip()
        imei = str(record.get("imeisv", record.get("imei", ""))).strip()
        apn = str(record.get("apn", "")).strip()
        attached = any(k in record for k in ("enb_ue_id", "mme_ue_id", "rnti", "cells"))
        if not ip and not imsi and not imei and not attached:
            continue
        key = (ip or "", imsi, apn)
        if key in seen:
            continue
        seen.add(key)
        found.append({
            "ip": ip,
            "imsi": imsi or None,
            "imei": imei or None,
            "apn": apn or None,
            "enb_ue_id": record.get("enb_ue_id"),
            "mme_ue_id": record.get("mme_ue_id"),
            "rnti": record.get("rnti"),
        })
    return found


def extract_phy_rates(payload: Any) -> dict[str, Any]:
    cells_raw = payload.get("cells") if isinstance(payload, dict) else None
    if not isinstance(cells_raw, dict):
        return {
            "success": False,
            "cells": [],
            "total_dl_bitrate_bps": 0,
            "total_ul_bitrate_bps": 0,
            "error": "stats JSON 找不到有效 cells。",
        }

    cells: list[dict[str, Any]] = []
    total_dl = 0
    total_ul = 0
    for cell_id, value in cells_raw.items():
        if not isinstance(value, dict):
            continue
        try:
            dl = int(float(value.get("dl_bitrate", 0) or 0))
        except (TypeError, ValueError):
            dl = 0
        try:
            ul = int(float(value.get("ul_bitrate", 0) or 0))
        except (TypeError, ValueError):
            ul = 0
        total_dl += dl
        total_ul += ul
        cells.append({
            "cell_id": str(cell_id),
            "dl_bitrate_bps": dl,
            "ul_bitrate_bps": ul,
            "dl_bitrate_mbps": round(dl / 1_000_000.0, 3),
            "ul_bitrate_mbps": round(ul / 1_000_000.0, 3),
            "active": dl > 0 or ul > 0,
        })

    return {
        "success": bool(cells),
        "cells": cells,
        "total_dl_bitrate_bps": total_dl,
        "total_ul_bitrate_bps": total_ul,
        "total_dl_bitrate_mbps": round(total_dl / 1_000_000.0, 3),
        "total_ul_bitrate_mbps": round(total_ul / 1_000_000.0, 3),
        "active_cell_count": sum(1 for cell in cells if cell["active"]),
    }

def ws_query(
    ssh: SSHClient,
    access: CallboxAccess,
    port: int,
    message: dict[str, Any],
) -> dict[str, Any]:
    payload = json.dumps(message, separators=(",", ":"))
    command = (
        "cd /root/enb && "
        f"./ws.js {shlex.quote(access.host + ':' + str(port))} "
        f"{shlex.quote(payload)}"
    )
    result = ssh.execute(command, access.command_timeout_sec)
    if not result.success:
        return {
            "success": False,
            "command": command,
            "error": result.stderr or result.stdout or "ws.js failed",
            "raw_stdout": result.stdout,
        }
    values = _decode_all_json(result.stdout)
    return {
        "success": bool(values),
        "command": command,
        "json_values": values,
        "raw_stdout": result.stdout,
        "error": None if values else "ws.js stdout 沒有可解析 JSON。",
    }

def query_phy_rates(
    ssh: SSHClient,
    access: CallboxAccess,
    policy: ConnectionPolicy,
) -> dict[str, Any]:
    query = ws_query(ssh, access, policy.stats_websocket_port, {"message": "stats"})
    if not query.get("success"):
        return {
            "success": False,
            "cells": [],
            "total_dl_bitrate_bps": 0,
            "total_ul_bitrate_bps": 0,
            "error": query.get("error"),
            "command": query.get("command"),
            "raw_stdout": query.get("raw_stdout"),
        }
    payload = _find_stats_payload(query["json_values"])
    if payload is None:
        return {
            "success": False,
            "cells": [],
            "total_dl_bitrate_bps": 0,
            "total_ul_bitrate_bps": 0,
            "error": "收到 stats 回覆，但解析到的 JSON 都沒有含 bitrate 的 cells。",
            "command": query.get("command"),
            "raw_stdout": query.get("raw_stdout"),
            "json_values": query.get("json_values"),
        }
    parsed = extract_phy_rates(payload)
    parsed["command"] = query.get("command")
    parsed["payload"] = payload
    return parsed

def query_ues(
    ssh: SSHClient,
    access: CallboxAccess,
    policy: ConnectionPolicy,
) -> dict[str, Any]:
    query = ws_query(ssh, access, policy.ue_websocket_port, {"message": "ue_get"})
    if not query.get("success"):
        return {
            "success": False,
            "ues": [],
            "error": query.get("error"),
            "command": query.get("command"),
        }
    # A single ws.js invocation can print multiple JSON objects.  Amarisoft may
    # place the UE records before a final acknowledgement object, so parsing
    # only json_values[-1] can silently discard the real UE/APN/IP data.
    # Aggregate and de-duplicate UE records from every decoded payload.
    all_ues: list[dict[str, Any]] = []
    seen: set[tuple[str, str, str, str]] = set()
    for payload in query.get("json_values") or []:
        for ue in extract_ues(payload):
            key = (
                str(ue.get("ip") or ""),
                str(ue.get("imsi") or ""),
                str(ue.get("apn") or ""),
                str(ue.get("rnti") or ""),
            )
            if key in seen:
                continue
            seen.add(key)
            all_ues.append(ue)
    return {
        "success": True,
        "ues": all_ues,
        "command": query.get("command"),
        "decoded_payload_count": len(query.get("json_values") or []),
    }


def select_data_ue(
    ues: list[dict[str, Any]],
    policy: ConnectionPolicy,
) -> tuple[dict[str, Any] | None, str]:
    """Select the dynamically assigned UE IP used by applications/iPerf.

    Preferred data APNs still win when multiple bearers are visible.  When
    Amarisoft reports exactly one UE IP, use that address regardless of APN
    name (including TestPLMN) instead of waiting for a fixed second address.
    """
    with_ip = [ue for ue in ues if isinstance(ue, dict) and ue.get("ip")]

    for preferred in policy.preferred_data_apns:
        for ue in with_ip:
            if str(ue.get("apn") or "").strip().lower() == preferred:
                return ue, f"apn:{preferred}"

    for ue in with_ip:
        apn_name = str(ue.get("apn") or "").strip().lower()
        if apn_name and apn_name not in policy.excluded_data_apns:
            return ue, f"apn:{apn_name}"

    unique_by_ip: dict[str, dict[str, Any]] = {}
    for ue in with_ip:
        candidate_ip = str(ue.get("ip") or "").strip()
        if candidate_ip:
            unique_by_ip.setdefault(candidate_ip, ue)

    if len(unique_by_ip) == 1:
        return next(iter(unique_by_ip.values())), "only_ue_ip_fallback"
    if len(unique_by_ip) > 1:
        return None, "multiple_ambiguous_ue_ips"
    return None, "not_available"


def match_ues(
    ues: list[dict[str, Any]],
    expected_imsi: str | None,
    expected_imei: str | None,
    apn: str | None,
) -> list[dict[str, Any]]:
    result = []
    for ue in ues:
        if expected_imsi and expected_imsi not in str(ue.get("imsi") or ""):
            continue
        if expected_imei and expected_imei not in str(ue.get("imei") or ""):
            continue
        if apn and apn.lower() not in str(ue.get("apn") or "").lower():
            continue
        result.append(ue)
    return result


def ping_from_callbox(ssh: SSHClient, ip: str, policy: ConnectionPolicy) -> dict[str, Any]:
    command = f"ping -c {policy.ping_count} -W {policy.ping_timeout_sec} {shlex.quote(ip)}"
    result = ssh.execute(command, max(10, policy.ping_timeout_sec * policy.ping_count + 5))
    return {
        "success": result.success,
        "ip": ip,
        "command": command,
        "stdout": result.stdout,
        "stderr": result.stderr,
        "exit_code": result.exit_code,
    }


def wait_for_connection(
    settings_path: Path,
    expected_imsi: str | None = None,
    expected_imei: str | None = None,
    apn: str | None = None,
    max_wait_sec: int | None = None,
    return_on_phy: bool = False,
) -> dict[str, Any]:
    access, policy = load_settings(settings_path)
    effective_max_wait_sec = (
        max(1, int(max_wait_sec))
        if max_wait_sec is not None
        else policy.max_wait_sec
    )
    started = time.monotonic()
    phy_connected_at: float | None = None
    attempts: list[dict[str, Any]] = []

    with SSHClient.from_callbox_settings(access) as ssh:
        while True:
            now = time.monotonic()
            elapsed = round(now - started, 1)
            phy = query_phy_rates(ssh, access, policy)
            ue_query = query_ues(ssh, access, policy)
            matched = match_ues(
                ue_query.get("ues", []),
                expected_imsi,
                expected_imei,
                apn,
            )

            phy_connected = (
                phy.get("success", False)
                and (
                    int(phy.get("total_dl_bitrate_bps", 0))
                    >= policy.min_phy_bitrate_bps
                    or int(phy.get("total_ul_bitrate_bps", 0))
                    >= policy.min_phy_bitrate_bps
                )
            )

            if phy_connected and phy_connected_at is None:
                phy_connected_at = now

            data_ue, data_ip_source = select_data_ue(matched, policy)
            ping_result = (
                ping_from_callbox(ssh, str(data_ue["ip"]), policy)
                if data_ue
                else None
            )

            data_ip_wait_sec = (
                round(now - phy_connected_at, 1)
                if phy_connected_at is not None
                else 0.0
            )

            attempts.append({
                "elapsed_sec": elapsed,
                "phy_connected": phy_connected,
                "phy_rate": phy,
                "ue_get_success": ue_query.get("success", False),
                "matched_ues": matched,
                "data_ue": data_ue,
                "data_ip_source": data_ip_source,
                "data_ip_wait_sec": data_ip_wait_sec,
                "ping": ping_result,
            })

            if phy_connected and (
                return_on_phy
                or data_ue
                or data_ip_wait_sec >= policy.data_ip_grace_sec
            ):
                return {
                    "success": True,
                    "connected": True,
                    "connection_basis": "phy_rate",
                    "elapsed_sec": elapsed,
                    "phy_connected_at_sec": round(
                        phy_connected_at - started,
                        1,
                    ) if phy_connected_at is not None else None,
                    "data_ip_wait_sec": data_ip_wait_sec,
                    "phy_rate": phy,
                    "ues": matched,
                    "ue": data_ue,
                    "data_ue": data_ue,
                    "data_ue_ip": data_ue.get("ip") if data_ue else None,
                    "data_ip_source": data_ip_source,
                    "ping": ping_result,
                    "ip_status": (
                        "reachable"
                        if ping_result and ping_result.get("success")
                        else (
                            "available_not_reachable"
                            if data_ue
                            else "not_available"
                        )
                    ),
                    "attempt_count": len(attempts),
                    "attempts": attempts[-20:],
                    "message": (
                        ("已偵測 PHY Rate；" if return_on_phy else "UE 已透過 PHY Rate 判定連線；")
                        + f"DL={phy.get('total_dl_bitrate_mbps', 0)} Mbps，"
                        f"UL={phy.get('total_ul_bitrate_mbps', 0)} Mbps；"
                        + (
                            f"Data IP={data_ue.get('ip')} "
                            f"({data_ip_source})。"
                            if data_ue
                            else "Data IP 尚未取得。"
                        )
                    ),
                }

            if (
                not policy.require_phy_rate
                and ping_result
                and ping_result.get("success")
            ):
                return {
                    "success": True,
                    "connected": True,
                    "connection_basis": "ue_ip_ping",
                    "elapsed_sec": elapsed,
                    "phy_rate": phy,
                    "ues": matched,
                    "ue": data_ue,
                    "data_ue": data_ue,
                    "data_ue_ip": data_ue.get("ip") if data_ue else None,
                    "data_ip_source": data_ip_source,
                    "ping": ping_result,
                    "attempt_count": len(attempts),
                    "attempts": attempts[-20:],
                    "message": "UE 已透過 Data IP Ping 判定連線。",
                }

            if elapsed >= effective_max_wait_sec:
                return {
                    "success": False,
                    "connected": False,
                    "connection_basis": None,
                    "elapsed_sec": elapsed,
                    "phy_rate": phy,
                    "ues": matched,
                    "ue": data_ue,
                    "data_ue": data_ue,
                    "data_ue_ip": data_ue.get("ip") if data_ue else None,
                    "data_ip_source": data_ip_source,
                    "ping": ping_result,
                    "attempt_count": len(attempts),
                    "attempts": attempts[-20:],
                    "error": "UE_CONNECTION_TIMEOUT",
                    "message": (
                        f"在 {effective_max_wait_sec} 秒安全上限內，"
                        "PHY DL/UL bitrate 仍為 0，尚未判定 UE 已連線。"
                    ),
                }

            time.sleep(policy.poll_interval_sec)


def check_once(
    settings_path: Path,
    expected_imsi: str | None = None,
    expected_imei: str | None = None,
    apn: str | None = None,
) -> dict[str, Any]:
    access, policy = load_settings(settings_path)
    with SSHClient.from_callbox_settings(access) as ssh:
        phy = query_phy_rates(ssh, access, policy)
        ue_query = query_ues(ssh, access, policy)
        matched = match_ues(ue_query.get("ues", []), expected_imsi, expected_imei, apn)
        phy_connected = (
            phy.get("success", False)
            and (
                int(phy.get("total_dl_bitrate_bps", 0)) >= policy.min_phy_bitrate_bps
                or int(phy.get("total_ul_bitrate_bps", 0)) >= policy.min_phy_bitrate_bps
            )
        )
        data_ue, data_ip_source = select_data_ue(matched, policy)
        ping_result = (
            ping_from_callbox(ssh, str(data_ue["ip"]), policy)
            if data_ue
            else None
        )
        return {
            "success": phy.get("success", False),
            "connected": phy_connected,
            "connection_basis": "phy_rate" if phy_connected else None,
            "phy_rate": phy,
            "ues": matched,
            "ue": data_ue,
            "data_ue": data_ue,
            "data_ue_ip": data_ue.get("ip") if data_ue else None,
            "data_ip_source": data_ip_source,
            "ping": ping_result,
            "ip_status": (
                "reachable"
                if ping_result and ping_result.get("success")
                else ("available_not_reachable" if data_ue else "not_available")
            ),
            "message": (
                "UE 已透過 PHY Rate 判定連線。"
                if phy_connected
                else "目前 PHY DL/UL bitrate 皆為 0。"
            ),
        }


def main() -> int:
    ap = argparse.ArgumentParser()
    ap.add_argument("--settings", required=True)
    ap.add_argument("--wait", action="store_true")
    ap.add_argument("--expected-imsi")
    ap.add_argument("--expected-imei")
    ap.add_argument("--apn")
    args = ap.parse_args()
    try:
        fn = wait_for_connection if args.wait else check_once
        result = fn(
            Path(args.settings),
            expected_imsi=args.expected_imsi,
            expected_imei=args.expected_imei,
            apn=args.apn,
        )
        print(json.dumps(result, ensure_ascii=False, indent=2))
        return 0 if result.get("success") and result.get("connected") else 1
    except Exception as exc:
        print(json.dumps({
            "success": False,
            "connected": False,
            "error": type(exc).__name__,
            "message": str(exc),
        }, ensure_ascii=False, indent=2))
        return 1


if __name__ == "__main__":
    raise SystemExit(main())
