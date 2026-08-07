from pathlib import Path

import pytest

from core.intent_validator import IntentValidationError, validate_intent
from plugins.amarisoft.nr_config_modifier import (
    apply_nr_band_config,
    calculate_nr,
    get_bandwidth_policy,
)

ROOT = Path(__file__).resolve().parents[1]
SPEC = ROOT / "plugins/amarisoft/nr_spec.json"


def test_n78_100_uses_per_bw_scs_and_channel_raster():
    result = calculate_nr("n78A(100)", SPEC)
    carrier = result.carriers[0]
    assert carrier.nr_arfcn == 623334
    assert carrier.subcarrier_spacing_khz == 30
    assert carrier.channel_raster_khz == 30


def test_n78_class_c_matches_csharp_logic():
    result = calculate_nr("n78C(100+100)", SPEC)
    assert [carrier.nr_arfcn for carrier in result.carriers] == [623334, 630000]


def test_inter_band_compact_input_matches_bcs():
    result = calculate_nr("n28A-n78A(20+100)", SPEC)
    assert result.bcs_skeleton == "n28A-n78A:20+100"
    assert [carrier.nr_arfcn for carrier in result.carriers] == [153600, 623334]


def test_bcs_is_authoritative_for_n1a():
    policy = get_bandwidth_policy("n1A", SPEC)
    assert policy.uses_bcs_allowed is True
    assert policy.per_carrier_allowed_bandwidths_mhz == [[20.0]]
    with pytest.raises(ValueError, match="BCS"):
        calculate_nr("n1A(50)", SPEC)


def test_no_bcs_falls_back_to_band_allowed_bws():
    policy = get_bandwidth_policy("n2A", SPEC)
    assert policy.uses_bcs_allowed is False
    assert 40.0 in policy.per_carrier_allowed_bandwidths_mhz[0]


def test_apply_combo_updates_multiple_nr_cells(tmp_path):
    output = tmp_path / "combo.cfg"
    result = apply_nr_band_config(
        ROOT / "plugins/amarisoft/AutoConfig_SA.cfg",
        SPEC,
        output,
        "SA",
        "n78C(100+100)",
    )
    text = output.read_text(encoding="utf-8")
    assert result.nr_cell_count == 2
    assert "#define NR_CELL 2" in text
    assert "623334" in text
    assert "630000" in text


def test_openclaw_validator_uses_bcs_default_and_rejects_other_bw():
    validated = validate_intent(ROOT, "set_nr_band", {"radio_mode": "SA", "band": 1})
    assert validated.parameters["bandwidth_mhz"] == 20.0
    with pytest.raises(IntentValidationError):
        validate_intent(ROOT, "set_nr_band", {"radio_mode": "SA", "band": 1, "bandwidth_mhz": 50})
