from __future__ import annotations

import json
import shlex
import socket
import time
from dataclasses import dataclass
from pathlib import Path
from typing import Any

from core.ssh import SSHClient


@dataclass(frozen=True)
class ServerControl:
    enabled: bool
    method: str
    username: str
    password: str
    ssh_port: int
    ssh_timeout_sec: int
    command_timeout_sec: int
    binary: str
    start_command: str
    stop_command: str
    retry_count: int
    retry_interval_sec: float
    listen_wait_sec: int


def load_server_control(settings_path: Path) -> ServerControl:
    raw = json.loads(settings_path.read_text(encoding="utf-8"))
    cfg = raw.get("iperf", {}).get("server_control", {})
    return ServerControl(
        enabled=bool(cfg.get("enabled", False)),
        method=str(cfg.get("method", "check_only")).strip().lower(),
        username=str(cfg.get("username", "")).strip(),
        password=str(cfg.get("password", "")),
        ssh_port=int(cfg.get("ssh_port", 22)),
        ssh_timeout_sec=int(cfg.get("ssh_timeout_sec", 20)),
        command_timeout_sec=int(cfg.get("command_timeout_sec", 30)),
        binary=str(cfg.get("binary", "iperf3")),
        start_command=str(
            cfg.get(
                "start_command",
                "nohup {binary} -s -p {port} -D >/tmp/networkautomation_iperf3.log 2>&1",
            )
        ),
        stop_command=str(cfg.get("stop_command", "pkill -f '{binary} -s -p {port}' || true")),
        retry_count=int(cfg.get("retry_count", 2)),
        retry_interval_sec=float(cfg.get("retry_interval_sec", 2)),
        listen_wait_sec=int(cfg.get("listen_wait_sec", 15)),
    )


def tcp_check_local(host: str, port: int, timeout_sec: float = 2.0) -> dict[str, Any]:
    started = time.monotonic()
    try:
        with socket.create_connection((host, port), timeout=timeout_sec):
            return {
                "success": True,
                "reachable": True,
                "host": host,
                "port": port,
                "elapsed_sec": round(time.monotonic() - started, 3),
                "method": "local_tcp",
            }
    except OSError as exc:
        return {
            "success": True,
            "reachable": False,
            "host": host,
            "port": port,
            "elapsed_sec": round(time.monotonic() - started, 3),
            "method": "local_tcp",
            "error": str(exc),
        }


def tcp_check_from_executor(
    ssh: SSHClient,
    host: str,
    port: int,
    timeout_sec: int = 3,
) -> dict[str, Any]:
    # Use Python on the executor because /dev/tcp and nc are not guaranteed.
    script = (
        "import socket,sys;"
        f"s=socket.socket();s.settimeout({int(timeout_sec)});"
        f"r=s.connect_ex(({host!r},{int(port)}));"
        "s.close();sys.exit(0 if r==0 else 1)"
    )
    command = f"python3 -c {shlex.quote(script)}"
    result = ssh.execute(command, timeout_sec + 5)
    return {
        "success": True,
        "reachable": result.success,
        "host": host,
        "port": port,
        "method": "executor_tcp",
        "command": command,
        "stdout": result.stdout,
        "stderr": result.stderr,
        "exit_code": result.exit_code,
    }


def _ssh_settings(host: str, control: ServerControl):
    settings = type("IperfServerSSH", (), {})()
    settings.host = host
    settings.port = control.ssh_port
    settings.username = control.username
    settings.password = control.password
    settings.ssh_timeout_sec = control.ssh_timeout_sec
    settings.command_timeout_sec = control.command_timeout_sec
    return settings


def start_server_via_ssh(
    host: str,
    port: int,
    control: ServerControl,
) -> dict[str, Any]:
    if not control.username:
        return {
            "success": False,
            "error": "SERVER_SSH_USERNAME_MISSING",
            "message": "iPerf Server SSH username 尚未設定。",
        }

    context = {
        "binary": control.binary,
        "port": port,
        "host": host,
    }
    start_command = control.start_command.format(**context)

    try:
        with SSHClient.from_callbox_settings(_ssh_settings(host, control)) as ssh:
            binary_check = ssh.execute(
                f"command -v {shlex.quote(control.binary)}",
                control.command_timeout_sec,
            )
            if not binary_check.success:
                return {
                    "success": False,
                    "error": "IPERF3_NOT_INSTALLED",
                    "message": f"{host} 找不到 {control.binary}。",
                    "binary_check": binary_check.to_dict(),
                }

            result = ssh.execute(start_command, control.command_timeout_sec)
            return {
                "success": result.success,
                "method": "ssh",
                "host": host,
                "port": port,
                "command": start_command,
                "stdout": result.stdout,
                "stderr": result.stderr,
                "exit_code": result.exit_code,
                "message": (
                    "iPerf Server 啟動命令已送出。"
                    if result.success
                    else "iPerf Server 啟動命令失敗。"
                ),
            }
    except Exception as exc:
        return {
            "success": False,
            "error": type(exc).__name__,
            "message": str(exc),
        }


def restart_server_via_ssh(
    host: str,
    port: int,
    control: ServerControl,
) -> dict[str, Any]:
    context = {
        "binary": control.binary,
        "port": port,
        "host": host,
    }
    try:
        with SSHClient.from_callbox_settings(_ssh_settings(host, control)) as ssh:
            stop_command = control.stop_command.format(**context)
            stop = ssh.execute(stop_command, control.command_timeout_sec)
            start_command = control.start_command.format(**context)
            start = ssh.execute(start_command, control.command_timeout_sec)
            return {
                "success": start.success,
                "method": "ssh_restart",
                "stop": stop.to_dict(),
                "start": start.to_dict(),
            }
    except Exception as exc:
        return {
            "success": False,
            "error": type(exc).__name__,
            "message": str(exc),
        }


def ensure_iperf_server(
    settings_path: Path,
    executor_ssh: SSHClient,
    host: str,
    port: int,
) -> dict[str, Any]:
    control = load_server_control(settings_path)
    initial = tcp_check_from_executor(executor_ssh, host, port)
    if initial["reachable"]:
        return {
            "success": True,
            "ready": True,
            "action": "already_listening",
            "initial_check": initial,
            "message": f"iPerf Server {host}:{port} 已在 Listen。",
        }

    if not control.enabled or control.method == "check_only":
        return {
            "success": False,
            "ready": False,
            "action": "check_only",
            "initial_check": initial,
            "error": "IPERF_SERVER_NOT_LISTENING",
            "message": (
                f"iPerf Server {host}:{port} 未 Listen，"
                "且自動啟動尚未設定。"
            ),
        }

    if control.method != "ssh":
        return {
            "success": False,
            "ready": False,
            "error": "UNSUPPORTED_SERVER_CONTROL_METHOD",
            "message": f"不支援的 Server Control method：{control.method}",
        }

    actions: list[dict[str, Any]] = []
    for attempt in range(1, control.retry_count + 2):
        action = (
            start_server_via_ssh(host, port, control)
            if attempt == 1
            else restart_server_via_ssh(host, port, control)
        )
        actions.append({
            "attempt": attempt,
            "action": action,
        })

        deadline = time.monotonic() + control.listen_wait_sec
        while time.monotonic() < deadline:
            check = tcp_check_from_executor(executor_ssh, host, port)
            if check["reachable"]:
                return {
                    "success": True,
                    "ready": True,
                    "action": "auto_started",
                    "attempt": attempt,
                    "initial_check": initial,
                    "actions": actions,
                    "final_check": check,
                    "message": f"iPerf Server {host}:{port} 已自動啟動。",
                }
            time.sleep(control.retry_interval_sec)

    final = tcp_check_from_executor(executor_ssh, host, port)
    return {
        "success": False,
        "ready": False,
        "action": "auto_start_failed",
        "initial_check": initial,
        "actions": actions,
        "final_check": final,
        "error": "IPERF_SERVER_START_FAILED",
        "message": (
            f"已嘗試自動啟動 iPerf Server，但 {host}:{port} 仍未 Listen。"
        ),
    }
