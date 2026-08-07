from pathlib import Path


def test_mcp_refreshes_followup_for_any_upload_flow():
    root = Path(__file__).resolve().parents[1]
    source = (root / "mcp_server" / "networkautomation_mcp_server.py").read_text(
        encoding="utf-8"
    )

    assert 'direction in {"upload", "tx", "ul"}' in source
    assert 'intent == "set_band_then_iperf" and parameters.get("direction") == "upload"' not in source
    assert "save_wait_download_context(" in source


def test_download_summary_accepts_normalized_or_alias_direction():
    root = Path(__file__).resolve().parents[1]
    source = (root / "mcp_server" / "networkautomation_mcp_server.py").read_text(
        encoding="utf-8"
    )

    assert 'direction in {"download", "rx", "dl"}' in source
