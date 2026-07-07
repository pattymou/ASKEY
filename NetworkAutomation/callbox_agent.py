#!/usr/bin/env python3
# -*- coding: utf-8 -*-
from __future__ import annotations

import argparse
import json
import subprocess
import sys
from pathlib import Path

BASE = Path(__file__).parent


def print_json(data: dict) -> None:
    print(json.dumps(data, ensure_ascii=False, indent=2))


def run_json(cmd: list[str]) -> dict:
    p = subprocess.run(cmd, capture_output=True, text=True)
    stdout = (p.stdout or "").strip()
    stderr = (p.stderr or "").strip()

    try:
        result = json.loads(stdout) if stdout else {}
    except Exception:
        result = {
            "success": False,
            "error": "INVALID_JSON_OUTPUT",
            "stdout": stdout,
        }

    result.setdefault("success", p.returncode == 0)
    result.setdefault("returncode", p.returncode)

    if stderr:
        result.setdefault("stderr", stderr)

    return result


def run_apply(args) -> dict:
    cmd = [
        sys.executable,
        str(BASE / "plugins" / "amarisoft" / "apply_lte_to_callbox.py"),
        "--settings",
        str(BASE / "callbox_settings.json"),
        "--cell",
        str(args.cell),
        "--band",
        str(args.band),
    ]

    if args.bandwidth is not None:
        cmd += ["--bandwidth", str(args.bandwidth)]

    if args.dl_earfcn is not None:
        cmd += ["--dl-earfcn", str(args.dl_earfcn)]

    if getattr(args, "dry_run", False):
        cmd.append("--dry-run")

    if getattr(args, "no_restart", False):
        cmd.append("--no-restart")

    return run_json(cmd)


def run_status(args) -> dict:
    return run_json([
        sys.executable,
        str(BASE / "plugins" / "amarisoft" / "status.py"),
        "--settings",
        str(BASE / "callbox_settings.json"),
    ])


def run_smart(args) -> dict:
    cmd = [
        sys.executable,
        str(BASE / "plugins" / "amarisoft" / "command_parser.py"),
        "--settings",
        str(BASE / "callbox_settings.json"),
        args.text,
    ]

    if args.execute:
        cmd.append("--execute")

    if args.dry_run:
        cmd.append("--dry-run")

    return run_json(cmd)


def add_set_band_parser(subparsers) -> None:
    p = subparsers.add_parser("set-band", help="Set Amarisoft LTE Band")
    p.add_argument("--cell", type=int, required=True)
    p.add_argument("--band", type=int, required=True)
    p.add_argument("--bandwidth", type=float, default=None)
    p.add_argument("--dl-earfcn", type=int, default=None)
    p.add_argument("--dry-run", action="store_true")
    p.add_argument("--no-restart", action="store_true")


def add_smart_parser(subparsers) -> None:
    p = subparsers.add_parser("smart", help="Parse natural language command")
    p.add_argument("text", help="Natural language command, e.g. 幫我切 B5 10MHz")
    p.add_argument("--execute", action="store_true", help="Execute after parsing")
    p.add_argument("--dry-run", action="store_true", help="Pass --dry-run when executing")


def build_parser():
    parser = argparse.ArgumentParser(description="NetworkAutomation Agent")
    sub = parser.add_subparsers(dest="cmd")

    add_set_band_parser(sub)
    add_smart_parser(sub)
    sub.add_parser("status", help="Get Amarisoft status")

    amari = sub.add_parser("amarisoft", help="Amarisoft Callbox tools")
    amari_sub = amari.add_subparsers(dest="amarisoft_cmd")
    add_set_band_parser(amari_sub)
    add_smart_parser(amari_sub)
    amari_sub.add_parser("status", help="Get Amarisoft status")

    return parser


def main() -> int:
    parser = build_parser()
    args = parser.parse_args()

    if args.cmd == "set-band":
        result = run_apply(args)
        print_json(result)
        return 0 if result.get("success") else 1

    if args.cmd == "status":
        result = run_status(args)
        print_json(result)
        return 0 if result.get("success") else 1

    if args.cmd == "smart":
        result = run_smart(args)
        print_json(result)
        return 0 if result.get("success") else 1

    if args.cmd == "amarisoft":
        if args.amarisoft_cmd == "set-band":
            result = run_apply(args)
            print_json(result)
            return 0 if result.get("success") else 1

        if args.amarisoft_cmd == "status":
            result = run_status(args)
            print_json(result)
            return 0 if result.get("success") else 1

        if args.amarisoft_cmd == "smart":
            result = run_smart(args)
            print_json(result)
            return 0 if result.get("success") else 1

    parser.print_help()
    return 2


if __name__ == "__main__":
    raise SystemExit(main())
