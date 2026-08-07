from __future__ import annotations

import argparse
import json
import math
import re
from dataclasses import asdict, dataclass, field
from pathlib import Path
from typing import Any, Optional, Sequence

try:
    from plugins.amarisoft.sdr_config import apply_sdr_defines
except ModuleNotFoundError:
    from sdr_config import apply_sdr_defines

GLOBAL_RASTER = [
    (0.0, 3000.0, 0.005, 0.0, 0),
    (3000.0, 24250.0, 0.015, 3000.0, 600000),
]
TIME_SLOT_MAP = {
    "DDDDDDDSUU": 2,
    "DDDDDDSUUU": 3,
    "DDDSUU_DDDD": 4,
    "DDDSU_DDSUU": 5,
    "DDDDDDDDSU": 6,
}


@dataclass(frozen=True)
class NrCarrierCalculation:
    carrier_index: int
    band: int
    ca_class: str
    duplex_type: str
    bandwidth_mhz: float
    subcarrier_spacing_khz: int
    channel_raster_khz: int
    center_frequency_mhz: float
    nr_arfcn: int


@dataclass(frozen=True)
class NrCalculationResult:
    normalized_input: str
    bcs_skeleton: str
    carriers: list[NrCarrierCalculation]


@dataclass(frozen=True)
class NrBandwidthPolicy:
    band_skeleton: str
    uses_bcs_allowed: bool
    allowed_profiles_mhz: list[list[float]]
    per_carrier_allowed_bandwidths_mhz: list[list[float]]


@dataclass(frozen=True)
class NrApplyResult:
    success: bool
    mode: str
    input_cfg: str
    output_cfg: str
    cell: int
    band: int
    duplex_type: str
    bandwidth_mhz: float
    nr_arfcn: int
    center_frequency_mhz: float
    mimo_dl: Optional[int]
    mimo_ul: Optional[int]
    modulation_dl: Optional[str]
    modulation_ul: Optional[str]
    mcs_dl: Optional[int]
    mcs_ul: Optional[int]
    time_slot: Optional[str]
    tdd_config: Optional[int]
    changes: list[dict[str, Any]]
    subcarrier_spacing_khz: int = 0
    channel_raster_khz: int = 0
    bcs_skeleton: str = ""
    sdr_allocation: dict[str, Any] | None = None


@dataclass(frozen=True)
class NrMultiApplyResult:
    success: bool
    mode: str
    input_cfg: str
    output_cfg: str
    start_cell: int
    nr_cell_count: int
    normalized_input: str
    bcs_skeleton: str
    carriers: list[NrCarrierCalculation]
    changes: list[dict[str, Any]] = field(default_factory=list)
    sdr_allocation: dict[str, Any] | None = None


def _load_spec(path: Path) -> dict[str, Any]:
    if not path.exists():
        raise FileNotFoundError(f"找不到 NR calculator 規格檔: {path}")
    raw = json.loads(path.read_text(encoding="utf-8-sig"))
    bands = raw.get("bands")
    if not isinstance(bands, dict):
        raise ValueError("nr_spec.json 缺少 bands。")
    raw["bcs_allowed"] = [
        re.sub(r"\s+", "", str(item))
        for item in raw.get("bcs_allowed", [])
        if str(item).strip()
    ]
    return raw


def _band_spec(raw: dict[str, Any], band: int | str) -> dict[str, Any]:
    key = str(band).lower()
    if not key.startswith("n"):
        key = f"n{key}"
    spec = raw["bands"].get(key)
    if not isinstance(spec, dict) or not spec.get("allowed_bws"):
        raise ValueError(f"nr_spec.json 不支援或查無 NR band: {key}")
    if float(spec.get("start_freq", 0)) <= 0 or int(spec.get("raster_khz", 0)) <= 0:
        raise ValueError(f"nr_spec.json 的 {key} 缺少 start_freq 或 raster_khz。")
    return spec


def _fmt(v: float) -> str:
    return str(int(v)) if float(v).is_integer() else f"{v:g}"


def _parse_bandwidth(value: str | float | int) -> float:
    text = re.sub(r"MHz", "", str(value), flags=re.I).strip()
    try:
        return float(text)
    except ValueError as exc:
        raise ValueError(f"NR bandwidth 數值解析錯誤: {value}") from exc


def _align(freq_mhz: float, raster_khz: int) -> float:
    if raster_khz <= 0:
        raise ValueError("Channel raster 必須大於 0 kHz。")
    step = raster_khz / 1000.0
    # C# Math.Round uses banker's rounding. Python round has the same tie policy.
    return round(freq_mhz / step) * step


def _freq_to_arfcn(freq_mhz: float) -> int:
    for fmin, fmax, delta, offset, nref in GLOBAL_RASTER:
        if fmin <= freq_mhz <= fmax:
            raw = nref + (freq_mhz - offset) / delta
            # Match C# MidpointRounding.AwayFromZero used in NrArfcnCalculator.cs.
            return int(math.floor(raw + 0.5)) if raw >= 0 else int(math.ceil(raw - 0.5))
    raise ValueError("中心頻率超出 nr_spec calculator 的 FR1 global raster 範圍。")


def _arfcn_to_freq(arfcn: int) -> float:
    for fmin, fmax, delta, offset, nref in GLOBAL_RASTER:
        max_arfcn = nref + (fmax - offset) / delta
        if nref <= arfcn <= max_arfcn:
            return offset + (arfcn - nref) * delta
    raise ValueError("NR-ARFCN 超出 nr_spec calculator 的 FR1 global raster 範圍。")


def _resolve_scs(spec: dict[str, Any], bandwidth_mhz: float) -> int:
    key = _fmt(bandwidth_mhz)
    mapping = spec.get("bandwidth_scs") or {}
    supported = [int(x) for x in mapping.get(key, [])]
    preferred = int(spec.get("default_scs_khz") or 0)
    legacy = int(spec.get("subcarrier_spacing_khz") or 0)
    if supported:
        if preferred in supported:
            return preferred
        if legacy in supported:
            return legacy
        mode_default = 30 if str(spec.get("type", "")).upper() == "TDD" else 15
        if mode_default in supported:
            return mode_default
        return supported[0]
    if preferred > 0:
        return preferred
    if legacy > 0:
        return legacy
    return 30 if str(spec.get("type", "")).upper() == "TDD" else 15


def _resolve_channel_raster(spec: dict[str, Any], scs_khz: int) -> int:
    mapping = spec.get("channel_raster_by_scs_khz") or {}
    value = mapping.get(str(scs_khz))
    if value is not None and int(value) > 0:
        return int(value)
    return int(spec["raster_khz"])


def _split_bcs(entry: str) -> tuple[str, str] | None:
    if ":" not in entry:
        return None
    skeleton, bandwidths = entry.split(":", 1)
    if not skeleton or not bandwidths:
        return None
    return skeleton, bandwidths


def _parse_component(component: str) -> tuple[str, str, list[float]]:
    match = re.fullmatch(r"(n\d+)([A-Za-z])(?:\((.*?)\))?", component, flags=re.I)
    if not match:
        raise ValueError(f"NR calculator 語法錯誤，無法解析: {component}")
    band = match.group(1).lower()
    ca_class = match.group(2).upper()
    if ca_class not in {"A", "C"}:
        raise ValueError(f"nr_calculator 目前只支援 NR CA Class A 與 Class C: {band}{ca_class}")
    bandwidth_text = match.group(3)
    bandwidths = [] if bandwidth_text is None else [_parse_bandwidth(x) for x in bandwidth_text.split("+")]
    return band, ca_class, bandwidths


def get_bandwidth_policy(user_band_skeleton: str, nr_spec_path: str | Path) -> NrBandwidthPolicy:
    raw = _load_spec(Path(nr_spec_path))
    clean = user_band_skeleton.replace("_", "-").replace(" ", "").strip()
    components = clean.split("-")
    normalized_parts: list[str] = []
    fallback: list[list[float]] = []
    expected_count = 0
    for raw_component in components:
        band, ca_class, _ = _parse_component(raw_component)
        spec = _band_spec(raw, band)
        count = 1 if ca_class == "A" else 2
        normalized_parts.append(f"{band}{ca_class}")
        allowed = [float(x) for x in spec["allowed_bws"]]
        fallback.extend([list(allowed) for _ in range(count)])
        expected_count += count

    skeleton = "-".join(normalized_parts)
    profiles: list[list[float]] = []
    for entry in raw["bcs_allowed"]:
        split = _split_bcs(entry)
        if not split or split[0].lower() != skeleton.lower():
            continue
        profile = [_parse_bandwidth(x) for x in split[1].split("+")]
        if len(profile) != expected_count:
            raise ValueError(
                f"nr_spec.json 的 bcs_allowed carrier 數量錯誤: {entry}，"
                f"預期 {expected_count} 個，實際 {len(profile)} 個。"
            )
        for idx, bandwidth in enumerate(profile):
            if not any(abs(x - bandwidth) < 1e-6 for x in fallback[idx]):
                raise ValueError(f"nr_spec.json 的 bcs_allowed 與 bands.allowed_bws 不一致: {entry}")
        profiles.append(profile)

    if profiles:
        per_carrier = []
        for idx in range(expected_count):
            values: list[float] = []
            for profile in profiles:
                if not any(abs(existing - profile[idx]) < 1e-6 for existing in values):
                    values.append(profile[idx])
            per_carrier.append(values)
    else:
        per_carrier = fallback

    return NrBandwidthPolicy(skeleton, bool(profiles), profiles, per_carrier)


def calculate_nr(user_input: str, nr_spec_path: str | Path) -> NrCalculationResult:
    if not str(user_input).strip():
        raise ValueError("NR calculator 輸入不能為空。")
    raw = _load_spec(Path(nr_spec_path))
    bcs_set = {entry.lower() for entry in raw["bcs_allowed"]}
    clean = str(user_input).replace("_", "-").replace(" ", "").strip()

    # Accept both calculator form n28A(20)-n78A(100) and the compact
    # BCS-like form n28A-n78A(20+100) / n28A-n78A:20+100.
    if ":" in clean and "(" not in clean:
        skeleton_text, aggregate_bw = clean.split(":", 1)
        clean = skeleton_text + "(" + aggregate_bw + ")"
    compact_parts = clean.split("-")
    if len(compact_parts) > 1 and all("(" not in part for part in compact_parts[:-1]) and "(" in compact_parts[-1]:
        last_match = re.fullmatch(r"(n\d+[A-Za-z])\((.*?)\)", compact_parts[-1], flags=re.I)
        if last_match:
            aggregate = [_parse_bandwidth(x) for x in last_match.group(2).split("+")]
            skeleton_parts = compact_parts[:-1] + [last_match.group(1)]
            expected_counts = []
            for skeleton_part in skeleton_parts:
                match = re.fullmatch(r"n\d+([A-Za-z])", skeleton_part, flags=re.I)
                if not match or match.group(1).upper() not in {"A", "C"}:
                    break
                expected_counts.append(1 if match.group(1).upper() == "A" else 2)
            if len(expected_counts) == len(skeleton_parts) and sum(expected_counts) == len(aggregate):
                rebuilt = []
                index = 0
                for skeleton_part, count in zip(skeleton_parts, expected_counts):
                    values = aggregate[index:index + count]
                    rebuilt.append(f"{skeleton_part}({'+'.join(_fmt(x) for x in values)})")
                    index += count
                clean = "-".join(rebuilt)

    parsed: list[tuple[str, str, str, list[float]]] = []
    band_skeletons: list[str] = []
    bandwidth_skeletons: list[str] = []
    normalized: list[str] = []

    for raw_component in clean.split("-"):
        band, ca_class, bandwidths = _parse_component(raw_component)
        spec = _band_spec(raw, band)
        expected = 1 if ca_class == "A" else 2
        if not bandwidths:
            maximum = float(spec["allowed_bws"][-1])
            bandwidths = [maximum] * expected
        if len(bandwidths) != expected:
            raise ValueError(f"{band}{ca_class} 應有 {expected} 個 bandwidth，實際收到 {len(bandwidths)} 個。")
        allowed = [float(x) for x in spec["allowed_bws"]]
        for bw in bandwidths:
            if not any(abs(x - bw) < 1e-6 for x in allowed):
                raise ValueError(
                    f"[3GPP bandwidth 拒絕] {band} 不支援 {_fmt(bw)} MHz；"
                    f"合法值: {','.join(_fmt(x) for x in allowed)}"
                )
        parsed.append((band, ca_class, str(spec.get("type", "")).upper(), bandwidths))
        band_skeletons.append(f"{band}{ca_class}")
        bandwidth_skeletons.append("+".join(_fmt(x) for x in bandwidths))
        normalized.append(f"{band}{ca_class}({'+'.join(_fmt(x) for x in bandwidths)})")

    band_skeleton = "-".join(band_skeletons)
    full_skeleton = f"{band_skeleton}:{'+'.join(bandwidth_skeletons)}"
    has_bcs_policy = any(
        split and split[0].lower() == band_skeleton.lower()
        for split in (_split_bcs(x) for x in raw["bcs_allowed"])
    )
    if has_bcs_policy and full_skeleton.lower() not in bcs_set:
        raise ValueError(f"[3GPP BCS 拒絕] nr_spec.json 不支援此組合: {full_skeleton}")

    carriers: list[NrCarrierCalculation] = []
    band_history: dict[str, int] = {}
    carrier_index = 1
    for band, ca_class, duplex, bandwidths in parsed:
        spec = _band_spec(raw, band)
        previous_count = band_history.get(band, 0)
        frequency_offset = previous_count * (float(spec["allowed_bws"][-1]) + 15.0)
        band_history[band] = previous_count + len(bandwidths)
        current_start = float(spec["start_freq"]) + frequency_offset

        centers: list[tuple[float, int, int]] = []
        first_bw = bandwidths[0]
        first_scs = _resolve_scs(spec, first_bw)
        first_raster = _resolve_channel_raster(spec, first_scs)
        first_center = _align(current_start + first_bw / 2.0, first_raster)
        centers.append((first_center, first_scs, first_raster))
        if ca_class == "C":
            second_bw = bandwidths[1]
            second_scs = _resolve_scs(spec, second_bw)
            second_raster = _resolve_channel_raster(spec, second_scs)
            second_ideal = first_center + first_bw / 2.0 + second_bw / 2.0
            second_center = _align(second_ideal, second_raster)
            centers.append((second_center, second_scs, second_raster))

        for bw, (center, scs, channel_raster) in zip(bandwidths, centers):
            arfcn = _freq_to_arfcn(center)
            # Match C#: report the exact global-raster frequency represented by ARFCN.
            center = _arfcn_to_freq(arfcn)
            carriers.append(
                NrCarrierCalculation(
                    carrier_index=carrier_index,
                    band=int(band[1:]),
                    ca_class=ca_class,
                    duplex_type=duplex,
                    bandwidth_mhz=bw,
                    subcarrier_spacing_khz=scs,
                    channel_raster_khz=channel_raster,
                    center_frequency_mhz=center,
                    nr_arfcn=arfcn,
                )
            )
            carrier_index += 1

    return NrCalculationResult("-".join(normalized), full_skeleton, carriers)


def calculate_default_arfcn(
    spec: dict[str, Any], bandwidth_mhz: float
) -> tuple[float, int]:
    """Backward-compatible single-carrier helper using the unified C# rules."""
    scs = _resolve_scs(spec, bandwidth_mhz)
    channel_raster = _resolve_channel_raster(spec, scs)
    center = _align(float(spec["start_freq"]) + bandwidth_mhz / 2.0, channel_raster)
    arfcn = _freq_to_arfcn(center)
    return _arfcn_to_freq(arfcn), arfcn


def replace_define(text: str, key: str, value: str) -> tuple[str, dict[str, Any]]:
    pattern = re.compile(rf"^(?P<prefix>\s*#define\s+{re.escape(key)}\s+)(?P<old>\S+)(?P<suffix>.*)$", re.M)
    matches = list(pattern.finditer(text))
    if not matches:
        raise ValueError(f"Define key not found in cfg: {key}")
    if len(matches) > 1:
        raise ValueError(f"Define key duplicated in cfg: {key}")
    old = matches[0].group("old")
    new = pattern.sub(lambda m: f"{m.group('prefix')}{value}{m.group('suffix')}", text, count=1)
    return new, {"key": key, "old": old, "new": value}


def parse_mimo(value: Optional[str], direction: str) -> Optional[int]:
    if value is None:
        return None
    text = str(value).strip().lower().replace(" ", "")
    match = re.fullmatch(r"([124])(?:x([124]))?", text)
    if not match or int(match.group(1)) != int(match.group(2) or match.group(1)):
        raise ValueError(f"MIMO {direction} 必須是 1x1、2x2 或 4x4")
    return int(match.group(1))


def parse_modulation(value: Optional[str], direction: str) -> Optional[str]:
    if value is None:
        return None
    text = str(value).strip().lower().replace("-", "")
    aliases = {"qam256": "qam256", "256qam": "qam256", "256": "qam256", "qam64": "qam64", "64qam": "qam64", "64": "qam64"}
    if direction == "UL":
        aliases = {"qam16": "qam16", "16qam": "qam16", "16": "qam16", "qam64": "qam64", "64qam": "qam64", "64": "qam64"}
    if text not in aliases:
        allowed = "qam64/qam256" if direction == "DL" else "qam16/qam64"
        raise ValueError(f"NR Modulation {direction} 只支援 {allowed}")
    return aliases[text]


def parse_mcs(value: Optional[str], direction: str) -> Optional[int]:
    if value is None:
        return None
    text = str(value).strip()
    if text.lower() == "best":
        return -1
    try:
        number = int(text)
    except ValueError as exc:
        raise ValueError(f"NR MCS {direction} 必須是 Best 或整數") from exc
    if not 0 <= number <= 28:
        raise ValueError(f"NR MCS {direction} 必須介於 0~28，或使用 Best")
    return number


def normalize_time_slot(value: Optional[str], duplex: str) -> tuple[Optional[str], Optional[int]]:
    if str(duplex).upper() != "TDD":
        if value is not None:
            raise ValueError("NR FDD band 沒有 Time Slot；請移除 time slot 參數。")
        return None, None
    if value is None:
        return "DDDSU_DDSUU", 5
    text = str(value).strip().upper().replace("-", "_").replace(" ", "")
    if text.isdigit() and int(text) in TIME_SLOT_MAP.values():
        code = int(text)
        name = next(key for key, item in TIME_SLOT_MAP.items() if item == code)
        return name, code
    if text not in TIME_SLOT_MAP:
        raise ValueError("Time Slot 只支援 DDDDDDDSUU、DDDDDDSUUU、DDDSUU_DDDD、DDDSU_DDSUU、DDDDDDDDSU（或代碼 2~6）。")
    return text, TIME_SLOT_MAP[text]


def _resolve_single_band_input(raw: dict[str, Any], band: int, bandwidth_mhz: Optional[float]) -> str:
    skeleton = f"n{int(band)}A"
    policy = get_bandwidth_policy(skeleton, _CURRENT_SPEC_PATH.get())
    if bandwidth_mhz is None:
        bw = policy.allowed_profiles_mhz[0][0] if policy.uses_bcs_allowed else max(policy.per_carrier_allowed_bandwidths_mhz[0])
    else:
        bw = float(bandwidth_mhz)
    allowed = policy.per_carrier_allowed_bandwidths_mhz[0]
    if not any(abs(item - bw) < 1e-6 for item in allowed):
        raise ValueError(
            f"n{band}A 不支援 {_fmt(bw)} MHz；可用頻寬："
            f"{', '.join(_fmt(x) for x in allowed)} MHz。"
        )
    return f"{skeleton}({_fmt(bw)})"


class _SpecPathContext:
    def __init__(self) -> None:
        self._value: Optional[Path] = None

    def set(self, value: Path) -> None:
        self._value = value

    def get(self) -> Path:
        if self._value is None:
            raise RuntimeError("NR spec path context 尚未初始化。")
        return self._value


_CURRENT_SPEC_PATH = _SpecPathContext()


def apply_nr_config(
    cfg_path: str | Path,
    nr_spec_path: str | Path,
    output_path: str | Path,
    mode: str,
    cell: int,
    band: int,
    bandwidth_mhz: Optional[float] = None,
    nr_arfcn: Optional[int] = None,
    mimo_dl: Optional[str] = None,
    mimo_ul: Optional[str] = None,
    modulation_dl: Optional[str] = None,
    modulation_ul: Optional[str] = None,
    mcs_dl: Optional[str] = None,
    mcs_ul: Optional[str] = None,
    time_slot: Optional[str] = None,
    amarisoft_model: str = "100M",
) -> NrApplyResult:
    mode = str(mode).upper()
    if mode not in {"SA", "ENDC"}:
        raise ValueError("mode 必須是 SA 或 ENDC")
    if not 1 <= int(cell) <= 3:
        raise ValueError("NR cell 必須是 1~3")
    cfg, spec_path, out = Path(cfg_path), Path(nr_spec_path), Path(output_path)
    if not cfg.exists():
        raise FileNotFoundError(f"CFG not found: {cfg}")
    _CURRENT_SPEC_PATH.set(spec_path)
    raw = _load_spec(spec_path)
    calculator_input = _resolve_single_band_input(raw, int(band), bandwidth_mhz)
    calculation = calculate_nr(calculator_input, spec_path)
    carrier = calculation.carriers[0]
    arfcn = int(nr_arfcn) if nr_arfcn is not None else carrier.nr_arfcn
    duplex = carrier.duplex_type
    dl_mimo, ul_mimo = parse_mimo(mimo_dl, "DL"), parse_mimo(mimo_ul, "UL")
    dl_qam, ul_qam = parse_modulation(modulation_dl, "DL"), parse_modulation(modulation_ul, "UL")
    dl_mcs, ul_mcs = parse_mcs(mcs_dl, "DL"), parse_mcs(mcs_ul, "UL")
    ts, tdd_cfg = normalize_time_slot(time_slot, duplex)
    text = cfg.read_text(encoding="utf-8")
    changes: list[dict[str, Any]] = []
    replacements = [
        ("NR_CELL", "1"),
        (f"NR_TDD_Cell_{cell}", "1" if duplex == "TDD" else "0"),
        (f"NR_EARFCN_{cell}_DL", str(arfcn)),
        (f"NR_BANDWIDTH_{cell}", _fmt(carrier.bandwidth_mhz)),
        (f"NR_BAND_{cell}", str(int(band))),
    ]
    optional = [
        (f"NR_ANTENNA_DL_{cell}", dl_mimo),
        (f"NR_ANTENNA_UL_{cell}", ul_mimo),
        (f"NR_DLQAM_{cell}", f'"{dl_qam}"' if dl_qam else None),
        (f"NR_ULQAM_{cell}", f'"{ul_qam}"' if ul_qam else None),
        (f"NR_DLMCS_{cell}", dl_mcs),
        (f"NR_ULMCS_{cell}", ul_mcs),
    ]
    replacements += [(key, str(value)) for key, value in optional if value is not None]
    if duplex == "TDD" and tdd_cfg is not None:
        replacements.append(("NR_TDD_CONFIG", str(tdd_cfg)))
    for key, value in replacements:
        text, change = replace_define(text, key, value)
        changes.append(change)
    text, sdr, sdr_changes = apply_sdr_defines(text, amarisoft_model)
    changes.extend(sdr_changes)
    out.parent.mkdir(parents=True, exist_ok=True)
    out.write_text(text, encoding="utf-8")
    return NrApplyResult(
        True,
        mode,
        str(cfg),
        str(out),
        int(cell),
        int(band),
        duplex,
        carrier.bandwidth_mhz,
        arfcn,
        _arfcn_to_freq(arfcn),
        dl_mimo,
        ul_mimo,
        dl_qam,
        ul_qam,
        dl_mcs,
        ul_mcs,
        ts,
        tdd_cfg,
        changes,
        carrier.subcarrier_spacing_khz,
        carrier.channel_raster_khz,
        calculation.bcs_skeleton,
        {
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


def apply_nr_band_config(
    cfg_path: str | Path,
    nr_spec_path: str | Path,
    output_path: str | Path,
    mode: str,
    band_config: str,
    start_cell: int = 1,
    amarisoft_model: str = "100M",
) -> NrMultiApplyResult:
    """Apply a full A/C/inter-band NR combination to consecutive NR cells.

    Examples: n78A(100), n78C(100+100), n28A-n78A(20+100).
    This is the Python equivalent of NrArfcnCalculator.TryCalculate().
    """
    mode = str(mode).upper()
    if mode not in {"SA", "ENDC"}:
        raise ValueError("mode 必須是 SA 或 ENDC")
    cfg, spec_path, out = Path(cfg_path), Path(nr_spec_path), Path(output_path)
    if not cfg.exists():
        raise FileNotFoundError(f"CFG not found: {cfg}")
    calculation = calculate_nr(band_config, spec_path)
    if start_cell < 1 or start_cell + len(calculation.carriers) - 1 > 3:
        raise ValueError("NR 組合超過 CFG 可用的 Cell 1~3 範圍。")
    text = cfg.read_text(encoding="utf-8")
    changes: list[dict[str, Any]] = []
    text, change = replace_define(text, "NR_CELL", str(len(calculation.carriers)))
    changes.append(change)
    for offset, carrier in enumerate(calculation.carriers):
        cell = start_cell + offset
        for key, value in [
            (f"NR_TDD_Cell_{cell}", "1" if carrier.duplex_type == "TDD" else "0"),
            (f"NR_EARFCN_{cell}_DL", str(carrier.nr_arfcn)),
            (f"NR_BANDWIDTH_{cell}", _fmt(carrier.bandwidth_mhz)),
            (f"NR_BAND_{cell}", str(carrier.band)),
        ]:
            text, change = replace_define(text, key, value)
            changes.append(change)
    text, sdr, sdr_changes = apply_sdr_defines(text, amarisoft_model)
    changes.extend(sdr_changes)
    out.parent.mkdir(parents=True, exist_ok=True)
    out.write_text(text, encoding="utf-8")
    return NrMultiApplyResult(
        True,
        mode,
        str(cfg),
        str(out),
        start_cell,
        len(calculation.carriers),
        calculation.normalized_input,
        calculation.bcs_skeleton,
        calculation.carriers,
        changes,
        {
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


def main() -> int:
    parser = argparse.ArgumentParser()
    parser.add_argument("--cfg", required=True)
    parser.add_argument("--nr-spec", required=True)
    parser.add_argument("--output", required=True)
    parser.add_argument("--mode", required=True, choices=["SA", "ENDC", "sa", "endc"])
    parser.add_argument("--cell", type=int, default=1)
    group = parser.add_mutually_exclusive_group(required=True)
    group.add_argument("--band", type=int)
    group.add_argument("--band-config", help="例如 n78A(100)、n78C(100+100)、n28A-n78A(20+100)")
    parser.add_argument("--bandwidth", type=float)
    parser.add_argument("--nr-arfcn", type=int)
    parser.add_argument("--mimo-dl")
    parser.add_argument("--mimo-ul")
    parser.add_argument("--modulation-dl")
    parser.add_argument("--modulation-ul")
    parser.add_argument("--mcs-dl")
    parser.add_argument("--mcs-ul")
    parser.add_argument("--time-slot")
    parser.add_argument("--amarisoft-model", default="100M", choices=["50M", "100M", "50", "100"])
    args = parser.parse_args()
    try:
        if args.band_config:
            if any(value is not None for value in (args.bandwidth, args.nr_arfcn, args.mimo_dl, args.mimo_ul, args.modulation_dl, args.modulation_ul, args.mcs_dl, args.mcs_ul, args.time_slot)):
                raise ValueError("--band-config 模式目前不接受單一 Cell 的 bandwidth/ARFCN/MIMO/QAM/MCS/time-slot 參數。")
            result: Any = apply_nr_band_config(
                args.cfg, args.nr_spec, args.output, args.mode, args.band_config, args.cell,
                amarisoft_model=args.amarisoft_model,
            )
        else:
            result = apply_nr_config(
                args.cfg,
                args.nr_spec,
                args.output,
                args.mode,
                args.cell,
                args.band,
                args.bandwidth,
                args.nr_arfcn,
                args.mimo_dl,
                args.mimo_ul,
                args.modulation_dl,
                args.modulation_ul,
                args.mcs_dl,
                args.mcs_ul,
                args.time_slot,
                amarisoft_model=args.amarisoft_model,
            )
        print(json.dumps(asdict(result), ensure_ascii=False, indent=2))
        return 0
    except Exception as exc:
        print(json.dumps({"success": False, "error": type(exc).__name__, "message": str(exc)}, ensure_ascii=False, indent=2))
        return 1


if __name__ == "__main__":
    raise SystemExit(main())
