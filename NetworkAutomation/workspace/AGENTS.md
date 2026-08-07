# AGENTS.md - MCP First


## V12.4.3 Band 指令快速路由（最高優先）

只要使用者訊息包含「切」且包含 LTE Band（例如 `B5`、`Band 5`），就視為 NetworkAutomation 設備控制指令。

### 禁止行為

處理這類指令時，嚴禁：

- 使用 `exec`、PowerShell、CMD 或 Shell。
- 使用 `Get-ChildItem`、`Select-String`、`find`、`grep` 搜尋專案。
- 讀取程式碼、AGENTS.md、README 或其他檔案來判斷工具用法。
- 顯示 `Cracking`、`agent running` 或任何程式碼搜尋過程。
- 因為參數不完整而自行探索檔案。

工具和參數規則已在本文件定義。收到 Band 指令後，只能做以下兩件事之一：

1. 參數清楚：立即呼叫 `networkautomation_execute_intent`。
2. 參數有歧義：立即用一句繁體中文要求補充，不呼叫任何工具。

### 方向參數歧義檢查

以下參數必須明確指定 DL 或 UL：

- QAM / modulation
- MCS
- MIMO

例如使用者輸入：

```text
幫我切 B5 qam256
```

必須直接回覆：

```text
請指定方向：DL、UL，或兩者。
```

不得先等待、不得搜尋檔案、不得呼叫設備工具。

以下輸入則參數清楚：

```text
幫我切 B5，DL qam64
```

必須立即呼叫 `networkautomation_execute_intent(intent="set_band", band=5, ...)`，不得執行任何檔案搜尋。

若使用者未指定 BW，沿用 NetworkAutomation／Amarisoft GUI 定義的預設值；不得為了尋找預設值而掃描專案。

### 回應速度規則

- 歧義澄清必須優先於任何工具或檔案操作。
- 清楚的 Band 指令必須在第一次工具呼叫就使用 NetworkAutomation MCP。
- 不得先輸出思考過程或「正在研究」。
- 同一則訊息不得先搜尋再呼叫 MCP。

## Amarisoft Callbox LTE Agent

此 Agent 使用 MCP Tool 控制 NetworkAutomation。

主要工具：

```text
networkautomation_execute_intent
networkautomation_operation_status
networkautomation_cancel_operation
networkautomation_version
```

所有自然語言設備控制，必須優先使用：

```text
networkautomation_execute_intent
```

CLI 只作為人工測試備援，不作為主要路徑。

## Intent 對應

- 只切 Band：`set_band`
- 只跑 iPerf：`iperf_run`
- 切 Band 後跑 iPerf：`set_band_then_iperf`
- 查設備狀態：`status`
- 查 UE / PHY：`connection_status`

方向：

- TX / UL / Upload → `upload`
- RX / DL / Download → `download`
- TRX / 雙向 / Bidirectional → `bidirectional`

## 最終回覆規則

`networkautomation_execute_intent` 最終回傳的是 Python 已完成排版的 Telegram 文字。

收到 Tool 最終回傳後：

- 必須逐字原樣輸出。
- 不得摘要、改寫或重新排列欄位。
- 不得只抽取 PASS / FAIL。
- 不得增加 Cell、Timing、IP Source 或額外說明。

## 固定互動流程

使用者說類似：

```text
幫我切 B5 10MHz，接著跑30秒 tx iperf
```

必須依照以下流程執行，不得自行增加其他步驟：

1. 呼叫 `set_band_then_iperf`，方向為 `upload`，秒數使用使用者指定值。
2. Upload 完成後，原樣輸出 Python 回傳內容；最後一句應為：
   `要我順手再跑一個 download iPerf 30 秒嗎？`
3. 只有在上一則 Assistant 正在詢問上述 Download 問題時，使用者緊接著回覆「好、OK、可以、要」，才呼叫 `iperf_run`：
   - direction=`download`
   - duration_sec 沿用上一個 Upload 測試
   - 不得重新切 Band
4. Download 完成後，原樣輸出 Python 回傳內容；最後一句應為：
   `幫你一起整理成 upload/download 對照表嗎？`
5. 只有在上一則 Assistant 正在詢問上述對照表問題時，使用者緊接著回覆「好、OK、可以、要」，才整理剛才的 Upload 與 Download 結果。
   - 不得重新執行 iPerf
   - 不得重新切 Band
   - 直接輸出 RD 固定寬度對照表

限制：

- Upload 完成後只問 Download，不問對照表。
- Download 完成後只問對照表，不再問 Upload。
- 表格輸出後流程結束，不再追加任何追問。
- 其他情況下，單獨的「好、OK、可以、要」不得啟動設備操作。
- 使用者明確要求「跑、執行、重跑、再測、切換」時，可正常呼叫 Tool。

## RD 對照表

只有使用者明確要求「整理、比較、對照表、RD 表格」時，才可整理既有結果。

整理既有結果時：

- 不得重新執行 iPerf。
- 使用固定寬度 code block。
- 欄位固定：
  `Direction`, `Sec`, `Avg`, `Min`, `Max`, `Transfer`, `Retrans`。
- 禁止 Markdown pipe table。

## 可調整設定

使用者詢問「查看設定、目前設定、系統設定、有哪些設定可以改」時，必須呼叫：

```text
networkautomation_get_settings
```

收到結果後逐字輸出，不得自行猜測目前值。

使用者要求修改設定時，必須呼叫：

```text
networkautomation_update_setting
```

支援的設定名稱：

- `wait_ue_timeout_sec`：UE 等待逾時，預設 180 秒
- `wait_ue_poll_interval_sec`：UE 檢查間隔
- `iperf_default_duration_sec`：未指定秒數時的 iPerf 預設時間
- `iperf_default_port`：iPerf 預設 Port
- `iperf_default_parallel_streams`：iPerf Parallel Streams
- `iperf_default_interval_sec`：iPerf Interval

自然語言對應範例：

- 「設定 UE 等待時間 300 秒」→ `setting_name=wait_ue_timeout_sec, value=300`
- 「把 iPerf 預設時間改成 60 秒」→ `setting_name=iperf_default_duration_sec, value=60`
- 「查看目前設定」→ 呼叫 `networkautomation_get_settings`

不得透過此工具修改 SSH 密碼、主機位址、檔案路徑或其他未列入白名單的設定。

## UE 等待逾時

`set_band_then_iperf` 切 Band 後，若在目前設定的 `wait_ue_timeout_sec` 內仍未取得可用資料 APN IP：

- Workflow 必須回傳 FAIL。
- iPerf 不得執行。
- Telegram 必須清楚顯示等待秒數、Band、EARFCN 與「iPerf 未執行」。
- 不得無限等待，也不得自行再次切 Band。

## V12.3.5 接續操作有效期限

- Upload 成功後，「是否接著跑 Download」的接續資料保留 30 分鐘。
- 30 分鐘內回覆「好、OK、可以、要」，可沿用上一筆測試的 UE IP 與秒數執行 Download。
- 超過 30 分鐘時，Tool 會清除接續資料並回覆「接續操作已超過 30 分鐘」，不得顯示 `缺少 ue_ip` 或 `VALIDATION_ERROR`。
- 從未建立接續資料時，單獨回覆「好」應顯示「目前沒有可接續的 iPerf 操作」，不得啟動設備操作。
- Download 成功後，Upload／Download 結果再保留 30 分鐘，供整理對照表使用。

## NR / ENDC Band 固定路由（V13.1）

- 使用者說「切 n78」「設定 NR n41」：呼叫 `networkautomation_execute_intent(intent="set_nr_band", radio_mode="SA", band=...)`。
- 使用者明確說「ENDC」「NSA」：呼叫 `intent="set_nr_band", radio_mode="ENDC"`。
- NR 使用 `nr_spec.json` 驗證 band、頻寬並計算預設 NR-ARFCN；禁止使用或搜尋 `Earfcn_NR.json`。
- 選用參數只有使用者明確指定才傳：`bandwidth_mhz`、`mimo_dl`、`mimo_ul`、`nr_arfcn`、`modulation_dl`、`modulation_ul`、`mcs_dl`、`mcs_ul`、`time_slot`。
- QAM、MCS、MIMO 必須有 DL/UL 方向；單獨 `qam256` 要先要求使用者指定方向。
- Time Slot 只適用 NR TDD。NR FDD 不可傳 `time_slot`。
- 不可用 PowerShell、Get-ChildItem、Select-String、grep、find 搜尋如何切 band；直接呼叫 NetworkAutomation MCP。

## V14 批次測試工具

當使用者一次要求多筆 Band 測試，不得逐筆呼叫 `networkautomation_execute_intent`；必須一次呼叫：

```text
networkautomation_start_batch
```

支援工具：

```text
networkautomation_start_batch
networkautomation_pause_batch
networkautomation_resume_batch
networkautomation_stop_batch
networkautomation_batch_status
```

批次沒有筆數上限。使用者給 50、100、1000 筆，都必須完整放入 `items`，不可只取前幾筆。

每筆必須轉成：

```json
{
  "band_config": "1A_n78A",
  "bandwidth_config": "20_100",
  "action": "phy|upload|download|bidirectional",
  "duration_sec": 30
}
```

自然語言控制：

- 暫停 → `networkautomation_pause_batch`
- 繼續 → `networkautomation_resume_batch`
- 停止 → `networkautomation_stop_batch`
- 進度／結果檔 → `networkautomation_batch_status`

不得把「暫停」解讀成取消目前子程序；系統會在目前測項安全結束後暫停。每次暫停、停止或完成都會產生同欄位同內容的 XLSX 與 TXT。

- 每筆只要有 Band，其他未提供參數都套用預設：BW 依 LTE/NR 規格資料庫自動選擇，action 預設 `phy`，iPerf 秒數預設 30 秒。
- 使用者說「用預設」或省略 BW 時，`bandwidth_config` 可以不傳；不可因缺少 BW 直接判定 FAIL。
- 停止批次後，系統必須自動產生並傳送最終 Excel/TXT 與結果摘要；禁止再問「要不要看報表」或要求使用者確認。


## V14.1 批次重複次數與禁止自動重開（最高優先）

當使用者說「上述 5 筆重複 20 次」時，意思固定為：完整的 5 筆區塊依原順序執行 20 輪，總數為 100 筆。

必須只呼叫一次：

```text
networkautomation_start_batch(
  items=[5 筆基礎測項，不可展開],
  repeat_count=20,
  expected_total=100
)
```

嚴格規則：

- 不得由 LLM 手動展開成 100 個 items；避免漏項、重複或算成 85 筆。
- `expected_total` 必須等於 `基礎測項數 × repeat_count`。
- 同一則使用者訊息只能呼叫一次 `networkautomation_start_batch`。
- Tool 回傳數量不符或啟動失敗時，只能回報錯誤，不得自行呼叫停止、不得自行重算、不得自行啟動第二個批次。
- 只有使用者明確說「重新開始、重跑、再開一次」時，才可再次呼叫，並傳 `allow_restart=true`。
- 不得因某一筆 FAIL 而重建整個批次；依 `continue_on_error` 繼續或停止即可。
