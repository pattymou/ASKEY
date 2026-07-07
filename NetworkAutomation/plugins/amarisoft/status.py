#!/usr/bin/env python3
# -*- coding: utf-8 -*-
from __future__ import annotations

import argparse
import json
import sys
from dataclasses import dataclass
from pathlib import Path
from typing import Any

PROJECT_ROOT = Path(__file__).resolve().parents[2]
if str(PROJECT_ROOT) not in sys.path:
    sys.path.insert(0, str(PROJECT_ROOT))

from core.ssh import SSHClient as SSHController
from plugins.amarisoft.state import load_callbox_state


@dataclass(frozen=True)
class CallboxSettings:
    host: str
    port: int
    username: str
    password: str
    remote_cfg_path: str
    ssh_timeout_sec: int
    command_timeout_sec: int


def load_settings(path: str | Path) -> CallboxSettings:
    raw = json.loads(Path(path).read_text(encoding="utf-8"))
    cb = raw.get("callbox", {})

    return CallboxSettings(
        host=str(cb["host"]),
        port=int(cb.get("port", 22)),
        username=str(cb["username"]),
        password=str(cb.get("password", "")),
        remote_cfg_path=str(cb.get("remote_cfg_path", "/root/enb/config/AutoConfig.cfg")),
        ssh_timeout_sec=int(cb.get("ssh_timeout_sec", 30)),
        command_timeout_sec=int(cb.get("command_timeout_sec", 120)),
    )


def get_service_status(settings: CallboxSettings) -> dict[str, Any]:
    with SSHController.from_callbox_settings(settings) as ssh:
        service = ssh.execute("service lte status", settings.command_timeout_sec)
        link = ssh.execute("readlink /root/enb/config/enb.cfg", settings.command_timeout_sec)
        cfg_exists = ssh.execute(f"test -f {settings.remote_cfg_path} && echo EXISTS || echo MISSING", settings.command_timeout_sec)

    service_text = (service.stdout or "") + "\n" + (service.stderr or "")
    active_running = "active (running)" in service_text

    return {
        "success": service.success and active_running and link.success,
        "service": {
            "success": service.success,
            "active_running": active_running,
            "command": service.to_dict(),
        },
        "symlink": {
            "success": link.success,
            "path": "/root/enb/config/enb.cfg",
            "target": link.stdout.strip(),
            "expected_target": "AutoConfig.cfg",
            "matched": link.stdout.strip() == "AutoConfig.cfg",
        },
        "remote_cfg": {
            "success": cfg_exists.success and "EXISTS" in (cfg_exists.stdout or ""),
            "path": settings.remote_cfg_path,
            "exists": "EXISTS" in (cfg_exists.stdout or ""),
            "command": cfg_exists.to_dict(),
        },
    }


def get_status(settings: CallboxSettings) -> dict[str, Any]:
    service_status = get_service_status(settings)
    state_status = load_callbox_state()
    state = state_status.get("state") or {}

    success = (
        service_status.get("success")
        and service_status.get("remote_cfg", {}).get("success")
        and service_status.get("symlink", {}).get("matched")
        and state_status.get("success")
    )

    return {
        "success": bool(success),
        "message": (
            "已確認 Amarisoft LTE service running，enb.cfg 指向 AutoConfig.cfg，並讀取到最後成功套用狀態。"
            if success else
            "取得 Amarisoft 狀態失敗，或尚未有成功套用狀態。"
        ),
        "service": service_status.get("service"),
        "symlink": service_status.get("symlink"),
        "remote_cfg": service_status.get("remote_cfg"),
        "state": state_status,
        "current": {
            "mode": state.get("mode"),
            "cell": state.get("cell"),
            "band": state.get("band"),
            "bandwidth_mhz": state.get("bandwidth_mhz"),
            "dl_earfcn": state.get("dl_earfcn"),
            "rb_dl": state.get("rb_dl"),
            "remote_cfg_path": state.get("remote_cfg_path"),
            "updated_at": state.get("updated_at"),
        } if state else None,
    }


def main() -> int:
    parser = argparse.ArgumentParser(description="Get Amarisoft status from service + state")
    parser.add_argument("--settings", default="callbox_settings.json")
    args = parser.parse_args()

    try:
        result = get_status(load_settings(args.settings))
        print(json.dumps(result, ensure_ascii=False, indent=2))
        return 0 if result.get("success") else 1

    except Exception as exc:
        print(json.dumps({
            "success": False,
            "error": str(exc),
            "message": "取得 Amarisoft 狀態失敗。"
        }, ensure_ascii=False, indent=2))
        return 1


if __name__ == "__main__":
    raise SystemExit(main())
