#!/usr/bin/env python3
# -*- coding: utf-8 -*-
"""Network Automation entry point for OpenClaw / future MCP.

Examples:
  python callbox_agent.py amarisoft set-band --cell 1 --band 5 --bandwidth 10 --dry-run
  python callbox_agent.py set-band --cell 1 --band 5 --bandwidth 10 --dry-run   # legacy compatible
  python callbox_agent.py iperf client --server 192.168.1.100 --time 10
"""
from __future__ import annotations

import argparse
import json
from pathlib import Path

from plugins.amarisoft.controller import AmarisoftController
from tools.iperf.runner import IperfRunner

BASE = Path(__file__).parent


def print_json(data: dict) -> None:
    print(json.dumps(data, ensure_ascii=False, indent=2))


def add_set_band_parser(sub) -> None:
    p = sub.add_parser("set-band", help="Generate LTE config, upload to Callbox, and restart LTE")
    p.add_argument("--cell", type=int, required=True)
    p.add_argument("--band", type=int, required=True)
    p.add_argument("--bandwidth", type=float, default=None)
    p.add_argument("--dl-earfcn", type=int, default=None)
    p.add_argument("--dry-run", action="store_true")
    p.add_argument("--no-restart", action="store_true")


def run_set_band(args) -> int:
    controller = AmarisoftController(BASE)
    result = controller.set_lte_band(
        cell=args.cell,
        band=args.band,
        bandwidth=args.bandwidth,
        dl_earfcn=args.dl_earfcn,
        dry_run=args.dry_run,
        no_restart=args.no_restart,
    )
    print_json(result)
    return 0 if result.get("success") else 1


def run_iperf_client(args) -> int:
    result = IperfRunner(args.bin).client(
        server=args.server,
        port=args.port,
        duration=args.time,
        parallel=args.parallel,
        protocol=args.protocol,
        reverse=args.reverse,
        bandwidth=args.bandwidth,
    )
    print_json(result)
    return 0 if result.get("success") else 1


def main() -> int:
    parser = argparse.ArgumentParser(description="Network Automation Agent entry point")
    sub = parser.add_subparsers(dest="cmd")

    # Legacy compatible command: python callbox_agent.py set-band ...
    add_set_band_parser(sub)

    # New plugin-style command: python callbox_agent.py amarisoft set-band ...
    amarisoft = sub.add_parser("amarisoft", help="Amarisoft Callbox tools")
    amarisoft_sub = amarisoft.add_subparsers(dest="amarisoft_cmd")
    add_set_band_parser(amarisoft_sub)

    # Generic iperf tool: python callbox_agent.py iperf client ...
    iperf = sub.add_parser("iperf", help="Generic iperf3 throughput tools")
    iperf_sub = iperf.add_subparsers(dest="iperf_cmd")
    c = iperf_sub.add_parser("client", help="Run iperf3 client and return JSON summary")
    c.add_argument("--server", required=True)
    c.add_argument("--port", type=int, default=5201)
    c.add_argument("--time", type=int, default=10)
    c.add_argument("--parallel", type=int, default=1)
    c.add_argument("--protocol", choices=["tcp", "udp"], default="tcp")
    c.add_argument("--reverse", action="store_true")
    c.add_argument("--bandwidth", default=None, help="UDP bandwidth, e.g. 100M")
    c.add_argument("--bin", default="iperf3", help="iperf3 binary path")

    args = parser.parse_args()

    if args.cmd == "set-band":
        return run_set_band(args)
    if args.cmd == "amarisoft" and args.amarisoft_cmd == "set-band":
        return run_set_band(args)
    if args.cmd == "iperf" and args.iperf_cmd == "client":
        return run_iperf_client(args)

    parser.print_help()
    return 2


if __name__ == "__main__":
    raise SystemExit(main())
