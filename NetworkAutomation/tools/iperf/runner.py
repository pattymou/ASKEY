from __future__ import annotations

import argparse
import ipaddress
import json
import shlex
import sys
import uuid
from datetime import datetime
from pathlib import Path
from typing import Any

ROOT = Path(__file__).resolve().parents[2]
if str(ROOT) not in sys.path:
    sys.path.insert(0, str(ROOT))

from core.ssh import SSHClient
from tools.iperf.server_manager import ensure_iperf_server


def mbps(value: Any) -> float | None:
    try:
        return round(float(value) / 1_000_000.0, 3)
    except (TypeError, ValueError):
        return None


def mbytes(value: Any) -> float | None:
    try:
        return round(float(value) / 1_000_000.0, 3)
    except (TypeError, ValueError):
        return None


def parse_json(stdout: str) -> dict[str, Any]:
    data = json.loads(stdout)
    if data.get("error"):
        raise RuntimeError(str(data["error"]))

    samples = []
    for interval in data.get("intervals", []):
        item = interval.get("sum") or interval.get("sum_received") or interval.get("sum_sent")
        if not isinstance(item, dict):
            continue
        samples.append({
            "second_start": item.get("start"),
            "second_end": item.get("end"),
            "throughput_mbps": mbps(item.get("bits_per_second")),
            "transfer_mbytes": mbytes(item.get("bytes")),
            "retransmissions": item.get("retransmits"),
            "packet_loss_percent": item.get("lost_percent"),
            "omitted": bool(item.get("omitted", False)),
        })

    valid = [
        s for s in samples
        if not s["omitted"] and s["throughput_mbps"] is not None
    ]
    rates = [s["throughput_mbps"] for s in valid]

    end = data.get("end", {})
    received = end.get("sum_received") or {}
    sent = end.get("sum_sent") or {}
    average = mbps(received.get("bits_per_second"))
    if average is None:
        average = mbps(sent.get("bits_per_second"))

    return {
        "summary": {
            "average_mbps": average,
            "minimum_mbps": min(rates) if rates else average,
            "maximum_mbps": max(rates) if rates else average,
            "total_transfer_mbytes": (
                mbytes(received.get("bytes"))
                if received.get("bytes") is not None
                else mbytes(sent.get("bytes"))
            ),
            "retransmissions": sent.get("retransmits"),
            "packet_loss_percent": (
                (end.get("sum") or {}).get("lost_percent")
            ),
            "sample_count": len(valid),
        },
        "samples": samples,
        "iperf_json": data,
    }



def _display(value: Any, digits: int = 3) -> str:
    if value is None:
        return "N/A"
    if isinstance(value, float):
        return f"{value:.{digits}f}"
    return str(value)


def load_radio_context(root: Path) -> dict[str, Any]:
    """Load the most recently confirmed LTE radio context, if available."""
    context_file = root / "state" / "last_radio_context.json"
    try:
        data = json.loads(context_file.read_text(encoding="utf-8"))
        return data if isinstance(data, dict) else {}
    except (OSError, json.JSONDecodeError):
        return {}


def build_complete_text_log(output: dict[str, Any]) -> str:
    """
    Build a complete, human-readable iPerf log.

    The text file contains:
    - Test identity and parameters
    - Precheck and iPerf server status
    - Summary statistics
    - Every interval sample
    - Raw command, stdout, stderr and exit code
    - Full normalized JSON result
    """
    parameters = output.get("parameters") or {}
    precheck = output.get("precheck") or {}
    server = precheck.get("iperf_server") or {}
    summary = output.get("summary") or {}
    samples = output.get("samples") or []
    raw = output.get("raw") or {}

    lines = [
        "NetworkAutomation Complete iPerf Log",
        "=" * 72,
        "",
        "[Test Information]",
        f"Test ID           : {output.get('test_id', 'N/A')}",
        f"Schema Version    : {output.get('schema_version', 'N/A')}",
        f"Test Type         : {output.get('test_type', 'N/A')}",
        f"Timestamp End     : {output.get('timestamp_end', 'N/A')}",
        f"Result            : {'PASS' if output.get('success') else 'FAIL'}",
        "",
        "[Parameters]",
        f"UE Data IP        : {parameters.get('ue_ip', 'N/A')}",
        f"LTE Band          : {'B' + str(parameters.get('band')) if parameters.get('band') is not None else 'N/A'}",
        f"Bandwidth         : {_display(parameters.get('bandwidth_mhz'))} MHz",
        f"Cell              : {parameters.get('cell', 'N/A')}",
        f"EARFCN            : {parameters.get('dl_earfcn', 'N/A')}",
        f"Direction         : {parameters.get('direction', 'N/A')}",
        f"Duration          : {parameters.get('duration_sec', 'N/A')} sec",
        f"Port              : {parameters.get('port', 'N/A')}",
        f"Parallel Streams  : {parameters.get('parallel_streams', 'N/A')}",
        f"Interval          : {parameters.get('interval_sec', 'N/A')} sec",
        f"Reverse (-R)      : {parameters.get('reverse', False)}",
        "",
        "[Precheck]",
        f"UE Ping           : {'PASS' if precheck.get('ue_ping') else 'FAIL'}",
        f"Ping Command      : {precheck.get('ping_command', 'N/A')}",
        f"Server Ready      : {server.get('ready', 'N/A')}",
        f"Server Action     : {server.get('action', 'N/A')}",
        f"Server Message    : {server.get('message', 'N/A')}",
        "",
        "[Summary]",
        f"Average           : {_display(summary.get('average_mbps'))} Mbps",
        f"Minimum           : {_display(summary.get('minimum_mbps'))} Mbps",
        f"Maximum           : {_display(summary.get('maximum_mbps'))} Mbps",
        f"Total Transfer    : {_display(summary.get('total_transfer_mbytes'))} MB",
        f"Retransmissions   : {_display(summary.get('retransmissions'), 0)}",
        f"Packet Loss       : {_display(summary.get('packet_loss_percent'))} %",
        f"Sample Count      : {summary.get('sample_count', 0)}",
        "",
        "[Interval Samples]",
        "No.  Start    End      Mbps       MB         Retrans  Loss(%)  Omitted",
        "---  -------  -------  ---------  ---------  -------  -------  -------",
    ]

    for index, sample in enumerate(samples, start=1):
        lines.append(
            f"{index:>3}  "
            f"{_display(sample.get('second_start')):>7}  "
            f"{_display(sample.get('second_end')):>7}  "
            f"{_display(sample.get('throughput_mbps')):>9}  "
            f"{_display(sample.get('transfer_mbytes')):>9}  "
            f"{_display(sample.get('retransmissions'), 0):>7}  "
            f"{_display(sample.get('packet_loss_percent')):>7}  "
            f"{str(sample.get('omitted', False)):>7}"
        )

    if not samples:
        lines.append("(No interval samples)")

    lines.extend([
        "",
        "=" * 72,
        "End of Log",
    ])

    return "\n".join(lines) + "\n"


def main() -> int:
    ap = argparse.ArgumentParser()
    ap.add_argument("--settings", required=True)
    ap.add_argument("--ue-ip", required=True)
    ap.add_argument("--direction", required=True, choices=["download", "upload"])
    ap.add_argument("--duration", required=True, type=int)
    ap.add_argument("--port", type=int, default=5201)
    ap.add_argument("--parallel", type=int, default=1)
    ap.add_argument("--interval", type=int, default=1)
    ap.add_argument("--band", type=int)
    ap.add_argument("--bandwidth", type=float)
    ap.add_argument("--cell", type=int)
    ap.add_argument("--dl-earfcn", type=int)
    args = ap.parse_args()

    try:
        ipaddress.ip_address(args.ue_ip)
        raw = json.loads(Path(args.settings).read_text(encoding="utf-8"))
        cfg = raw["iperf"]
        ex = cfg["executor"]
        settings = type("IperfSettings", (), {})()
        for key, value in {
            "host": ex["host"],
            "port": ex.get("port", 22),
            "username": ex["username"],
            "password": ex.get("password", ""),
            "ssh_timeout_sec": ex.get("ssh_timeout_sec", 30),
            "command_timeout_sec": ex.get("command_timeout_sec", 120),
        }.items():
            setattr(settings, key, value)

        command = [
            cfg.get("binary", "iperf3"),
            "-c", args.ue_ip,
            "-p", str(args.port),
            "-P", str(args.parallel),
            "-t", str(args.duration),
            "-i", str(args.interval),
            "-J",
        ]
        if args.direction == "upload":
            command.append("-R")

        command_text = " ".join(shlex.quote(item) for item in command)
        ping_command = f"ping -c 1 -W 2 {shlex.quote(args.ue_ip)}"

        with SSHClient.from_callbox_settings(settings) as ssh:
            ping = ssh.execute(ping_command, 10)
            if not ping.success:
                raise RuntimeError(
                    f"UE {args.ue_ip} 無法從 iPerf Executor Ping 通，停止測試。"
                )

            server = ensure_iperf_server(
                Path(args.settings),
                ssh,
                args.ue_ip,
                args.port,
            )
            if not server.get("ready"):
                raise RuntimeError(server.get("message") or "iPerf Server 未就緒。")

            result = ssh.execute(
                command_text,
                max(settings.command_timeout_sec, args.duration + 180),
            )

        if not result.success:
            raise RuntimeError(result.stderr or result.stdout or "iperf3 failed")

        parsed = parse_json(result.stdout)
        test_id = (
            f"iperf-{datetime.now().strftime('%Y%m%d-%H%M%S')}-"
            f"{uuid.uuid4().hex[:6]}"
        )
        saved_radio = load_radio_context(ROOT)
        radio_context = {
            "band": args.band if args.band is not None else saved_radio.get("band"),
            "bandwidth_mhz": (
                args.bandwidth
                if args.bandwidth is not None
                else saved_radio.get("bandwidth_mhz")
            ),
            "cell": args.cell if args.cell is not None else saved_radio.get("cell"),
            "dl_earfcn": (
                args.dl_earfcn
                if args.dl_earfcn is not None
                else saved_radio.get("dl_earfcn")
            ),
        }

        output = {
            "success": True,
            "schema_version": "2.0",
            "test_id": test_id,
            "test_type": "iperf3",
            "timestamp_end": datetime.now().astimezone().isoformat(timespec="seconds"),
            "parameters": {
                "ue_ip": args.ue_ip,
                **radio_context,
                "direction": args.direction,
                "duration_sec": args.duration,
                "port": args.port,
                "parallel_streams": args.parallel,
                "interval_sec": args.interval,
                "reverse": args.direction == "upload",
            },
            "precheck": {
                "ue_ping": True,
                "ping_command": ping_command,
                "iperf_server": server,
            },
            "summary": parsed["summary"],
            "samples": parsed["samples"],
            "raw": {
                "command": command_text,
                "stdout": result.stdout,
                "stderr": result.stderr,
                "exit_code": result.exit_code,
                "iperf_json": parsed["iperf_json"],
            },
            "storage_targets": {
                "timeseries": "samples",
                "neo4j": "parameters + context + test relationship",
                "qdrant": "summary + future notes/log analysis",
            },
        }

        out_dir = (
            ROOT
            / cfg.get("result_dir", "results/iperf")
            / datetime.now().strftime("%Y-%m-%d")
        )
        out_dir.mkdir(parents=True, exist_ok=True)
        result_file = out_dir / f"{test_id}.json"
        text_log_file = out_dir / f"{test_id}.txt"

        output["result_file"] = str(result_file)
        output["text_log_file"] = str(text_log_file)

        result_file.write_text(
            json.dumps(output, ensure_ascii=False, indent=2),
            encoding="utf-8",
        )

        # UTF-8 with BOM keeps Traditional Chinese readable in Windows Notepad.
        text_log_file.write_text(
            build_complete_text_log(output),
            encoding="utf-8-sig",
        )

        print(json.dumps(output, ensure_ascii=False, indent=2))
        return 0

    except Exception as exc:
        print(json.dumps({
            "success": False,
            "error": type(exc).__name__,
            "message": str(exc),
        }, ensure_ascii=False, indent=2))
        return 1


if __name__ == "__main__":
    raise SystemExit(main())
