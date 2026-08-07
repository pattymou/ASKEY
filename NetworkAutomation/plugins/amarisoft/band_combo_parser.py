from __future__ import annotations

import re
from dataclasses import dataclass
from typing import Iterable

_CLASS_CARRIERS = {"A": 1, "C": 2, "D": 3, "E": 4, "F": 5}
_TOKEN_RE = re.compile(r"^(n?)(\d+)([ACDEF])$", re.IGNORECASE)


@dataclass(frozen=True)
class BandToken:
    technology: str  # LTE or NR
    band: int
    ca_class: str
    carrier_count: int

    @property
    def canonical(self) -> str:
        prefix = "n" if self.technology == "NR" else ""
        return f"{prefix}{self.band}{self.ca_class}"


@dataclass(frozen=True)
class RadioCombination:
    raw_band_config: str
    canonical_band_config: str
    lte_tokens: tuple[BandToken, ...]
    nr_tokens: tuple[BandToken, ...]
    lte_bandwidths_mhz: tuple[float, ...]
    nr_bandwidths_mhz: tuple[float, ...]

    @property
    def mode(self) -> str:
        if self.lte_tokens and self.nr_tokens:
            return "ENDC"
        if self.nr_tokens:
            return "SA"
        return "LTE"

    @property
    def nr_calculator_input(self) -> str | None:
        if not self.nr_tokens:
            return None
        bands = "-".join(token.canonical for token in self.nr_tokens)
        bandwidths = "+".join(_format_number(value) for value in self.nr_bandwidths_mhz)
        return f"{bands}({bandwidths})"


def _format_number(value: float) -> str:
    return str(int(value)) if float(value).is_integer() else f"{value:g}"


def _clean_band_text(value: str) -> str:
    text = str(value or "").strip().replace(" ", "")
    text = text.replace("＋", "+").replace("－", "-").replace("＿", "_")
    text = text.replace("B", "", 1) if re.match(r"^[Bb]\d", text) else text
    return text


def _parse_side(text: str, expected: str) -> tuple[BandToken, ...]:
    if not text:
        return ()
    result: list[BandToken] = []
    for raw in text.split("-"):
        match = _TOKEN_RE.fullmatch(raw)
        if not match:
            raise ValueError(f"Band 格式不正確：{raw}。範例：1A、1A-3A、n78C、n1A-n78A。")
        nr_prefix, band_text, ca_class = match.groups()
        technology = "NR" if nr_prefix else "LTE"
        if technology != expected:
            if expected == "LTE":
                raise ValueError(f"底線前只能放 LTE Band，不能放 {raw}。")
            raise ValueError(f"底線後只能放 NR Band，必須以 n 開頭：{raw}。")
        ca_class = ca_class.upper()
        result.append(BandToken(technology, int(band_text), ca_class, _CLASS_CARRIERS[ca_class]))
    return tuple(result)


def _parse_bandwidth_side(text: str, expected_count: int, label: str) -> tuple[float, ...]:
    if expected_count == 0:
        if text.strip():
            raise ValueError(f"{label} 沒有 Band，但卻指定了 BW：{text}。")
        return ()
    values_text = str(text or "").strip().lower().replace("mhz", "").replace(" ", "")
    values_text = values_text.replace("＋", "+")
    if not values_text:
        raise ValueError(f"缺少 {label} BW；需要 {expected_count} 個 BW。")
    try:
        values = tuple(float(item) for item in values_text.split("+") if item != "")
    except ValueError as exc:
        raise ValueError(f"{label} BW 格式不正確：{text}。") from exc
    if len(values) != expected_count:
        raise ValueError(f"{label} 需要 {expected_count} 個 BW，實際收到 {len(values)} 個：{text}。")
    if any(value <= 0 for value in values):
        raise ValueError(f"{label} BW 必須大於 0。")
    return values


def parse_radio_combination(band_config: str, bandwidth_config: str) -> RadioCombination:
    """Parse the team's canonical notation.

    Rules:
      * '_' separates LTE (left) from NR (right).
      * '-' separates bands inside the same RAT.
      * LTE tokens have no 'n'; NR tokens start with 'n'.
      * A/C/D/E/F expand to 1/2/3/4/5 component carriers.
      * BW uses the same '_' boundary and '+' between component carriers.

    Examples:
      1A                  + 20
      1A-3A              + 20+20
      n78C               + 100+100
      n1A-n78A           + 20+100
      1A-3A_n78C         + 20+20_100+100
    """
    band_text = _clean_band_text(band_config)
    if not band_text:
        raise ValueError("缺少 band_config。")
    if band_text.count("_") > 1:
        raise ValueError("Band 組合最多只能有一個 '_'，用來分隔 LTE 與 NR。")

    if "_" in band_text:
        lte_text, nr_text = band_text.split("_", 1)
        lte_tokens = _parse_side(lte_text, "LTE")
        nr_tokens = _parse_side(nr_text, "NR")
        if not lte_tokens or not nr_tokens:
            raise ValueError("'_' 左邊必須有 LTE，右邊必須有 NR。")
    elif band_text.lower().startswith("n"):
        lte_tokens = ()
        nr_tokens = _parse_side(band_text, "NR")
    else:
        lte_tokens = _parse_side(band_text, "LTE")
        nr_tokens = ()

    bw_text = str(bandwidth_config or "").strip().replace(" ", "")
    if bw_text.count("_") > 1:
        raise ValueError("BW 組合最多只能有一個 '_'，用來分隔 LTE 與 NR。")
    if lte_tokens and nr_tokens:
        if "_" not in bw_text:
            raise ValueError("LTE+NR 組合的 BW 必須用 '_' 分隔，例如 20_100 或 20+20_100+100。")
        lte_bw_text, nr_bw_text = bw_text.split("_", 1)
    elif lte_tokens:
        lte_bw_text, nr_bw_text = bw_text, ""
    else:
        lte_bw_text, nr_bw_text = "", bw_text

    lte_count = sum(token.carrier_count for token in lte_tokens)
    nr_count = sum(token.carrier_count for token in nr_tokens)
    lte_bws = _parse_bandwidth_side(lte_bw_text, lte_count, "LTE")
    nr_bws = _parse_bandwidth_side(nr_bw_text, nr_count, "NR")

    canonical_lte = "-".join(token.canonical for token in lte_tokens)
    canonical_nr = "-".join(token.canonical for token in nr_tokens)
    canonical = f"{canonical_lte}_{canonical_nr}" if lte_tokens and nr_tokens else (canonical_lte or canonical_nr)
    return RadioCombination(
        raw_band_config=str(band_config),
        canonical_band_config=canonical,
        lte_tokens=lte_tokens,
        nr_tokens=nr_tokens,
        lte_bandwidths_mhz=lte_bws,
        nr_bandwidths_mhz=nr_bws,
    )


def expand_lte_carriers(combo: RadioCombination) -> Iterable[tuple[int, float]]:
    index = 0
    for token in combo.lte_tokens:
        for _ in range(token.carrier_count):
            yield token.band, combo.lte_bandwidths_mhz[index]
            index += 1
