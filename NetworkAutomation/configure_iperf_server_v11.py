from __future__ import annotations

import argparse
import json
import shutil
from datetime import datetime
from pathlib import Path


def main() -> int:
    parser = argparse.ArgumentParser(
        description="Configure V11 automatic iPerf server management."
    )
    parser.add_argument("--settings", default="callbox_settings.json")
    parser.add_argument(
        "--mode",
        choices=["check-only", "ssh"],
        required=True,
        help="check-only only verifies port; ssh can start the server automatically.",
    )
    parser.add_argument("--username")
    parser.add_argument("--password", default="")
    parser.add_argument("--ssh-port", type=int, default=22)
    parser.add_argument("--binary", default="iperf3")
    parser.add_argument(
        "--start-command",
        default="nohup {binary} -s -p {port} -D >/tmp/networkautomation_iperf3.log 2>&1",
    )
    parser.add_argument(
        "--stop-command",
        default="pkill -f '{binary} -s -p {port}' || true",
    )
    args = parser.parse_args()

    path = Path(args.settings)
    raw = json.loads(path.read_text(encoding="utf-8"))

    backup = path.with_name(
        f"{path.name}.v11_{datetime.now().strftime('%Y%m%d_%H%M%S')}.bak"
    )
    shutil.copy2(path, backup)

    enabled = args.mode == "ssh"
    if enabled and not args.username:
        raise SystemExit("--mode ssh 必須提供 --username")

    raw.setdefault("iperf", {})["server_control"] = {
        "enabled": enabled,
        "method": "ssh" if enabled else "check_only",
        "username": args.username or "",
        "password": args.password,
        "ssh_port": args.ssh_port,
        "ssh_timeout_sec": 20,
        "command_timeout_sec": 30,
        "binary": args.binary,
        "start_command": args.start_command,
        "stop_command": args.stop_command,
        "retry_count": 2,
        "retry_interval_sec": 2,
        "listen_wait_sec": 15,
    }

    path.write_text(
        json.dumps(raw, ensure_ascii=False, indent=2),
        encoding="utf-8",
    )
    print(json.dumps({
        "success": True,
        "settings": str(path.resolve()),
        "backup": str(backup.resolve()),
        "mode": args.mode,
        "note": (
            "check-only 會確認 5201，但不會啟動。"
            if not enabled
            else "SSH 自動啟動已設定。"
        ),
    }, ensure_ascii=False, indent=2))
    return 0


if __name__ == "__main__":
    raise SystemExit(main())
