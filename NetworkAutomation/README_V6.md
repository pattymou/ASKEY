# NetworkAutomation V6 - State-based Status

V6 不再解析 AutoConfig.cfg 內容判斷 runtime 狀態。

流程：

```text
set-band 成功
  ↓
確認 AutoConfig.cfg 存在
  ↓
確認 enb.cfg -> AutoConfig.cfg
  ↓
確認 service lte active running
  ↓
寫入 state/callbox_state.json
  ↓
status 回傳最後成功套用狀態 + service 狀態
```

## 覆蓋 / 新增

```text
callbox_agent.py
plugins/amarisoft/apply_lte_to_callbox.py
plugins/amarisoft/status.py
plugins/amarisoft/state.py
plugins/amarisoft/verify.py
```

## 測試

```bash
python callbox_agent.py amarisoft set-band --cell 1 --band 5 --bandwidth 10
python callbox_agent.py amarisoft status
```

## 成功條件

```json
"success": true
```

status 會出現：

```json
"current": {
  "mode": "LTE",
  "cell": 1,
  "band": 5,
  "bandwidth_mhz": 10.0,
  "dl_earfcn": 2525,
  "rb_dl": 50
}
```
