#!/usr/bin/env python3
# -*- coding: utf-8 -*-
from __future__ import annotations
import re
from typing import Any
from core.ssh.client import shell_quote

def extract_define(cfg_text: str, key: str) -> str | None:
    m = re.search(rf"^\\s*#define\\s+{re.escape(key)}\\s+(?P<value>\\S+)", cfg_text, re.MULTILINE)
    return m.group("value") if m else None

def verify_remote_cfg(ssh: Any, remote_cfg_path: str, modifier_result: dict) -> dict:
    changes = modifier_result.get("changes") or []
    if not changes: return {"success": False, "verified": False, "error": "NO_EXPECTED_CHANGES"}
    r = ssh.execute(f"cat {shell_quote(remote_cfg_path)}")
    if not r.success: return {"success": False, "verified": False, "error": "REMOTE_CFG_READ_FAILED", "command": r.to_dict()}
    checks=[]; ok=True
    for c in changes:
        key, expected = str(c["key"]), str(c["new"])
        actual = extract_define(r.stdout, key)
        matched = actual == expected
        ok = ok and matched
        checks.append({"key": key, "expected": expected, "actual": actual, "matched": matched})
    return {"success": ok, "verified": ok, "type": "remote_cfg_define_check", "remote_cfg_path": remote_cfg_path, "checks": checks}

def verify_symlink(ssh: Any, remote_link_path: str, expected_target: str) -> dict:
    r = ssh.execute(f"readlink {shell_quote(remote_link_path)}")
    actual = r.stdout.strip()
    return {"success": r.success and actual == expected_target, "verified": True,
            "remote_link_path": remote_link_path, "expected_target": expected_target,
            "actual_target": actual, "command": r.to_dict()}

def verify_service(ssh: Any, verify_commands: list[str], command_timeout_sec: int) -> dict:
    if not verify_commands: verify_commands = ["service lte status"]
    results=[]; ok=True; active=False
    for command in verify_commands:
        r = ssh.execute(command, command_timeout_sec)
        text = (r.stdout or "") + "\n" + (r.stderr or "")
        if "active (running)" in text: active=True
        results.append(r.to_dict()); ok = ok and r.success
    return {"success": ok and active, "verified": True, "active_running": active, "commands": results}
