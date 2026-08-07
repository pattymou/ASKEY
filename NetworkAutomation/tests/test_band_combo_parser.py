from pathlib import Path

from core.intent_validator import validate_intent
from plugins.amarisoft.band_combo_parser import parse_radio_combination

ROOT = Path(__file__).resolve().parents[1]


def test_team_notation_examples():
    cases = [
        ("1A", "20", "LTE", "1A"),
        ("1A-3A", "20+20", "LTE", "1A-3A"),
        ("n78A", "100", "SA", "n78A"),
        ("n78C", "100+100", "SA", "n78C"),
        ("n1A-n78A", "20+100", "SA", "n1A-n78A"),
        ("1A_n78A", "20_100", "ENDC", "1A_n78A"),
        ("1A-3A_n78C", "20+20_100+100", "ENDC", "1A-3A_n78C"),
    ]
    for bands, bandwidths, mode, canonical in cases:
        parsed = parse_radio_combination(bands, bandwidths)
        assert parsed.mode == mode
        assert parsed.canonical_band_config == canonical


def test_validator_routes_combo_to_cli():
    nr = validate_intent(ROOT, "set_nr_band", {
        "band_config": "1A_n78A",
        "bandwidth_config": "20_100",
        "radio_mode": "ENDC",
    })
    assert "--band-config" in nr.cli_args
    assert nr.parameters["nr_carriers"][0]["nr_arfcn"] == 623334

    lte = validate_intent(ROOT, "set_band", {
        "band_config": "1A-3A",
        "bandwidth_config": "20+20",
    })
    assert lte.parameters["lte_carriers"] == [
        {"band": 1, "bandwidth_mhz": 20.0},
        {"band": 3, "bandwidth_mhz": 20.0},
    ]
