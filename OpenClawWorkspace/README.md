# OpenClaw Workspace - NetworkAutomation 版

這包檔案全部都是 UTF-8 with BOM，Windows 記事本打開不會亂碼。

## 放置位置

請先備份原本資料夾，再覆蓋到：

```text
C:\Users\admin\.openclaw\workspace
```

## 覆蓋檔案

```text
AGENTS.md
PROMPT.md
SYSTEM.md
TOOLS.md
USER.md
Callbox_LTE_Agent_Rules.md
```

## 測試語句

對龍蝦說：

```text
幫我把 Callbox Cell1 切到 LTE Band5，10MHz
```

它應該執行：

```powershell
python D:\NetworkAutomation\callbox_agent.py amarisoft set-band --cell 1 --band 5 --bandwidth 10
```
