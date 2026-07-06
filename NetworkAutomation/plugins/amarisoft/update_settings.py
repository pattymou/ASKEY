#!/usr/bin/env python3
# -*- coding: utf-8 -*-
"""
update_settings.py

專門用來安全修改與讀取 callbox_settings.json 的工具。
此工具限定只能修改或讀取 host, username, password 三個欄位，絕對碰不到其他本機路徑或重啟指令。
"""

import argparse
import json
from pathlib import Path


def get_callbox_settings(settings_path: str | Path) -> dict:
    path = Path(settings_path)
    if not path.exists():
        raise FileNotFoundError(f"找不到設定檔：{path}")

    with path.open("r", encoding="utf-8") as f:
        data = json.load(f)

    # 安全撈取連線資訊回傳
    callbox = data.get("callbox", {})
    return {
        "success": True,
        "host": callbox.get("host", "未設定"),
        "username": callbox.get("username", "未設定"),
        "password": callbox.get("password", "未設定"),
    }


def update_callbox_settings(
    settings_path: str | Path,
    ip: str | None = None,
    username: str | None = None,
    password: str | None = None,
) -> dict:
    path = Path(settings_path)
    if not path.exists():
        raise FileNotFoundError(f"找不到設定檔：{path}")

    with path.open("r", encoding="utf-8") as f:
        data = json.load(f)

    changes = {}

    if ip is not None:
        old_ip = data.get("callbox", {}).get("host")
        data["callbox"]["host"] = ip
        changes["host"] = {"old": old_ip, "new": ip}

    if username is not None:
        old_user = data.get("callbox", {}).get("username")
        data["callbox"]["username"] = username
        changes["username"] = {"old": old_user, "new": username}

    if password is not None:
        old_pwd = data.get("callbox", {}).get("password")
        data["callbox"]["password"] = password
        changes["password"] = {"old": old_pwd, "new": password}

    if changes:
        with path.open("w", encoding="utf-8") as f:
            json.dump(data, f, indent=2, ensure_ascii=False)
        return {"success": True, "message": "設定檔已安全更新", "changes": changes}
    else:
        return {"success": True, "message": "未偵測到任何變更項目", "changes": {}}


def main():
    parser = argparse.ArgumentParser(description="安全管理 Callbox 連線設定檔")
    parser.add_argument(
        "--settings",
        default="C:/CallboxAgent/callbox_settings.json",
        help="JSON 設定檔路徑",
    )
    parser.add_argument("--ip", default=None, help="新的 Callbox IP 地址 (host)")
    parser.add_argument("--username", default=None, help="新的 SSH 帳號 (username)")
    parser.add_argument("--password", default=None, help="新的 SSH 密碼 (password)")
    parser.add_argument(
        "--show", action="store_true", help="顯示目前設定檔中的連線資訊"
    )

    args = parser.parse_args()

    try:
        if args.show:
            result = get_callbox_settings(args.settings)
        else:
            result = update_callbox_settings(
                settings_path=args.settings,
                ip=args.ip,
                username=args.username,
                password=args.password,
            )
        print(json.dumps(result, ensure_ascii=False, indent=2))
    except Exception as e:
        error_result = {"success": False, "error": str(e)}
        print(json.dumps(error_result, ensure_ascii=False, indent=2))


if __name__ == "__main__":
    main()