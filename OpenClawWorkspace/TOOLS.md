# TOOLS.md

## NetworkAutomation Root

```text
D:\NetworkAutomation
```

## Amarisoft LTE set-band

```powershell
python D:\NetworkAutomation\callbox_agent.py amarisoft set-band --cell <CELL> --band <BAND> --bandwidth <BANDWIDTH>
```

可選：

```powershell
--dl-earfcn <EARFCN>
```

## Amarisoft LTE status

```powershell
python D:\NetworkAutomation\callbox_agent.py amarisoft status
```

工具會回傳 JSON：

- success=true：完成
- success=false：失敗
- message：優先回覆給使用者
