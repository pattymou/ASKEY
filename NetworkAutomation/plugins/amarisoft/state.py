#!/usr/bin/env python3
# -*- coding: utf-8 -*-
from __future__ import annotations

import json
from datetime import datetime
from pathlib import Path
from typing import Any


PROJECT_ROOT = Path(__file__).resolve().parents[2]
STATE_DIR = PROJECT_ROOT / "state"
STATE_FILE = STATE_DIR / "callbox_state.json"


def now_iso() -> str:
    return datetime.now().strftime("%Y-%m-%d %H:%M:%S")


def save_callbox_state(data: dict[str, Any]) -> dict[str, Any]:
    STATE_DIR.mkdir(parents=True, exist_ok=True)

    state = {
        "success": True,
        "updated_at": now_iso(),
        **data,
    }

    STATE_FILE.write_text(json.dumps(state, ensure_ascii=False, indent=2), encoding="utf-8")
    return {
        "success": True,
        "state_file": str(STATE_FILE),
        "state": state,
    }


def load_callbox_state() -> dict[str, Any]:
    if not STATE_FILE.exists():
        return {
            "success": False,
            "error": "STATE_FILE_NOT_FOUND",
            "state_file": str(STATE_FILE),
            "state": None,
        }

    try:
        state = json.loads(STATE_FILE.read_text(encoding="utf-8"))
        return {
            "success": True,
            "state_file": str(STATE_FILE),
            "state": state,
        }
    except Exception as exc:
        return {
            "success": False,
            "error": "STATE_FILE_READ_FAILED",
            "message": str(exc),
            "state_file": str(STATE_FILE),
            "state": None,
        }
