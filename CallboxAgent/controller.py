import subprocess
import json
from pathlib import Path
import sys


class CallboxController:

    def __init__(self, base_path):
        self.base = Path(base_path)

    # -------------------------
    # 1. execute apply
    # -------------------------
    def apply_lte(self, cell, band, bandwidth):

        cmd = [
            sys.executable,
            str(self.base / "apply_lte_to_callbox.py"),
            "--settings", str(self.base / "callbox_settings.json"),
            "--cell", str(cell),
            "--band", str(band),
            "--bandwidth", str(bandwidth),
        ]

        p = subprocess.run(cmd, capture_output=True, text=True)

        try:
            return json.loads(p.stdout)
        except:
            return {
                "success": False,
                "error": "INVALID_OUTPUT",
                "stdout": p.stdout,
                "stderr": p.stderr
            }

    # -------------------------
    # 2. future hooks
    # -------------------------
    def restart(self):
        return {"success": True, "action": "restart_todo"}

    def status(self):
        return {"success": True, "status": "todo"}