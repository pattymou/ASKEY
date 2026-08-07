from __future__ import annotations

import json
from pathlib import Path


def main() -> int:
    root = Path(__file__).resolve().parent
    mcp_file = root / "mcp_server/networkautomation_mcp_server.py"
    log_file = root / "logs/mcp_execution.log"

    text = mcp_file.read_text(encoding="utf-8")
    version_ok = "11.1.0-MCP-Progress-Heartbeat" in text
    context_ok = "ctx.report_progress" in text
    async_ok = "asyncio.create_subprocess_exec" in text

    result = {
        "success": version_ok and context_ok and async_ok,
        "checks": {
            "version": version_ok,
            "progress_heartbeat": context_ok,
            "async_subprocess": async_ok,
        },
        "mcp_log": str(log_file),
        "mcp_log_exists": log_file.exists(),
    }
    print(json.dumps(result, ensure_ascii=False, indent=2))
    return 0 if result["success"] else 1


if __name__ == "__main__":
    raise SystemExit(main())
