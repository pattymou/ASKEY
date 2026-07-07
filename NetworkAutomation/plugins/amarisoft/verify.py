#!/usr/bin/env python3
# -*- coding: utf-8 -*-
from __future__ import annotations

from typing import Any

from core.ssh.client import shell_quote


def verify_symlink(ssh: Any, remote_link_path: str, expected_target: str) -> dict[str, Any]:
    r = ssh.execute(f"readlink {shell_quote(remote_link_path)}")
    actual = r.stdout.strip()

    return {
        "success": r.success and actual == expected_target,
        "verified": True,
        "remote_link_path": remote_link_path,
        "expected_target": expected_target,
        "actual_target": actual,
        "command": r.to_dict(),
    }


def verify_service(ssh: Any, verify_commands: list[str], command_timeout_sec: int) -> dict[str, Any]:
    if not verify_commands:
        verify_commands = ["service lte status"]

    results = []
    all_success = True
    active_running = False

    for command in verify_commands:
        r = ssh.execute(command, command_timeout_sec)
        text = (r.stdout or "") + "\n" + (r.stderr or "")

        if "active (running)" in text:
            active_running = True

        results.append(r.to_dict())
        all_success = all_success and r.success

    return {
        "success": all_success and active_running,
        "verified": True,
        "active_running": active_running,
        "commands": results,
    }


def verify_remote_cfg_exists(ssh: Any, remote_cfg_path: str) -> dict[str, Any]:
    r = ssh.execute(f"test -f {shell_quote(remote_cfg_path)} && echo EXISTS || echo MISSING")
    exists = "EXISTS" in (r.stdout or "")

    return {
        "success": r.success and exists,
        "verified": True,
        "remote_cfg_path": remote_cfg_path,
        "exists": exists,
        "command": r.to_dict(),
    }
