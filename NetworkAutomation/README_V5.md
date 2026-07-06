# NetworkAutomation V5 - Status Verify

覆蓋/新增：
- callbox_agent.py
- plugins/amarisoft/apply_lte_to_callbox.py
- plugins/amarisoft/verify.py
- plugins/amarisoft/status.py

測試：
python callbox_agent.py amarisoft status
python callbox_agent.py amarisoft set-band --cell 1 --band 5 --bandwidth 10

成功條件：
"success": true
"runtime_verify": { "success": true }
