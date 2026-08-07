from pathlib import Path
from unittest.mock import patch
import sys
import types

sys.modules.setdefault("paramiko", types.SimpleNamespace())

from core.telegram_reply_formatter import format_telegram_reply
from plugins.amarisoft import ue_connection


def test_set_band_timeout_has_clear_telegram_reply():
    result = {
        "success": False,
        "ue_wait_timeout_sec": 180,
        "modifier": {"band": 5, "bandwidth_mhz": 10, "dl_earfcn": 2525},
        "connection": {
            "connected": False,
            "error": "UE_CONNECTION_TIMEOUT",
            "elapsed_sec": 180,
        },
    }
    text = format_telegram_reply("amarisoft.set_band", result)
    assert "180 秒" in text
    assert "工作已結束" in text
    assert "UE 未連線" in text


def test_wait_for_connection_uses_override_timeout():
    class FakeSSH:
        def __enter__(self):
            return self
        def __exit__(self, *args):
            return False

    fake_access = object()
    fake_policy = type("Policy", (), {
        "max_wait_sec": 600,
        "min_phy_bitrate_bps": 1,
        "data_ip_grace_sec": 0,
        "require_phy_rate": True,
        "poll_interval_sec": 0,
    })()

    monotonic_values = iter([0, 0, 181])
    with patch.object(ue_connection, "load_settings", return_value=(fake_access, fake_policy)), \
         patch.object(ue_connection.SSHClient, "from_callbox_settings", return_value=FakeSSH()), \
         patch.object(ue_connection, "query_phy_rates", return_value={
             "success": True,
             "total_dl_bitrate_bps": 0,
             "total_ul_bitrate_bps": 0,
             "total_dl_bitrate_mbps": 0,
             "total_ul_bitrate_mbps": 0,
         }), \
         patch.object(ue_connection, "query_ues", return_value={"success": True, "ues": []}), \
         patch.object(ue_connection, "match_ues", return_value=[]), \
         patch.object(ue_connection, "select_data_ue", return_value=(None, "not_available")), \
         patch.object(ue_connection.time, "monotonic", side_effect=lambda: next(monotonic_values)), \
         patch.object(ue_connection.time, "sleep", return_value=None):
        result = ue_connection.wait_for_connection(Path("dummy.json"), max_wait_sec=180)

    assert result["success"] is False
    assert result["error"] == "UE_CONNECTION_TIMEOUT"
    assert "180 秒" in result["message"]
