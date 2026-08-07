from __future__ import annotations

import argparse
import json
import os
import socket
import subprocess
import sys
import time
from functools import partial
from http.server import SimpleHTTPRequestHandler, ThreadingHTTPServer
from pathlib import Path
from typing import Any
from urllib.parse import quote
from urllib.request import urlopen

DEFAULT_PORT = 8765
PORT_SCAN_COUNT = 20
HEALTH_PATH = "/__networkautomation_report_health__"


class _ReportHandler(SimpleHTTPRequestHandler):
    server_version = "NetworkAutomationReportServer/1.0"

    def do_GET(self) -> None:  # noqa: N802 - stdlib handler name
        if self.path.split("?", 1)[0] == HEALTH_PATH:
            payload = json.dumps(
                {"ok": True, "root": str(getattr(self.server, "report_root", ""))},
                ensure_ascii=False,
            ).encode("utf-8")
            self.send_response(200)
            self.send_header("Content-Type", "application/json; charset=utf-8")
            self.send_header("Content-Length", str(len(payload)))
            self.end_headers()
            self.wfile.write(payload)
            return
        super().do_GET()

    def log_message(self, format: str, *args: Any) -> None:
        # Do not create console/log noise for every browser request.
        return


def _state_path(root: Path) -> Path:
    state_dir = root / "state"
    state_dir.mkdir(parents=True, exist_ok=True)
    return state_dir / "report_link_server.json"


def _read_state(root: Path) -> dict[str, Any]:
    path = _state_path(root)
    try:
        data = json.loads(path.read_text(encoding="utf-8"))
        return data if isinstance(data, dict) else {}
    except (OSError, ValueError, TypeError):
        return {}


def _write_state(root: Path, data: dict[str, Any]) -> None:
    path = _state_path(root)
    temp = path.with_suffix(".tmp")
    temp.write_text(json.dumps(data, ensure_ascii=False, indent=2), encoding="utf-8")
    temp.replace(path)


def _health(port: int, expected_root: Path | None = None) -> bool:
    try:
        with urlopen(f"http://127.0.0.1:{port}{HEALTH_PATH}", timeout=0.6) as response:
            if response.status != 200:
                return False
            payload = json.loads(response.read().decode("utf-8"))
        if not payload.get("ok"):
            return False
        if expected_root is not None:
            actual = Path(str(payload.get("root") or "")).resolve()
            return actual == expected_root.resolve()
        return True
    except Exception:
        return False


def _port_available(port: int) -> bool:
    with socket.socket(socket.AF_INET, socket.SOCK_STREAM) as sock:
        sock.setsockopt(socket.SOL_SOCKET, socket.SO_REUSEADDR, 1)
        try:
            sock.bind(("127.0.0.1", port))
        except OSError:
            return False
    return True


def _hidden_popen_kwargs() -> dict[str, Any]:
    kwargs: dict[str, Any] = {
        "stdin": subprocess.DEVNULL,
        "stdout": subprocess.DEVNULL,
        "stderr": subprocess.DEVNULL,
        "close_fds": True,
    }
    if os.name == "nt":
        startupinfo = subprocess.STARTUPINFO()
        startupinfo.dwFlags |= subprocess.STARTF_USESHOWWINDOW
        startupinfo.wShowWindow = subprocess.SW_HIDE
        kwargs["startupinfo"] = startupinfo
        flags = 0
        for name in ("CREATE_NO_WINDOW", "DETACHED_PROCESS", "CREATE_NEW_PROCESS_GROUP"):
            flags |= int(getattr(subprocess, name, 0))
        kwargs["creationflags"] = flags
    else:
        kwargs["start_new_session"] = True
    return kwargs


def ensure_report_server(root: Path, report_dir: Path) -> str | None:
    """Ensure a hidden localhost HTTP server exposes ``report_dir``.

    Telegram cannot turn a Windows path such as ``D:\\...`` into a clickable
    link.  A localhost HTTP URL is clickable in Telegram Web on the same PC and
    opens the exact report file without uploading it to an external service.
    """
    root = root.resolve()
    report_dir = report_dir.resolve()
    report_dir.mkdir(parents=True, exist_ok=True)

    state = _read_state(root)
    try:
        saved_port = int(state.get("port") or 0)
    except (TypeError, ValueError):
        saved_port = 0
    if saved_port and _health(saved_port, report_dir):
        return f"http://127.0.0.1:{saved_port}"

    # Reuse an already-running compatible server if a prior state file was lost.
    for port in range(DEFAULT_PORT, DEFAULT_PORT + PORT_SCAN_COUNT):
        if _health(port, report_dir):
            _write_state(root, {"port": port, "pid": None, "report_dir": str(report_dir)})
            return f"http://127.0.0.1:{port}"

    script = Path(__file__).resolve()
    for port in range(DEFAULT_PORT, DEFAULT_PORT + PORT_SCAN_COUNT):
        if not _port_available(port):
            continue
        try:
            process = subprocess.Popen(
                [
                    sys.executable,
                    str(script),
                    "--serve",
                    str(report_dir),
                    "--port",
                    str(port),
                ],
                cwd=str(root),
                env={**os.environ, "PYTHONIOENCODING": "utf-8", "PYTHONUTF8": "1"},
                **_hidden_popen_kwargs(),
            )
        except OSError:
            continue

        deadline = time.monotonic() + 5.0
        while time.monotonic() < deadline:
            if _health(port, report_dir):
                _write_state(
                    root,
                    {"port": port, "pid": process.pid, "report_dir": str(report_dir)},
                )
                return f"http://127.0.0.1:{port}"
            if process.poll() is not None:
                break
            time.sleep(0.15)
    return None


def report_url(base_url: str | None, path: str | Path) -> str | None:
    if not base_url:
        return None
    return f"{base_url.rstrip('/')}/{quote(Path(path).name)}"


def serve(report_dir: Path, port: int) -> int:
    report_dir = report_dir.resolve()
    report_dir.mkdir(parents=True, exist_ok=True)
    handler = partial(_ReportHandler, directory=str(report_dir))
    server = ThreadingHTTPServer(("127.0.0.1", port), handler)
    server.report_root = str(report_dir)  # type: ignore[attr-defined]
    server.serve_forever(poll_interval=0.5)
    return 0


def main(argv: list[str] | None = None) -> int:
    parser = argparse.ArgumentParser()
    parser.add_argument("--serve", type=Path)
    parser.add_argument("--port", type=int, default=DEFAULT_PORT)
    args = parser.parse_args(argv)
    if args.serve:
        return serve(args.serve, args.port)
    return 2


if __name__ == "__main__":
    raise SystemExit(main())
