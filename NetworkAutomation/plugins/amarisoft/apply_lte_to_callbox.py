#!/usr/bin/env python3
# -*- coding: utf-8 -*-
from __future__ import annotations

import argparse
import json
import os
import subprocess
import sys
from dataclasses import dataclass
from pathlib import Path
from typing import Any

PROJECT_ROOT = Path(__file__).resolve().parents[2]
if str(PROJECT_ROOT) not in sys.path:
    sys.path.insert(0, str(PROJECT_ROOT))

from core.ssh import SSHClient as SSHController
from plugins.amarisoft.verify import verify_remote_cfg_exists, verify_service, verify_symlink
from plugins.amarisoft.state import save_callbox_state


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
    verify_commands: list[str]
    ssh_timeout_sec: int
    command_timeout_sec: int


@dataclass(frozen=True)
class Settings:
    local: LocalSettings
    callbox: CallboxSettings


def log(msg: str) -> None:
    print(msg, file=sys.stderr)


def expand_path(value: str, base_dir: Path | None = None) -> Path:
    p = Path(os.path.expandvars(os.path.expanduser(value)))
    if not p.is_absolute() and base_dir is not None:
        p = base_dir / p
    return p


def default_restart_commands() -> list[str]:
    return [
        "cd /root/enb/config && ln -sfn AutoConfig.cfg enb.cfg",
        "service lte restart",
    ]


def load_settings(path: str | Path) -> Settings:
    settings_path = Path(path)
    raw = json.loads(settings_path.read_text(encoding="utf-8"))
    lr = raw.get("local", {})
    cr = raw.get("callbox", {})

    base_dir = expand_path(lr.get("base_dir"), settings_path.parent) if lr.get("base_dir") else settings_path.parent

    local = LocalSettings(
        modifier_py=expand_path(lr["modifier_py"], base_dir),
        input_cfg=expand_path(lr["input_cfg"], base_dir),
        earfcn_json=expand_path(lr["earfcn_json"], base_dir),
        output_dir=expand_path(lr.get("output_dir", "generated"), base_dir),
        output_pattern=str(lr.get("output_pattern", "AutoConfig.cfg")),
    )

    callbox = CallboxSettings(
        host=str(cr["host"]),
        port=int(cr.get("port", 22)),
        username=str(cr["username"]),
        password=str(cr.get("password", "")),
        remote_cfg_path=str(cr.get("remote_cfg_path", "/root/enb/config/AutoConfig.cfg")),
        remote_backup_dir=str(cr.get("remote_backup_dir", "/root/enb/config/backup")),
        restart_commands=[str(x) for x in cr.get("restart_commands", [])] or default_restart_commands(),
        verify_commands=[str(x) for x in cr.get("verify_commands", ["service lte status"])],
        ssh_timeout_sec=int(cr.get("ssh_timeout_sec", 30)),
        command_timeout_sec=int(cr.get("command_timeout_sec", 120)),
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
    return settings.local.output_dir / settings.local.output_pattern.format(
        cell=cell,
        band=band,
        bandwidth=bandwidth or "auto",
    )


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
        "--cfg", str(settings.local.input_cfg),
        "--earfcn-json", str(settings.local.earfcn_json),
        "--cell", str(cell),
        "--band", str(band),
        "--output", str(output_path),
    ]

    if bandwidth is not None:
        cmd += ["--bandwidth", str(bandwidth)]

    if dl_earfcn is not None:
        cmd += ["--dl-earfcn", str(dl_earfcn)]

    log("[1/6] Generating LTE config...")
    p = subprocess.run(cmd, capture_output=True, text=True)

    if p.stderr.strip():
        log(p.stderr.strip())

    if p.returncode != 0:
        if p.stdout.strip():
            log(p.stdout.strip())
        raise RuntimeError(f"lte_config_modifier.py failed with exit code {p.returncode}")

    try:
        result = json.loads(p.stdout)
    except Exception:
        result = {
            "success": True,
            "output_cfg": str(output_path),
        }

    result.setdefault("output_cfg", str(output_path))
    return result


def remote_apply_and_verify(
    settings: Settings,
    local_cfg: Path,
    no_restart: bool,
) -> dict[str, Any]:
    cb = settings.callbox

    with SSHController.from_callbox_settings(cb) as ssh:
        log("[2/6] Backing up remote AutoConfig.cfg...")
        backup = ssh.backup_file(cb.remote_cfg_path, cb.remote_backup_dir)

        log("[3/6] Uploading generated cfg as remote AutoConfig.cfg...")
        upload = ssh.upload(local_cfg, cb.remote_cfg_path)

        if no_restart:
            restart = {
                "success": True,
                "skipped": True,
                "reason": "--no-restart",
            }
        else:
            log("[4/6] Applying enb.cfg link and restarting LTE...")
            command_results = []
            for command in cb.restart_commands:
                r = ssh.execute(command, cb.command_timeout_sec)
                command_results.append(r.to_dict())
                if not r.success:
                    raise RuntimeError(f"restart command failed, exit code {r.exit_code}: {command}")
            restart = {
                "success": True,
                "skipped": False,
                "commands": command_results,
            }

        log("[5/6] Verifying cfg exists, symlink, and service...")
        cfg_verify = verify_remote_cfg_exists(ssh, cb.remote_cfg_path)
        symlink_verify = verify_symlink(ssh, "/root/enb/config/enb.cfg", "AutoConfig.cfg")
        service_verify = verify_service(ssh, cb.verify_commands, cb.command_timeout_sec)

    verify_success = (
        bool(cfg_verify.get("success"))
        and bool(symlink_verify.get("success"))
        and bool(service_verify.get("success"))
    )

    return {
        "success": bool(restart.get("success")) and verify_success,
        "backup": backup,
        "upload": upload,
        "restart": restart,
        "verify": {
            "success": verify_success,
            "cfg_exists": cfg_verify,
            "symlink": symlink_verify,
            "service": service_verify,
        },
    }


def build_message(result: dict[str, Any]) -> str:
    m = result.get("modifier", {})
    cell = result.get("cell")
    band = result.get("band")
    bandwidth = m.get("bandwidth_mhz", result.get("bandwidth"))
    earfcn = m.get("dl_earfcn", result.get("dl_earfcn"))
    rb = m.get("rb_dl")

    if result.get("dry_run"):
        return f"Dry-run 完成：已產生 Cell{cell} Band{band} 設定檔，尚未上傳與重啟。"

    if result.get("success"):
        return (
            f"切 Band 完成，已確認設定已套用："
            f"Cell{cell}, Band{band}, Bandwidth={bandwidth}MHz, "
            f"DL_EARFCN={earfcn}, RB_DL={rb}；"
            f"AutoConfig.cfg 已上傳，enb.cfg 已指向 AutoConfig.cfg，LTE service 已啟動。"
        )

    return "切 Band 流程未完全成功，請查看 error / verify 欄位。"


def main() -> int:
    parser = argparse.ArgumentParser(description="Apply Amarisoft LTE config, verify, and update state")
    parser.add_argument("--settings", default="callbox_settings.json")
    parser.add_argument("--cell", type=int, default=1)
    parser.add_argument("--band", type=int, required=True)
    parser.add_argument("--bandwidth", type=float, default=None)
    parser.add_argument("--dl-earfcn", type=int, default=None)
    parser.add_argument("--dry-run", action="store_true")
    parser.add_argument("--no-restart", action="store_true")
    args = parser.parse_args()

    try:
        settings = load_settings(args.settings)
        ensure_local_files(settings)

        output_path = build_output_path(settings, args.cell, args.band, args.bandwidth)

        modifier = generate_lte_config(
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
            "mode": "LTE",
            "cell": args.cell,
            "band": args.band,
            "bandwidth": args.bandwidth,
            "dl_earfcn": args.dl_earfcn,
            "generated_cfg": str(output_path),
            "remote_cfg_path": settings.callbox.remote_cfg_path,
            "modifier": modifier,
            "dry_run": bool(args.dry_run),
            "upload": None,
            "restart": None,
            "verify": None,
            "state": None,
            "message": "",
        }

        if args.dry_run:
            result["message"] = build_message(result)
            print(json.dumps(result, ensure_ascii=False, indent=2))
            return 0

        remote = remote_apply_and_verify(settings, output_path, bool(args.no_restart))

        result["upload"] = {
            "success": bool(remote["upload"].get("success")),
            "backup": remote["backup"],
            "upload": remote["upload"],
        }
        result["restart"] = remote["restart"]
        result["verify"] = remote["verify"]
        result["success"] = bool(remote["success"])

        if result["success"]:
            log("[6/6] Saving callbox state...")
            state_result = save_callbox_state({
                "mode": "LTE",
                "cell": args.cell,
                "band": args.band,
                "bandwidth_mhz": modifier.get("bandwidth_mhz", args.bandwidth),
                "dl_earfcn": modifier.get("dl_earfcn", args.dl_earfcn),
                "rb_dl": modifier.get("rb_dl"),
                "remote_cfg_path": settings.callbox.remote_cfg_path,
                "generated_cfg": str(output_path),
                "verify": {
                    "cfg_exists": remote["verify"]["cfg_exists"].get("success"),
                    "symlink": remote["verify"]["symlink"].get("success"),
                    "service": remote["verify"]["service"].get("success"),
                    "service_running": remote["verify"]["service"].get("active_running"),
                },
            })
            result["state"] = state_result

        result["message"] = build_message(result)

        print(json.dumps(result, ensure_ascii=False, indent=2))
        return 0 if result["success"] else 1

    except Exception as exc:
        print(json.dumps({
            "success": False,
            "error": str(exc),
            "message": "切 Band 失敗，請查看 error。"
        }, ensure_ascii=False, indent=2))
        return 1


if __name__ == "__main__":
    raise SystemExit(main())
