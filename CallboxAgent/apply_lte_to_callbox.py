#!/usr/bin/env python3
# -*- coding: utf-8 -*-
"""
apply_lte_to_callbox.py

Generate an Amarisoft LTE config by calling lte_config_modifier.py, then upload it
to the Callbox and restart LTE service by SSH.

Important:
- Callbox IP / username / password / remote paths are read from callbox_settings.json.
- They are not hardcoded in this Python file.
- LTE band / EARFCN / bandwidth validation is still handled by lte_config_modifier.py.

Example:
    python apply_lte_to_callbox.py --settings C:\CallboxAgent\callbox_settings.json --cell 1 --band 5 --bandwidth 10

Dry run only:
    python apply_lte_to_callbox.py --settings C:\CallboxAgent\callbox_settings.json --cell 1 --band 5 --bandwidth 10 --dry-run
"""

from __future__ import annotations

import argparse
import json
import os
import posixpath
import subprocess
import sys
from dataclasses import dataclass
from datetime import datetime
from pathlib import Path
from typing import Any


@dataclass(frozen=True)
class LocalSettings:
    modifier_py: Path
    input_cfg: Path
    earfcn_json: Path
    output_dir: Path
    output_pattern: str


@dataclass(frozen=True)
class CallboxSettings:
    host: str
    port: int
    username: str
    password: str
    remote_cfg_path: str
    remote_backup_dir: str
    restart_commands: list[str]
    ssh_timeout_sec: int
    command_timeout_sec: int


@dataclass(frozen=True)
class Settings:
    local: LocalSettings
    callbox: CallboxSettings


def expand_path(value: str, base_dir: Path | None = None) -> Path:
    expanded = os.path.expandvars(os.path.expanduser(value))
    p = Path(expanded)
    if not p.is_absolute() and base_dir is not None:
        p = base_dir / p
    return p


def load_settings(path: str | Path) -> Settings:
    settings_path = Path(path)
    if not settings_path.exists():
        raise FileNotFoundError(f"settings file not found: {settings_path}")

    raw = json.loads(settings_path.read_text(encoding="utf-8"))
    local_raw: dict[str, Any] = raw.get("local", {})
    callbox_raw: dict[str, Any] = raw.get("callbox", {})

    base_dir_value = local_raw.get("base_dir")
    base_dir = expand_path(base_dir_value) if base_dir_value else settings_path.parent

    local = LocalSettings(
        modifier_py=expand_path(local_raw["modifier_py"], base_dir),
        input_cfg=expand_path(local_raw["input_cfg"], base_dir),
        earfcn_json=expand_path(local_raw["earfcn_json"], base_dir),
        output_dir=expand_path(local_raw.get("output_dir", "generated"), base_dir),
        output_pattern=str(local_raw.get("output_pattern", "AutoConfig_LTE_B{band}_Cell{cell}.cfg")),
    )

    callbox = CallboxSettings(
        host=str(callbox_raw["host"]),
        port=int(callbox_raw.get("port", 22)),
        username=str(callbox_raw["username"]),
        password=str(callbox_raw.get("password", "")),
        remote_cfg_path=str(callbox_raw["remote_cfg_path"]),
        remote_backup_dir=str(callbox_raw.get("remote_backup_dir", "/root/enb/config/backup")),
        restart_commands=[str(x) for x in callbox_raw.get("restart_commands", [])],
        ssh_timeout_sec=int(callbox_raw.get("ssh_timeout_sec", 30)),
        command_timeout_sec=int(callbox_raw.get("command_timeout_sec", 120)),
    )

    return Settings(local=local, callbox=callbox)


def ensure_local_files(settings: Settings) -> None:
    missing = []
    for label, path in [
        ("modifier_py", settings.local.modifier_py),
        ("input_cfg", settings.local.input_cfg),
        ("earfcn_json", settings.local.earfcn_json),
    ]:
        if not path.exists():
            missing.append(f"{label}: {path}")

    if missing:
        raise FileNotFoundError("Missing required local files:\n" + "\n".join(missing))


def build_output_path(settings: Settings, cell: int, band: int, bandwidth: float | None) -> Path:
    name = settings.local.output_pattern.format(cell=cell, band=band, bandwidth=bandwidth or "auto")
    return settings.local.output_dir / name


def generate_lte_config(
    settings: Settings,
    cell: int,
    band: int,
    bandwidth: float | None,
    dl_earfcn: int | None,
    output_path: Path,
) -> dict[str, Any]:
    output_path.parent.mkdir(parents=True, exist_ok=True)

    cmd = [
        sys.executable,
        str(settings.local.modifier_py),
        "--cfg",
        str(settings.local.input_cfg),
        "--earfcn-json",
        str(settings.local.earfcn_json),
        "--cell",
        str(cell),
        "--band",
        str(band),
        "--output",
        str(output_path),
    ]

    if bandwidth is not None:
        cmd.extend(["--bandwidth", str(bandwidth)])

    if dl_earfcn is not None:
        cmd.extend(["--dl-earfcn", str(dl_earfcn)])

    print("[1/4] Generating LTE config...")
    print("Command:", " ".join(cmd))

    proc = subprocess.run(cmd, text=True, capture_output=True)
    if proc.stdout.strip():
        print(proc.stdout.strip())
    if proc.stderr.strip():
        print(proc.stderr.strip(), file=sys.stderr)

    if proc.returncode != 0:
        raise RuntimeError(f"lte_config_modifier.py failed with exit code {proc.returncode}")

    try:
        return json.loads(proc.stdout)
    except Exception:
        return {"success": True, "output_cfg": str(output_path)}


def import_paramiko():
    try:
        import paramiko  # type: ignore
        return paramiko
    except ModuleNotFoundError as exc:
        raise RuntimeError(
            "Python package 'paramiko' is required for SSH/SFTP.\n"
            "Install it on the Callbox control PC with:\n"
            "    python -m pip install paramiko"
        ) from exc


def connect_ssh(settings: CallboxSettings):
    paramiko = import_paramiko()
    client = paramiko.SSHClient()
    client.set_missing_host_key_policy(paramiko.AutoAddPolicy())
    client.connect(
        hostname=settings.host,
        port=settings.port,
        username=settings.username,
        password=settings.password,
        timeout=settings.ssh_timeout_sec,
        look_for_keys=False,
        allow_agent=False,
    )
    return client


def run_remote_command(ssh, command: str, timeout_sec: int) -> tuple[int, str, str]:
    stdin, stdout, stderr = ssh.exec_command(command, timeout=timeout_sec)
    exit_code = stdout.channel.recv_exit_status()
    out = stdout.read().decode("utf-8", errors="replace")
    err = stderr.read().decode("utf-8", errors="replace")
    return exit_code, out, err


def remote_backup_and_upload(settings: Settings, local_cfg: Path) -> None:
    callbox = settings.callbox
    print("[2/4] Connecting to Callbox...")
    ssh = connect_ssh(callbox)

    try:
        timestamp = datetime.now().strftime("%Y%m%d_%H%M%S")
        remote_name = posixpath.basename(callbox.remote_cfg_path)
        remote_backup_path = posixpath.join(callbox.remote_backup_dir, f"{remote_name}.{timestamp}.bak")

        backup_cmd = (
            f"mkdir -p {shell_quote(callbox.remote_backup_dir)} && "
            f"if [ -f {shell_quote(callbox.remote_cfg_path)} ]; then "
            f"cp {shell_quote(callbox.remote_cfg_path)} {shell_quote(remote_backup_path)}; "
            f"else echo 'WARN: remote cfg not found, skip backup'; fi"
        )

        print("[3/4] Backing up remote config...")
        code, out, err = run_remote_command(ssh, backup_cmd, callbox.command_timeout_sec)
        if out.strip():
            print(out.strip())
        if err.strip():
            print(err.strip(), file=sys.stderr)
        if code != 0:
            raise RuntimeError(f"remote backup failed, exit code {code}")

        print(f"Remote backup path: {remote_backup_path}")

        remote_dir = posixpath.dirname(callbox.remote_cfg_path)
        mkdir_cmd = f"mkdir -p {shell_quote(remote_dir)}"
        code, out, err = run_remote_command(ssh, mkdir_cmd, callbox.command_timeout_sec)
        if code != 0:
            raise RuntimeError(f"remote mkdir failed, exit code {code}: {err}")

        print("Uploading new config...")
        sftp = ssh.open_sftp()
        try:
            sftp.put(str(local_cfg), callbox.remote_cfg_path)
        finally:
            sftp.close()

        print(f"Uploaded: {local_cfg} -> {callbox.username}@{callbox.host}:{callbox.remote_cfg_path}")

    finally:
        ssh.close()


def restart_callbox_lte(settings: Settings) -> None:
    commands = settings.callbox.restart_commands
    if not commands:
        print("[4/4] No restart commands configured. Skip restart.")
        return

    print("[4/4] Restarting Callbox LTE...")
    ssh = connect_ssh(settings.callbox)
    try:
        for command in commands:
            print(f"Remote command: {command}")
            code, out, err = run_remote_command(ssh, command, settings.callbox.command_timeout_sec)
            if out.strip():
                print(out.strip())
            if err.strip():
                print(err.strip(), file=sys.stderr)
            if code != 0:
                raise RuntimeError(f"restart command failed, exit code {code}: {command}")
    finally:
        ssh.close()


def shell_quote(value: str) -> str:
    return "'" + value.replace("'", "'\\''") + "'"


def build_arg_parser() -> argparse.ArgumentParser:
    parser = argparse.ArgumentParser(description="Generate LTE cfg, upload it to Amarisoft Callbox, and restart LTE.")
    parser.add_argument("--settings", default="callbox_settings.json", help="Path to callbox_settings.json")
    parser.add_argument("--cell", type=int, default=1, help="LTE cell index, 1~8. Default: 1")
    parser.add_argument("--band", type=int, required=True, help="LTE Band, e.g. 5")
    parser.add_argument("--bandwidth", type=float, default=None, help="LTE bandwidth MHz, e.g. 10. Default: max supported bandwidth from JSON")
    parser.add_argument("--dl-earfcn", type=int, default=None, help="Optional DL EARFCN. Default: Earfcn_Middle from JSON")
    parser.add_argument("--dry-run", action="store_true", help="Only generate local cfg. Do not SSH/SFTP/restart Callbox")
    parser.add_argument("--no-restart", action="store_true", help="Upload cfg but do not run restart commands")
    return parser


def main() -> int:
    parser = build_arg_parser()
    args = parser.parse_args()

    try:
        settings = load_settings(args.settings)
        ensure_local_files(settings)
        output_path = build_output_path(settings, args.cell, args.band, args.bandwidth)

        result = generate_lte_config(
            settings=settings,
            cell=args.cell,
            band=args.band,
            bandwidth=args.bandwidth,
            dl_earfcn=args.dl_earfcn,
            output_path=output_path,
        )

        if args.dry_run:
            print("Dry run enabled. Skip upload and restart.")
            print(f"Generated config: {output_path}")
            return 0

        remote_backup_and_upload(settings, output_path)

        if args.no_restart:
            print("--no-restart enabled. Skip restart.")
            return 0

        restart_callbox_lte(settings)

        print("Done. LTE config was generated, uploaded, and restart commands were executed.")
        print(json.dumps(result, ensure_ascii=False, indent=2))
        return 0

    except Exception as exc:
        print(json.dumps({"success": False, "error": str(exc)}, ensure_ascii=False, indent=2), file=sys.stderr)
        return 1


if __name__ == "__main__":
    raise SystemExit(main())
