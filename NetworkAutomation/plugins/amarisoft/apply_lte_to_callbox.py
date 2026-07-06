#!/usr/bin/env python3
# -*- coding: utf-8 -*-
"""
apply_lte_to_callbox.py

Generate an Amarisoft LTE config by calling lte_config_modifier.py, then upload it
to the Callbox and restart LTE service by SSH.

V4 change:
- SSH/SFTP logic is moved to controller/ssh_controller.py.
- stdout is reserved for final JSON only, so OpenClaw / MCP can parse it safely.
- progress logs go to stderr.

Example:
    python apply_lte_to_callbox.py --settings C:\\CallboxAgent\\callbox_settings.json --cell 1 --band 5 --bandwidth 10

Dry run only:
    python apply_lte_to_callbox.py --settings C:\\CallboxAgent\\callbox_settings.json --cell 1 --band 5 --bandwidth 10 --dry-run
"""

from __future__ import annotations

import argparse
import json
import os
import subprocess
import sys
from pathlib import Path
# Allow this plugin script to be executed directly while importing project core modules.
PROJECT_ROOT = Path(__file__).resolve().parents[2]
if str(PROJECT_ROOT) not in sys.path:
    sys.path.insert(0, str(PROJECT_ROOT))
from dataclasses import dataclass
from typing import Any

from core.ssh import SSHClient as SSHController


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


def log(message: str) -> None:
    print(message, file=sys.stderr)


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

    log("[1/4] Generating LTE config...")
    log("Command: " + " ".join(cmd))

    proc = subprocess.run(cmd, text=True, capture_output=True)
    if proc.stdout.strip():
        log(proc.stdout.strip())
    if proc.stderr.strip():
        log(proc.stderr.strip())

    if proc.returncode != 0:
        raise RuntimeError(f"lte_config_modifier.py failed with exit code {proc.returncode}")

    try:
        result = json.loads(proc.stdout)
    except Exception:
        result = {"success": True, "output_cfg": str(output_path)}

    result.setdefault("output_cfg", str(output_path))
    return result


def remote_backup_and_upload(settings: Settings, local_cfg: Path) -> dict[str, Any]:
    callbox = settings.callbox
    log("[2/4] Connecting to Callbox...")

    with SSHController.from_callbox_settings(callbox) as ssh:
        log("[3/4] Backing up remote config...")
        backup_result = ssh.backup_file(callbox.remote_cfg_path, callbox.remote_backup_dir)
        if backup_result.get("warning"):
            log(str(backup_result["warning"]))
        log(f"Remote backup path: {backup_result['backup_path']}")

        log("Uploading new config...")
        upload_result = ssh.upload(local_cfg, callbox.remote_cfg_path)
        log(f"Uploaded: {local_cfg} -> {callbox.username}@{callbox.host}:{callbox.remote_cfg_path}")

    return {
        "success": True,
        "backup": backup_result,
        "upload": upload_result,
    }


def restart_callbox_lte(settings: Settings) -> dict[str, Any]:
    commands = settings.callbox.restart_commands
    if not commands:
        log("[4/4] No restart commands configured. Skip restart.")
        return {"success": True, "skipped": True, "commands": []}

    log("[4/4] Restarting Callbox LTE...")
    with SSHController.from_callbox_settings(settings.callbox) as ssh:
        command_results = []
        for command in commands:
            log(f"Remote command: {command}")
            result = ssh.execute(command, settings.callbox.command_timeout_sec)
            command_results.append(result.to_dict())
            if result.stdout.strip():
                log(result.stdout.strip())
            if result.stderr.strip():
                log(result.stderr.strip())
            if not result.success:
                raise RuntimeError(f"restart command failed, exit code {result.exit_code}: {command}")

    return {"success": True, "skipped": False, "commands": command_results}


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

        modifier_result = generate_lte_config(
            settings=settings,
            cell=args.cell,
            band=args.band,
            bandwidth=args.bandwidth,
            dl_earfcn=args.dl_earfcn,
            output_path=output_path,
        )

        result: dict[str, Any] = {
            "success": True,
            "action": "apply_lte_to_callbox",
            "cell": args.cell,
            "band": args.band,
            "bandwidth": args.bandwidth,
            "dl_earfcn": args.dl_earfcn,
            "generated_cfg": str(output_path),
            "modifier": modifier_result,
            "dry_run": bool(args.dry_run),
            "upload": None,
            "restart": None,
        }

        if args.dry_run:
            log("Dry run enabled. Skip upload and restart.")
            print(json.dumps(result, ensure_ascii=False, indent=2))
            return 0

        result["upload"] = remote_backup_and_upload(settings, output_path)

        if args.no_restart:
            log("--no-restart enabled. Skip restart.")
            result["restart"] = {"success": True, "skipped": True, "reason": "--no-restart"}
            print(json.dumps(result, ensure_ascii=False, indent=2))
            return 0

        result["restart"] = restart_callbox_lte(settings)

        log("Done. LTE config was generated, uploaded, and restart commands were executed.")
        print(json.dumps(result, ensure_ascii=False, indent=2))
        return 0

    except Exception as exc:
        print(json.dumps({"success": False, "error": str(exc)}, ensure_ascii=False, indent=2))
        return 1


if __name__ == "__main__":
    raise SystemExit(main())
