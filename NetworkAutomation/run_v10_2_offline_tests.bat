@echo off
cd /d "%~dp0"
python -m py_compile mcp_server\networkautomation_mcp_server.py core\mcp_result_compactor.py
if errorlevel 1 exit /b 1
python tests\test_v10_2_offline.py
pause
