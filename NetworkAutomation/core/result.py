#!/usr/bin/env python3
# -*- coding: utf-8 -*-
from __future__ import annotations

import json
import time
from dataclasses import dataclass, field
from typing import Any


@dataclass
class Result:
    success: bool
    action: str = ""
    message: str = ""
    data: dict[str, Any] = field(default_factory=dict)
    error: str | None = None
    warnings: list[str] = field(default_factory=list)
    elapsed_sec: float | None = None
    returncode: int | None = None

    @classmethod
    def ok(cls, action: str = "", message: str = "", data: dict[str, Any] | None = None, **kwargs: Any) -> "Result":
        return cls(success=True, action=action, message=message, data=data or {}, **kwargs)

    @classmethod
    def fail(cls, action: str = "", error: str = "", message: str = "", data: dict[str, Any] | None = None, **kwargs: Any) -> "Result":
        return cls(success=False, action=action, message=message, data=data or {}, error=error, **kwargs)

    def to_dict(self) -> dict[str, Any]:
        d = {
            "success": self.success,
            "action": self.action,
            "message": self.message,
            "data": self.data,
        }
        if self.error:
            d["error"] = self.error
        if self.warnings:
            d["warnings"] = self.warnings
        if self.elapsed_sec is not None:
            d["elapsed_sec"] = self.elapsed_sec
        if self.returncode is not None:
            d["returncode"] = self.returncode
        return d

    def to_json(self) -> str:
        return json.dumps(self.to_dict(), ensure_ascii=False, indent=2)


class Timer:
    def __init__(self) -> None:
        self.start = time.perf_counter()

    def elapsed(self) -> float:
        return round(time.perf_counter() - self.start, 3)
