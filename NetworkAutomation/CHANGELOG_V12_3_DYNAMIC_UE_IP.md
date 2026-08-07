# V12.3 Dynamic UE IP Selection

## Selection priority

1. Use the UE IP explicitly supplied by the user.
2. Otherwise prefer the UE IP whose APN matches the requested APN.
3. Otherwise prefer APN `internet`.
4. Otherwise use the first non-`TestPLMN` data APN.
5. If only one unique UE IP exists, use it as `only_ue_ip_fallback` and let the mandatory pre-iPerf Ping validate reachability.
6. If multiple ambiguous UE IPs exist, do not guess; continue polling and fail safely at the workflow timeout.

## Problem fixed

When PHY Rate was already present but Amarisoft exposed only `192.168.2.2 / TestPLMN`, the workflow kept waiting for a non-existent `192.168.3.2` data bearer and timed out. The workflow now selects the sole live IP and proceeds to the existing Ping validation before iPerf.

## Files changed

- `core/workflow_executor.py`
- `plugins/amarisoft/ue_connection.py`
- `tests/test_v12_2_7_connection_ip_detection.py`
