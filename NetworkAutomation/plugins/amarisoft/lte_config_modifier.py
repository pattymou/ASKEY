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

try:
    from plugins.amarisoft.sdr_config import apply_sdr_defines
except ModuleNotFoundError:
    from sdr_config import apply_sdr_defines


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
    mimo_dl: int | None
    mimo_ul: int | None
    modulation_dl: str | None
    modulation_ul: str | None
    mcs_dl: int | None
    mcs_ul: int | None
    changes: list[dict[str, Any]]
    sdr_allocation: dict[str, Any] | None = None


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



def parse_mimo(value: Optional[str], direction: str) -> Optional[int]:
    if value is None:
        return None
    text = str(value).strip().lower().replace(" ", "")
    match = re.fullmatch(r"([124])(?:x([124]))?", text)
    if not match:
        raise ValueError(f"MIMO {direction} 必須是 1x1、2x2 或 4x4")
    first = int(match.group(1))
    second = int(match.group(2) or match.group(1))
    # GUI stores one antenna count per direction. Require a symmetric notation
    # so a value such as 4x2 cannot be silently misinterpreted.
    if first != second:
        raise ValueError(f"MIMO {direction} 目前只支援 1x1、2x2 或 4x4")
    return first


def parse_modulation(value: Optional[str], direction: str) -> tuple[Optional[str], Optional[str]]:
    if value is None:
        return None, None
    normalized = str(value).strip().lower().replace("-", "")
    allowed = {"qam64", "64qam", "64", "qam256", "256qam", "256"}
    if normalized not in allowed:
        raise ValueError(f"Modulation {direction} 只支援 qam64 或 qam256")
    canonical = "qam256" if "256" in normalized else "qam64"
    # Amarisoft GUI maps LTE DL/UL qam256 -> true, qam64 -> false.
    return canonical, "true" if canonical == "qam256" else "false"


def parse_mcs(value: Optional[str], direction: str) -> Optional[int]:
    if value is None:
        return None
    text = str(value).strip()
    if text.lower() == "best":
        return -1
    try:
        number = int(text)
    except ValueError as exc:
        raise ValueError(f"MCS {direction} 必須是 Best 或整數") from exc
    if not 0 <= number <= 28:
        raise ValueError(f"MCS {direction} 必須介於 0~28，或使用 Best")
    return number


def apply_lte_config(
    cfg_path: str | Path,
    earfcn_json_path: str | Path,
    output_path: str | Path,
    cell: int,
    band: int,
    bandwidth_mhz: Optional[float] = None,
    dl_earfcn: Optional[int] = None,
    mimo_dl: Optional[str] = None,
    mimo_ul: Optional[str] = None,
    modulation_dl: Optional[str] = None,
    modulation_ul: Optional[str] = None,
    mcs_dl: Optional[str] = None,
    mcs_ul: Optional[str] = None,
    backup: bool = False,
    backup_dir: Optional[str | Path] = None,
    amarisoft_model: str = "100M",
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
    final_mimo_dl = parse_mimo(mimo_dl, "DL")
    final_mimo_ul = parse_mimo(mimo_ul, "UL")
    final_modulation_dl, dl_qam_value = parse_modulation(modulation_dl, "DL")
    final_modulation_ul, ul_qam_value = parse_modulation(modulation_ul, "UL")
    final_mcs_dl = parse_mcs(mcs_dl, "DL")
    final_mcs_ul = parse_mcs(mcs_ul, "UL")

    if band_info.mode not in MODE_TO_LTE_TDD_CELL_VALUE:
        raise ValueError(f"Unsupported LTE mode in JSON: {band_info.mode}")

    text = read_text_keep_encoding(cfg)
    changes: list[dict[str, Any]] = []

    replacements = [
        (f"LTE_Cell_{cell}_EARFCN_DL", str(final_dl_earfcn)),
        (f"LTE_Cell_{cell}_RB_DL", str(rb_dl)),
        (f"LTE_TDD_Cell_{cell}", str(MODE_TO_LTE_TDD_CELL_VALUE[band_info.mode])),
    ]
    optional_replacements = [
        (f"LTE_Cell_{cell}_ANTENNA_DL", final_mimo_dl),
        (f"LTE_Cell_{cell}_ANTENNA_UL", final_mimo_ul),
        (f"LTE_Cell_{cell}_DLQAM", dl_qam_value),
        (f"LTE_Cell_{cell}_ULQAM", ul_qam_value),
        (f"LTE_Cell_{cell}_DLMCS", final_mcs_dl),
        (f"LTE_Cell_{cell}_ULMCS", final_mcs_ul),
    ]
    replacements.extend((key, str(value)) for key, value in optional_replacements if value is not None)

    for key, value in replacements:
        text, change = replace_define(text, key, value)
        changes.append(change)

    text, sdr, sdr_changes = apply_sdr_defines(text, amarisoft_model)
    changes.extend(sdr_changes)

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
        mimo_dl=final_mimo_dl,
        mimo_ul=final_mimo_ul,
        modulation_dl=final_modulation_dl,
        modulation_ul=final_modulation_ul,
        mcs_dl=final_mcs_dl,
        mcs_ul=final_mcs_ul,
        changes=changes,
        sdr_allocation={
            "model": sdr.model,
            "lte_cells": sdr.lte_cells,
            "nr_cells": sdr.nr_cells,
            "lte_sdr_count": sdr.lte_sdr_count,
            "nr_sdr_count": sdr.nr_sdr_count,
            "total_sdr_count": sdr.total_sdr_count,
            "max_sdr_count": sdr.max_sdr_count,
            "sdr_card": sdr.sdr_card,
            "per_cell": sdr.per_cell,
        },
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
        "mimo_dl": result.mimo_dl,
        "mimo_ul": result.mimo_ul,
        "modulation_dl": result.modulation_dl,
        "modulation_ul": result.modulation_ul,
        "mcs_dl": "Best" if result.mcs_dl == -1 else result.mcs_dl,
        "mcs_ul": "Best" if result.mcs_ul == -1 else result.mcs_ul,
        "changes": result.changes,
        "sdr_allocation": result.sdr_allocation,
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
    parser.add_argument("--mimo-dl", default=None, help="Optional LTE DL MIMO: 1x1, 2x2, 4x4")
    parser.add_argument("--mimo-ul", default=None, help="Optional LTE UL MIMO: 1x1, 2x2, 4x4")
    parser.add_argument("--modulation-dl", default=None, help="Optional LTE DL modulation: qam64 or qam256")
    parser.add_argument("--modulation-ul", default=None, help="Optional LTE UL modulation: qam64 or qam256")
    parser.add_argument("--mcs-dl", default=None, help="Optional LTE DL MCS: Best or 0~28")
    parser.add_argument("--mcs-ul", default=None, help="Optional LTE UL MCS: Best or 0~28")
    parser.add_argument("--backup", action="store_true", help="Backup output file first if output exists")
    parser.add_argument("--backup-dir", default=None, help="Backup directory")
    parser.add_argument("--amarisoft-model", default="100M", choices=["50M", "100M", "50", "100"], help="Amarisoft SDR card model")
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
            mimo_dl=args.mimo_dl,
            mimo_ul=args.mimo_ul,
            modulation_dl=args.modulation_dl,
            modulation_ul=args.modulation_ul,
            mcs_dl=args.mcs_dl,
            mcs_ul=args.mcs_ul,
            backup=args.backup,
            backup_dir=args.backup_dir,
            amarisoft_model=args.amarisoft_model,
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
