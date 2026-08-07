from __future__ import annotations

from dataclasses import asdict, dataclass, field
from datetime import datetime
from typing import Any


@dataclass
class ToolResult:
    success: bool
    tool: str
    human_summary: str
    machine_result: dict[str, Any]
    schema_version: str = "1.0"
    timestamp: str = field(
        default_factory=lambda: datetime.now().astimezone().isoformat(timespec="seconds")
    )
    result_file: str | None = None

    def to_dict(self) -> dict[str, Any]:
        return asdict(self)
