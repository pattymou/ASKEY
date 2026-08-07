from pathlib import Path

ROOT = Path(__file__).resolve().parents[1]


def test_nr_runner_uses_mode_specific_remote_config_and_symlink():
    text = (ROOT / "plugins/amarisoft/apply_nr_to_callbox.py").read_text(encoding="utf-8")
    assert '"AutoConfig_SA.cfg" if mode == "SA" else "AutoConfig_ENDC.cfg"' in text
    assert "ln -sfn" in text
    assert "readlink -f" in text
    assert "remote_mode_cfg_path" in text
    assert "ssh.upload(out,remote_mode_cfg_path)" in text


def test_lte_runner_still_targets_lte_config():
    text = (ROOT / "plugins/amarisoft/apply_lte_to_callbox.py").read_text(encoding="utf-8")
    assert "ln -sfn AutoConfig.cfg enb.cfg" in text
