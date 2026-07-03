#!/usr/bin/env python3
# -*- coding: utf-8 -*-
"""
lte_config_modifier.py

First LTE version for Amarisoft Callbox AutoConfig.cfg modification.

Function:
- Read Earfcn_LTE.json
- Validate LTE Band / DL EARFCN / Channel Bandwidth
- Modify Amarisoft cfg #define values:
    LTE_Cell_X_EARFCN_DL
    LTE_Cell_X_RB_DL
    LTE_TDD_Cell_X
- Generate a new cfg file
- Optional backup output file behavior

Example:
    python lte_config_modifier.py \
        --cfg AutoConfig.cfg \
        --earfcn-json Earfcn_LTE.json \
        --cell 1 \
        --band 5 \
        --bandwidth 10 \
        --output AutoConfig_B5.cfg

    python lte_config_modifier.py \
        --cfg AutoConfig.cfg \
        --earfcn-json Earfcn_LTE.json \
        --cell 1 \
        --band 3 \
        --bandwidth 20 \
        --dl-earfcn 1575 \
        --output AutoConfig_B3.cfg
"""

from __future__ import annotations

import argparse
import json
import re
import shutil
from dataclasses import dataclass
from datetime import datetime
from pathlib import Path
from typing import Any, Optional


RB_MAP: dict[float, int] = {
    1.4: 6,
    3.0: 15,
    5.0: 25,
    10.0: 50,
    15.0: 75,
    20.0: 100,
}

# Amarisoft config in your file uses:
# 0 = FDD, 1 = TDD, 2 = SDL
MODE_TO_LTE_TDD_CELL_VALUE = {
    "FDD": 0,
    "TDD": 1,
    "SDL": 2,
}


@dataclass(frozen=True)
class LteBandInfo:
    band: int
    earfcn_low: int
    earfcn_middle: int
    earfcn_high: int
    mode: str
    channel_bandwidth: list[float]


@dataclass(frozen=True)
class LteApplyResult:
    success: bool
    input_cfg: str
    output_cfg: str
    cell: int
    band: int
    mode: str
    dl_earfcn: int
    bandwidth_mhz: float
    rb_dl: int
    changes: list[dict[str, Any]]


class LteBandDatabase:
    def __init__(self, json_path: str | Path) -> None:
        self.json_path = Path(json_path)
        if not self.json_path.exists():
            raise FileNotFoundError(f"EARFCN JSON not found: {self.json_path}")

        with self.json_path.open("r", encoding="utf-8") as f:
            raw = json.load(f)

        if "band" not in raw or not isinstance(raw["band"], list):
            raise ValueError("Invalid Earfcn_LTE.json format. Missing list field: band")

        self._bands: dict[int, LteBandInfo] = {}
        for item in raw["band"]:
            info = self._parse_band_item(item)
            self._bands[info.band] = info

    @staticmethod
    def _parse_band_item(item: dict[str, Any]) -> LteBandInfo:
        bandwidth_text = str(item["Channel_BandWidth"])
        bandwidths = [float(x.strip()) for x in bandwidth_text.split(",") if x.strip()]

        return LteBandInfo(
            band=int(item["Band"]),
            earfcn_low=int(item["Earfcn_Low"]),
            earfcn_middle=int(item["Earfcn_Middle"]),
            earfcn_high=int(item["Earfcn_High"]),
            mode=str(item["Mode"]).upper().strip(),
            channel_bandwidth=bandwidths,
        )

    def get_band(self, band: int) -> LteBandInfo:
        if band not in self._bands:
            supported = ", ".join(str(x) for x in sorted(self._bands.keys()))
            raise ValueError(f"LTE Band {band} not found. Supported bands: {supported}")
        return self._bands[band]

    def get_default_bandwidth(self, band: int) -> float:
        info = self.get_band(band)
        # For MVP, use the maximum supported bandwidth from JSON.
        return max(info.channel_bandwidth)

    def validate_bandwidth(self, info: LteBandInfo, bandwidth_mhz: float) -> None:
        normalized = float(bandwidth_mhz)
        if normalized not in info.channel_bandwidth:
            allowed = ", ".join(format_bandwidth(x) for x in info.channel_bandwidth)
            raise ValueError(
                f"LTE Band {info.band} does not support {format_bandwidth(normalized)}MHz. "
                f"Allowed bandwidth: {allowed}MHz"
            )

        if normalized not in RB_MAP:
            raise ValueError(f"Bandwidth {normalized}MHz has no RB mapping in RB_MAP")

    def validate_dl_earfcn(self, info: LteBandInfo, dl_earfcn: int) -> None:
        if not (info.earfcn_low <= dl_earfcn <= info.earfcn_high):
            raise ValueError(
                f"DL EARFCN {dl_earfcn} is out of LTE Band {info.band} range. "
                f"Allowed range: {info.earfcn_low}~{info.earfcn_high}"
            )


def format_bandwidth(value: float) -> str:
    if float(value).is_integer():
        return str(int(value))
    return str(value)


def read_text_keep_encoding(path: Path) -> str:
    # Amarisoft cfg is normally ASCII/UTF-8. Keep UTF-8 for Chinese comments if any.
    return path.read_text(encoding="utf-8")


def replace_define(text: str, key: str, value: str) -> tuple[str, dict[str, Any]]:
    """
    Replace one #define line while preserving spacing and trailing comment.

    Example:
        #define LTE_Cell_1_EARFCN_DL    300
    becomes:
        #define LTE_Cell_1_EARFCN_DL    2525
    """
    pattern = re.compile(
        rf"^(?P<prefix>\s*#define\s+{re.escape(key)}\s+)(?P<old>\S+)(?P<suffix>.*)$",
        re.MULTILINE,
    )

    matches = list(pattern.finditer(text))
    if len(matches) == 0:
        raise ValueError(f"Define key not found in cfg: {key}")
    if len(matches) > 1:
        raise ValueError(f"Define key duplicated in cfg: {key}")

    match = matches[0]
    old_value = match.group("old")

    def repl(m: re.Match[str]) -> str:
        return f"{m.group('prefix')}{value}{m.group('suffix')}"

    new_text = pattern.sub(repl, text, count=1)
    change = {
        "key": key,
        "old": old_value,
        "new": value,
    }
    return new_text, change


def backup_file(path: Path, backup_dir: Optional[Path] = None) -> Path:
    if not path.exists():
        raise FileNotFoundError(f"Cannot backup missing file: {path}")

    timestamp = datetime.now().strftime("%Y%m%d_%H%M%S")
    backup_name = f"{path.stem}.{timestamp}{path.suffix}.bak"
    target_dir = backup_dir or path.parent
    target_dir.mkdir(parents=True, exist_ok=True)
    backup_path = target_dir / backup_name
    shutil.copy2(path, backup_path)
    return backup_path


def apply_lte_config(
    cfg_path: str | Path,
    earfcn_json_path: str | Path,
    output_path: str | Path,
    cell: int,
    band: int,
    bandwidth_mhz: Optional[float] = None,
    dl_earfcn: Optional[int] = None,
    backup: bool = False,
    backup_dir: Optional[str | Path] = None,
) -> LteApplyResult:
    if not (1 <= int(cell) <= 8):
        raise ValueError("cell must be 1~8")

    cfg = Path(cfg_path)
    output = Path(output_path)
    if not cfg.exists():
        raise FileNotFoundError(f"CFG not found: {cfg}")

    db = LteBandDatabase(earfcn_json_path)
    band_info = db.get_band(int(band))

    final_bandwidth = float(bandwidth_mhz) if bandwidth_mhz is not None else db.get_default_bandwidth(int(band))
    db.validate_bandwidth(band_info, final_bandwidth)

    final_dl_earfcn = int(dl_earfcn) if dl_earfcn is not None else band_info.earfcn_middle
    db.validate_dl_earfcn(band_info, final_dl_earfcn)

    rb_dl = RB_MAP[final_bandwidth]

    if band_info.mode not in MODE_TO_LTE_TDD_CELL_VALUE:
        raise ValueError(f"Unsupported LTE mode in JSON: {band_info.mode}")

    text = read_text_keep_encoding(cfg)
    changes: list[dict[str, Any]] = []

    replacements = [
        (f"LTE_Cell_{cell}_EARFCN_DL", str(final_dl_earfcn)),
        (f"LTE_Cell_{cell}_RB_DL", str(rb_dl)),
        (f"LTE_TDD_Cell_{cell}", str(MODE_TO_LTE_TDD_CELL_VALUE[band_info.mode])),
    ]

    for key, value in replacements:
        text, change = replace_define(text, key, value)
        changes.append(change)

    output.parent.mkdir(parents=True, exist_ok=True)

    if backup and output.exists():
        backup_file(output, Path(backup_dir) if backup_dir else None)

    output.write_text(text, encoding="utf-8")

    return LteApplyResult(
        success=True,
        input_cfg=str(cfg),
        output_cfg=str(output),
        cell=int(cell),
        band=int(band),
        mode=band_info.mode,
        dl_earfcn=final_dl_earfcn,
        bandwidth_mhz=final_bandwidth,
        rb_dl=rb_dl,
        changes=changes,
    )


def result_to_dict(result: LteApplyResult) -> dict[str, Any]:
    return {
        "success": result.success,
        "input_cfg": result.input_cfg,
        "output_cfg": result.output_cfg,
        "cell": result.cell,
        "band": result.band,
        "mode": result.mode,
        "dl_earfcn": result.dl_earfcn,
        "bandwidth_mhz": result.bandwidth_mhz,
        "rb_dl": result.rb_dl,
        "changes": result.changes,
    }


def build_arg_parser() -> argparse.ArgumentParser:
    parser = argparse.ArgumentParser(
        description="Modify Amarisoft LTE AutoConfig.cfg by LTE Band / EARFCN / Bandwidth."
    )
    parser.add_argument("--cfg", required=True, help="Input Amarisoft cfg path")
    parser.add_argument("--earfcn-json", required=True, help="Earfcn_LTE.json path")
    parser.add_argument("--output", required=True, help="Output cfg path")
    parser.add_argument("--cell", type=int, default=1, help="LTE cell index, 1~8. Default: 1")
    parser.add_argument("--band", type=int, required=True, help="LTE band, e.g. 5")
    parser.add_argument("--bandwidth", type=float, default=None, help="LTE bandwidth MHz, e.g. 10. Default: max supported bandwidth from JSON")
    parser.add_argument("--dl-earfcn", type=int, default=None, help="Optional DL EARFCN. Default: Earfcn_Middle from JSON")
    parser.add_argument("--backup", action="store_true", help="Backup output file first if output exists")
    parser.add_argument("--backup-dir", default=None, help="Backup directory")
    return parser


def main() -> int:
    parser = build_arg_parser()
    args = parser.parse_args()

    try:
        result = apply_lte_config(
            cfg_path=args.cfg,
            earfcn_json_path=args.earfcn_json,
            output_path=args.output,
            cell=args.cell,
            band=args.band,
            bandwidth_mhz=args.bandwidth,
            dl_earfcn=args.dl_earfcn,
            backup=args.backup,
            backup_dir=args.backup_dir,
        )
        print(json.dumps(result_to_dict(result), ensure_ascii=False, indent=2))
        return 0
    except Exception as exc:
        error = {
            "success": False,
            "error": str(exc),
        }
        print(json.dumps(error, ensure_ascii=False, indent=2))
        return 1


if __name__ == "__main__":
    raise SystemExit(main())
