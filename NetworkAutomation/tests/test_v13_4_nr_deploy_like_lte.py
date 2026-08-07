from pathlib import Path

ROOT = Path(__file__).resolve().parents[1]


def test_nr_deploys_to_same_remote_autoconfig_as_lte():
    text = (ROOT / "plugins/amarisoft/apply_nr_to_callbox.py").read_text(encoding="utf-8")
    assert "remote_cfg_path = s.callbox.remote_cfg_path" in text
    assert "ssh.upload(out,remote_cfg_path)" in text
    assert "for command in s.callbox.restart_commands" in text
    assert "verify(\n                ssh,\n                remote_cfg_path" in text


def test_nr_does_not_switch_to_sa_or_endc_remote_files():
    text = (ROOT / "plugins/amarisoft/apply_nr_to_callbox.py").read_text(encoding="utf-8")
    assert 'active_cfg_name = "AutoConfig_SA.cfg"' not in text
    assert "readlink -f" not in text
    assert "ln -sfn {shlex.quote(active_cfg_name)}" not in text


def test_lte_deployment_code_is_unchanged():
    text = (ROOT / "plugins/amarisoft/apply_lte_to_callbox.py").read_text(encoding="utf-8")
    assert "ssh.upload(out, s.callbox.remote_cfg_path)" in text
    assert "for command in s.callbox.restart_commands" in text
