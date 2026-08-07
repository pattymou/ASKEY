from pathlib import Path
from plugins.amarisoft.nr_config_modifier import apply_nr_config

ROOT=Path(__file__).resolve().parents[1]

def test_sa_n78_defaults(tmp_path):
    out=tmp_path/'sa.cfg'
    r=apply_nr_config(ROOT/'plugins/amarisoft/AutoConfig_SA.cfg',ROOT/'plugins/amarisoft/nr_spec.json',out,'SA',1,78)
    text=out.read_text(encoding='utf-8')
    assert r.bandwidth_mhz == 100
    assert '#define NR_BAND_1' in text and '78' in text
    assert str(r.nr_arfcn) in text
    assert r.time_slot == 'DDDSU_DDSUU'

def test_endc_n1_fdd_no_timeslot(tmp_path):
    out=tmp_path/'endc.cfg'
    r=apply_nr_config(ROOT/'plugins/amarisoft/AutoConfig_ENDC.cfg',ROOT/'plugins/amarisoft/nr_spec.json',out,'ENDC',1,1,20,modulation_dl='qam64')
    text=out.read_text(encoding='utf-8')
    assert r.duplex_type == 'FDD'
    assert '"qam64"' in text

def test_fdd_reject_timeslot(tmp_path):
    try:
        apply_nr_config(ROOT/'plugins/amarisoft/AutoConfig_SA.cfg',ROOT/'plugins/amarisoft/nr_spec.json',tmp_path/'x.cfg','SA',1,1,20,time_slot='DDDSU_DDSUU')
    except ValueError as e:
        assert 'FDD' in str(e)
    else:
        raise AssertionError('expected ValueError')
