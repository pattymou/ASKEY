#!/usr/bin/env python3
# -*- coding: utf-8 -*-
from __future__ import annotations
import argparse, json, sys
from dataclasses import dataclass
from pathlib import Path
from typing import Any
PROJECT_ROOT = Path(__file__).resolve().parents[2]
if str(PROJECT_ROOT) not in sys.path: sys.path.insert(0, str(PROJECT_ROOT))
from core.ssh import SSHClient as SSHController
from core.ssh.client import shell_quote
from plugins.amarisoft.verify import extract_define

@dataclass(frozen=True)
class CallboxSettings:
    host: str; port: int; username: str; password: str
    remote_cfg_path: str; ssh_timeout_sec: int; command_timeout_sec: int

def load_settings(path) -> CallboxSettings:
    raw = json.loads(Path(path).read_text(encoding="utf-8")); cb = raw.get("callbox", {})
    return CallboxSettings(str(cb["host"]), int(cb.get("port",22)), str(cb["username"]),
        str(cb.get("password","")), str(cb.get("remote_cfg_path","/root/enb/config/AutoConfig.cfg")),
        int(cb.get("ssh_timeout_sec",30)), int(cb.get("command_timeout_sec",120)))

def parse_cfg(cfg_text: str, cell: int = 1) -> dict[str, Any]:
    earfcn = extract_define(cfg_text, f"LTE_Cell_{cell}_EARFCN_DL")
    rb = extract_define(cfg_text, f"LTE_Cell_{cell}_RB_DL")
    tdd = extract_define(cfg_text, f"LTE_TDD_Cell_{cell}")
    rb_to_bw = {"6":1.4,"15":3,"25":5,"50":10,"75":15,"100":20}
    return {"cell": cell,
            "lte_earfcn_dl": int(earfcn) if earfcn and earfcn.isdigit() else earfcn,
            "rb_dl": int(rb) if rb and rb.isdigit() else rb,
            "bandwidth_mhz_inferred": rb_to_bw.get(str(rb)),
            "tdd": tdd}

def get_status(settings: CallboxSettings) -> dict[str, Any]:
    with SSHController.from_callbox_settings(settings) as ssh:
        service = ssh.execute("service lte status", settings.command_timeout_sec)
        link = ssh.execute("readlink /root/enb/config/enb.cfg", settings.command_timeout_sec)
        cfg = ssh.execute(f"cat {shell_quote(settings.remote_cfg_path)}", settings.command_timeout_sec)
    text = (service.stdout or "") + "\n" + (service.stderr or "")
    running = "active (running)" in text
    cfg_status = parse_cfg(cfg.stdout, 1) if cfg.success else {}
    success = service.success and running and link.success and cfg.success
    return {"success": success,
            "message": "已取得 Amarisoft 狀態。" if success else "取得 Amarisoft 狀態失敗或 LTE service 非 running。",
            "service": {"success": service.success, "active_running": running, "command": service.to_dict()},
            "config": {"success": cfg.success, "remote_cfg_path": settings.remote_cfg_path, **cfg_status},
            "symlink": {"success": link.success, "path": "/root/enb/config/enb.cfg",
                        "target": link.stdout.strip(), "expected_target": "AutoConfig.cfg",
                        "matched": link.stdout.strip() == "AutoConfig.cfg"}}

def main():
    p = argparse.ArgumentParser(); p.add_argument("--settings", default="callbox_settings.json"); args = p.parse_args()
    try:
        result = get_status(load_settings(args.settings))
        print(json.dumps(result, ensure_ascii=False, indent=2)); return 0 if result.get("success") else 1
    except Exception as exc:
        print(json.dumps({"success": False, "error": str(exc), "message": "取得 Amarisoft 狀態失敗。"}, ensure_ascii=False, indent=2)); return 1
if __name__ == "__main__": raise SystemExit(main())
