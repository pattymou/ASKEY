from pathlib import Path
import tempfile

from core.intent_validator import validate_intent
from plugins.amarisoft.lte_config_modifier import apply_lte_config

ROOT = Path(__file__).resolve().parents[1]


def test_intent_builds_all_optional_cli_args():
    result = validate_intent(ROOT, "set_band", {
        "band": 5,
        "modulation_dl": "qam64",
        "modulation_ul": "qam256",
        "mimo_dl": "4x4",
        "mimo_ul": "2x2",
        "mcs_dl": "Best",
        "mcs_ul": 20,
    })
    joined = " ".join(result.cli_args)
    assert "--modulation-dl qam64" in joined
    assert "--modulation-ul qam256" in joined
    assert "--mimo-dl 4x4" in joined
    assert "--mimo-ul 2x2" in joined
    assert "--mcs-dl Best" in joined
    assert "--mcs-ul 20" in joined
    assert result.parameters["bandwidth_mhz"] == 10.0


def test_modifier_updates_directional_values():
    with tempfile.TemporaryDirectory() as tmp:
        output = Path(tmp) / "AutoConfig.cfg"
        result = apply_lte_config(
            ROOT / "plugins/amarisoft/AutoConfig.cfg",
            ROOT / "plugins/amarisoft/Earfcn_LTE.json",
            output,
            cell=1,
            band=5,
            modulation_dl="qam64",
            modulation_ul="qam256",
            mimo_dl="4x4",
            mimo_ul="2x2",
            mcs_dl="Best",
            mcs_ul="20",
        )
        text = output.read_text(encoding="utf-8")
        assert "#define LTE_Cell_1_DLQAM false" in text
        assert "#define LTE_Cell_1_ULQAM true" in text
        assert "#define LTE_Cell_1_ANTENNA_DL 4" in text
        assert "#define LTE_Cell_1_ANTENNA_UL 2" in text
        assert "#define LTE_Cell_1_DLMCS -1" in text
        assert "#define LTE_Cell_1_ULMCS 20" in text
        assert result.modulation_dl == "qam64"
        assert result.modulation_ul == "qam256"
