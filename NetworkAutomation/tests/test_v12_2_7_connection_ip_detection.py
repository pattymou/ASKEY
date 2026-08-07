from __future__ import annotations

import sys
import types
from pathlib import Path

ROOT = Path(__file__).resolve().parents[1]
if str(ROOT) not in sys.path:
    sys.path.insert(0, str(ROOT))

from core.workflow_executor import select_iperf_ip

# Offline unit test: ue_connection imports core.ssh, which imports paramiko.
# A tiny stub is enough because this test only exercises pure parsing helpers.
if "paramiko" not in sys.modules:
    sys.modules["paramiko"] = types.SimpleNamespace()
from plugins.amarisoft.ue_connection import extract_ues


def main() -> None:
    # Normalized connection result: use data_ue_ip directly.
    result = {
        "machine_result": {
            "success": True,
            "connected": True,
            "data_ue_ip": "192.168.3.2",
            "data_ip_source": "apn:internet",
            "ues": [],
        }
    }
    assert select_iperf_ip(result, None, None) == ("192.168.3.2", "apn:internet")

    # Nested legacy result: use data_ue.ip.
    result = {
        "machine_result": {
            "connection": {
                "data_ue": {"ip": "192.168.3.2", "apn": "internet"},
                "data_ip_source": "apn:internet",
            }
        }
    }
    assert select_iperf_ip(result, None, None) == ("192.168.3.2", "apn:internet")

    # UE payload can occur before a final acknowledgement payload.
    payloads = [
        {"message": "ue_get", "ue_list": [{"ip": "192.168.3.2", "apn": "internet", "rnti": 100}]},
        {"message": "ready", "status": "ok"},
    ]
    aggregated = []
    for payload in payloads:
        aggregated.extend(extract_ues(payload))
    assert any(ue.get("ip") == "192.168.3.2" for ue in aggregated)

    # Existing ues[] fallback remains supported.
    result = {"machine_result": {"ues": [{"ip": "192.168.3.2", "apn": "internet"}]}}
    assert select_iperf_ip(result, None, None) == ("192.168.3.2", "apn:internet")

    # When TestPLMN is the only actual UE IP, use it dynamically after PHY is up.
    result = {"machine_result": {"ues": [{"ip": "192.168.2.2", "apn": "TestPLMN"}]}}
    assert select_iperf_ip(result, None, None) == (
        "192.168.2.2",
        "only_ue_ip_fallback",
    )

    # Multiple same-priority IPs remain ambiguous rather than selected arbitrarily.
    result = {
        "machine_result": {
            "ues": [
                {"ip": "192.168.2.2", "apn": "TestPLMN"},
                {"ip": "192.168.2.3", "apn": "TestPLMN"},
            ]
        }
    }
    assert select_iperf_ip(result, None, None) == (
        None,
        "multiple_ambiguous_ue_ips",
    )

    # When APN metadata is unavailable, one unique detected IP is still used
    # dynamically; no address is hard-coded.
    result = {"machine_result": {"ues": [{"ip": "10.20.30.40", "apn": None}]}}
    assert select_iperf_ip(result, None, None) == (
        "10.20.30.40",
        "only_ue_ip_fallback",
    )

    # Explicit user input still has highest priority.
    assert select_iperf_ip(result, "10.10.10.10", None) == (
        "10.10.10.10",
        "explicit",
    )

    print("V12.3 dynamic UE IP selection tests: PASS")


if __name__ == "__main__":
    main()
