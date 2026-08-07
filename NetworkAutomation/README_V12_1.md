# NetworkAutomation V12.1 — Data IP Accuracy + Telegram UI Polish

## 修正

UE 連線與 UE Data IP 是兩件不同的事：

```text
PHY Rate > 0
→ UE 已連線

APN=internet / 192.168.3.x
→ 真正提供應用程式與 iPerf 使用的 UE Data IP
```

`TestPLMN / 192.168.2.x` 不再顯示為 UE Data IP。

## 新流程

PHY 出現後：

```text
先判定 UE 已連線
→ 最多再等 30 秒資料 APN
→ 優先選 APN=internet
→ 排除 TestPLMN
→ 取得後立即回覆
```

若 30 秒內仍沒有資料 APN：

```text
UE 連線：已連線
UE Data IP：等待資料 APN
```

不會顯示錯誤的 192.168.2.2。

## Telegram 顯示改善

結果分成：

```text
【Band】
【Connection】
【iPerf】
【Timing】
```

並顯示：

- Connection Basis
- UE Data IP
- IP Source
- PHY DL/UL
- LTE Service 穩定耗時
- UE/PHY/Data IP 等待時間
- Operation 總耗時

## 覆蓋／新增

```text
plugins\amarisoft\ue_connection.py
plugins\amarisoft\apply_lte_to_callbox.py
core\summary_formatter.py
core\operation_worker.py
core\mcp_result_compactor.py
mcp_server\networkautomation_mcp_server.py
workspace\PROMPT.md
tests\test_v12_1_data_ip_summary.py
README_V12_1.md
```

## 未修改

```text
AutoConfig.cfg
Earfcn_LTE.json
callbox_settings.json
callbox_agent.py
tools\iperf\runner.py
```

## 測試

```powershell
python tests\test_v12_1_data_ip_summary.py
```

套用後複製 Prompt，並重啟背景 Gateway。
