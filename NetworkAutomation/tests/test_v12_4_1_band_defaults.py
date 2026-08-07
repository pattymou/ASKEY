from pathlib import Path
from core.intent_validator import validate_intent, IntentValidationError
ROOT=Path(__file__).resolve().parents[1]

def test_defaults():
    v=validate_intent(ROOT,"set_band",{"band":5})
    assert v.parameters["mimo_dl"]=="4x4"
    assert v.parameters["mimo_ul"]=="2x2"
    assert v.parameters["modulation_dl"]=="qam256"
    assert v.parameters["modulation_ul"]=="qam256"

def test_separate_qam():
    v=validate_intent(ROOT,"set_band",{"band":5,"modulation_dl":"qam64"})
    assert v.parameters["modulation_dl"]=="qam64"
    assert v.parameters["modulation_ul"]=="qam256"

def test_lte_rejects_timeslot():
    try:
        validate_intent(ROOT,"set_band",{"band":5,"time_slot":"DDDSU"})
    except IntentValidationError:
        return
    raise AssertionError("LTE time_slot should fail")
