from pathlib import Path
import pytest

ROOT = Path(__file__).resolve().parents[1]

def test_sa_uses_sa_template_and_sets_one_nr_cell(tmp_path):
    from plugins.amarisoft.nr_config_modifier import apply_nr_config
    out = tmp_path / "sa.cfg"
    r = apply_nr_config(ROOT/"plugins/amarisoft/AutoConfig_SA.cfg", ROOT/"plugins/amarisoft/nr_spec.json", out, "SA", 1, 78, modulation_dl="qam256")
    text = out.read_text(encoding="utf-8")
    assert "#define NR_CELL 1" in text
    assert '#define NR_DLQAM_1    "qam256"' in text
    assert "#define NR_BAND_1        78" in text

def test_endc_requires_lte_anchor():
    from core.intent_validator import validate_intent, IntentValidationError
    with pytest.raises(IntentValidationError):
        validate_intent(ROOT, "set_nr_band", {"radio_mode":"ENDC", "band":78})

def test_nr_ul_qam256_rejected():
    from plugins.amarisoft.nr_config_modifier import parse_modulation
    with pytest.raises(ValueError):
        parse_modulation("qam256", "UL")
