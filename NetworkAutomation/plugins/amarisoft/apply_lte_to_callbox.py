from __future__ import annotations

import argparse
import json
import os
import subprocess
import sys
import time
from dataclasses import dataclass
from pathlib import Path

try:
    sys.stdout.reconfigure(encoding="utf-8", errors="strict")
    sys.stderr.reconfigure(encoding="utf-8", errors="strict")
except Exception:
    pass

ROOT = Path(__file__).resolve().parents[2]
if str(ROOT) not in sys.path:
    sys.path.insert(0, str(ROOT))



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
    stable_poll_interval_sec: float
    stable_max_wait_sec: int
    stable_required_success_count: int
    amarisoft_model: str


@dataclass(frozen=True)
class Settings:
    local: LocalSettings
    callbox: CallboxSettings


def pth(value: str, base: Path) -> Path:
    p = Path(os.path.expandvars(os.path.expanduser(value)))
    return p if p.is_absolute() else base / p


def load(path: Path) -> Settings:
    raw = json.loads(path.read_text(encoding="utf-8"))
    l, c = raw["local"], raw["callbox"]
    base = pth(l.get("base_dir", "."), path.parent)
    return Settings(
        LocalSettings(
            pth(l["modifier_py"], base),
            pth(l["input_cfg"], base),
            pth(l["earfcn_json"], base),
            pth(l.get("output_dir", "generated"), base),
            l.get("output_pattern", "AutoConfig_LTE_B{band}_Cell{cell}.cfg"),
        ),
        CallboxSettings(
            str(c["host"]),
            int(c.get("port", 22)),
            str(c["username"]),
            str(c.get("password", "")),
            str(c.get("remote_cfg_path", "/root/enb/config/AutoConfig.cfg")),
            str(c.get("remote_backup_dir", "/root/enb/config/backup")),
            list(c.get("restart_commands", [
                "cd /root/enb/config && ln -sfn AutoConfig.cfg enb.cfg",
                "service lte restart",
            ])),
            int(c.get("ssh_timeout_sec", 30)),
            int(c.get("command_timeout_sec", 120)),
            float(c.get("stable_poll_interval_sec", 2)),
            int(c.get("stable_max_wait_sec", 300)),
            int(c.get("stable_required_success_count", 2)),
            str(c.get("amarisoft_model", "100M")),
        ),
    )


def wait_lte_service(ssh, c: CallboxSettings) -> dict:
    started = time.monotonic()
    consecutive = 0
    attempts = []
    while True:
        elapsed = round(time.monotonic() - started, 1)
        r = ssh.execute("service lte status", c.command_timeout_sec)
        running = r.success and "active (running)" in (r.stdout + r.stderr)
        consecutive = consecutive + 1 if running else 0
        attempts.append({
            "elapsed_sec": elapsed,
            "running": running,
            "consecutive_success": consecutive,
        })
        if consecutive >= c.stable_required_success_count:
            return {
                "success": True,
                "elapsed_sec": elapsed,
                "attempts": attempts[-20:],
            }
        if elapsed >= c.stable_max_wait_sec:
            return {
                "success": False,
                "elapsed_sec": elapsed,
                "attempts": attempts[-20:],
                "message": "LTE service 未在安全上限內穩定。",
            }
        time.sleep(c.stable_poll_interval_sec)


def verify_remote_cfg(
    ssh,
    remote_path: str,
    cell: int,
    dl_earfcn: int,
    rb_dl: int,
) -> dict:
    command = (
        f"grep -E '^#define LTE_Cell_{cell}_(EARFCN_DL|RB_DL)' "
        f"{remote_path}"
    )
    result = ssh.execute(command, 30)
    text = result.stdout
    earfcn_ok = str(dl_earfcn) in text
    rb_ok = str(rb_dl) in text
    return {
        "success": result.success and earfcn_ok and rb_ok,
        "command": command,
        "earfcn_ok": earfcn_ok,
        "rb_ok": rb_ok,
        "stdout": text,
        "stderr": result.stderr,
    }


def main() -> int:
    ap = argparse.ArgumentParser()
    ap.add_argument("--settings", required=True)
    ap.add_argument("--cell", type=int, default=1)
    ap.add_argument("--band", type=int, required=True)
    ap.add_argument("--bandwidth", type=float)
    ap.add_argument("--dl-earfcn", type=int)
    ap.add_argument("--mimo-dl")
    ap.add_argument("--mimo-ul")
    ap.add_argument("--modulation-dl")
    ap.add_argument("--modulation-ul")
    ap.add_argument("--mcs-dl")
    ap.add_argument("--mcs-ul")
    ap.add_argument("--dry-run", action="store_true")
    ap.add_argument("--no-restart", action="store_true")
    ap.add_argument("--skip-ue-wait", action="store_true")
    ap.add_argument("--phy-only-ready", action="store_true", help="PHY Rate 一出現就完成，不等待 Data IP")
    ap.add_argument("--expected-imsi")
    ap.add_argument("--expected-imei")
    ap.add_argument("--apn")
    args = ap.parse_args()

    try:
        settings_path = Path(args.settings)
        s = load(settings_path)
        out = s.local.output_dir / s.local.output_pattern.format(
            cell=args.cell,
            band=args.band,
            bandwidth=args.bandwidth or "auto",
        )
        out.parent.mkdir(parents=True, exist_ok=True)

        cmd = [
            sys.executable,
            str(s.local.modifier_py),
            "--cfg", str(s.local.input_cfg),
            "--earfcn-json", str(s.local.earfcn_json),
            "--cell", str(args.cell),
            "--band", str(args.band),
            "--output", str(out),
            "--amarisoft-model", s.callbox.amarisoft_model,
        ]
        if args.bandwidth is not None:
            cmd += ["--bandwidth", str(args.bandwidth)]
        if args.dl_earfcn is not None:
            cmd += ["--dl-earfcn", str(args.dl_earfcn)]
        for value, flag in (
            (args.mimo_dl, "--mimo-dl"),
            (args.mimo_ul, "--mimo-ul"),
            (args.modulation_dl, "--modulation-dl"),
            (args.modulation_ul, "--modulation-ul"),
            (args.mcs_dl, "--mcs-dl"),
            (args.mcs_ul, "--mcs-ul"),
        ):
            if value is not None:
                cmd += [flag, str(value)]

        modifier_process = subprocess.run(cmd, capture_output=True, text=True)
        if modifier_process.returncode != 0:
            raise RuntimeError(modifier_process.stdout or modifier_process.stderr)
        modifier = json.loads(modifier_process.stdout)

        result = {
            "success": True,
            "stage_success": {
                "config_generated": True,
                "uploaded": False,
                "lte_service_stable": False,
                "remote_config_verified": False,
                "ue_connected": False,
            },
            "modifier": modifier,
            "generated_cfg": str(out),
            "dry_run": args.dry_run,
        }

        if args.dry_run:
            result["message"] = "Dry-run 完成，未操作 Callbox。"
            print(json.dumps(result, ensure_ascii=False, indent=2))
            return 0

        from core.ssh import SSHClient
        with SSHClient.from_callbox_settings(s.callbox) as ssh:
            result["backup"] = ssh.backup_file(
                s.callbox.remote_cfg_path,
                s.callbox.remote_backup_dir,
            )
            result["upload"] = ssh.upload(out, s.callbox.remote_cfg_path)
            result["stage_success"]["uploaded"] = bool(result["upload"].get("success"))

            if args.no_restart:
                result["restart"] = {"success": True, "skipped": True}
                result["lte_service"] = {"success": True, "skipped": True}
                result["stage_success"]["lte_service_stable"] = True
            else:
                commands = []
                for command in s.callbox.restart_commands:
                    r = ssh.execute(command, s.callbox.command_timeout_sec)
                    commands.append(r.to_dict())
                    if not r.success:
                        raise RuntimeError(f"restart failed: {command}")
                result["restart"] = {"success": True, "commands": commands}
                result["lte_service"] = wait_lte_service(ssh, s.callbox)
                result["stage_success"]["lte_service_stable"] = bool(
                    result["lte_service"].get("success")
                )
                if not result["stage_success"]["lte_service_stable"]:
                    raise RuntimeError(result["lte_service"].get("message"))

            result["config_verify"] = verify_remote_cfg(
                ssh,
                s.callbox.remote_cfg_path,
                args.cell,
                int(modifier.get("dl_earfcn")),
                int(modifier.get("rb_dl")),
            )
            result["stage_success"]["remote_config_verified"] = bool(
                result["config_verify"].get("success")
            )
            if not result["stage_success"]["remote_config_verified"]:
                raise RuntimeError("遠端 AutoConfig.cfg 驗證失敗。")

        if args.skip_ue_wait:
            result["connection"] = {
                "success": True,
                "connected": False,
                "skipped": True,
                "message": "已略過 UE 連線等待。",
            }
        else:
            from plugins.amarisoft.ue_connection import wait_for_connection
            result["connection"] = wait_for_connection(
                settings_path,
                expected_imsi=args.expected_imsi,
                expected_imei=args.expected_imei,
                apn=args.apn,
                return_on_phy=args.phy_only_ready,
            )
            result["stage_success"]["ue_connected"] = bool(
                result["connection"].get("connected")
            )

        state = {
            "mode": "LTE",
            "cell": args.cell,
            "band": args.band,
            "bandwidth_mhz": modifier.get("bandwidth_mhz"),
            "dl_earfcn": modifier.get("dl_earfcn"),
            "rb_dl": modifier.get("rb_dl"),
            "mimo_dl": modifier.get("mimo_dl"),
            "mimo_ul": modifier.get("mimo_ul"),
            "modulation_dl": modifier.get("modulation_dl"),
            "modulation_ul": modifier.get("modulation_ul"),
            "mcs_dl": modifier.get("mcs_dl"),
            "mcs_ul": modifier.get("mcs_ul"),
            "service_stable": result["stage_success"]["lte_service_stable"],
            "remote_config_verified": result["stage_success"]["remote_config_verified"],
            "ue_connected": result["stage_success"]["ue_connected"],
            "connection_basis": result["connection"].get("connection_basis"),
            "phy_rate": result["connection"].get("phy_rate"),
            "ue": result["connection"].get("data_ue") or result["connection"].get("ue"),
            "data_ue_ip": result["connection"].get("data_ue_ip"),
            "data_ip_source": result["connection"].get("data_ip_source"),
        }
        state_path = ROOT / "state/callbox_state.json"
        state_path.write_text(
            json.dumps(state, ensure_ascii=False, indent=2),
            encoding="utf-8",
        )
        result["state"] = state

        # Band apply and remote verification are successful even if UE has not attached.
        # Overall success requires UE connection unless explicitly skipped.
        result["success"] = (
            result["stage_success"]["config_generated"]
            and result["stage_success"]["uploaded"]
            and result["stage_success"]["lte_service_stable"]
            and result["stage_success"]["remote_config_verified"]
            and (
                args.skip_ue_wait
                or result["stage_success"]["ue_connected"]
            )
        )

        if result["success"]:
            ue_ip = result["connection"].get("data_ue_ip")
            result["message"] = (
                f"Band 切換完成：Cell{args.cell}, B{args.band}, "
                f"{modifier.get('bandwidth_mhz')}MHz；"
                f"LTE service 穩定、遠端 config 正確、UE 已連線；"
                f"PHY DL={result['connection'].get('phy_rate', {}).get('total_dl_bitrate_mbps', 0)} Mbps，"
                f"PHY UL={result['connection'].get('phy_rate', {}).get('total_ul_bitrate_mbps', 0)} Mbps"
                f"{f'，IP {ue_ip}' if ue_ip else ''}。"
            )
            code = 0
        else:
            result["message"] = (
                "Band 與 LTE service 已完成，但 UE 尚未在安全上限內恢復連線。"
            )
            code = 1

        print(json.dumps(result, ensure_ascii=False, indent=2))
        return code

    except Exception as exc:
        print(json.dumps({
            "success": False,
            "error": type(exc).__name__,
            "message": str(exc),
        }, ensure_ascii=False, indent=2))
        return 1


if __name__ == "__main__":
    raise SystemExit(main())
