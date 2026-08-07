from __future__ import annotations

import json
import os
import tempfile
import time
from contextlib import contextmanager
from datetime import datetime, timezone
from pathlib import Path
from typing import Any

if os.name == "nt":
    import msvcrt


FINAL_STATES = {"completed", "failed", "cancelled"}


def utc_now() -> str:
    return datetime.now(timezone.utc).astimezone().isoformat(timespec="seconds")


class OperationStore:
    """Atomic, cross-process-safe file-backed operation state store."""

    def __init__(self, root: Path) -> None:
        self.root = root
        self.directory = root / "state" / "operations"
        self.directory.mkdir(parents=True, exist_ok=True)

    def path(self, operation_id: str) -> Path:
        return self.directory / f"{operation_id}.json"

    def lock_path(self, operation_id: str) -> Path:
        return self.directory / f"{operation_id}.lock"

    def result_path(self, operation_id: str) -> Path:
        return self.directory / f"{operation_id}.result.json"

    def worker_stdout_path(self, operation_id: str) -> Path:
        return self.directory / f"{operation_id}.worker.stdout.log"

    def worker_stderr_path(self, operation_id: str) -> Path:
        return self.directory / f"{operation_id}.worker.stderr.log"

    @contextmanager
    def _operation_lock(
        self,
        operation_id: str,
        timeout_sec: float = 15.0,
        poll_sec: float = 0.05,
    ):
        """
        Serialize state updates from MCP, OperationManager and Worker.

        Windows may reject os.replace() while another process is reading or
        replacing the same file. A separate lock file prevents concurrent
        writers and makes the state transition deterministic.
        """
        lock_path = self.lock_path(operation_id)
        handle = lock_path.open("a+b")
        if lock_path.stat().st_size == 0:
            handle.write(b"\0")
            handle.flush()

        deadline = time.monotonic() + timeout_sec
        acquired = False

        try:
            while time.monotonic() < deadline:
                try:
                    handle.seek(0)
                    if os.name == "nt":
                        msvcrt.locking(handle.fileno(), msvcrt.LK_NBLCK, 1)
                    else:
                        import fcntl
                        fcntl.flock(handle.fileno(), fcntl.LOCK_EX | fcntl.LOCK_NB)
                    acquired = True
                    break
                except (OSError, PermissionError):
                    time.sleep(poll_sec)

            if not acquired:
                raise TimeoutError(
                    f"Operation state lock timeout：{operation_id}"
                )

            yield

        finally:
            if acquired:
                try:
                    handle.seek(0)
                    if os.name == "nt":
                        msvcrt.locking(handle.fileno(), msvcrt.LK_UNLCK, 1)
                    else:
                        import fcntl
                        fcntl.flock(handle.fileno(), fcntl.LOCK_UN)
                except OSError:
                    pass
            handle.close()

    def read(self, operation_id: str) -> dict[str, Any]:
        path = self.path(operation_id)
        if not path.exists():
            return {
                "success": False,
                "operation_id": operation_id,
                "status": "not_found",
                "message": "找不到此 Operation。",
            }

        last_error: Exception | None = None
        for _ in range(20):
            try:
                return json.loads(path.read_text(encoding="utf-8"))
            except (PermissionError, OSError, json.JSONDecodeError) as exc:
                last_error = exc
                time.sleep(0.05)

        return {
            "success": False,
            "operation_id": operation_id,
            "status": "corrupt",
            "error": type(last_error).__name__ if last_error else "ReadError",
            "message": str(last_error or "Operation state 無法讀取。"),
        }

    def _replace_with_retry(
        self,
        source: str,
        target: Path,
        timeout_sec: float = 10.0,
    ) -> None:
        deadline = time.monotonic() + timeout_sec
        last_error: Exception | None = None

        while time.monotonic() < deadline:
            try:
                os.replace(source, target)
                return
            except (PermissionError, OSError) as exc:
                last_error = exc
                time.sleep(0.05)

        raise PermissionError(
            f"無法更新 Operation state：{target}；"
            f"最後錯誤：{last_error}"
        )

    def write(self, state: dict[str, Any]) -> None:
        operation_id = str(state["operation_id"])

        with self._operation_lock(operation_id):
            state["updated_at"] = utc_now()
            target = self.path(operation_id)
            payload = json.dumps(state, ensure_ascii=False, indent=2)

            fd, temp_name = tempfile.mkstemp(
                prefix=f"{operation_id}.",
                suffix=".tmp",
                dir=str(self.directory),
            )
            try:
                with os.fdopen(
                    fd,
                    "w",
                    encoding="utf-8",
                    newline="\n",
                ) as handle:
                    handle.write(payload)
                    handle.flush()
                    os.fsync(handle.fileno())

                self._replace_with_retry(temp_name, target)

            finally:
                try:
                    if os.path.exists(temp_name):
                        os.unlink(temp_name)
                except OSError:
                    pass

    def update(self, operation_id: str, **changes: Any) -> dict[str, Any]:
        with self._operation_lock(operation_id):
            state = self.read(operation_id)
            if state.get("status") in {"not_found", "corrupt"}:
                return state

            state.update(changes)
            state["updated_at"] = utc_now()

            target = self.path(operation_id)
            payload = json.dumps(state, ensure_ascii=False, indent=2)

            fd, temp_name = tempfile.mkstemp(
                prefix=f"{operation_id}.",
                suffix=".tmp",
                dir=str(self.directory),
            )
            try:
                with os.fdopen(
                    fd,
                    "w",
                    encoding="utf-8",
                    newline="\n",
                ) as handle:
                    handle.write(payload)
                    handle.flush()
                    os.fsync(handle.fileno())

                self._replace_with_retry(temp_name, target)

            finally:
                try:
                    if os.path.exists(temp_name):
                        os.unlink(temp_name)
                except OSError:
                    pass

            return state

    def list_states(self) -> list[dict[str, Any]]:
        states: list[dict[str, Any]] = []
        for path in self.directory.glob("*.json"):
            if path.name.endswith(".result.json"):
                continue
            try:
                states.append(json.loads(path.read_text(encoding="utf-8")))
            except (PermissionError, OSError, json.JSONDecodeError):
                continue
        return states

    def find_active(self) -> list[dict[str, Any]]:
        return [
            state
            for state in self.list_states()
            if state.get("status") not in FINAL_STATES
        ]

    def find_active_by_fingerprint(
        self,
        fingerprint: str,
    ) -> dict[str, Any] | None:
        for state in self.find_active():
            if state.get("fingerprint") == fingerprint:
                return state
        return None

    def write_result(
        self,
        operation_id: str,
        result: dict[str, Any],
    ) -> Path:
        target = self.result_path(operation_id)
        payload = json.dumps(result, ensure_ascii=False, indent=2)

        with self._operation_lock(operation_id):
            fd, temp_name = tempfile.mkstemp(
                prefix=f"{operation_id}.result.",
                suffix=".tmp",
                dir=str(self.directory),
            )
            try:
                with os.fdopen(
                    fd,
                    "w",
                    encoding="utf-8",
                    newline="\n",
                ) as handle:
                    handle.write(payload)
                    handle.flush()
                    os.fsync(handle.fileno())

                self._replace_with_retry(temp_name, target)

            finally:
                try:
                    if os.path.exists(temp_name):
                        os.unlink(temp_name)
                except OSError:
                    pass

        return target

    def read_result(self, operation_id: str) -> dict[str, Any] | None:
        path = self.result_path(operation_id)
        if not path.exists():
            return None

        for _ in range(20):
            try:
                return json.loads(path.read_text(encoding="utf-8"))
            except (PermissionError, OSError, json.JSONDecodeError):
                time.sleep(0.05)
        return None
