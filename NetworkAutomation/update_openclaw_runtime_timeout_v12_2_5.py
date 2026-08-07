from __future__ import annotations

import json
import shutil
from datetime import datetime
from pathlib import Path


def main() -> int:
    path = Path.home() / ".openclaw" / "openclaw.json"
    if not path.exists():
        raise SystemExit(f"找不到設定檔：{path}")

    backup = path.with_name(
        f"openclaw.json.v12_2_5_{datetime.now().strftime('%Y%m%d_%H%M%S')}.bak"
    )
    shutil.copy2(path, backup)

    raw = json.loads(path.read_text(encoding="utf-8-sig"))
    raw.setdefault("agents", {}).setdefault("defaults", {})["timeoutSeconds"] = 600
    server = raw.setdefault("mcp", {}).setdefault("servers", {}).setdefault(
        "networkautomation", {}
    )
    server["requestTimeoutMs"] = 600000
    server.setdefault("connectionTimeoutMs", 30000)

    path.write_text(json.dumps(raw, ensure_ascii=False, indent=2), encoding="utf-8")
    print(json.dumps({
        "success": True,
        "config": str(path),
        "backup": str(backup),
        "agents.defaults.timeoutSeconds": 600,
        "mcp.networkautomation.requestTimeoutMs": 600000,
    }, ensure_ascii=False, indent=2))
    return 0


if __name__ == "__main__":
    raise SystemExit(main())
