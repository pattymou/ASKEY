from __future__ import annotations

import json
import os
import tempfile
from dataclasses import dataclass
from pathlib import Path
from typing import Any


class UserSettingError(ValueError):
    pass


@dataclass(frozen=True)
class SettingSpec:
    path: tuple[str, ...]
    label: str
    unit: str
    minimum: int
    maximum: int
    default: int


EDITABLE_SETTINGS: dict[str, SettingSpec] = {
    "wait_ue_timeout_sec": SettingSpec(
        ("workflow", "wait_ue_timeout_sec"), "UE 等待逾時", "秒", 10, 1800, 180
    ),
    "wait_ue_poll_interval_sec": SettingSpec(
        ("workflow", "wait_ue_poll_interval_sec"), "UE 檢查間隔", "秒", 1, 30, 3
    ),
    "iperf_default_duration_sec": SettingSpec(
        ("workflow", "iperf_default_duration_sec"), "iPerf 預設時間", "秒", 1, 86400, 30
    ),
    "iperf_default_port": SettingSpec(
        ("workflow", "iperf_default_port"), "iPerf 預設 Port", "", 1, 65535, 5201
    ),
    "iperf_default_parallel_streams": SettingSpec(
        ("workflow", "iperf_default_parallel_streams"), "iPerf Parallel Streams", "", 1, 128, 1
    ),
    "iperf_default_interval_sec": SettingSpec(
        ("workflow", "iperf_default_interval_sec"), "iPerf Interval", "秒", 1, 60, 1
    ),
}

ALIASES = {
    "ue_timeout": "wait_ue_timeout_sec",
    "ue_wait": "wait_ue_timeout_sec",
    "ue等待": "wait_ue_timeout_sec",
    "ue等待時間": "wait_ue_timeout_sec",
    "ue逾時": "wait_ue_timeout_sec",
    "iperf_duration": "iperf_default_duration_sec",
    "iperf時間": "iperf_default_duration_sec",
    "iperf秒數": "iperf_default_duration_sec",
    "port": "iperf_default_port",
    "parallel": "iperf_default_parallel_streams",
    "interval": "iperf_default_interval_sec",
}


def settings_path(root: Path) -> Path:
    return root / "callbox_settings.json"


def load_settings(root: Path) -> dict[str, Any]:
    path = settings_path(root)
    try:
        data = json.loads(path.read_text(encoding="utf-8"))
    except FileNotFoundError as exc:
        raise UserSettingError(f"找不到設定檔：{path}") from exc
    except json.JSONDecodeError as exc:
        raise UserSettingError(f"設定檔 JSON 格式錯誤：{exc}") from exc
    if not isinstance(data, dict):
        raise UserSettingError("callbox_settings.json 根節點必須是物件。")
    return data


def _get_path(data: dict[str, Any], path: tuple[str, ...], default: Any) -> Any:
    node: Any = data
    for key in path:
        if not isinstance(node, dict) or key not in node:
            return default
        node = node[key]
    return node


def _set_path(data: dict[str, Any], path: tuple[str, ...], value: Any) -> None:
    node = data
    for key in path[:-1]:
        child = node.get(key)
        if not isinstance(child, dict):
            child = {}
            node[key] = child
        node = child
    node[path[-1]] = value


def get_editable_settings(root: Path) -> dict[str, Any]:
    data = load_settings(root)
    values: dict[str, Any] = {}
    for key, spec in EDITABLE_SETTINGS.items():
        value = _get_path(data, spec.path, spec.default)
        try:
            value = int(value)
        except (TypeError, ValueError):
            value = spec.default
        values[key] = {
            "value": value,
            "label": spec.label,
            "unit": spec.unit,
            "minimum": spec.minimum,
            "maximum": spec.maximum,
        }
    return values


def normalize_key(key: str) -> str:
    normalized = str(key or "").strip().lower().replace("-", "_").replace(" ", "")
    normalized = ALIASES.get(normalized, normalized)
    if normalized not in EDITABLE_SETTINGS:
        allowed = "、".join(EDITABLE_SETTINGS)
        raise UserSettingError(f"不支援的設定項目：{key}。可用項目：{allowed}")
    return normalized


def update_editable_setting(root: Path, key: str, value: Any) -> dict[str, Any]:
    normalized = normalize_key(key)
    spec = EDITABLE_SETTINGS[normalized]
    try:
        parsed = int(value)
    except (TypeError, ValueError) as exc:
        raise UserSettingError(f"{spec.label} 必須是整數。") from exc
    if not spec.minimum <= parsed <= spec.maximum:
        raise UserSettingError(
            f"{spec.label} 必須介於 {spec.minimum} 到 {spec.maximum}{spec.unit}。"
        )

    data = load_settings(root)
    old_value = _get_path(data, spec.path, spec.default)
    _set_path(data, spec.path, parsed)

    path = settings_path(root)
    fd, tmp_name = tempfile.mkstemp(prefix=path.name + ".", suffix=".tmp", dir=str(path.parent))
    try:
        with os.fdopen(fd, "w", encoding="utf-8", newline="\n") as handle:
            json.dump(data, handle, ensure_ascii=False, indent=2)
            handle.write("\n")
            handle.flush()
            os.fsync(handle.fileno())
        os.replace(tmp_name, path)
    finally:
        if os.path.exists(tmp_name):
            os.unlink(tmp_name)

    return {
        "success": True,
        "key": normalized,
        "label": spec.label,
        "unit": spec.unit,
        "old_value": int(old_value),
        "new_value": parsed,
        "settings_file": str(path),
    }


def workflow_setting(root: Path, key: str) -> int:
    spec = EDITABLE_SETTINGS[key]
    data = load_settings(root)
    raw = _get_path(data, spec.path, spec.default)
    try:
        value = int(raw)
    except (TypeError, ValueError):
        return spec.default
    return value if spec.minimum <= value <= spec.maximum else spec.default


def format_settings_for_telegram(root: Path) -> str:
    values = get_editable_settings(root)
    lines = ["目前 NetworkAutomation 設定", ""]
    for item in values.values():
        suffix = f" {item['unit']}" if item["unit"] else ""
        lines.append(f"• {item['label']}：{item['value']}{suffix}")
    lines.extend([
        "",
        "修改範例：",
        "設定 UE 等待時間 300 秒",
        "設定 iPerf 預設時間 60 秒",
    ])
    return "\n".join(lines)
