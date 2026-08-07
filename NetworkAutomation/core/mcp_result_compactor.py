from __future__ import annotations

import json
from datetime import datetime
from pathlib import Path
from typing import Any


def save_full_result(root: Path, intent: str, result: dict[str, Any]) -> str:
    folder = root / "results" / "mcp" / datetime.now().strftime("%Y-%m-%d")
    folder.mkdir(parents=True, exist_ok=True)
    name = f"{datetime.now().strftime('%H%M%S_%f')[:-3]}_{intent}.json"
    path = folder / name
    path.write_text(json.dumps(result, ensure_ascii=False, indent=2), encoding="utf-8")
    return str(path)


def key_details(intent: str, result: dict[str, Any]) -> dict[str, Any]:
    machine = result.get("machine_result")
    if not isinstance(machine, dict):
        machine = {}

    if intent == "set_band":
        modifier = machine.get("modifier") or {}
        connection = machine.get("connection") or {}
        phy = connection.get("phy_rate") or {}
        ue = connection.get("data_ue") or connection.get("ue") or {}
        state = machine.get("state") or {}
        metrics = machine.get("operation_metrics") or {}
        return {
            "cell": state.get("cell", modifier.get("cell")),
            "band": state.get("band", modifier.get("band")),
            "bandwidth_mhz": state.get("bandwidth_mhz", modifier.get("bandwidth_mhz")),
            "dl_earfcn": state.get("dl_earfcn", modifier.get("dl_earfcn")),
            "rb_dl": state.get("rb_dl", modifier.get("rb_dl")),
            "connection_basis": connection.get("connection_basis"),
            "phy_dl_mbps": phy.get("total_dl_bitrate_mbps"),
            "phy_ul_mbps": phy.get("total_ul_bitrate_mbps"),
            "ue_ip": connection.get("data_ue_ip") or ue.get("ip"),
            "data_ip_source": connection.get("data_ip_source"),
            "service_stable": state.get("service_stable"),
            "remote_config_verified": state.get("remote_config_verified"),
            "ue_connected": state.get("ue_connected"),
            "total_elapsed_sec": metrics.get("total_elapsed_sec"),
        }

    if intent == "iperf_run":
        summary = machine.get("summary") or {}
        parameters = machine.get("parameters") or {}
        return {
            "ue_ip": parameters.get("ue_ip"),
            "direction": parameters.get("direction"),
            "duration_sec": parameters.get("duration_sec"),
            "port": parameters.get("port"),
            "parallel_streams": parameters.get("parallel_streams"),
            "average_mbps": summary.get("average_mbps"),
            "minimum_mbps": summary.get("minimum_mbps"),
            "maximum_mbps": summary.get("maximum_mbps"),
            "total_transfer_mbytes": summary.get("total_transfer_mbytes"),
            "retransmissions": summary.get("retransmissions"),
            "packet_loss_percent": summary.get("packet_loss_percent"),
            "result_file": machine.get("result_file"),
        }

    if intent == "connection_status":
        phy = machine.get("phy_rate") or {}
        ue = machine.get("ue") or {}
        return {
            "connected": machine.get("connected"),
            "connection_basis": machine.get("connection_basis"),
            "phy_dl_mbps": phy.get("total_dl_bitrate_mbps"),
            "phy_ul_mbps": phy.get("total_ul_bitrate_mbps"),
            "ue_ip": ue.get("ip"),
        }

    if intent == "status":
        return {"service_running": machine.get("service_running"), "state": machine.get("state")}

    return {}


def compact_result(root: Path, intent: str, result: dict[str, Any]) -> dict[str, Any]:
    full_result_file = save_full_result(root, intent, result)
    success = bool(result.get("success"))
    human_summary = result.get("human_summary")
    message = result.get("message")
    if not human_summary:
        machine = result.get("machine_result")
        if isinstance(machine, dict):
            message = message or machine.get("message")
    return {
        "success": success,
        "status": "completed" if success else "failed",
        "intent": intent,
        "human_summary": human_summary or message or ("操作完成。" if success else "操作失敗。"),
        "details": key_details(intent, result),
        "full_result_file": full_result_file,
        "returncode": result.get("returncode"),
    }
