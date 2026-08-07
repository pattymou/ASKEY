from __future__ import annotations

import json
import sys
import tempfile
from datetime import datetime, timedelta, timezone
from pathlib import Path

ROOT = Path(__file__).resolve().parents[1]
if str(ROOT) not in sys.path:
    sys.path.insert(0, str(ROOT))

from core.followup_context import (
    clear_followup_context,
    load_followup_context,
    save_wait_download_context,
)


def main() -> None:
    with tempfile.TemporaryDirectory() as temp_dir:
        root = Path(temp_dir)
        status, context = load_followup_context(root)
        assert status == "missing" and context is None

        saved = save_wait_download_context(
            root,
            ue_ip="192.168.2.2",
            duration_sec=30,
            operation_id="op-test",
            result={"success": True},
        )
        assert saved["stage"] == "wait_download_confirm"
        status, context = load_followup_context(root)
        assert status == "active"
        assert context and context["ue_ip"] == "192.168.2.2"
        assert context["duration_sec"] == 30

        path = root / "followup_context.json"
        expired = dict(context)
        expired["expires_at"] = (
            datetime.now(timezone.utc) - timedelta(seconds=1)
        ).isoformat()
        path.write_text(json.dumps(expired), encoding="utf-8")
        status, _ = load_followup_context(root)
        assert status == "expired"
        assert not path.exists()

        clear_followup_context(root)

    print("V12.3.5 follow-up context tests: PASS")


if __name__ == "__main__":
    main()
