#!/usr/bin/env python3
# -*- coding: utf-8 -*-
from __future__ import annotations

import argparse
import json
import os
import subprocess
import sys
from pathlib import Path

from core.result_builder import build_tool_result
from core.workflow_executor import run_band_then_iperf

ROOT = Path(__file__).resolve().parent

try:
    sys.stdout.reconfigure(encoding="utf-8", errors="replace")
    sys.stderr.reconfigure(encoding="utf-8", errors="replace")
except Exception:
    pass


def run_child(command: list[str]) -> dict:
    child_env = os.environ.copy()
    child_env["PYTHONIOENCODING"] = "utf-8"
    child_env["PYTHONUTF8"] = "1"
    process = subprocess.run(
        command,
        capture_output=True,
        text=True,
        encoding="utf-8",
        errors="strict",
        cwd=str(ROOT),
        env=child_env,
    )
    stdout = (process.stdout or "").strip()
    stderr = (process.stderr or "").strip()
    try:
        result = json.loads(stdout) if stdout else {}
    except json.JSONDecodeError:
        result = {"success": False, "error": "INVALID_JSON_OUTPUT", "stdout": stdout}
    result.setdefault("success", process.returncode == 0)
    result.setdefault("returncode", process.returncode)
    if stderr:
        result.setdefault("stderr", stderr)
    return result


def build_parser() -> argparse.ArgumentParser:
    parser = argparse.ArgumentParser(description="NetworkAutomation V10.3 CLI")
    domains = parser.add_subparsers(dest="domain")

    amarisoft = domains.add_parser("amarisoft")
    actions = amarisoft.add_subparsers(dest="action")
    set_band = actions.add_parser("set-band")
    set_band.add_argument("--cell", type=int, default=1)
    set_band.add_argument("--band", type=int)
    set_band.add_argument("--bandwidth", type=float)
    set_band.add_argument("--band-config")
    set_band.add_argument("--bandwidth-config")
    set_band.add_argument("--dl-earfcn", type=int)
    set_band.add_argument("--mimo-dl")
    set_band.add_argument("--mimo-ul")
    set_band.add_argument("--modulation-dl")
    set_band.add_argument("--modulation-ul")
    set_band.add_argument("--mcs-dl")
    set_band.add_argument("--mcs-ul")
    set_band.add_argument("--dry-run", action="store_true")
    set_band.add_argument("--no-restart", action="store_true")
    set_band.add_argument("--skip-ue-wait", action="store_true")
    set_band.add_argument("--phy-only-ready", action="store_true", help="PHY Rate 一出現就完成，不等待 Data IP")
    set_band.add_argument("--expected-imsi")
    set_band.add_argument("--expected-imei")
    set_band.add_argument("--apn")

    set_nr = actions.add_parser("set-nr-band")
    set_nr.add_argument("--mode", required=True, choices=["SA", "ENDC", "sa", "endc"])
    set_nr.add_argument("--cell", type=int, default=1)
    set_nr.add_argument("--band", type=int)
    set_nr.add_argument("--bandwidth", type=float)
    set_nr.add_argument("--band-config")
    set_nr.add_argument("--bandwidth-config")
    set_nr.add_argument("--nr-arfcn", type=int)
    set_nr.add_argument("--mimo-dl")
    set_nr.add_argument("--mimo-ul")
    set_nr.add_argument("--modulation-dl")
    set_nr.add_argument("--modulation-ul")
    set_nr.add_argument("--mcs-dl")
    set_nr.add_argument("--mcs-ul")
    set_nr.add_argument("--time-slot")
    set_nr.add_argument("--lte-band", type=int)
    set_nr.add_argument("--lte-bandwidth", type=float)
    set_nr.add_argument("--lte-earfcn", type=int)
    set_nr.add_argument("--dry-run", action="store_true")
    set_nr.add_argument("--no-restart", action="store_true")
    set_nr.add_argument("--skip-ue-wait", action="store_true")
    set_nr.add_argument("--phy-only-ready", action="store_true", help="PHY Rate 一出現就完成，不等待 Data IP")
    set_nr.add_argument("--expected-imsi")
    set_nr.add_argument("--expected-imei")
    set_nr.add_argument("--apn")

    actions.add_parser("status")
    connection = actions.add_parser("connection")
    connection.add_argument("--wait", action="store_true")
    connection.add_argument("--expected-imsi")
    connection.add_argument("--expected-imei")
    connection.add_argument("--apn")

    iperf = domains.add_parser("iperf")
    ia = iperf.add_subparsers(dest="action")
    run = ia.add_parser("run")
    run.add_argument("--ue-ip", required=True)
    run.add_argument("--direction", required=True, choices=["download", "upload", "bidirectional"])
    run.add_argument("--duration", required=True, type=int)
    run.add_argument("--port", type=int, default=5201)
    run.add_argument("--parallel", type=int, default=1)
    run.add_argument("--interval", type=int, default=1)
    run.add_argument("--band", type=int)
    run.add_argument("--bandwidth", type=float)
    run.add_argument("--cell", type=int)
    run.add_argument("--dl-earfcn", type=int)

    workflow = domains.add_parser("workflow")
    wa = workflow.add_subparsers(dest="action")
    combo = wa.add_parser("band-iperf")
    combo.add_argument("--cell", type=int, default=1)
    combo.add_argument("--band", type=int, required=True)
    combo.add_argument("--bandwidth", type=float, required=True)
    combo.add_argument("--direction", required=True, choices=["download", "upload", "bidirectional"])
    combo.add_argument("--duration", required=True, type=int)
    combo.add_argument("--ue-ip")
    combo.add_argument("--port", type=int, default=5201)
    combo.add_argument("--parallel", type=int, default=1)
    combo.add_argument("--interval", type=int, default=1)
    combo.add_argument("--dl-earfcn", type=int)
    combo.add_argument("--expected-imsi")
    combo.add_argument("--expected-imei")
    combo.add_argument("--apn")
    return parser


def resolve(args: argparse.Namespace) -> tuple[str, list[str]]:
    settings = str(ROOT / "callbox_settings.json")
    if args.domain == "amarisoft" and args.action == "set-band":
        if args.band_config:
            command = [sys.executable, str(ROOT / "plugins/amarisoft/apply_lte_combo_to_callbox.py"),
                       "--settings", settings,
                       "--band-config", str(args.band_config),
                       "--bandwidth-config", str(args.bandwidth_config)]
            for value, flag in (
                (args.expected_imsi, "--expected-imsi"),
                (args.expected_imei, "--expected-imei"),
                (args.apn, "--apn"),
            ):
                if value is not None:
                    command += [flag, str(value)]
        else:
            if args.band is None:
                raise ValueError("set-band 必須指定 --band 或 --band-config")
            command = [sys.executable, str(ROOT / "plugins/amarisoft/apply_lte_to_callbox.py"),
                       "--settings", settings, "--cell", str(args.cell), "--band", str(args.band)]
            for value, flag in (
                (args.bandwidth, "--bandwidth"),
                (args.dl_earfcn, "--dl-earfcn"),
                (args.mimo_dl, "--mimo-dl"),
                (args.mimo_ul, "--mimo-ul"),
                (args.modulation_dl, "--modulation-dl"),
                (args.modulation_ul, "--modulation-ul"),
                (args.mcs_dl, "--mcs-dl"),
                (args.mcs_ul, "--mcs-ul"),
                (args.expected_imsi, "--expected-imsi"),
                (args.expected_imei, "--expected-imei"),
                (args.apn, "--apn"),
            ):
                if value is not None:
                    command += [flag, str(value)]
        if args.dry_run: command.append("--dry-run")
        if args.no_restart: command.append("--no-restart")
        if args.skip_ue_wait: command.append("--skip-ue-wait")
        if args.phy_only_ready: command.append("--phy-only-ready")
        return "amarisoft.set_band", command
    if args.domain == "amarisoft" and args.action == "set-nr-band":
        command = [sys.executable, str(ROOT / "plugins/amarisoft/apply_nr_to_callbox.py"),
                   "--settings", settings, "--mode", str(args.mode).upper(),
                   "--cell", str(args.cell)]
        if args.band_config:
            command += ["--band-config", str(args.band_config)]
            if args.bandwidth_config:
                command += ["--bandwidth-config", str(args.bandwidth_config)]
        elif args.band is not None:
            command += ["--band", str(args.band)]
        else:
            raise ValueError("set-nr-band 必須指定 --band 或 --band-config")
        for value, flag in (
            (args.bandwidth, "--bandwidth"),
            (args.nr_arfcn, "--nr-arfcn"),
            (args.mimo_dl, "--mimo-dl"),
            (args.mimo_ul, "--mimo-ul"),
            (args.modulation_dl, "--modulation-dl"),
            (args.modulation_ul, "--modulation-ul"),
            (args.mcs_dl, "--mcs-dl"),
            (args.mcs_ul, "--mcs-ul"),
            (args.time_slot, "--time-slot"),
            (args.lte_band, "--lte-band"),
            (args.lte_bandwidth, "--lte-bandwidth"),
            (args.lte_earfcn, "--lte-earfcn"),
            (args.expected_imsi, "--expected-imsi"),
            (args.expected_imei, "--expected-imei"),
            (args.apn, "--apn"),
        ):
            if value is not None:
                command += [flag, str(value)]
        if args.dry_run: command.append("--dry-run")
        if args.no_restart: command.append("--no-restart")
        if args.skip_ue_wait: command.append("--skip-ue-wait")
        if args.phy_only_ready: command.append("--phy-only-ready")
        return "amarisoft.set_nr_band", command
    if args.domain == "amarisoft" and args.action == "status":
        return "amarisoft.status", [sys.executable, str(ROOT / "plugins/amarisoft/status.py"), "--settings", settings]
    if args.domain == "amarisoft" and args.action == "connection":
        command = [sys.executable, str(ROOT / "plugins/amarisoft/ue_connection.py"), "--settings", settings]
        if args.wait: command.append("--wait")
        for value, flag in ((args.expected_imsi, "--expected-imsi"), (args.expected_imei, "--expected-imei"), (args.apn, "--apn")):
            if value: command += [flag, value]
        return "amarisoft.connection", command
    if args.domain == "iperf" and args.action == "run":
        if args.direction != "bidirectional":
            command = [sys.executable, str(ROOT / "tools/iperf/runner.py"), "--settings", settings,
                       "--ue-ip", args.ue_ip, "--direction", args.direction, "--duration", str(args.duration),
                       "--port", str(args.port), "--parallel", str(args.parallel), "--interval", str(args.interval)]
            for value, flag in ((args.band, "--band"), (args.bandwidth, "--bandwidth"),
                                (args.cell, "--cell"), (args.dl_earfcn, "--dl-earfcn")):
                if value is not None:
                    command += [flag, str(value)]
            return "iperf.run", command
        # handled directly in main
        return "iperf.bidirectional", []
    raise ValueError("未指定有效 domain/action")


def main() -> int:
    parser = build_parser()
    args = parser.parse_args()

    if args.domain == "workflow" and args.action == "band-iperf":
        machine = run_band_then_iperf(
            ROOT, cell=args.cell, band=args.band, bandwidth=args.bandwidth,
            direction=args.direction, duration=args.duration, ue_ip=args.ue_ip,
            port=args.port, parallel=args.parallel, interval=args.interval,
            dl_earfcn=args.dl_earfcn, expected_imsi=args.expected_imsi,
            expected_imei=args.expected_imei, apn=args.apn,
        )
        envelope = build_tool_result("workflow.band_iperf", machine).to_dict()
        sys.stdout.write(json.dumps(envelope, ensure_ascii=False, indent=2) + "\n")
        return 0 if envelope["success"] else 1

    if args.domain == "iperf" and args.action == "run" and args.direction == "bidirectional":
        results = []
        for direction in ("download", "upload"):
            command = [sys.executable, str(ROOT / "tools/iperf/runner.py"), "--settings", str(ROOT / "callbox_settings.json"),
                       "--ue-ip", args.ue_ip, "--direction", direction, "--duration", str(args.duration),
                       "--port", str(args.port), "--parallel", str(args.parallel), "--interval", str(args.interval)]
            result = run_child(command)
            results.append({"direction": direction, "result": result})
            if not result.get("success"):
                break
        machine = {"success": len(results) == 2 and all(x["result"].get("success") for x in results),
                   "ue_ip": args.ue_ip, "duration_sec_each": args.duration, "results": results}
        envelope = build_tool_result("iperf.bidirectional", machine).to_dict()
        sys.stdout.write(json.dumps(envelope, ensure_ascii=False, indent=2) + "\n")
        return 0 if envelope["success"] else 1

    try:
        tool, command = resolve(args)
    except ValueError:
        parser.print_help()
        return 2
    machine = run_child(command)
    envelope = build_tool_result(tool, machine).to_dict()
    sys.stdout.write(json.dumps(envelope, ensure_ascii=False, indent=2) + "\n")
    return 0 if envelope["success"] else 1


if __name__ == "__main__":
    raise SystemExit(main())
