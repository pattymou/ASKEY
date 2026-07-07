#!/usr/bin/env python3
# -*- coding: utf-8 -*-
from __future__ import annotations

import argparse
import json
import re
import subprocess
import sys
from pathlib import Path

PROJECT_ROOT = Path(__file__).resolve().parents[2]


def normalize_text(text: str) -> str:
    t = text.strip()
    t = t.replace("Ｂ", "B").replace("ｂ", "b")
    t = t.replace("Ｍ", "M").replace("ｍ", "m")
    t = t.replace("Ｈ", "H").replace("ｚ", "z")
    t = t.replace("，", " ").replace(",", " ").replace("、", " ")
    return t


def parse_cell(text: str) -> int:
    for p in [r"cell\s*([1-8])", r"小區\s*([1-8])"]:
        m = re.search(p, text, re.IGNORECASE)
        if m:
            return int(m.group(1))
    return 1


def parse_band(text: str) -> int | None:
    for p in [
        r"(?:LTE\s*)?Band\s*([0-9]{1,3})",
        r"\bB\s*([0-9]{1,3})\b",
        r"切\s*([0-9]{1,3})\s*頻",
        r"第\s*([0-9]{1,3})\s*頻",
    ]:
        m = re.search(p, text, re.IGNORECASE)
        if m:
            return int(m.group(1))
    return None


def parse_bandwidth(text: str) -> float | None:
    for p in [
        r"([0-9]+(?:\.[0-9]+)?)\s*(?:MHz|MHZ|mhz|Mhz|M|m)\b",
        r"頻寬\s*([0-9]+(?:\.[0-9]+)?)",
        r"帶寬\s*([0-9]+(?:\.[0-9]+)?)",
    ]:
        m = re.search(p, text)
        if m:
            v = float(m.group(1))
            return int(v) if v.is_integer() else v
    return None


def parse_earfcn(text: str) -> int | None:
    for p in [r"DL\s*EARFCN\s*([0-9]+)", r"EARFCN\s*([0-9]+)"]:
        m = re.search(p, text, re.IGNORECASE)
        if m:
            return int(m.group(1))
    return None


def parse_command(text: str) -> dict:
    original = text
    text = normalize_text(text)

    if re.search(r"(狀態|目前|現在|status)", text, re.IGNORECASE):
        return {
            "success": True,
            "intent": "status",
            "params": {},
            "command": f'python "{PROJECT_ROOT / "callbox_agent.py"}" amarisoft status',
            "message": "解析完成：查詢 Amarisoft 狀態。",
        }

    band = parse_band(text)
    if band is None:
        return {
            "success": False,
            "error": "BAND_NOT_FOUND",
            "message": "沒有解析到 LTE Band。請例如輸入：幫我切 B5 10MHz。",
            "text": original,
        }

    bandwidth = parse_bandwidth(text)
    if bandwidth is None:
        return {
            "success": False,
            "need_clarification": True,
            "missing": "bandwidth",
            "band": band,
            "message": f"請問 Band{band} 要使用多少 MHz？例如：10MHz。",
        }

    cell = parse_cell(text)
    dl_earfcn = parse_earfcn(text)

    cmd_parts = [
        "python",
        str(PROJECT_ROOT / "callbox_agent.py"),
        "amarisoft",
        "set-band",
        "--cell", str(cell),
        "--band", str(band),
        "--bandwidth", str(bandwidth),
    ]

    if dl_earfcn is not None:
        cmd_parts += ["--dl-earfcn", str(dl_earfcn)]

    return {
        "success": True,
        "intent": "set_band",
        "mode": "LTE",
        "params": {
            "cell": cell,
            "band": band,
            "bandwidth": bandwidth,
            "dl_earfcn": dl_earfcn,
        },
        "command": " ".join(f'"{x}"' if " " in x else x for x in cmd_parts),
        "message": f"解析完成：Cell{cell} LTE Band{band}，Bandwidth {bandwidth}MHz。",
        "note": "Band/EARFCN/Bandwidth 合法性由 lte_config_modifier.py 做最終檢查。",
    }


def execute_parsed(parsed: dict, dry_run: bool = False) -> dict:
    if not parsed.get("success"):
        return parsed

    if parsed["intent"] == "status":
        cmd = [sys.executable, str(PROJECT_ROOT / "callbox_agent.py"), "amarisoft", "status"]
    else:
        p = parsed["params"]
        cmd = [
            sys.executable,
            str(PROJECT_ROOT / "callbox_agent.py"),
            "amarisoft",
            "set-band",
            "--cell", str(p["cell"]),
            "--band", str(p["band"]),
            "--bandwidth", str(p["bandwidth"]),
        ]
        if p.get("dl_earfcn") is not None:
            cmd += ["--dl-earfcn", str(p["dl_earfcn"])]
        if dry_run:
            cmd.append("--dry-run")

    proc = subprocess.run(cmd, capture_output=True, text=True)
    stdout = (proc.stdout or "").strip()
    stderr = (proc.stderr or "").strip()

    try:
        tool_result = json.loads(stdout) if stdout else {}
    except Exception:
        tool_result = {"success": False, "error": "INVALID_TOOL_JSON_OUTPUT", "stdout": stdout}

    tool_result.setdefault("success", proc.returncode == 0)
    tool_result.setdefault("returncode", proc.returncode)

    if stderr:
        tool_result.setdefault("stderr", stderr)

    return {
        "success": bool(tool_result.get("success")),
        "parsed": parsed,
        "tool_result": tool_result,
        "message": tool_result.get("message") or parsed.get("message"),
    }


def main() -> int:
    parser = argparse.ArgumentParser(description="Amarisoft natural language command parser")
    parser.add_argument("text")
    parser.add_argument("--settings", default=str(PROJECT_ROOT / "callbox_settings.json"))  # kept for compatibility
    parser.add_argument("--execute", action="store_true")
    parser.add_argument("--dry-run", action="store_true")
    args = parser.parse_args()

    try:
        parsed = parse_command(args.text)
        result = execute_parsed(parsed, dry_run=args.dry_run) if args.execute else parsed
        print(json.dumps(result, ensure_ascii=False, indent=2))
        return 0 if result.get("success") else 1
    except Exception as exc:
        print(json.dumps({
            "success": False,
            "error": type(exc).__name__,
            "message": str(exc),
        }, ensure_ascii=False, indent=2))
        return 1


if __name__ == "__main__":
    raise SystemExit(main())
