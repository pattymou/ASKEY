from __future__ import annotations

import re
from dataclasses import dataclass
from typing import Any


@dataclass(frozen=True)
class SdrAllocation:
    model: str
    lte_cells: int
    nr_cells: int
    lte_sdr_count: int
    nr_sdr_count: int
    total_sdr_count: int
    max_sdr_count: int
    sdr_card: str
    per_cell: list[dict[str, Any]]


def _normalize_model(value: str | None) -> str:
    text = str(value or "100M").strip().upper().replace("HZ", "")
    aliases = {"50": "50M", "50M": "50M", "100": "100M", "100M": "100M"}
    if text not in aliases:
        raise ValueError("Amarisoft model 必須是 50M 或 100M。")
    return aliases[text]


def _read_define(text: str, key: str, *, required: bool = True, default: str | None = None) -> str:
    pattern = re.compile(rf"^\s*#define\s+{re.escape(key)}\s+(?P<value>\S+)", re.MULTILINE)
    matches = list(pattern.finditer(text))
    if len(matches) > 1:
        raise ValueError(f"Define key duplicated in cfg: {key}")
    if not matches:
        if required:
            raise ValueError(f"Define key not found in cfg: {key}")
        if default is None:
            raise ValueError(f"Define key not found in cfg: {key}")
        return default
    return matches[0].group("value").strip().strip('"')


def _replace_define(text: str, key: str, value: str) -> tuple[str, dict[str, Any]]:
    pattern = re.compile(
        rf"^(?P<prefix>\s*#define\s+{re.escape(key)}\s+)(?P<old>\S+)(?P<suffix>.*)$",
        re.MULTILINE,
    )
    matches = list(pattern.finditer(text))
    if not matches:
        raise ValueError(f"Define key not found in cfg: {key}")
    if len(matches) > 1:
        raise ValueError(f"Define key duplicated in cfg: {key}")
    old = matches[0].group("old")
    updated = pattern.sub(lambda m: f"{m.group('prefix')}{value}{m.group('suffix')}", text, count=1)
    return updated, {"key": key, "old": old, "new": value}


def _as_int(text: str, key: str) -> int:
    try:
        return int(float(text))
    except ValueError as exc:
        raise ValueError(f"{key} 不是有效數字: {text}") from exc


def _lte_cards(mimo_dl: int) -> int:
    # Amarisoft GUI v10: LTE 4x4 consumes two SDR cards; 1x1/2x2 consumes one.
    return 2 if mimo_dl >= 4 else 1


def _nr_cards(mimo_dl: int, bandwidth_mhz: float, model: str) -> int:
    # Amarisoft GUI v10 logic:
    # - 100M model: NR 4x4 => 2 cards, 1x1/2x2 => 1 card.
    # - 50M model at 100 MHz doubles that requirement because each card is 50 MHz.
    base = 2 if mimo_dl >= 4 else 1
    if model == "50M" and abs(float(bandwidth_mhz) - 100.0) < 1e-6:
        base *= 2
    return base


def calculate_sdr_allocation(text: str, amarisoft_model: str | None = "100M") -> SdrAllocation:
    model = _normalize_model(amarisoft_model)
    lte_cells = _as_int(_read_define(text, "N_CELL", required=False, default="0"), "N_CELL")
    nr_cells = _as_int(_read_define(text, "NR_CELL", required=False, default="0"), "NR_CELL")
    per_cell: list[dict[str, Any]] = []
    lte_total = 0
    nr_total = 0

    for cell in range(1, lte_cells + 1):
        key = f"LTE_Cell_{cell}_ANTENNA_DL"
        mimo = _as_int(_read_define(text, key), key)
        cards = _lte_cards(mimo)
        lte_total += cards
        per_cell.append({"rat": "LTE", "cell": cell, "mimo_dl": mimo, "sdr_count": cards})

    for cell in range(1, nr_cells + 1):
        mimo_key = f"NR_ANTENNA_DL_{cell}"
        bw_key = f"NR_BANDWIDTH_{cell}"
        mimo = _as_int(_read_define(text, mimo_key), mimo_key)
        try:
            bandwidth = float(_read_define(text, bw_key))
        except ValueError as exc:
            raise ValueError(f"{bw_key} 不是有效數字。") from exc
        cards = _nr_cards(mimo, bandwidth, model)
        nr_total += cards
        per_cell.append(
            {
                "rat": "NR",
                "cell": cell,
                "mimo_dl": mimo,
                "bandwidth_mhz": bandwidth,
                "sdr_count": cards,
            }
        )

    total = lte_total + nr_total
    limit = 8 if model == "100M" else 6
    if total < 1:
        raise ValueError("CFG 沒有啟用任何 LTE/NR cell，無法設定 SDR_Count。")
    if total > limit:
        raise ValueError(
            f"所需 SDR card={total}，超過 Amarisoft {model} 上限 {limit}。"
        )

    card = "".join(f"dev{i}=/dev/sdr{i}," for i in range(total))
    return SdrAllocation(model, lte_cells, nr_cells, lte_total, nr_total, total, limit, card, per_cell)


def apply_sdr_defines(
    text: str,
    amarisoft_model: str | None = "100M",
) -> tuple[str, SdrAllocation, list[dict[str, Any]]]:
    allocation = calculate_sdr_allocation(text, amarisoft_model)
    changes: list[dict[str, Any]] = []
    text, change = _replace_define(text, "SDR_Count", str(allocation.total_sdr_count))
    changes.append(change)
    quoted_card = f'"{allocation.sdr_card}"'
    text, change = _replace_define(text, "SDR_Card", quoted_card)
    changes.append(change)
    return text, allocation, changes
