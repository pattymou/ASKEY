from __future__ import annotations

import json
from datetime import datetime
from pathlib import Path
from typing import Any

from core.exceptions import ResultWriteError


class JsonResultWriter:
    def __init__(self, root: str | Path) -> None:
        self.root = Path(root)

    def write(self, category: str, name: str, payload: dict[str, Any]) -> Path:
        folder = self.root / category / datetime.now().strftime("%Y-%m-%d")
        folder.mkdir(parents=True, exist_ok=True)
        target = folder / f"{name}.json"
        try:
            target.write_text(
                json.dumps(payload, ensure_ascii=False, indent=2),
                encoding="utf-8",
            )
        except OSError as exc:
            raise ResultWriteError(f"無法寫入結果：{target}: {exc}") from exc
        return target
