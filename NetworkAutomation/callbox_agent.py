#!/usr/bin/env python3
# -*- coding: utf-8 -*-
from __future__ import annotations
import argparse, json, subprocess, sys
from pathlib import Path
BASE = Path(__file__).parent

def print_json(data): print(json.dumps(data, ensure_ascii=False, indent=2))

def run_json(cmd):
    p = subprocess.run(cmd, capture_output=True, text=True)
    stdout, stderr = (p.stdout or "").strip(), (p.stderr or "").strip()
    try:
        result = json.loads(stdout) if stdout else {}
    except Exception:
        result = {"success": False, "error": "INVALID_JSON_OUTPUT", "stdout": stdout}
    result.setdefault("success", p.returncode == 0)
    result.setdefault("returncode", p.returncode)
    if stderr: result.setdefault("stderr", stderr)
    return result

def add_set_band(sub):
    p = sub.add_parser("set-band")
    p.add_argument("--cell", type=int, required=True)
    p.add_argument("--band", type=int, required=True)
    p.add_argument("--bandwidth", type=float, default=None)
    p.add_argument("--dl-earfcn", type=int, default=None)
    p.add_argument("--dry-run", action="store_true")
    p.add_argument("--no-restart", action="store_true")

def run_apply(args):
    cmd = [sys.executable, str(BASE/"plugins"/"amarisoft"/"apply_lte_to_callbox.py"),
           "--settings", str(BASE/"callbox_settings.json"),
           "--cell", str(args.cell), "--band", str(args.band)]
    if args.bandwidth is not None: cmd += ["--bandwidth", str(args.bandwidth)]
    if args.dl_earfcn is not None: cmd += ["--dl-earfcn", str(args.dl_earfcn)]
    if args.dry_run: cmd.append("--dry-run")
    if args.no_restart: cmd.append("--no-restart")
    return run_json(cmd)

def run_status(args):
    return run_json([sys.executable, str(BASE/"plugins"/"amarisoft"/"status.py"),
                     "--settings", str(BASE/"callbox_settings.json")])

def main():
    parser = argparse.ArgumentParser(description="NetworkAutomation Agent")
    sub = parser.add_subparsers(dest="cmd")
    add_set_band(sub)
    sub.add_parser("status")
    am = sub.add_parser("amarisoft")
    amsub = am.add_subparsers(dest="amarisoft_cmd")
    add_set_band(amsub)
    amsub.add_parser("status")
    args = parser.parse_args()

    if args.cmd == "set-band" or (args.cmd=="amarisoft" and args.amarisoft_cmd=="set-band"):
        result = run_apply(args); print_json(result); return 0 if result.get("success") else 1
    if args.cmd == "status" or (args.cmd=="amarisoft" and args.amarisoft_cmd=="status"):
        result = run_status(args); print_json(result); return 0 if result.get("success") else 1
    parser.print_help(); return 2

if __name__ == "__main__": raise SystemExit(main())
