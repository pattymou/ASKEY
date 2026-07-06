#!/usr/bin/env python3
# -*- coding: utf-8 -*-
"""
tools/iperf/runner.py

Generic iperf3 tool for NetworkAutomation.

Goals:
- Not tied to Amarisoft.
- Can run locally or on a remote SSH host.
- stdout returns JSON only.
- stderr is for debug/progress logs.
"""

from __future__ import annotations

import argparse
import json
import shutil
import subprocess
import sys
import time
from dataclasses import dataclass
from typing import Any


def print_json(data: dict[str, Any]) -> None:
    print(json.dumps(data, ensure_ascii=False, indent=2))


def now_ms() -> int:
    return int(time.time() * 1000)


@dataclass(frozen=True)
class CommandResult:
    success: bool
    command: str
    returncode: int
    stdout: str
    stderr: str
    elapsed_ms: int


def run_local_command(cmd: list[str], timeout_sec: int | None = None) -> CommandResult:
    start = now_ms()
    p = subprocess.run(cmd, capture_output=True, text=True, timeout=timeout_sec)
    elapsed = now_ms() - start
    return CommandResult(
        success=p.returncode == 0,
        command=" ".join(cmd),
        returncode=p.returncode,
        stdout=p.stdout or "",
        stderr=p.stderr or "",
        elapsed_ms=elapsed,
    )


def shell_quote(value: str) -> str:
    return "'" + value.replace("'", "'\\''") + "'"


def run_remote_command(
    command: str,
    host: str,
    port: int,
    username: str,
    password: str,
    timeout_sec: int | None = None,
) -> CommandResult:
    try:
        import paramiko  # type: ignore
    except ModuleNotFoundError as exc:
        raise RuntimeError("Remote iperf requires paramiko: python -m pip install paramiko") from exc

    start = now_ms()
    client = paramiko.SSHClient()
    client.set_missing_host_key_policy(paramiko.AutoAddPolicy())

    try:
        client.connect(
            hostname=host,
            port=int(port),
            username=username,
            password=password,
            timeout=30,
            look_for_keys=False,
            allow_agent=False,
        )
        _, stdout, stderr = client.exec_command(command, timeout=timeout_sec)
        returncode = stdout.channel.recv_exit_status()
        out = stdout.read().decode("utf-8", errors="replace")
        err = stderr.read().decode("utf-8", errors="replace")
    finally:
        client.close()

    elapsed = now_ms() - start
    return CommandResult(
        success=returncode == 0,
        command=command,
        returncode=returncode,
        stdout=out,
        stderr=err,
        elapsed_ms=elapsed,
    )


def build_iperf_client_command(args: argparse.Namespace) -> list[str]:
    cmd = [
        "iperf3",
        "-c", args.server,
        "-p", str(args.port),
        "-t", str(args.time),
        "-P", str(args.parallel),
        "-J",
    ]

    if args.protocol == "udp":
        cmd.append("-u")
        if args.bandwidth:
            cmd.extend(["-b", args.bandwidth])
        if args.udp_length is not None:
            cmd.extend(["-l", str(args.udp_length)])

    if args.reverse:
        cmd.append("-R")

    return cmd


def check_iperf() -> dict[str, Any]:
    path = shutil.which("iperf3")
    if not path:
        return {
            "success": False,
            "tool": "iperf3",
            "error": "IPERF3_NOT_FOUND",
            "message": "iperf3 was not found in PATH.",
            "hint": "Install iperf3 and make sure iperf3.exe is in PATH.",
        }

    result = run_local_command(["iperf3", "--version"], timeout_sec=10)
    first_line = result.stdout.splitlines()[0] if result.stdout.splitlines() else ""
    return {
        "success": result.success,
        "tool": "iperf3",
        "path": path,
        "version": first_line,
        "command": result.command,
        "returncode": result.returncode,
        "stderr": result.stderr.strip() or None,
    }


def parse_iperf_json(raw: dict[str, Any], protocol: str) -> dict[str, Any]:
    end = raw.get("end", {})

    if protocol == "udp":
        summary = end.get("sum", {}) or end.get("sum_received", {}) or {}
        bps = summary.get("bits_per_second")
        return {
            "throughput_mbps": round(float(bps) / 1_000_000, 3) if bps is not None else None,
            "jitter_ms": summary.get("jitter_ms"),
            "lost_packets": summary.get("lost_packets"),
            "packets": summary.get("packets"),
            "lost_percent": summary.get("lost_percent"),
            "retransmits": None,
        }

    # TCP: prefer receiver side if available.
    summary = end.get("sum_received") or end.get("sum_sent") or {}
    bps = summary.get("bits_per_second")
    retransmits = None
    if end.get("sum_sent"):
        retransmits = end["sum_sent"].get("retransmits")

    return {
        "throughput_mbps": round(float(bps) / 1_000_000, 3) if bps is not None else None,
        "retransmits": retransmits,
        "jitter_ms": None,
        "lost_packets": None,
        "packets": None,
        "lost_percent": None,
    }


def client(args: argparse.Namespace) -> dict[str, Any]:
    cmd_list = build_iperf_client_command(args)

    timeout_sec = int(args.time) + 30

    if args.ssh_host:
        if not args.ssh_user or args.ssh_password is None:
            return {
                "success": False,
                "tool": "iperf3",
                "error": "MISSING_SSH_CREDENTIALS",
                "message": "--ssh-user and --ssh-password are required when --ssh-host is used.",
            }

        remote_command = " ".join(shell_quote(x) for x in cmd_list)
        cmd_result = run_remote_command(
            command=remote_command,
            host=args.ssh_host,
            port=args.ssh_port,
            username=args.ssh_user,
            password=args.ssh_password,
            timeout_sec=timeout_sec,
        )
        execution = {
            "mode": "remote_ssh",
            "ssh_host": args.ssh_host,
            "ssh_port": args.ssh_port,
            "ssh_user": args.ssh_user,
        }
    else:
        cmd_result = run_local_command(cmd_list, timeout_sec=timeout_sec)
        execution = {"mode": "local"}

    if not cmd_result.success:
        return {
            "success": False,
            "tool": "iperf3",
            "role": "client",
            "execution": execution,
            "command": cmd_result.command,
            "returncode": cmd_result.returncode,
            "stderr": cmd_result.stderr.strip(),
            "stdout": cmd_result.stdout.strip(),
            "elapsed_ms": cmd_result.elapsed_ms,
        }

    try:
        raw = json.loads(cmd_result.stdout)
    except Exception:
        return {
            "success": False,
            "tool": "iperf3",
            "role": "client",
            "error": "INVALID_IPERF_JSON",
            "execution": execution,
            "command": cmd_result.command,
            "stdout": cmd_result.stdout.strip(),
            "stderr": cmd_result.stderr.strip(),
            "elapsed_ms": cmd_result.elapsed_ms,
        }

    summary = parse_iperf_json(raw, args.protocol)

    return {
        "success": True,
        "tool": "iperf3",
        "role": "client",
        "execution": execution,
        "server": args.server,
        "port": args.port,
        "duration_sec": args.time,
        "parallel": args.parallel,
        "protocol": args.protocol,
        "reverse": bool(args.reverse),
        "bandwidth": args.bandwidth,
        "summary": summary,
        "command": cmd_result.command,
        "elapsed_ms": cmd_result.elapsed_ms,
    }


def build_parser() -> argparse.ArgumentParser:
    parser = argparse.ArgumentParser(description="Generic iperf3 runner")
    sub = parser.add_subparsers(dest="cmd")

    sub.add_parser("check", help="Check iperf3 availability")

    c = sub.add_parser("client", help="Run iperf3 client")
    c.add_argument("--server", required=True)
    c.add_argument("--port", type=int, default=5201)
    c.add_argument("--time", type=int, default=10)
    c.add_argument("--parallel", type=int, default=1)
    c.add_argument("--protocol", choices=["tcp", "udp"], default="tcp")
    c.add_argument("--reverse", action="store_true")
    c.add_argument("--bandwidth", default=None)
    c.add_argument("--udp-length", type=int, default=None)

    c.add_argument("--ssh-host", default=None)
    c.add_argument("--ssh-port", type=int, default=22)
    c.add_argument("--ssh-user", default=None)
    c.add_argument("--ssh-password", default=None)

    return parser


def main() -> int:
    parser = build_parser()
    args = parser.parse_args()

    try:
        if args.cmd == "check":
            result = check_iperf()
        elif args.cmd == "client":
            result = client(args)
        else:
            parser.print_help()
            return 2

        print_json(result)
        return 0 if result.get("success") else 1

    except Exception as exc:
        print_json({
            "success": False,
            "tool": "iperf3",
            "error": type(exc).__name__,
            "message": str(exc),
        })
        return 1


if __name__ == "__main__":
    raise SystemExit(main())
