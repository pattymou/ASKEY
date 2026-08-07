from __future__ import annotations

import argparse
import json
import re
import sys
from pathlib import Path
from typing import Any

ROOT = Path(__file__).resolve().parents[2]
if str(ROOT) not in sys.path:
    sys.path.insert(0, str(ROOT))

from core.user_settings import workflow_setting
from plugins.amarisoft.apply_lte_to_callbox import load, wait_lte_service
from plugins.amarisoft.band_combo_parser import expand_lte_carriers, parse_radio_combination
from plugins.amarisoft.lte_config_modifier import apply_lte_config, result_to_dict


def _extract_define(text: str, key: str) -> str:
    match = re.search(rf"^\s*#define\s+{re.escape(key)}\s+(\S+)", text, re.M)
    if not match:
        raise RuntimeError(f"產生的 CFG 找不到 {key}。")
    return match.group(1).strip('"')


def main() -> int:
    parser = argparse.ArgumentParser()
    parser.add_argument('--settings', required=True)
    parser.add_argument('--band-config', required=True)
    parser.add_argument('--bandwidth-config', required=True)
    parser.add_argument('--dry-run', action='store_true')
    parser.add_argument('--no-restart', action='store_true')
    parser.add_argument('--skip-ue-wait', action='store_true')
    parser.add_argument('--phy-only-ready', action='store_true', help='PHY Rate 一出現就完成，不等待 Data IP')
    parser.add_argument('--expected-imsi')
    parser.add_argument('--expected-imei')
    parser.add_argument('--apn')
    args = parser.parse_args()
    try:
        combo = parse_radio_combination(args.band_config, args.bandwidth_config)
        if combo.mode != 'LTE':
            raise ValueError('apply_lte_combo_to_callbox 只接受純 LTE 組合，例如 1A 或 1A-3A。')
        settings_path = Path(args.settings)
        settings = load(settings_path)
        current = settings.local.input_cfg
        results: list[dict[str, Any]] = []
        for cell, (band, bandwidth) in enumerate(expand_lte_carriers(combo), start=1):
            output = settings.local.output_dir / f'LTE_combo_stage_{cell}.tmp.cfg'
            result = apply_lte_config(
                current,
                settings.local.earfcn_json,
                output,
                cell,
                band,
                bandwidth,
                amarisoft_model=settings.callbox.amarisoft_model,
            )
            results.append(result_to_dict(result))
            current = output
        final_output = settings.local.output_dir / f"AutoConfig_LTE_{combo.canonical_band_config.replace('-', '_')}.cfg"
        final_output.write_bytes(current.read_bytes())
        text = final_output.read_text(encoding='utf-8-sig')
        defines = {}
        for cell, result in enumerate(results, start=1):
            defines[f'LTE_Cell_{cell}_EARFCN_DL'] = _extract_define(text, f'LTE_Cell_{cell}_EARFCN_DL')
            defines[f'LTE_Cell_{cell}_RB_DL'] = _extract_define(text, f'LTE_Cell_{cell}_RB_DL')
        payload: dict[str, Any] = {
            'success': True,
            'mode': 'LTE',
            'band_config': combo.canonical_band_config,
            'bandwidth_config': args.bandwidth_config,
            'carriers': results,
            'generated_cfg': str(final_output),
            'local_config_verify': {'success': True, 'defines': defines},
            'dry_run': args.dry_run,
            'stage_success': {'config_generated': True, 'uploaded': False, 'lte_service_stable': False, 'remote_config_verified': False, 'ue_connected': False},
        }
        if args.dry_run:
            payload['message'] = f"LTE {combo.canonical_band_config} dry-run 完成。"
            print(json.dumps(payload, ensure_ascii=False, indent=2)); return 0

        from core.ssh import SSHClient
        remote_path = settings.callbox.remote_cfg_path
        with SSHClient.from_callbox_settings(settings.callbox) as ssh:
            payload['backup'] = ssh.backup_file(remote_path, settings.callbox.remote_backup_dir)
            payload['upload'] = ssh.upload(final_output, remote_path)
            payload['stage_success']['uploaded'] = bool(payload['upload'].get('success'))
            if not payload['stage_success']['uploaded']:
                raise RuntimeError('上傳 AutoConfig.cfg 失敗。')
            if args.no_restart:
                payload['stage_success']['lte_service_stable'] = True
            else:
                restart_results = []
                for command in settings.callbox.restart_commands:
                    response = ssh.execute(command, settings.callbox.command_timeout_sec)
                    restart_results.append(response.to_dict())
                    if not response.success:
                        raise RuntimeError(response.stderr or response.stdout)
                payload['restart'] = {'success': True, 'commands': restart_results}
                payload['lte_service'] = wait_lte_service(ssh, settings.callbox)
                payload['stage_success']['lte_service_stable'] = bool(payload['lte_service'].get('success'))
            keys = '|'.join(re.escape(key) for key in defines)
            response = ssh.execute(f"grep -E '^#define ({keys})' {remote_path}", 30)
            remote_text = response.stdout + response.stderr
            payload['config_verify'] = {'success': response.success and all(value in remote_text for value in defines.values()), 'stdout': response.stdout, 'stderr': response.stderr}
            payload['stage_success']['remote_config_verified'] = bool(payload['config_verify']['success'])

        if args.skip_ue_wait:
            payload['connection'] = {'success': True, 'connected': False, 'skipped': True}
        else:
            from plugins.amarisoft.ue_connection import wait_for_connection
            timeout = int(workflow_setting(ROOT, 'wait_ue_timeout_sec'))
            payload['ue_wait_timeout_sec'] = timeout
            payload['connection'] = wait_for_connection(settings_path, expected_imsi=args.expected_imsi, expected_imei=args.expected_imei, apn=args.apn, max_wait_sec=timeout, return_on_phy=args.phy_only_ready)
            payload['stage_success']['ue_connected'] = bool(payload['connection'].get('connected'))
        payload['success'] = all((payload['stage_success']['config_generated'], payload['stage_success']['uploaded'], payload['stage_success']['lte_service_stable'], payload['stage_success']['remote_config_verified'])) and (args.skip_ue_wait or payload['stage_success']['ue_connected'])
        payload['message'] = f"LTE Band 組合切換完成：{combo.canonical_band_config}，BW {args.bandwidth_config}。" if payload['success'] else 'LTE config 已套用，但 UE 尚未連線。'
        print(json.dumps(payload, ensure_ascii=False, indent=2)); return 0 if payload['success'] else 1
    except Exception as exc:
        print(json.dumps({'success': False, 'error': type(exc).__name__, 'message': str(exc)}, ensure_ascii=False, indent=2)); return 1


if __name__ == '__main__':
    raise SystemExit(main())
