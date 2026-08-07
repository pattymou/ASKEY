from __future__ import annotations

import argparse
import hashlib
import json
import os
import re
import subprocess
import sys
from pathlib import Path
from typing import Any

ROOT = Path(__file__).resolve().parents[2]
if str(ROOT) not in sys.path:
    sys.path.insert(0, str(ROOT))

from core.user_settings import workflow_setting
from plugins.amarisoft.apply_lte_to_callbox import load, wait_lte_service
from plugins.amarisoft.band_combo_parser import expand_lte_carriers, parse_radio_combination

try:
    sys.stdout.reconfigure(encoding="utf-8", errors="strict")
    sys.stderr.reconfigure(encoding="utf-8", errors="strict")
except Exception:
    pass


def _sha256_file(path: Path) -> str:
    return hashlib.sha256(path.read_bytes()).hexdigest()


def _extract_define(text: str, key: str) -> str:
    match = re.search(rf"^\s*#define\s+{re.escape(key)}\s+(\S+)", text, re.M)
    if not match:
        raise RuntimeError(f"產生的 CFG 找不到 {key}。")
    return match.group(1).strip('"')


def _verify_remote(ssh: Any, path: str, expected: dict[str, str]) -> dict[str, Any]:
    keys = "|".join(re.escape(key) for key in expected)
    command = f"grep -E '^#define ({keys})' {path}"
    response = ssh.execute(command, 30)
    text = response.stdout + response.stderr
    missing = [f"{key}={value}" for key, value in expected.items() if key not in text or str(value) not in text]
    return {
        "success": bool(response.success and not missing),
        "command": command,
        "stdout": response.stdout,
        "stderr": response.stderr,
        "missing": missing,
    }


def _apply_lte_combo(base_cfg: Path, combo: Any, settings: Any, generated_dir: Path) -> tuple[Path, list[dict[str, Any]]]:
    from plugins.amarisoft.lte_config_modifier import apply_lte_config, result_to_dict

    current = base_cfg
    results: list[dict[str, Any]] = []
    for cell, (band, bandwidth) in enumerate(expand_lte_carriers(combo), start=1):
        output = generated_dir / f"ENDC_LTE_stage_{cell}.tmp.cfg"
        result = apply_lte_config(
            current,
            ROOT / "plugins/amarisoft/Earfcn_LTE.json",
            output,
            cell,
            band,
            bandwidth,
            amarisoft_model=settings.callbox.amarisoft_model,
        )
        results.append(result_to_dict(result))
        current = output
    return current, results


def main() -> int:
    parser = argparse.ArgumentParser()
    parser.add_argument("--settings", required=True)
    parser.add_argument("--mode", required=True, choices=["SA", "ENDC", "sa", "endc"])
    parser.add_argument("--cell", type=int, default=1)
    parser.add_argument("--band", type=int)
    parser.add_argument("--bandwidth", type=float)
    parser.add_argument("--band-config")
    parser.add_argument("--bandwidth-config")
    parser.add_argument("--nr-arfcn", type=int)
    parser.add_argument("--mimo-dl")
    parser.add_argument("--mimo-ul")
    parser.add_argument("--modulation-dl")
    parser.add_argument("--modulation-ul")
    parser.add_argument("--mcs-dl")
    parser.add_argument("--mcs-ul")
    parser.add_argument("--time-slot")
    parser.add_argument("--lte-band", type=int)
    parser.add_argument("--lte-bandwidth", type=float)
    parser.add_argument("--lte-earfcn", type=int)
    parser.add_argument("--dry-run", action="store_true")
    parser.add_argument("--no-restart", action="store_true")
    parser.add_argument("--skip-ue-wait", action="store_true")
    parser.add_argument("--phy-only-ready", action="store_true", help="PHY Rate 一出現就完成，不等待 Data IP")
    parser.add_argument("--expected-imsi")
    parser.add_argument("--expected-imei")
    parser.add_argument("--apn")
    args = parser.parse_args()

    try:
        mode = args.mode.upper()
        settings_path = Path(args.settings)
        settings = load(settings_path)
        spec = ROOT / "plugins/amarisoft/nr_spec.json"
        generated_dir = ROOT / "generated"
        generated_dir.mkdir(parents=True, exist_ok=True)
        template = ROOT / f"plugins/amarisoft/AutoConfig_{mode}.cfg"
        nr_input = template
        lte_anchor: Any = None
        combo = None

        if args.band_config:
            if not args.bandwidth_config:
                raise ValueError("使用 --band-config 時必須指定 --bandwidth-config。")
            combo = parse_radio_combination(args.band_config, args.bandwidth_config)
            if combo.mode == "LTE":
                raise ValueError("這個工具只處理 NR/ENDC；純 LTE 請使用 set-band。")
            if combo.mode != mode:
                raise ValueError(f"{combo.canonical_band_config} 判定為 {combo.mode}，但 --mode 是 {mode}。")
            if any(value is not None for value in (args.band, args.bandwidth, args.nr_arfcn, args.lte_band, args.lte_bandwidth, args.lte_earfcn)):
                raise ValueError("--band-config 模式不能再混用單一 band/bandwidth/ARFCN 參數。")
            if combo.lte_tokens:
                nr_input, lte_anchor = _apply_lte_combo(template, combo, settings, generated_dir)
            safe_name = re.sub(r"[^A-Za-z0-9_-]+", "_", combo.canonical_band_config)
            output = generated_dir / f"AutoConfig_{mode}_{safe_name}.cfg"
            nr_calculator_input = combo.nr_calculator_input
            assert nr_calculator_input is not None
            command = [
                sys.executable,
                str(ROOT / "plugins/amarisoft/nr_config_modifier.py"),
                "--cfg", str(nr_input),
                "--nr-spec", str(spec),
                "--output", str(output),
                "--mode", mode,
                "--cell", str(args.cell),
                "--band-config", nr_calculator_input,
                "--amarisoft-model", settings.callbox.amarisoft_model,
            ]
        else:
            if args.band is None:
                raise ValueError("必須指定 --band，或使用 --band-config/--bandwidth-config。")
            output = generated_dir / f"AutoConfig_{mode}_NR_n{args.band}_Cell{args.cell}.cfg"
            if mode == "ENDC" and args.lte_band is None:
                raise ValueError("ENDC 必須指定 LTE band；也可使用 1A_n78A 這種 band_config。")
            if mode == "ENDC" and args.lte_band is not None:
                from plugins.amarisoft.lte_config_modifier import apply_lte_config, result_to_dict
                intermediate = generated_dir / f"AutoConfig_ENDC_LTE_B{args.lte_band}_Cell1.tmp.cfg"
                lte_result = apply_lte_config(
                    template,
                    ROOT / "plugins/amarisoft/Earfcn_LTE.json",
                    intermediate,
                    1,
                    args.lte_band,
                    args.lte_bandwidth,
                    args.lte_earfcn,
                    amarisoft_model=settings.callbox.amarisoft_model,
                )
                lte_anchor = result_to_dict(lte_result)
                nr_input = intermediate
            command = [
                sys.executable,
                str(ROOT / "plugins/amarisoft/nr_config_modifier.py"),
                "--cfg", str(nr_input),
                "--nr-spec", str(spec),
                "--output", str(output),
                "--mode", mode,
                "--cell", str(args.cell),
                "--band", str(args.band),
                "--amarisoft-model", settings.callbox.amarisoft_model,
            ]
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
            ):
                if value is not None:
                    command += [flag, str(value)]

        child_env = os.environ.copy()
        child_env["PYTHONIOENCODING"] = "utf-8"
        child_env["PYTHONUTF8"] = "1"
        completed = subprocess.run(
            command,
            capture_output=True,
            text=True,
            encoding="utf-8",
            errors="strict",
            cwd=str(ROOT),
            env=child_env,
        )
        if completed.returncode != 0:
            raise RuntimeError(completed.stdout or completed.stderr)
        modifier = json.loads(completed.stdout)

        generated_text = output.read_text(encoding="utf-8-sig")
        expected_defines: dict[str, str] = {}
        if combo:
            for offset, carrier in enumerate(modifier["carriers"]):
                cell = args.cell + offset
                expected_defines[f"NR_BAND_{cell}"] = str(carrier["band"])
                expected_defines[f"NR_BANDWIDTH_{cell}"] = str(int(carrier["bandwidth_mhz"]) if float(carrier["bandwidth_mhz"]).is_integer() else carrier["bandwidth_mhz"])
                expected_defines[f"NR_EARFCN_{cell}_DL"] = str(carrier["nr_arfcn"])
        else:
            expected_defines = {
                f"NR_BAND_{args.cell}": str(args.band),
                f"NR_BANDWIDTH_{args.cell}": str(int(modifier["bandwidth_mhz"]) if float(modifier["bandwidth_mhz"]).is_integer() else modifier["bandwidth_mhz"]),
                f"NR_EARFCN_{args.cell}_DL": str(modifier["nr_arfcn"]),
            }
        local_values = {key: _extract_define(generated_text, key) for key in expected_defines}
        if local_values != expected_defines:
            raise RuntimeError(f"本機 CFG 驗證失敗：expected={expected_defines}, actual={local_values}")

        result: dict[str, Any] = {
            "success": True,
            "mode": mode,
            "band_config": combo.canonical_band_config if combo else f"n{args.band}A",
            "bandwidth_config": args.bandwidth_config if combo else str(modifier["bandwidth_mhz"]),
            "modifier": modifier,
            "lte_anchor": lte_anchor,
            "generated_cfg": str(output),
            "dry_run": args.dry_run,
            "runtime_files": {
                "apply_nr_to_callbox": str(Path(__file__).resolve()),
                "nr_config_modifier": str((ROOT / "plugins/amarisoft/nr_config_modifier.py").resolve()),
                "nr_spec": str(spec.resolve()),
                "nr_config_modifier_sha256": _sha256_file(ROOT / "plugins/amarisoft/nr_config_modifier.py"),
                "nr_spec_sha256": _sha256_file(spec),
            },
            "local_config_verify": {"success": True, "defines": local_values},
            "stage_success": {
                "config_generated": True,
                "uploaded": False,
                "lte_service_stable": False,
                "remote_config_verified": False,
                "ue_connected": False,
            },
        }
        if args.dry_run:
            result["message"] = f"{mode} {result['band_config']} dry-run 完成，未操作 Callbox。"
            print(json.dumps(result, ensure_ascii=False, indent=2))
            return 0

        from core.ssh import SSHClient
        remote_cfg_path = settings.callbox.remote_cfg_path
        result["remote_cfg_path"] = remote_cfg_path
        result["source_template"] = str(template)
        with SSHClient.from_callbox_settings(settings.callbox) as ssh:
            result["backup"] = ssh.backup_file(remote_cfg_path, settings.callbox.remote_backup_dir)
            result["upload"] = ssh.upload(output, remote_cfg_path)
            result["stage_success"]["uploaded"] = bool(result["upload"].get("success"))
            if not result["stage_success"]["uploaded"]:
                raise RuntimeError("上傳 AutoConfig.cfg 失敗。")
            if args.no_restart:
                result["restart"] = {"success": True, "skipped": True}
                result["lte_service"] = {"success": True, "skipped": True}
                result["stage_success"]["lte_service_stable"] = True
            else:
                commands = []
                for restart_command in settings.callbox.restart_commands:
                    response = ssh.execute(restart_command, settings.callbox.command_timeout_sec)
                    commands.append(response.to_dict())
                    if not response.success:
                        raise RuntimeError(f"restart failed: {restart_command}: {response.stderr or response.stdout}")
                result["restart"] = {"success": True, "commands": commands}
                result["lte_service"] = wait_lte_service(ssh, settings.callbox)
                result["stage_success"]["lte_service_stable"] = bool(result["lte_service"].get("success"))
                if not result["stage_success"]["lte_service_stable"]:
                    raise RuntimeError(result["lte_service"].get("message"))
            result["config_verify"] = _verify_remote(ssh, remote_cfg_path, expected_defines)
            result["stage_success"]["remote_config_verified"] = bool(result["config_verify"].get("success"))
            if not result["stage_success"]["remote_config_verified"]:
                raise RuntimeError("遠端 AutoConfig.cfg 的 NR 組合設定驗證失敗。")

        if args.skip_ue_wait:
            result["connection"] = {"success": True, "connected": False, "skipped": True}
        else:
            from plugins.amarisoft.ue_connection import wait_for_connection
            timeout = int(workflow_setting(ROOT, "wait_ue_timeout_sec"))
            result["ue_wait_timeout_sec"] = timeout
            result["connection"] = wait_for_connection(
                settings_path,
                expected_imsi=args.expected_imsi,
                expected_imei=args.expected_imei,
                apn=args.apn,
                max_wait_sec=timeout,
                return_on_phy=args.phy_only_ready,
            )
            result["stage_success"]["ue_connected"] = bool(result["connection"].get("connected"))
        result["success"] = all((
            result["stage_success"]["config_generated"],
            result["stage_success"]["uploaded"],
            result["stage_success"]["lte_service_stable"],
            result["stage_success"]["remote_config_verified"],
        )) and (args.skip_ue_wait or result["stage_success"]["ue_connected"])
        result["message"] = (
            f"{mode} Band 組合切換完成：{result['band_config']}，BW {result['bandwidth_config']}。"
            if result["success"]
            else f"{mode} config 已套用，但 UE 尚未連線。"
        )
        print(json.dumps(result, ensure_ascii=False, indent=2))
        return 0 if result["success"] else 1
    except Exception as exc:
        print(json.dumps({"success": False, "error": type(exc).__name__, "message": str(exc)}, ensure_ascii=False, indent=2))
        return 1


if __name__ == "__main__":
    raise SystemExit(main())
