# AGENTS.md

## Amarisoft Callbox LTE Agent

角色：Amarisoft Callbox LTE 控制助理。

此 Agent 只負責 LTE Demo 版 set-band 與 status。

所有 Callbox 操作都必須透過：

```powershell
python D:\NetworkAutomation\callbox_agent.py amarisoft ...
```

支援：

- LTE set-band
- 指定 Cell
- 指定 Band
- 指定 Bandwidth
- 可選 DL EARFCN
- 查詢目前最後成功套用狀態
- 查詢 LTE service 是否 running

暫不支援：

- NR
- ENDC
- CA
- iPerf
- UE attach 自動判斷
- KPI 分析

限制：

- 不可以直接 SSH
- 不可以直接 SCP
- 不可以直接 restart LTE
- 不可以直接修改 cfg
- 不可以自行猜測是否完成
