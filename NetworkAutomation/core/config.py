from __future__ import annotations

import json
from dataclasses import dataclass
from pathlib import Path
from typing import Any

from core.exceptions import ConfigurationError


@dataclass(frozen=True)
class ProjectConfig:
    path: Path
    data: dict[str, Any]

    @classmethod
    def load(cls, path: str | Path) -> "ProjectConfig":
        config_path = Path(path).resolve()
        if not config_path.exists():
            raise ConfigurationError(f"設定檔不存在：{config_path}")
        try:
            data = json.loads(config_path.read_text(encoding="utf-8"))
        except (OSError, json.JSONDecodeError) as exc:
            raise ConfigurationError(f"無法讀取設定檔：{exc}") from exc
        if not isinstance(data, dict):
            raise ConfigurationError("設定檔最外層必須是 JSON object")
        return cls(path=config_path, data=data)

    def section(self, name: str) -> dict[str, Any]:
        value = self.data.get(name)
        if not isinstance(value, dict):
            raise ConfigurationError(f"設定檔缺少區塊：{name}")
        return value
