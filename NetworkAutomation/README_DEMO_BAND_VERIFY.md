# NetworkAutomation Demo Band Verify

覆蓋/新增這些檔案：

```text
callbox_agent.py
plugins/amarisoft/apply_lte_to_callbox.py
plugins/amarisoft/verify.py
OPENCLAW_DEMO_RULES.md
```

正式測試：

```bash
python callbox_agent.py amarisoft set-band --cell 1 --band 5 --bandwidth 10
```

成功條件：

```json
"success": true,
"verify": {
  "cfg": {
    "success": true
  }
}
```

龍蝦回覆可直接使用 JSON 裡的 `message` 欄位。
