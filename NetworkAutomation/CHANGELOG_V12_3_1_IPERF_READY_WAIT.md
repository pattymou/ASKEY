# V12.3.1 iPerf Ready Wait

- 修正 PHY Rate 剛出現時立即啟動 iPerf 的競態問題。
- iPerf 啟動前必須同時滿足：
  - UE PHY 已連線。
  - 已依規則選到 UE IP。
  - 自動選取的 UE IP 可由 Callbox Ping 通。
  - 上述狀態連續穩定 5 秒。
- `amarisoft connection` 單次檢查新增 Ping 與 `ip_status` 回傳。
- 唯一 UE IP fallback 規則維持不變，例如只有 `192.168.2.2 / TestPLMN` 時可選取，但必須先 Ping 成功。
