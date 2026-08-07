# V12.3.2 KeyError 修正

- 修正 `workflow_setting(root, "iperf_ready_stable_sec")` 因未註冊於 `EDITABLE_SETTINGS` 而產生的 `KeyError`。
- iPerf 啟動前的穩定等待固定為 5 秒 (`IPERF_READY_STABLE_SEC = 5`)。
- 保留 V12.3.1 的動態 UE IP 選擇邏輯：指定 IP 優先、internet APN 優先、唯一 UE IP fallback。
- 移除 `callbox_settings.json` 中未被設定系統支援的 `iperf_ready_stable_sec` 欄位。
