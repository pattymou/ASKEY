from __future__ import annotations

import argparse
import json
from pathlib import Path

from core.operation_manager import OperationManager

ROOT = Path(__file__).resolve().parent
manager = OperationManager(ROOT)


def main() -> int:
    parser = argparse.ArgumentParser(description="NetworkAutomation V12 Operation CLI")
    sub = parser.add_subparsers(dest="command", required=True)

    status = sub.add_parser("status")
    status.add_argument("operation_id")

    sub.add_parser("active")

    cancel = sub.add_parser("cancel")
    cancel.add_argument("operation_id")

    args = parser.parse_args()

    if args.command == "status":
        payload = {
            "operation": manager.read(args.operation_id),
            "result": manager.read_result(args.operation_id),
        }
    elif args.command == "active":
        recovered = manager.recover_stale_operations()
        payload = {
            "recovered": recovered,
            "operations": manager.store.find_active(),
        }
    else:
        payload = manager.cancel(args.operation_id)

    print(json.dumps(payload, ensure_ascii=False, indent=2))
    return 0


if __name__ == "__main__":
    raise SystemExit(main())
