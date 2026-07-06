# NetworkAutomation Phase 2 - Generic iperf Tool

## 放置位置

請把檔案放成：

```text
NetworkAutomation/
  callbox_agent.py
  tools/
    iperf/
      runner.py
```

## 測試 1：確認 iperf3

```bash
python callbox_agent.py iperf check
```

## 測試 2：本機跑 iperf client

前提：另一台機器已經跑：

```bash
iperf3 -s
```

然後執行：

```bash
python callbox_agent.py iperf client --server 192.168.1.100 --time 10 --parallel 4
```

## 測試 3：UDP

```bash
python callbox_agent.py iperf client --server 192.168.1.100 --protocol udp --bandwidth 100M --time 10
```

## 測試 4：遠端 SSH 執行 iperf client

這會在遠端機器執行 iperf3 client：

```bash
python callbox_agent.py iperf client --server 192.168.1.100 --time 10 ^
  --ssh-host 192.168.1.50 --ssh-user root --ssh-password password
```

## 注意

- iperf3 必須安裝在「執行 iperf client 的那台機器」。
- 如果使用 --ssh-host，iperf3 要裝在遠端 SSH 主機上。
- stdout 只輸出 JSON，方便 OpenClaw / MCP 解析。
