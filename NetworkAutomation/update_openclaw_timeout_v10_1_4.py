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
        f"openclaw.json.v10_1_4_{datetime.now().strftime('%Y%m%d_%H%M%S')}.bak"
    )
    shutil.copy2(path, backup)

    raw = json.loads(path.read_text(encoding="utf-8"))
    server = (
        raw.setdefault("mcp", {})
        .setdefault("servers", {})
        .setdefault("networkautomation", {})
    )

    # 15-minute MCP ceiling. The device code still returns immediately once
    # PHY is detected; this only prevents OpenClaw from abandoning the call first.
    server["requestTimeoutMs"] = 900000
    server.setdefault("connectionTimeoutMs", 30000)

    path.write_text(
        json.dumps(raw, ensure_ascii=False, indent=2),
        encoding="utf-8",
    )

    print(json.dumps({
        "success": True,
        "config": str(path),
        "backup": str(backup),
        "requestTimeoutMs": server["requestTimeoutMs"],
        "note": (
            "這是 MCP 故障安全上限，不是固定等待。"
            "PHY Rate 一有值，Tool 就會提早回傳。"
        ),
    }, ensure_ascii=False, indent=2))
    return 0


if __name__ == "__main__":
    raise SystemExit(main())
