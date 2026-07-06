#!/usr/bin/env python3
# -*- coding: utf-8 -*-
"""
callbox_agent.py

Single entry point for OpenClaw / future MCP tools.

Supported:
  1) Amarisoft LTE band control
     python callbox_agent.py set-band --cell 1 --band 5 --bandwidth 10 --dry-run

  2) Generic iperf tool
     python callbox_agent.py iperf check
     python callbox_agent.py iperf client --server 192.168.1.100 --time 10 --parallel 4
"""

from __future__ import annotations

import argparse
import json
import subprocess
import sys
from pathlib import Path

from controller import CallboxController


BASE = Path(__file__).parent
controller = CallboxController(BASE)


def print_json(data: dict) -> None:
    print(json.dumps(data, ensure_ascii=False, indent=2))


def run_iperf_tool(args: argparse.Namespace) -> dict:
    cmd = [
        sys.executable,
        str(BASE / "tools" / "iperf" / "runner.py"),
    ]

    if args.iperf_cmd == "check":
        cmd.append("check")

    elif args.iperf_cmd == "client":
        cmd.extend([
            "client",
            "--server", args.server,
            "--port", str(args.port),
            "--time", str(args.time),
            "--parallel", str(args.parallel),
            "--protocol", args.protocol,
        ])

        if args.reverse:
            cmd.append("--reverse")
        if args.bandwidth:
            cmd.extend(["--bandwidth", args.bandwidth])
        if args.udp_length:
            cmd.extend(["--udp-length", str(args.udp_length)])

        # Optional remote execution target.
        if args.ssh_host:
            cmd.extend(["--ssh-host", args.ssh_host])
        if args.ssh_port:
            cmd.extend(["--ssh-port", str(args.ssh_port)])
        if args.ssh_user:
            cmd.extend(["--ssh-user", args.ssh_user])
        if args.ssh_password:
            cmd.extend(["--ssh-password", args.ssh_password])

    else:
        return {
            "success": False,
            "error": "UNKNOWN_IPERF_COMMAND",
            "message": f"Unsupported iperf command: {args.iperf_cmd}",
        }

    p = subprocess.run(cmd, capture_output=True, text=True)
    stdout = (p.stdout or "").strip()
    stderr = (p.stderr or "").strip()

    try:
        result = json.loads(stdout) if stdout else {}
    except Exception:
        result = {
            "success": False,
            "error": "INVALID_JSON_OUTPUT_FROM_IPERF_TOOL",
            "stdout": stdout,
        }

    result.setdefault("success", p.returncode == 0)
    result.setdefault("returncode", p.returncode)
    if stderr:
        result.setdefault("stderr", stderr)
    return result


def build_parser() -> argparse.ArgumentParser:
    parser = argparse.ArgumentParser(description="NetworkAutomation entry point")
    sub = parser.add_subparsers(dest="cmd")

    # Backward-compatible Amarisoft command.
    p = sub.add_parser("set-band", help="Generate LTE config, upload to Callbox, and restart LTE")
    p.add_argument("--cell", type=int, required=True)
    p.add_argument("--band", type=int, required=True)
    p.add_argument("--bandwidth", type=float, default=None)
    p.add_argument("--dl-earfcn", type=int, default=None)
    p.add_argument("--dry-run", action="store_true")
    p.add_argument("--no-restart", action="store_true")

    # New Phase 2 generic iperf tool.
    iperf = sub.add_parser("iperf", help="Generic iperf3 throughput tool")
    iperf_sub = iperf.add_subparsers(dest="iperf_cmd")

    iperf_sub.add_parser("check", help="Check iperf3 availability")

    client = iperf_sub.add_parser("client", help="Run iperf3 client test")
    client.add_argument("--server", required=True, help="iperf3 server IP or hostname")
    client.add_argument("--port", type=int, default=5201)
    client.add_argument("--time", type=int, default=10)
    client.add_argument("--parallel", type=int, default=1)
    client.add_argument("--protocol", choices=["tcp", "udp"], default="tcp")
    client.add_argument("--reverse", action="store_true", help="iperf3 reverse mode")
    client.add_argument("--bandwidth", default=None, help="UDP target bandwidth, e.g. 100M")
    client.add_argument("--udp-length", type=int, default=None, help="UDP packet length")

    # Optional remote execution target through SSH.
    client.add_argument("--ssh-host", default=None, help="Run iperf client command on remote host through SSH")
    client.add_argument("--ssh-port", type=int, default=22)
    client.add_argument("--ssh-user", default=None)
    client.add_argument("--ssh-password", default=None)

    return parser


def main() -> int:
    parser = build_parser()
    args = parser.parse_args()

    if args.cmd == "set-band":
        result = controller.apply_lte(
            cell=args.cell,
            band=args.band,
            bandwidth=args.bandwidth,
            dl_earfcn=args.dl_earfcn,
            dry_run=args.dry_run,
            no_restart=args.no_restart,
        )
        print_json(result)
        return 0 if result.get("success") else 1

    if args.cmd == "iperf":
        if not args.iperf_cmd:
            parser.print_help()
            return 2
        result = run_iperf_tool(args)
        print_json(result)
        return 0 if result.get("success") else 1

    parser.print_help()
    return 2


if __name__ == "__main__":
    raise SystemExit(main())
