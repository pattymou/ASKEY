from __future__ import annotations

from datetime import datetime
from typing import Any

from core.result import ToolResult
from core.summary_formatter import format_summary


def build_tool_result(tool: str, machine_result: dict[str, Any]) -> ToolResult:
    return ToolResult(
        success=bool(machine_result.get("success")),
        tool=tool,
        human_summary=format_summary(tool, machine_result),
        machine_result=machine_result,
        timestamp=datetime.now().astimezone().isoformat(timespec="seconds"),
    )
