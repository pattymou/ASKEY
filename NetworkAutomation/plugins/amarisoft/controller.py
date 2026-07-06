#!/usr/bin/env python3
# -*- coding: utf-8 -*-
"""Amarisoft plugin controller.

This layer is Amarisoft-specific. It may call Amarisoft scripts and know about
LTE cfg/restart flow. Generic SSH stays in core/ssh.
"""
from __future__ import annotations

import json
import subprocess
import sys
from pathlib import Path
from typing import Any


class AmarisoftController:
    def __init__(self, base_path: str | Path = ".") -> None:
        self.base = Path(base_path).resolve()
        self.settings_path = self.base / "callbox_settings.json"
        self.apply_script = self.base / "plugins" / "amarisoft" / "apply_lte_to_callbox.py"

    def _run_json(self, cmd: list[str]) -> dict[str, Any]:
        p = subprocess.run(cmd, capture_output=True, text=True)
        stdout = (p.stdout or "").strip()
        stderr = (p.stderr or "").strip()
        try:
            result = json.loads(stdout) if stdout else {}
        except Exception:
            result = {
                "success": False,
                "error": "INVALID_JSON_OUTPUT",
                "stdout": stdout,
                "stderr": stderr,
            }
        result.setdefault("success", p.returncode == 0)
        result.setdefault("returncode", p.returncode)
        if stderr:
            result.setdefault("stderr", stderr)
        return result

    def set_lte_band(
        self,
        cell: int,
        band: int,
        bandwidth: float | None = None,
        dl_earfcn: int | None = None,
        dry_run: bool = False,
        no_restart: bool = False,
    ) -> dict[str, Any]:
        cmd = [
            sys.executable,
            str(self.apply_script),
            "--settings", str(self.settings_path),
            "--cell", str(cell),
            "--band", str(band),
        ]
        if bandwidth is not None:
            cmd.extend(["--bandwidth", str(bandwidth)])
        if dl_earfcn is not None:
            cmd.extend(["--dl-earfcn", str(dl_earfcn)])
        if dry_run:
            cmd.append("--dry-run")
        if no_restart:
            cmd.append("--no-restart")
        return self._run_json(cmd)


# Backward-compatible name for old callbox_agent.py imports.
CallboxController = AmarisoftController
