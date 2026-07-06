#!/usr/bin/env python3
# -*- coding: utf-8 -*-
from __future__ import annotations
import argparse, json, os, subprocess, sys
from dataclasses import dataclass
from pathlib import Path
from typing import Any
PROJECT_ROOT = Path(__file__).resolve().parents[2]
if str(PROJECT_ROOT) not in sys.path: sys.path.insert(0, str(PROJECT_ROOT))
from core.ssh import SSHClient as SSHController
from plugins.amarisoft.verify import verify_remote_cfg, verify_service, verify_symlink
from plugins.amarisoft.status import get_status, load_settings as load_status_settings

@dataclass(frozen=True)
class LocalSettings:
    modifier_py: Path; input_cfg: Path; earfcn_json: Path; output_dir: Path; output_pattern: str
@dataclass(frozen=True)
class CallboxSettings:
    host: str; port: int; username: str; password: str; remote_cfg_path: str; remote_backup_dir: str
    restart_commands: list[str]; verify_commands: list[str]; ssh_timeout_sec: int; command_timeout_sec: int
@dataclass(frozen=True)
class Settings:
    local: LocalSettings; callbox: CallboxSettings

def log(msg): print(msg, file=sys.stderr)
def expand_path(value, base_dir=None):
    p = Path(os.path.expandvars(os.path.expanduser(value)))
    return base_dir / p if base_dir is not None and not p.is_absolute() else p
def default_restart_commands(): return ["cd /root/enb/config && ln -sfn AutoConfig.cfg enb.cfg", "service lte restart"]

def load_settings(path) -> Settings:
    settings_path = Path(path); raw=json.loads(settings_path.read_text(encoding="utf-8"))
    lr, cr = raw.get("local",{}), raw.get("callbox",{})
    base_dir = expand_path(lr.get("base_dir"), settings_path.parent) if lr.get("base_dir") else settings_path.parent
    local = LocalSettings(expand_path(lr["modifier_py"], base_dir), expand_path(lr["input_cfg"], base_dir),
        expand_path(lr["earfcn_json"], base_dir), expand_path(lr.get("output_dir","generated"), base_dir),
        str(lr.get("output_pattern","AutoConfig.cfg")))
    callbox = CallboxSettings(str(cr["host"]), int(cr.get("port",22)), str(cr["username"]), str(cr.get("password","")),
        str(cr.get("remote_cfg_path","/root/enb/config/AutoConfig.cfg")),
        str(cr.get("remote_backup_dir","/root/enb/config/backup")),
        [str(x) for x in cr.get("restart_commands", [])] or default_restart_commands(),
        [str(x) for x in cr.get("verify_commands", ["service lte status"])],
        int(cr.get("ssh_timeout_sec",30)), int(cr.get("command_timeout_sec",120)))
    return Settings(local, callbox)

def ensure_local_files(settings):
    missing=[f"{label}: {path}" for label,path in [("modifier_py",settings.local.modifier_py),("input_cfg",settings.local.input_cfg),("earfcn_json",settings.local.earfcn_json)] if not path.exists()]
    if missing: raise FileNotFoundError("Missing required local files:\n" + "\n".join(missing))

def build_output_path(settings, cell, band, bandwidth):
    return settings.local.output_dir / settings.local.output_pattern.format(cell=cell, band=band, bandwidth=bandwidth or "auto")

def generate_lte_config(settings, cell, band, bandwidth, dl_earfcn, output_path):
    output_path.parent.mkdir(parents=True, exist_ok=True)
    cmd=[sys.executable, str(settings.local.modifier_py), "--cfg", str(settings.local.input_cfg), "--earfcn-json", str(settings.local.earfcn_json), "--cell", str(cell), "--band", str(band), "--output", str(output_path)]
    if bandwidth is not None: cmd += ["--bandwidth", str(bandwidth)]
    if dl_earfcn is not None: cmd += ["--dl-earfcn", str(dl_earfcn)]
    log("[1/6] Generating LTE config...")
    p=subprocess.run(cmd, capture_output=True, text=True)
    if p.stderr.strip(): log(p.stderr.strip())
    if p.returncode != 0:
        if p.stdout.strip(): log(p.stdout.strip())
        raise RuntimeError(f"lte_config_modifier.py failed with exit code {p.returncode}")
    try: result=json.loads(p.stdout)
    except Exception: result={"success": True, "output_cfg": str(output_path)}
    result.setdefault("output_cfg", str(output_path)); return result

def remote_apply_and_verify(settings, local_cfg, modifier_result, no_restart):
    cb=settings.callbox
    with SSHController.from_callbox_settings(cb) as ssh:
        log("[2/6] Backing up remote AutoConfig.cfg...")
        backup=ssh.backup_file(cb.remote_cfg_path, cb.remote_backup_dir)
        log("[3/6] Uploading generated cfg as remote AutoConfig.cfg...")
        upload=ssh.upload(local_cfg, cb.remote_cfg_path)
        if no_restart: restart={"success": True, "skipped": True, "reason": "--no-restart"}
        else:
            log("[4/6] Applying enb.cfg link and restarting LTE...")
            res=[]
            for command in cb.restart_commands:
                r=ssh.execute(command, cb.command_timeout_sec); res.append(r.to_dict())
                if not r.success: raise RuntimeError(f"restart command failed, exit code {r.exit_code}: {command}")
            restart={"success": True, "skipped": False, "commands": res}
        log("[5/6] Verifying remote cfg, symlink, and service...")
        cfg_v=verify_remote_cfg(ssh, cb.remote_cfg_path, modifier_result)
        link_v=verify_symlink(ssh, "/root/enb/config/enb.cfg", "AutoConfig.cfg")
        svc_v=verify_service(ssh, cb.verify_commands, cb.command_timeout_sec)
    ok=bool(cfg_v.get("success")) and bool(link_v.get("success")) and bool(svc_v.get("success"))
    return {"success": bool(restart.get("success")) and ok, "backup": backup, "upload": upload, "restart": restart, "verify": {"success": ok, "cfg": cfg_v, "symlink": link_v, "service": svc_v}}

def build_message(result):
    m=result.get("modifier",{}); status=result.get("status") or {}
    if result.get("dry_run"): return f"Dry-run 完成：已產生 Cell{result.get('cell')} Band{result.get('band')} 設定檔，尚未上傳與重啟。"
    if result.get("success"):
        return f"切 Band 完成，已確認遠端 AutoConfig.cfg 已套用：Cell{result.get('cell')}, Band{result.get('band')}, Bandwidth={m.get('bandwidth_mhz', result.get('bandwidth'))}MHz, DL_EARFCN={m.get('dl_earfcn', result.get('dl_earfcn'))}, RB_DL={m.get('rb_dl')}；enb.cfg 已指向 AutoConfig.cfg，LTE service running={status.get('service',{}).get('active_running')}。"
    return "切 Band 流程未完全成功，請查看 error / verify 欄位。"

def main():
    p=argparse.ArgumentParser(); p.add_argument("--settings", default="callbox_settings.json"); p.add_argument("--cell", type=int, default=1); p.add_argument("--band", type=int, required=True); p.add_argument("--bandwidth", type=float); p.add_argument("--dl-earfcn", type=int); p.add_argument("--dry-run", action="store_true"); p.add_argument("--no-restart", action="store_true"); args=p.parse_args()
    try:
        settings=load_settings(args.settings); ensure_local_files(settings)
        out=build_output_path(settings,args.cell,args.band,args.bandwidth)
        mod=generate_lte_config(settings,args.cell,args.band,args.bandwidth,args.dl_earfcn,out)
        result={"success": True,"action":"apply_lte_to_callbox","cell":args.cell,"band":args.band,"bandwidth":args.bandwidth,"dl_earfcn":args.dl_earfcn,"generated_cfg":str(out),"remote_cfg_path":settings.callbox.remote_cfg_path,"modifier":mod,"dry_run":bool(args.dry_run),"upload":None,"restart":None,"verify":None,"status":None,"runtime_verify":None,"message":""}
        if args.dry_run:
            result["message"]=build_message(result); print(json.dumps(result, ensure_ascii=False, indent=2)); return 0
        remote=remote_apply_and_verify(settings,out,mod,bool(args.no_restart))
        result["upload"]={"success": bool(remote["upload"].get("success")), "backup": remote["backup"], "upload": remote["upload"]}
        result["restart"]=remote["restart"]; result["verify"]=remote["verify"]
        log("[6/6] Reading Amarisoft status...")
        status=get_status(load_status_settings(args.settings)); result["status"]=status
        expected_rb, actual_rb=mod.get("rb_dl"), status.get("config",{}).get("rb_dl")
        expected_e, actual_e=mod.get("dl_earfcn"), status.get("config",{}).get("lte_earfcn_dl")
        match=status.get("success") and str(expected_rb)==str(actual_rb) and str(expected_e)==str(actual_e)
        result["runtime_verify"]={"success": bool(match), "expected":{"dl_earfcn":expected_e,"rb_dl":expected_rb}, "actual":{"dl_earfcn":actual_e,"rb_dl":actual_rb,"bandwidth_mhz_inferred":status.get("config",{}).get("bandwidth_mhz_inferred"),"service_running":status.get("service",{}).get("active_running")}}
        result["success"]=bool(remote["success"]) and bool(match); result["message"]=build_message(result)
        print(json.dumps(result, ensure_ascii=False, indent=2)); return 0 if result["success"] else 1
    except Exception as exc:
        print(json.dumps({"success": False, "error": str(exc), "message": "切 Band 失敗，請查看 error。"}, ensure_ascii=False, indent=2)); return 1
if __name__=="__main__": raise SystemExit(main())
