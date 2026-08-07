from __future__ import annotations

from dataclasses import dataclass
from typing import Callable

from core.result import ToolResult


@dataclass(frozen=True)
class WorkflowStep:
    name: str
    action: Callable[[], ToolResult]


def run_steps(steps: list[WorkflowStep]) -> list[ToolResult]:
    results: list[ToolResult] = []
    for step in steps:
        result = step.action()
        results.append(result)
        if not result.success:
            break
    return results
