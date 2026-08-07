from __future__ import annotations

import ipaddress
import json
from dataclasses import asdict, dataclass
from pathlib import Path
from typing import Any, Literal

IntentName = Literal[
    "set_band",
    "iperf_run",
    "set_band_then_iperf",
    "status",
    "connection_status",
    "set_nr_band",
]


class IntentValidationError(ValueError):
    pass


@dataclass(frozen=True)
class ValidatedIntent:
    intent: IntentName
    parameters: dict[str, Any]
    cli_args: list[str]

    def to_dict(self) -> dict[str, Any]:
        return asdict(self)


def _integer(name: str, value: Any, low: int, high: int) -> int:
    try:
        parsed = int(value)
    except (TypeError, ValueError) as exc:
        raise IntentValidationError(f"{name} 必須是整數。") from exc
    if not low <= parsed <= high:
        raise IntentValidationError(f"{name} 必須介於 {low} 到 {high}。")
    return parsed


def _number(name: str, value: Any, low: float, high: float) -> float:
    try:
        parsed = float(value)
    except (TypeError, ValueError) as exc:
        raise IntentValidationError(f"{name} 必須是數字。") from exc
    if not low <= parsed <= high:
        raise IntentValidationError(f"{name} 必須介於 {low:g} 到 {high:g}。")
    return parsed


def _lte_records(root: Path) -> list[dict[str, Any]]:
    path = root / "plugins/amarisoft/Earfcn_LTE.json"
    raw = json.loads(path.read_text(encoding="utf-8"))
    records = raw.get("band")
    if not isinstance(records, list):
        raise IntentValidationError("Earfcn_LTE.json 找不到 band 清單。")
    return records


def _band_record(root: Path, band: int) -> dict[str, Any]:
    for record in _lte_records(root):
        try:
            if int(record.get("Band")) == band:
                return record
        except (TypeError, ValueError):
            continue
    raise IntentValidationError(f"LTE Band {band} 不存在於 Earfcn_LTE.json。")


def _bandwidths(record: dict[str, Any]) -> list[float]:
    values: list[float] = []
    for item in str(record.get("Channel_BandWidth", "")).split(","):
        try:
            values.append(float(item.strip()))
        except ValueError:
            pass
    return values


def _normalize_direction(value: Any) -> str:
    direction = str(value or "").strip().lower()
    aliases = {
        "download": "download",
        "dl": "download",
        "rx": "download",
        "receive": "download",
        "upload": "upload",
        "ul": "upload",
        "tx": "upload",
        "transmit": "upload",
        "bidirectional": "bidirectional",
        "both": "bidirectional",
        "dual": "bidirectional",
        "trx": "bidirectional",
        "txrx": "bidirectional",
        "rx/tx": "bidirectional",
        "tx/rx": "bidirectional",
        "雙向": "bidirectional",
    }
    normalized = aliases.get(direction)
    if not normalized:
        raise IntentValidationError(
            "direction 必須是 download/rx、upload/tx 或 bidirectional/trx/雙向。"
        )
    return normalized


def _normalize_mimo(name: str, value: Any) -> str:
    text = str(value).strip().lower().replace(" ", "")
    if text not in {"1x1", "2x2", "4x4"}:
        raise IntentValidationError(f"{name} 必須是 1x1、2x2 或 4x4。")
    return text


def _normalize_modulation(name: str, value: Any) -> str:
    text = str(value).strip().lower().replace("-", "")
    aliases = {"64": "qam64", "64qam": "qam64", "qam64": "qam64",
               "256": "qam256", "256qam": "qam256", "qam256": "qam256"}
    if text not in aliases:
        raise IntentValidationError(f"{name} 只支援 qam64 或 qam256。")
    return aliases[text]


def _normalize_mcs(name: str, value: Any) -> str:
    text = str(value).strip()
    if text.lower() == "best":
        return "Best"
    number = _integer(name, text, 0, 28)
    return str(number)


def _validate_band(root: Path, p: dict[str, Any]) -> tuple[dict[str, Any], list[str]]:
    if p.get("band_config") is not None:
        from plugins.amarisoft.band_combo_parser import expand_lte_carriers, parse_radio_combination
        if p.get("bandwidth_config") is None:
            raise IntentValidationError("使用 band_config 時必須同時指定 bandwidth_config。")
        try:
            combo = parse_radio_combination(str(p["band_config"]), str(p["bandwidth_config"]))
        except ValueError as exc:
            raise IntentValidationError(str(exc)) from exc
        if combo.mode != "LTE":
            raise IntentValidationError("set_band 只接受純 LTE 組合；含 NR 請使用 set_nr_band。")
        carriers = []
        for band, bandwidth in expand_lte_carriers(combo):
            record = _band_record(root, band)
            supported = _bandwidths(record)
            if bandwidth not in supported:
                display = ", ".join(f"{v:g}" for v in supported)
                raise IntentValidationError(f"LTE B{band} 不支援 {bandwidth:g} MHz；可用頻寬：{display} MHz。")
            carriers.append({"band": band, "bandwidth_mhz": bandwidth})
        normalized = {
            "band_config": combo.canonical_band_config,
            "bandwidth_config": str(p["bandwidth_config"]).replace(" ", ""),
            "lte_carriers": carriers,
        }
        args = ["amarisoft", "set-band", "--band-config", combo.canonical_band_config, "--bandwidth-config", normalized["bandwidth_config"]]
        for key, flag in (("expected_imsi", "--expected-imsi"), ("expected_imei", "--expected-imei"), ("apn", "--apn")):
            if p.get(key):
                value = str(p[key]).strip(); normalized[key] = value; args += [flag, value]
        return normalized, args
    if p.get("band") is None:
        raise IntentValidationError("缺少 band。")
    band = _integer("band", p["band"], 1, 256)
    record = _band_record(root, band)
    supported = _bandwidths(record)
    if not supported:
        raise IntentValidationError(f"Band {band} 沒有可用頻寬設定。")
    bandwidth = (
        _number("bandwidth_mhz", p["bandwidth_mhz"], 1.4, 100)
        if p.get("bandwidth_mhz") is not None
        else max(supported)
    )
    cell = _integer("cell", p.get("cell", 1), 1, 16)
    if bandwidth not in supported:
        display = ", ".join(str(int(v)) if v.is_integer() else str(v) for v in supported)
        raise IntentValidationError(
            f"Band {band} 不支援 {bandwidth:g} MHz；可用頻寬：{display} MHz。"
        )

    normalized: dict[str, Any] = {
        "cell": cell,
        "band": band,
        "bandwidth_mhz": bandwidth,
    }
    args = [
        "amarisoft", "set-band",
        "--cell", str(cell),
        "--band", str(band),
        "--bandwidth", str(bandwidth),
    ]

    if p.get("dl_earfcn") is not None:
        earfcn = _integer("dl_earfcn", p["dl_earfcn"], 0, 999999)
        low, high = int(record["Earfcn_Low"]), int(record["Earfcn_High"])
        if not low <= earfcn <= high:
            raise IntentValidationError(
                f"DL EARFCN {earfcn} 不在 Band {band} 的 {low}~{high} 內。"
            )
        normalized["dl_earfcn"] = earfcn
        args += ["--dl-earfcn", str(earfcn)]

    for key, flag, normalizer in (
        ("mimo_dl", "--mimo-dl", _normalize_mimo),
        ("mimo_ul", "--mimo-ul", _normalize_mimo),
        ("modulation_dl", "--modulation-dl", _normalize_modulation),
        ("modulation_ul", "--modulation-ul", _normalize_modulation),
        ("mcs_dl", "--mcs-dl", _normalize_mcs),
        ("mcs_ul", "--mcs-ul", _normalize_mcs),
    ):
        if p.get(key) is not None:
            value = normalizer(key, p[key])
            normalized[key] = value
            args += [flag, value]

    for key, flag in (
        ("expected_imsi", "--expected-imsi"),
        ("expected_imei", "--expected-imei"),
        ("apn", "--apn"),
    ):
        if p.get(key):
            value = str(p[key]).strip()
            normalized[key] = value
            args += [flag, value]
    return normalized, args


def _nr_records(root: Path) -> dict[str, Any]:
    path = root / "plugins/amarisoft/nr_spec.json"
    raw = json.loads(path.read_text(encoding="utf-8"))
    bands = raw.get("bands")
    if not isinstance(bands, dict):
        raise IntentValidationError("nr_spec.json 找不到 bands。")
    return bands


def _normalize_nr_modulation(name: str, value: Any) -> str:
    text = str(value).strip().lower().replace("-", "")
    aliases = {"64":"qam64","64qam":"qam64","qam64":"qam64",
               "256":"qam256","256qam":"qam256","qam256":"qam256"}
    if name == "modulation_ul":
        aliases = {"16":"qam16","16qam":"qam16","qam16":"qam16",
                   "64":"qam64","64qam":"qam64","qam64":"qam64"}
    if text not in aliases:
        allowed = "qam64 或 qam256" if name == "modulation_dl" else "qam16 或 qam64"
        raise IntentValidationError(f"{name} 只支援 {allowed}。")
    return aliases[text]


def _validate_nr_band(root: Path, p: dict[str, Any]) -> tuple[dict[str, Any], list[str]]:
    # Preferred path: the team's explicit combination notation.
    # Examples: n78A / n78C / n1A-n78A / 1A-3A_n78C.
    if p.get("band_config") is not None:
        from plugins.amarisoft.band_combo_parser import expand_lte_carriers, parse_radio_combination
        from plugins.amarisoft.nr_config_modifier import calculate_nr

        bandwidth_config = p.get("bandwidth_config")
        if bandwidth_config is None:
            raise IntentValidationError(
                "使用 band_config 時必須同時指定 bandwidth_config；"
                "例如 band_config=n78C、bandwidth_config=100+100。"
            )
        try:
            combo = parse_radio_combination(str(p["band_config"]), str(bandwidth_config))
        except ValueError as exc:
            raise IntentValidationError(str(exc)) from exc
        if combo.mode == "LTE":
            raise IntentValidationError(
                "純 LTE 組合請使用 set_band；set_nr_band 的 band_config 必須包含 NR。"
            )

        requested_mode = str(p.get("radio_mode") or p.get("mode") or combo.mode).upper()
        if requested_mode not in {"SA", "ENDC"}:
            raise IntentValidationError("radio_mode 必須是 SA 或 ENDC。")
        if requested_mode != combo.mode:
            raise IntentValidationError(
                f"Band 組合 {combo.canonical_band_config} 判定為 {combo.mode}，"
                f"但 radio_mode 指定為 {requested_mode}。"
            )

        # Validate all LTE component carriers using the existing LTE database.
        for lte_band, lte_bw in expand_lte_carriers(combo):
            record = _band_record(root, lte_band)
            if lte_bw not in _bandwidths(record):
                display = ", ".join(f"{v:g}" for v in _bandwidths(record))
                raise IntentValidationError(
                    f"LTE B{lte_band} 不支援 {lte_bw:g} MHz；可用頻寬：{display} MHz。"
                )

        # Validate the complete NR BCS and calculate every NR carrier.
        assert combo.nr_calculator_input is not None
        try:
            calculation = calculate_nr(
                combo.nr_calculator_input,
                root / "plugins/amarisoft/nr_spec.json",
            )
        except Exception as exc:
            raise IntentValidationError(str(exc)) from exc

        cell = _integer("cell", p.get("cell", 1), 1, 3)
        normalized = {
            "radio_mode": combo.mode,
            "cell": cell,
            "band_config": combo.canonical_band_config,
            "bandwidth_config": str(bandwidth_config).replace(" ", ""),
            "nr_band_config": combo.nr_calculator_input,
            "lte_carriers": [
                {"band": band, "bandwidth_mhz": bw}
                for band, bw in expand_lte_carriers(combo)
            ],
            "nr_carriers": [
                {
                    "band": carrier.band,
                    "bandwidth_mhz": carrier.bandwidth_mhz,
                    "nr_arfcn": carrier.nr_arfcn,
                    "ca_class": carrier.ca_class,
                }
                for carrier in calculation.carriers
            ],
        }
        args = [
            "amarisoft", "set-nr-band",
            "--mode", combo.mode,
            "--cell", str(cell),
            "--band-config", combo.canonical_band_config,
            "--bandwidth-config", str(bandwidth_config).replace(" ", ""),
        ]
        for key, flag, fn in (
            ("mimo_dl", "--mimo-dl", _normalize_mimo),
            ("mimo_ul", "--mimo-ul", _normalize_mimo),
            ("modulation_dl", "--modulation-dl", _normalize_nr_modulation),
            ("modulation_ul", "--modulation-ul", _normalize_nr_modulation),
            ("mcs_dl", "--mcs-dl", _normalize_mcs),
            ("mcs_ul", "--mcs-ul", _normalize_mcs),
        ):
            if p.get(key) is not None:
                value = fn(key, p[key])
                normalized[key] = value
                args += [flag, value]
        if p.get("time_slot") is not None:
            value = str(p["time_slot"]).strip().upper()
            normalized["time_slot"] = value
            args += ["--time-slot", value]
        for key, flag in (
            ("expected_imsi", "--expected-imsi"),
            ("expected_imei", "--expected-imei"),
            ("apn", "--apn"),
        ):
            if p.get(key):
                value = str(p[key]).strip()
                normalized[key] = value
                args += [flag, value]
        return normalized, args

    # Legacy single-NR-band interface retained for backward compatibility.
    mode = str(p.get("radio_mode") or p.get("mode") or "SA").upper()
    if mode not in {"SA", "ENDC"}:
        raise IntentValidationError("radio_mode 必須是 SA 或 ENDC。")
    if p.get("band") is None:
        raise IntentValidationError(
            "缺少 NR band。也可以改用 band_config，例如 n78C 或 1A_n78A。"
        )
    band = _integer("band", p["band"], 1, 999)
    spec = _nr_records(root).get(f"n{band}")
    if not isinstance(spec, dict) or not spec.get("allowed_bws"):
        raise IntentValidationError(f"nr_spec.json 不支援或查無 n{band}。")
    allowed = [float(x) for x in spec["allowed_bws"]]
    nr_spec_raw = json.loads((root / "plugins/amarisoft/nr_spec.json").read_text(encoding="utf-8-sig"))
    bcs_prefix = f"n{band}A:".lower()
    bcs_allowed = []
    for item in nr_spec_raw.get("bcs_allowed", []):
        text = "".join(str(item).split())
        if text.lower().startswith(bcs_prefix):
            try:
                profile = [float(value) for value in text.split(":", 1)[1].split("+")]
            except (ValueError, IndexError):
                continue
            if len(profile) == 1 and profile[0] not in bcs_allowed:
                bcs_allowed.append(profile[0])
    effective_allowed = bcs_allowed or allowed
    bandwidth = (_number("bandwidth_mhz", p["bandwidth_mhz"], 1, 400)
                 if p.get("bandwidth_mhz") is not None
                 else (effective_allowed[0] if bcs_allowed else max(effective_allowed)))
    if bandwidth not in effective_allowed:
        display = ", ".join(str(int(v)) if v.is_integer() else str(v) for v in effective_allowed)
        raise IntentValidationError(f"n{band}A 不支援 {bandwidth:g} MHz；可用頻寬：{display} MHz。")
    cell = _integer("cell", p.get("cell", 1), 1, 3)
    normalized={"radio_mode":mode,"cell":cell,"band":band,"bandwidth_mhz":bandwidth}
    args=["amarisoft","set-nr-band","--mode",mode,"--cell",str(cell),"--band",str(band),"--bandwidth",str(bandwidth)]
    if p.get("nr_arfcn") is not None:
        v=_integer("nr_arfcn",p["nr_arfcn"],0,3279165); normalized["nr_arfcn"]=v; args += ["--nr-arfcn",str(v)]
    for key,flag,fn in (("mimo_dl","--mimo-dl",_normalize_mimo),("mimo_ul","--mimo-ul",_normalize_mimo),
                        ("modulation_dl","--modulation-dl",_normalize_nr_modulation),("modulation_ul","--modulation-ul",_normalize_nr_modulation),
                        ("mcs_dl","--mcs-dl",_normalize_mcs),("mcs_ul","--mcs-ul",_normalize_mcs)):
        if p.get(key) is not None:
            v=fn(key,p[key]); normalized[key]=v; args += [flag,v]
    if p.get("time_slot") is not None:
        if str(spec.get("type","")).upper() != "TDD":
            raise IntentValidationError(f"n{band} 是 FDD，不能設定 time slot。")
        v=str(p["time_slot"]).strip().upper(); normalized["time_slot"]=v; args += ["--time-slot",v]
    if mode == "ENDC" and p.get("lte_band") is None:
        raise IntentValidationError("ENDC 是 LTE + NR 組合 Band，必須同時指定 LTE band；純 NR 請使用 SA。")
    if mode == "ENDC" and p.get("lte_band") is not None:
        lte_band=_integer("lte_band",p["lte_band"],1,256); lte_record=_band_record(root,lte_band); normalized["lte_band"]=lte_band; args += ["--lte-band",str(lte_band)]
        if p.get("lte_bandwidth_mhz") is not None:
            lbw=_number("lte_bandwidth_mhz",p["lte_bandwidth_mhz"],1.4,20); allowed_lte=_bandwidths(lte_record)
            if lbw not in allowed_lte: raise IntentValidationError(f"LTE B{lte_band} 不支援 {lbw:g} MHz。")
            normalized["lte_bandwidth_mhz"]=lbw; args += ["--lte-bandwidth",str(lbw)]
        if p.get("lte_earfcn") is not None:
            le=_integer("lte_earfcn",p["lte_earfcn"],0,999999); normalized["lte_earfcn"]=le; args += ["--lte-earfcn",str(le)]
    for key,flag in (("expected_imsi","--expected-imsi"),("expected_imei","--expected-imei"),("apn","--apn")):
        if p.get(key):
            v=str(p[key]).strip(); normalized[key]=v; args += [flag,v]
    return normalized,args


def _validate_iperf(p: dict[str, Any], require_ip: bool) -> tuple[dict[str, Any], list[str]]:
    direction = _normalize_direction(p.get("direction"))
    ue_ip = str(p.get("ue_ip") or "").strip()
    if require_ip and not ue_ip:
        raise IntentValidationError("缺少 ue_ip。")
    if ue_ip:
        try:
            ipaddress.ip_address(ue_ip)
        except ValueError as exc:
            raise IntentValidationError(f"UE IP 格式不正確：{ue_ip}") from exc

    duration = _integer("duration_sec", p.get("duration_sec"), 1, 86400)
    port = _integer("port", p.get("port", 5201), 1, 65535)
    parallel = _integer("parallel_streams", p.get("parallel_streams", 1), 1, 128)
    interval = _integer("interval_sec", p.get("interval_sec", 1), 1, 60)

    normalized = {
        "ue_ip": ue_ip or None,
        "direction": direction,
        "duration_sec": duration,
        "port": port,
        "parallel_streams": parallel,
        "interval_sec": interval,
    }
    args = [
        "iperf", "run",
        "--direction", direction,
        "--duration", str(duration),
        "--port", str(port),
        "--parallel", str(parallel),
        "--interval", str(interval),
    ]
    if ue_ip:
        args += ["--ue-ip", ue_ip]
    return normalized, args


def validate_intent(root: Path, intent: str, parameters: dict[str, Any] | None) -> ValidatedIntent:
    name = str(intent).strip().lower()
    p = parameters or {}

    if name == "set_band":
        normalized, args = _validate_band(root, p)
        return ValidatedIntent("set_band", normalized, args)

    if name == "set_nr_band":
        normalized, args = _validate_nr_band(root, p)
        return ValidatedIntent("set_nr_band", normalized, args)

    if name == "iperf_run":
        normalized, args = _validate_iperf(p, require_ip=True)
        return ValidatedIntent("iperf_run", normalized, args)

    if name == "set_band_then_iperf":
        band_params, _ = _validate_band(root, p)
        iperf_params, _ = _validate_iperf(p, require_ip=False)
        normalized = {**band_params, **iperf_params}
        args = [
            "workflow", "band-iperf",
            "--cell", str(band_params["cell"]),
            "--band", str(band_params["band"]),
            "--bandwidth", str(band_params["bandwidth_mhz"]),
            "--direction", str(iperf_params["direction"]),
            "--duration", str(iperf_params["duration_sec"]),
            "--port", str(iperf_params["port"]),
            "--parallel", str(iperf_params["parallel_streams"]),
            "--interval", str(iperf_params["interval_sec"]),
        ]
        if iperf_params.get("ue_ip"):
            args += ["--ue-ip", str(iperf_params["ue_ip"])]
        for key, flag in (
            ("dl_earfcn", "--dl-earfcn"),
            ("expected_imsi", "--expected-imsi"),
            ("expected_imei", "--expected-imei"),
            ("apn", "--apn"),
        ):
            if band_params.get(key) is not None:
                args += [flag, str(band_params[key])]
        return ValidatedIntent("set_band_then_iperf", normalized, args)

    if name == "status":
        return ValidatedIntent("status", {}, ["amarisoft", "status"])

    if name == "connection_status":
        args = ["amarisoft", "connection"]
        normalized = {}
        for key, flag in (
            ("expected_imsi", "--expected-imsi"),
            ("expected_imei", "--expected-imei"),
            ("apn", "--apn"),
        ):
            if p.get(key):
                value = str(p[key]).strip()
                normalized[key] = value
                args += [flag, value]
        return ValidatedIntent("connection_status", normalized, args)

    raise IntentValidationError(
        "不支援的 intent。允許：set_band、set_nr_band、iperf_run、set_band_then_iperf、status、connection_status。"
    )
