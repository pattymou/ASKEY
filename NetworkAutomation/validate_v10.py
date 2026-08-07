from pathlib import Path
import hashlib, json, py_compile, sys

ROOT = Path(__file__).resolve().parent
required = [
    "callbox_agent.py",
    "callbox_settings.json",
    "core/intent_validator.py",
    "core/operation_manager.py",
    "core/operation_worker.py",
    "plugins/amarisoft/AutoConfig.cfg",
    "plugins/amarisoft/Earfcn_LTE.json",
    "plugins/amarisoft/ue_connection.py",
    "tools/iperf/runner.py",
    "mcp_server/networkautomation_mcp_server.py",
    "workspace/PROMPT.md",
]
missing = [name for name in required if not (ROOT / name).exists()]
if missing:
    print(json.dumps({"success": False, "missing": missing}, ensure_ascii=False, indent=2))
    raise SystemExit(1)

for path in ROOT.rglob("*.py"):
    if "__pycache__" not in path.parts:
        py_compile.compile(str(path), doraise=True)

print(json.dumps({
    "success": True,
    "version": "10.0.0",
    "python_compile": "PASS",
    "protected_hashes": {
        "AutoConfig.cfg": hashlib.sha256((ROOT / "plugins/amarisoft/AutoConfig.cfg").read_bytes()).hexdigest(),
        "Earfcn_LTE.json": hashlib.sha256((ROOT / "plugins/amarisoft/Earfcn_LTE.json").read_bytes()).hexdigest(),
    }
}, ensure_ascii=False, indent=2))
