from __future__ import annotations

from typing import Any


def _machine(value: dict[str, Any] | None) -> dict[str, Any]:
    if not isinstance(value, dict):
        return {}
    nested = value.get("machine_result")
    return nested if isinstance(nested, dict) else value


def _number(value: Any, max_decimals: int = 3) -> str:
    if value is None:
        return "N/A"
    try:
        number = float(value)
    except (TypeError, ValueError):
        return str(value)

    # Integer fields such as duration=30 must remain "30".
    # The previous implementation formatted 30 with 0 decimals as "30",
    # then stripped trailing zeroes and incorrectly changed it to "3".
    if max_decimals <= 0:
        return str(int(round(number)))

    text = f"{number:.{max_decimals}f}".rstrip("0").rstrip(".")
    return text or "0"


def _direction(value: Any) -> str:
    name = str(value or "").strip().lower()
    mapping = {
        "tx": "Upload",
        "ul": "Upload",
        "upload": "Upload",
        "rx": "Download",
        "dl": "Download",
        "download": "Download",
        "trx": "TRX",
        "bidirectional": "TRX",
    }
    return mapping.get(name, name.title() or "iPerf")


def _connection(data: dict[str, Any]) -> dict[str, Any]:
    return data.get("connection") or {}


def _data_ip(data: dict[str, Any]) -> str:
    connection = _connection(data)
    data_ue = connection.get("data_ue") or connection.get("ue") or {}
    ip = (
        connection.get("data_ue_ip")
        or data_ue.get("ip")
        or data.get("ue_ip")
    )
    source = str(
        connection.get("data_ip_source")
        or data.get("ue_ip_source")
        or ""
    ).lower()

    if "testplmn" in source:
        return "N/A"
    return str(ip or "N/A")


def _band_values(data: dict[str, Any]) -> tuple[Any, Any, Any]:
    state = data.get("state") or {}
    modifier = data.get("modifier") or {}
    band = state.get("band", modifier.get("band", "N/A"))
    bandwidth = state.get(
        "bandwidth_mhz",
        modifier.get("bandwidth_mhz", "N/A"),
    )
    earfcn = state.get(
        "dl_earfcn",
        modifier.get("dl_earfcn", "N/A"),
    )
    return band, bandwidth, earfcn




def _arfcn_text(data: dict[str, Any], nr_carriers: list[dict[str, Any]] | None = None) -> str:
    """Return LTE EARFCN(s) followed by NR-ARFCN(s) in carrier order."""
    values: list[str] = []

    lte_anchor = data.get("lte_anchor") or []
    if isinstance(lte_anchor, dict):
        lte_anchor = [lte_anchor]
    if isinstance(lte_anchor, list):
        for item in lte_anchor:
            if not isinstance(item, dict):
                continue
            value = item.get("dl_earfcn")
            if value not in (None, ""):
                values.append(str(value))

    if isinstance(nr_carriers, list):
        for item in nr_carriers:
            if not isinstance(item, dict):
                continue
            value = item.get("nr_arfcn")
            if value not in (None, ""):
                values.append(str(value))

    return "+".join(values) if values else "N/A"

def _service_stable(data: dict[str, Any]) -> bool:
    stages = data.get("stage_success") or {}
    state = data.get("state") or {}
    return bool(
        stages.get("lte_service_stable")
        or state.get("service_stable")
        or (data.get("lte_service") or {}).get("success")
    )


def _ue_connected(data: dict[str, Any]) -> bool:
    stages = data.get("stage_success") or {}
    state = data.get("state") or {}
    connection = _connection(data)
    return bool(
        stages.get("ue_connected")
        or state.get("ue_connected")
        or connection.get("connected")
    )


def _phy(data: dict[str, Any]) -> tuple[str, str]:
    phy = (_connection(data).get("phy_rate") or {})
    return (
        _number(phy.get("total_dl_bitrate_mbps")),
        _number(phy.get("total_ul_bitrate_mbps")),
    )


def _iperf_values(data: dict[str, Any]) -> dict[str, str]:
    summary = data.get("summary") or {}
    parameters = data.get("parameters") or {}
    return {
        "direction": _direction(parameters.get("direction")),
        "duration": _number(parameters.get("duration_sec"), 0),
        "ip": str(parameters.get("ue_ip") or "N/A"),
        "average": _number(summary.get("average_mbps")),
        "minimum": _number(summary.get("minimum_mbps")),
        "maximum": _number(summary.get("maximum_mbps")),
        "transfer": _number(summary.get("total_transfer_mbytes")),
        "retrans": (
            "N/A"
            if summary.get("retransmissions") is None
            else _number(summary.get("retransmissions"), 0)
        ),
    }


def _single_band_success(data: dict[str, Any]) -> str:
    dl, ul = _phy(data)
    stable = "Stable" if _service_stable(data) else "Not Stable"
    ue_text = "UE 已連線" if _ue_connected(data) else "UE 未連線"
    if data.get("band_config") and isinstance(data.get("carriers"), list):
        carrier_text = ", ".join(
            f"B{item.get('band')} {_number(item.get('bandwidth_mhz'))}MHz (EARFCN {item.get('dl_earfcn')})"
            for item in data.get("carriers", [])
        )
        return "\n".join([
            f"OK，已完成 LTE Band 組合 {data.get('band_config')}。",
            "",
            "設定結果：PASS",
            "",
            f"• LTE Carriers：{carrier_text}",
            f"• 連線狀態：{stable}（{ue_text}）",
            f"• 速率：PHY DL：{dl} Mbps / UL：{ul} Mbps",
        ])
    band, bandwidth, earfcn = _band_values(data)
    return "\n".join([
        f"OK，已經幫你切到 B{band} {_number(bandwidth)}MHz 了。",
        "",
        "設定結果：PASS",
        "",
        f"• Band：B{band}（{_number(bandwidth)}MHz，EARFCN：{earfcn}）",
        f"• 連線狀態：{stable}（{ue_text}）",
        f"• 速率：PHY DL：{dl} Mbps / UL：{ul} Mbps",
    ])


def _nr_band_success(data: dict[str, Any]) -> str:
    modifier = data.get("modifier") or {}
    mode = str(data.get("mode") or "NR").upper()
    dl, ul = _phy(data)
    stable = "Stable" if _service_stable(data) else "Not Stable"
    ue_text = "UE 已連線" if _ue_connected(data) else "UE 未連線"

    lines = [
        f"OK，已完成 {mode} Band 設定。",
        "",
        "設定結果：PASS",
        "",
    ]
    band_config = data.get("band_config")
    bandwidth_config = data.get("bandwidth_config")
    carriers = modifier.get("carriers") if isinstance(modifier, dict) else None
    if band_config and isinstance(carriers, list):
        arfcn_text = _arfcn_text(data, carriers)
        lines.append(f"• Band 組合：{band_config}（BW：{bandwidth_config}，ARFCN：{arfcn_text}）")
        lte_anchor = data.get("lte_anchor") or []
        if isinstance(lte_anchor, list) and lte_anchor:
            lte_text = ", ".join(
                f"B{item.get('band')} {_number(item.get('bandwidth_mhz'))}MHz "
                f"（EARFCN：{item.get('dl_earfcn', 'N/A')}）"
                for item in lte_anchor
            )
            lines.append(f"• LTE Carriers：{lte_text}")
    else:
        band = modifier.get("band", "N/A")
        bandwidth = modifier.get("bandwidth_mhz", "N/A")
        arfcn = modifier.get("nr_arfcn", "N/A")
        lines.append(f"• NR Band：n{band}（{_number(bandwidth)}MHz，ARFCN：{arfcn}）")
        lte_anchor = data.get("lte_anchor") or {}
        if mode == "ENDC" and isinstance(lte_anchor, dict) and lte_anchor:
            lines.append(
                f"• LTE Anchor：B{lte_anchor.get('band', 'N/A')}（"
                f"{_number(lte_anchor.get('bandwidth_mhz', 'N/A'))}MHz，"
                f"EARFCN：{lte_anchor.get('dl_earfcn', 'N/A')}）"
            )
    lines.extend([
        f"• 連線狀態：{stable}（{ue_text}）",
        f"• 速率：PHY DL：{dl} Mbps / UL：{ul} Mbps",
    ])
    return "\n".join(lines)


def _iperf_section(index: int, data: dict[str, Any]) -> list[str]:
    values = _iperf_values(data)
    return [
        f"{index}. {values['direction']} iperf（{values['duration']}s）：PASS",
        "",
        f"• 平均速度：{values['average']} Mbps",
        f"• 最高/最低：{values['maximum']} / {values['minimum']} Mbps",
        f"• 總傳輸量：{values['transfer']} MB",
    ]


def _follow_up_question(direction: str, duration: str) -> str:
    if direction == "Upload":
        return f"要我順手再跑一個 download iPerf {duration} 秒嗎？"
    if direction == "Download":
        return "幫你一起整理成 upload/download 對照表嗎？"
    return ""


def _standalone_iperf_success(data: dict[str, Any]) -> str:
    values = _iperf_values(data)
    lines = [
        f"已跑完，{values['direction'].lower()} iPerf {values['duration']} 秒。",
        "",
        f"• UE IP：{values['ip']}",
        f"• 平均：{values['average']} Mbps",
        f"• 最低：{values['minimum']} Mbps",
        f"• 最高：{values['maximum']} Mbps",
        f"• 總傳輸量：{values['transfer']} MB",
    ]
    if values["retrans"] != "N/A":
        lines.append(f"• Retransmissions：{values['retrans']}")

    follow_up = _follow_up_question(
        values["direction"],
        values["duration"],
    )
    if follow_up:
        lines.extend(["", follow_up])

    return "\n".join(lines)


def _band_iperf_success(data: dict[str, Any]) -> str:
    band_result = _machine(data.get("band_result") or {})
    band, bandwidth, earfcn = _band_values(band_result)
    stable = "Stable" if _service_stable(band_result) else "Not Stable"
    ip = (
        data.get("ue_ip")
        or _data_ip(band_result)
        or "N/A"
    )

    lines = [
        "這次成功了！",
        "",
        "1. Band 設定：PASS",
        "",
        f"• Band：B{band}（{_number(bandwidth)}MHz，EARFCN：{earfcn}）",
        f"• 連線：{stable}（UE IP：{ip}）",
        "",
    ]

    results = data.get("iperf_results") or []
    for index, item in enumerate(results, start=2):
        result = _machine(item.get("result") or {})
        lines.extend(_iperf_section(index, result))
        if index < len(results) + 1:
            lines.append("")

    if len(results) == 1:
        values = _iperf_values(
            _machine(results[0].get("result") or {})
        )
        follow_up = _follow_up_question(
            values["direction"],
            values["duration"],
        )
        if follow_up:
            lines.extend(["", follow_up])

    return "\n".join(lines).rstrip()


def _failure(tool: str, data: dict[str, Any]) -> str:
    message = str(
        data.get("message")
        or data.get("human_summary")
        or data.get("error")
        or "未知錯誤"
    )
    stage = data.get("stage")

    if tool == "amarisoft.set_band":
        connection = _connection(data)
        if connection.get("error") == "UE_CONNECTION_TIMEOUT":
            timeout_sec = data.get("ue_wait_timeout_sec") or connection.get("elapsed_sec") or 180
            carriers = data.get("carriers")
            if data.get("band_config") and isinstance(carriers, list):
                carrier_values = [item for item in carriers if isinstance(item, dict)]
                bandwidth_text = "+".join(
                    _number(item.get("bandwidth_mhz")) for item in carrier_values
                ) or str(data.get("bandwidth_config") or "N/A")
                earfcn_text = "+".join(
                    str(item.get("dl_earfcn"))
                    for item in carrier_values
                    if item.get("dl_earfcn") not in (None, "")
                ) or "N/A"
                band_line = (
                    f"• Band 組合：{data.get('band_config')}"
                    f"（BW：{bandwidth_text}，EARFCN：{earfcn_text}）"
                )
            else:
                band, bandwidth, earfcn = _band_values(data)
                band_line = f"• Band：B{band}（{_number(bandwidth)}MHz，EARFCN：{earfcn}）"
            return "\n".join([
                "❌ Band 設定完成，但 UE 未連線",
                "",
                band_line,
                f"• 等待 UE／PHY 逾時：{_number(timeout_sec, 0)} 秒",
                "• 目前 PHY DL／UL：0 Mbps",
                "• 工作已結束，不會繼續卡住",
                "",
                "請確認 UE 是否 Attach、SIM／RF 是否正常，再重新下指令。",
            ])

    if tool == "amarisoft.set_nr_band":
        connection = _connection(data)
        if connection.get("error") == "UE_CONNECTION_TIMEOUT":
            modifier = data.get("modifier") or {}
            mode = str(data.get("mode") or "NR").upper()
            carriers = modifier.get("carriers") if isinstance(modifier, dict) else None
            if data.get("band_config") and isinstance(carriers, list):
                arfcn_text = _arfcn_text(data, carriers)
                band_line = f"• Band 組合：{data.get('band_config')}（BW：{data.get('bandwidth_config')}，ARFCN：{arfcn_text}）"
            else:
                band = modifier.get("band", "N/A")
                bandwidth = modifier.get("bandwidth_mhz", "N/A")
                arfcn = modifier.get("nr_arfcn", "N/A")
                band_line = f"• NR Band：n{band}（{_number(bandwidth)}MHz，ARFCN：{arfcn}）"
            timeout_sec = data.get("ue_wait_timeout_sec") or connection.get("elapsed_sec") or 180
            return "\n".join([
                f"❌ {mode} NR 設定完成，但 UE 未連線",
                "",
                band_line,
                f"• 等待 UE／PHY 逾時：{_number(timeout_sec, 0)} 秒",
                "• 目前 PHY DL／UL：0 Mbps",
                "• 工作已結束，不會繼續卡住",
                "",
                "請確認 UE 是否 Attach、SIM／RF、SCS、ARFCN 與 Time Slot 是否正確，再重新下指令。",
            ])

    if tool == "workflow.band_iperf":
        band_result = _machine(data.get("band_result") or {})
        band, bandwidth, earfcn = _band_values(band_result)
        if stage == "wait_ue_timeout":
            timeout_sec = data.get("wait_ue_timeout_sec", 180)
            return "\n".join([
                "❌ Workflow Failed",
                "",
                f"• Band：B{band}（{_number(bandwidth)}MHz，EARFCN：{earfcn}）",
                f"• 等待 UE 連線逾時：{timeout_sec} 秒",
                "• Result：FAIL",
                "• iPerf：未執行",
                "",
                "請確認 UE 是否 Attach、PHY 是否正常，以及資料 APN 是否取得 IP。",
            ])
        lines = [
            "結果失敗。Band 設定已完成，但 iPerf 執行失敗。",
            "",
            f"• Band：B{band}（{_number(bandwidth)}MHz，EARFCN：{earfcn}）",
        ]
        if stage:
            lines.append(f"• 失敗階段：{stage}")
        lines.append(f"• 錯誤：{message}")
        return "\n".join(lines)

    return f"結果失敗。\n\n• 錯誤：{message}"


def format_telegram_reply(tool: str, result: dict[str, Any]) -> str:
    data = _machine(result)
    success = bool(data.get("success", result.get("success")))

    if not success:
        return _failure(tool, data)

    if tool == "amarisoft.set_band":
        return _single_band_success(data)

    if tool == "amarisoft.set_nr_band":
        return _nr_band_success(data)

    if tool == "workflow.band_iperf":
        return _band_iperf_success(data)

    if tool == "iperf.run":
        return _standalone_iperf_success(data)

    if tool == "iperf.bidirectional":
        results = data.get("results") or []
        lines = ["這次成功了！", ""]
        for index, item in enumerate(results, start=1):
            item_result = _machine(item.get("result") or {})
            lines.extend(_iperf_section(index, item_result))
            if index < len(results):
                lines.append("")
        return "\n".join(lines).rstrip()

    existing = result.get("human_summary") or data.get("message")
    return str(existing or ("PASS" if success else "FAIL"))
