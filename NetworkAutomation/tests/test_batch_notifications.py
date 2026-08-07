from __future__ import annotations

import json
import subprocess

from core.batch_notifier import (
    _target_from_session,
    format_batch_final,
    format_item_finished,
    format_item_started,
    resolve_recent_telegram_target,
)


def test_resolve_telegram_target_from_session_key() -> None:
    payload = {
        "sessions": [
            {
                "key": "agent:main:telegram:direct:123456789",
                "updatedAt": "2026-08-03T15:00:00+08:00",
            }
        ]
    }

    def fake_runner(args, timeout):
        return subprocess.CompletedProcess(args, 0, json.dumps(payload), "")

    assert resolve_recent_telegram_target(fake_runner) == "123456789"
    assert _target_from_session(payload["sessions"][0]) == "123456789"


def test_batch_messages_include_progress_and_result() -> None:
    item = {
        "band_config": "1A_n78A",
        "bandwidth_config": "20_100",
        "action": "upload",
        "duration_sec": 30,
    }
    started = format_item_started(2, 3, item)
    assert "[2/3]" in started
    assert "1A_n78A" in started

    row = {
        "Band": "1A_n78A",
        "BW": "20_100",
        "ARFCN": "300+623334",
        "UE連線狀態": "已連線",
        "PHY DL Mbps": 13.4,
        "PHY UL Mbps": 5.15,
        "測試類型": "PHY+iPerf",
        "iPerf方向": "upload",
        "iPerf平均 Mbps": 10.1,
        "iPerf最低 Mbps": 9.8,
        "iPerf最高 Mbps": 10.5,
        "傳輸量 MB": 37.9,
        "結果": "PASS",
        "錯誤原因": "",
    }
    finished = format_item_finished(2, 3, row)
    assert "PASS" in finished
    assert "300+623334" in finished
    assert "下一筆：[3/3]" in finished

    final = format_batch_final("completed", {"total": 3, "results": [row]})
    assert "全部完成" in final
    assert "Excel 與文字報表" in final
