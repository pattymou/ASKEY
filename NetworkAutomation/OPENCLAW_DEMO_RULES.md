# OpenClaw Demo Rule - Amarisoft Set Band

## Goal

When user asks to switch Amarisoft LTE Band, call this command:

```bash
python D:\NetworkAutomation\callbox_agent.py amarisoft set-band --cell 1 --band 5 --bandwidth 10
```

Adjust `--cell`, `--band`, and `--bandwidth` based on user request.

## User phrase examples

- 幫我把 Callbox 切到 Band5
- Amarisoft Cell1 切到 LTE Band5，頻寬 10MHz
- 幫我切 B5 10M

## Required response behavior

After command returns JSON:

- If `success == true` and `verify.cfg.success == true`, respond:
  「切 Band 完成，已確認遠端 AutoConfig.cfg 已套用到指定 Band / Bandwidth / EARFCN。」

- If `success == false`, respond with `message` and `error`.

## Do not do

- Do not SSH manually.
- Do not edit AutoConfig.cfg manually.
- Do not run random Linux commands.
- Only call `callbox_agent.py`.
