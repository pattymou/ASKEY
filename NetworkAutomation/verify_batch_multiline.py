from __future__ import annotations

import argparse
import hashlib
import inspect
from pathlib import Path

from core import batch_notifier
from core.batch_notifier import BatchNotifier


def sha256(path: Path) -> str:
    h = hashlib.sha256()
    with path.open('rb') as stream:
        for chunk in iter(lambda: stream.read(1024 * 1024), b''):
            h.update(chunk)
    return h.hexdigest()


def main() -> int:
    parser = argparse.ArgumentParser(description='Verify active NetworkAutomation batch detail fix.')
    parser.add_argument('--send-test', action='store_true', help='Send a multiline test message to the recent Telegram target.')
    args = parser.parse_args()

    notifier_path = Path(inspect.getfile(batch_notifier)).resolve()
    worker_path = (Path(__file__).resolve().parent / "core" / "batch_worker.py").resolve()
    print(f'batch_notifier.py: {notifier_path}')
    print(f'batch_worker.py:   {worker_path}')
    print(f'transport version: {getattr(batch_notifier, "TRANSPORT_VERSION", "MISSING")}')
    print(f'notifier sha256:   {sha256(notifier_path)}')
    print(f'worker sha256:     {sha256(worker_path)}')

    source = worker_path.read_text(encoding='utf-8')
    checks = {
        'multiline transport v3': getattr(batch_notifier, 'TRANSPORT_VERSION', '') == 'openclaw-multiline-v3',
        'complete result formatter': '1. Band 設定：' in source,
        'iPerf detail fields': '平均速度：' in source and '總傳輸量：' in source,
        'no diagnostic v2 footer': '[Batch detail formatter v2]' not in source,
    }
    for label, ok in checks.items():
        print(f'[{"PASS" if ok else "FAIL"}] {label}')
    if not all(checks.values()):
        return 2

    if args.send_test:
        message = (
            '✅ 批次詳細訊息傳輸測試\n\n'
            '1. Band 設定：PASS\n\n'
            '• Band：B5（10MHz，ARFCN：2525）\n'
            '• 連線：Stable（UE 已連線）\n'
            '• PHY DL／UL：14.1 / 5.14 Mbps\n\n'
            '2. Upload iPerf（30s）：PASS\n\n'
            '• 平均速度：4.781 Mbps\n'
            '• 最高／最低：5.231 / 4.333 Mbps\n'
            '• 總傳輸量：17.927 MB'
        )
        notifier = BatchNotifier(Path.cwd())
        ok, error = notifier.send(message)
        print('Telegram multiline test:', 'PASS' if ok else f'FAIL: {error}')
        return 0 if ok else 3
    return 0


if __name__ == '__main__':
    raise SystemExit(main())
