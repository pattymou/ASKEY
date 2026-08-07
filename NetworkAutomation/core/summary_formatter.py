from __future__ import annotations

from typing import Any


def _machine(data: dict[str, Any]) -> dict[str, Any]:
    nested = data.get("machine_result")
    return nested if isinstance(nested, dict) else data


def _sparkline(samples: list[dict[str, Any]]) -> str:
    values = [
        float(item["throughput_mbps"])
        for item in samples
        if item.get("throughput_mbps") is not None
        and not item.get("omitted")
    ]
    if not values:
        return "N/A"
    blocks = "▁▂▃▄▅▆▇█"
    low, high = min(values), max(values)
    if high <= low:
        return blocks[4] * len(values)
    return "".join(
        blocks[min(7, int(round((value - low) / (high - low) * 7)))]
        for value in values
    )


def _iperf_congestion(data: dict[str, Any]) -> str:
    raw = data.get("raw") or {}
    iperf_json = raw.get("iperf_json") or {}
    end = iperf_json.get("end") or {}
    return str(
        end.get("sender_tcp_congestion")
        or end.get("receiver_tcp_congestion")
        or "N/A"
    ).upper()


def _data_ip(connection: dict[str, Any]) -> tuple[str, str]:
    data_ue = connection.get("data_ue") or connection.get("ue") or {}
    ip = (
        connection.get("data_ue_ip")
        or data_ue.get("ip")
    )
    source = str(connection.get("data_ip_source") or "not_available")
    if source.lower().endswith("testplmn"):
        return "等待資料 APN", "not_available"
    return (str(ip), source) if ip else ("等待資料 APN", source)


def _total_time(data: dict[str, Any]) -> Any:
    metrics = data.get("operation_metrics") or {}
    return metrics.get("total_elapsed_sec")


def _iperf_summary(data: dict[str, Any], title: str | None = None) -> str:
    summary = data.get("summary", {})
    parameters = data.get("parameters", {})
    retrans = summary.get("retransmissions")
    retrans_text = "N/A" if retrans is None else str(retrans)
    lines = []
    if title:
        lines.append(title)
    lines.extend([
        f"方向：{str(parameters.get('direction', '')).title()}",
        f"UE Data IP：{parameters.get('ue_ip', 'N/A')}",
        f"測試時間：{parameters.get('duration_sec', 'N/A')} 秒",
        f"平均速度：{summary.get('average_mbps', 'N/A')} Mbps",
        f"最低速度：{summary.get('minimum_mbps', 'N/A')} Mbps",
        f"最高速度：{summary.get('maximum_mbps', 'N/A')} Mbps",
        f"總傳輸量：{summary.get('total_transfer_mbytes', 'N/A')} MB",
        f"Retransmissions：{retrans_text}",
        f"TCP Congestion：{_iperf_congestion(data)}",
        f"速度走勢：{_sparkline(data.get('samples') or [])}",
    ])
    return "\n".join(lines)


def format_summary(tool: str, data: dict[str, Any]) -> str:
    d = _machine(data)

    if tool == "amarisoft.set_band":
        stages = d.get("stage_success", {})
        modifier = d.get("modifier", {})
        state = d.get("state") or {}
        connection = d.get("connection") or {}
        phy = connection.get("phy_rate") or {}
        data_ip, ip_source = _data_ip(connection)
        lte_elapsed = (d.get("lte_service") or {}).get("elapsed_sec")
        attach_elapsed = connection.get("elapsed_sec")
        total_elapsed = _total_time(d)

        if d.get("success"):
            lines = [
                "PASS｜LTE Band 設定完成",
                "",
                "【Band】",
                f"Cell：{state.get('cell', modifier.get('cell', 'N/A'))}",
                f"Band：B{state.get('band', modifier.get('band', 'N/A'))}",
                f"頻寬：{modifier.get('bandwidth_mhz', 'N/A')} MHz",
                f"DL EARFCN：{modifier.get('dl_earfcn', 'N/A')}",
                "",
                "【Connection】",
                f"LTE Service：{'Stable' if stages.get('lte_service_stable') else 'Not Stable'}",
                f"UE 連線：{'已連線' if stages.get('ue_connected') else '未連線'}",
                f"判定依據：{connection.get('connection_basis', 'N/A')}",
                f"UE Data IP：{data_ip}",
                f"IP 來源：{ip_source}",
                f"PHY DL：{phy.get('total_dl_bitrate_mbps', 'N/A')} Mbps",
                f"PHY UL：{phy.get('total_ul_bitrate_mbps', 'N/A')} Mbps",
            ]
            if lte_elapsed is not None or attach_elapsed is not None or total_elapsed is not None:
                lines.extend(["", "【Timing】"])
                if lte_elapsed is not None:
                    lines.append(f"LTE Service 穩定：{lte_elapsed} 秒")
                if attach_elapsed is not None:
                    lines.append(f"UE/PHY/Data IP：{attach_elapsed} 秒")
                if total_elapsed is not None:
                    lines.append(f"總耗時：{total_elapsed} 秒")
            lines.extend(["", "結果：PASS"])
            return "\n".join(lines)

        return (
            "FAIL｜LTE Band 設定未完整完成\n\n"
            + str(d.get("message") or d.get("error") or "未知錯誤")
        )

    if tool == "amarisoft.connection":
        phy = d.get("phy_rate") or {}
        data_ip, ip_source = _data_ip(d)
        if d.get("connected"):
            return "\n".join([
                "PASS｜UE 已連線",
                "",
                f"判定依據：{d.get('connection_basis', 'N/A')}",
                f"UE Data IP：{data_ip}",
                f"IP 來源：{ip_source}",
                f"PHY DL：{phy.get('total_dl_bitrate_mbps', 'N/A')} Mbps",
                f"PHY UL：{phy.get('total_ul_bitrate_mbps', 'N/A')} Mbps",
                "",
                "結果：PASS",
            ])
        return f"WARNING｜UE 尚未連線\n\n{d.get('message', '')}"

    if tool == "iperf.run":
        if not d.get("success"):
            return (
                "FAIL｜iPerf 測試失敗\n\n"
                + str(d.get("message") or d.get("error") or "")
            )
        lines = [
            "PASS｜iPerf 測試完成",
            "",
            _iperf_summary(d),
        ]
        total_elapsed = _total_time(d)
        if total_elapsed is not None:
            lines.extend(["", f"Operation 總耗時：{total_elapsed} 秒"])
        lines.extend(["", "結果：PASS"])
        return "\n".join(lines)

    if tool == "iperf.bidirectional":
        if not d.get("success"):
            return "FAIL｜雙向 iPerf 測試失敗"
        parts = ["PASS｜雙向 iPerf 測試完成"]
        for item in d.get("results", []):
            parts.append(
                _iperf_summary(
                    _machine(item.get("result") or {}),
                    title=f"【{str(item.get('direction')).title()}】",
                )
            )
        total_elapsed = _total_time(d)
        if total_elapsed is not None:
            parts.append(f"Operation 總耗時：{total_elapsed} 秒")
        parts.append("結果：PASS")
        return "\n\n".join(parts)

    if tool == "workflow.band_iperf":
        if not d.get("success"):
            return (
                "FAIL｜Band + iPerf 流程失敗\n\n"
                f"Stage：{d.get('stage')}\n{d.get('message', '')}"
            )

        band = _machine(d.get("band_result") or {})
        state = band.get("state") or {}
        connection = band.get("connection") or {}
        phy = connection.get("phy_rate") or {}
        data_ip = d.get("ue_ip") or connection.get("data_ue_ip") or "N/A"
        ip_source = d.get("ue_ip_source") or connection.get("data_ip_source") or "N/A"

        parts = [
            "PASS｜Band + iPerf 自動流程完成",
            "\n".join([
                "【Band / Connection】",
                f"Band：B{state.get('band', 'N/A')}",
                f"頻寬：{state.get('bandwidth_mhz', 'N/A')} MHz",
                f"UE 連線：{'已連線' if connection.get('connected') else '未連線'}",
                f"判定依據：{connection.get('connection_basis', 'N/A')}",
                f"UE Data IP：{data_ip}",
                f"IP 來源：{ip_source}",
                f"PHY DL：{phy.get('total_dl_bitrate_mbps', 'N/A')} Mbps",
                f"PHY UL：{phy.get('total_ul_bitrate_mbps', 'N/A')} Mbps",
            ]),
        ]

        for item in d.get("iperf_results", []):
            parts.append(
                _iperf_summary(
                    _machine(item.get("result") or {}),
                    title=f"【iPerf {str(item.get('direction')).title()}】",
                )
            )

        timings = []
        lte_elapsed = (band.get("lte_service") or {}).get("elapsed_sec")
        connection_elapsed = connection.get("elapsed_sec")
        if lte_elapsed is not None:
            timings.append(f"LTE Service 穩定：{lte_elapsed} 秒")
        if connection_elapsed is not None:
            timings.append(f"UE/PHY/Data IP：{connection_elapsed} 秒")
        total_elapsed = _total_time(d)
        if total_elapsed is not None:
            timings.append(f"Operation 總耗時：{total_elapsed} 秒")
        if timings:
            parts.append("【Timing】\n" + "\n".join(timings))

        parts.append("結果：PASS")
        return "\n\n".join(parts)

    if tool == "amarisoft.status":
        return (
            "PASS｜Callbox 狀態\n\n"
            f"LTE Service：{'Running' if d.get('service_running') else 'Not Running'}"
        )

    return str(d.get("message") or ("PASS" if d.get("success") else "FAIL"))
