from __future__ import annotations

import json
import os
import tempfile
from datetime import datetime, timedelta, timezone
from pathlib import Path
from typing import Any

FOLLOWUP_TTL_SEC = 30 * 60
CONTEXT_FILENAME = "followup_context.json"


def _now() -> datetime:
    return datetime.now(timezone.utc)


def _parse_timestamp(value: Any) -> datetime | None:
    if not isinstance(value, str) or not value.strip():
        return None
    try:
        parsed = datetime.fromisoformat(value.replace("Z", "+00:00"))
    except ValueError:
        return None
    if parsed.tzinfo is None:
        parsed = parsed.replace(tzinfo=timezone.utc)
    return parsed.astimezone(timezone.utc)


def _path(root: Path) -> Path:
    return root / CONTEXT_FILENAME


def _atomic_write(path: Path, payload: dict[str, Any]) -> None:
    path.parent.mkdir(parents=True, exist_ok=True)
    fd, temp_name = tempfile.mkstemp(
        prefix=f".{path.name}.", suffix=".tmp", dir=str(path.parent)
    )
    try:
        with os.fdopen(fd, "w", encoding="utf-8") as handle:
            json.dump(payload, handle, ensure_ascii=False, indent=2)
            handle.flush()
            os.fsync(handle.fileno())
        os.replace(temp_name, path)
    finally:
        if os.path.exists(temp_name):
            os.unlink(temp_name)


def clear_followup_context(root: Path) -> None:
    try:
        _path(root).unlink()
    except FileNotFoundError:
        pass


def save_wait_download_context(
    root: Path,
    *,
    ue_ip: str,
    duration_sec: int,
    operation_id: str,
    result: dict[str, Any],
    ttl_sec: int = FOLLOWUP_TTL_SEC,
) -> dict[str, Any]:
    created_at = _now()
    payload: dict[str, Any] = {
        "schema_version": "1.0",
        "stage": "wait_download_confirm",
        "created_at": created_at.isoformat(),
        "expires_at": (created_at + timedelta(seconds=ttl_sec)).isoformat(),
        "ttl_sec": ttl_sec,
        "ue_ip": ue_ip,
        "duration_sec": int(duration_sec),
        "upload_operation_id": operation_id,
        "upload_result": result,
    }
    _atomic_write(_path(root), payload)
    return payload


def save_wait_summary_context(
    root: Path,
    *,
    previous: dict[str, Any],
    operation_id: str,
    result: dict[str, Any],
    ttl_sec: int = FOLLOWUP_TTL_SEC,
) -> dict[str, Any]:
    created_at = _now()
    payload = dict(previous)
    payload.update(
        {
            "stage": "wait_summary_confirm",
            "created_at": created_at.isoformat(),
            "expires_at": (created_at + timedelta(seconds=ttl_sec)).isoformat(),
            "ttl_sec": ttl_sec,
            "download_operation_id": operation_id,
            "download_result": result,
        }
    )
    _atomic_write(_path(root), payload)
    return payload


def load_followup_context(root: Path) -> tuple[str, dict[str, Any] | None]:
    """Return (status, context), where status is active, expired, or missing."""
    path = _path(root)
    try:
        with path.open("r", encoding="utf-8") as handle:
            payload = json.load(handle)
    except FileNotFoundError:
        return "missing", None
    except (OSError, json.JSONDecodeError):
        clear_followup_context(root)
        return "missing", None

    if not isinstance(payload, dict):
        clear_followup_context(root)
        return "missing", None

    expires_at = _parse_timestamp(payload.get("expires_at"))
    if expires_at is None or _now() >= expires_at:
        clear_followup_context(root)
        return "expired", payload
    return "active", payload


def extract_successful_ue_ip(result: dict[str, Any]) -> str | None:
    machine = result.get("machine_result")
    if not isinstance(machine, dict):
        return None
    candidates = [
        machine.get("ue_ip"),
        (machine.get("parameters") or {}).get("ue_ip")
        if isinstance(machine.get("parameters"), dict)
        else None,
    ]
    for candidate in candidates:
        if isinstance(candidate, str) and candidate.strip():
            return candidate.strip()
    return None
