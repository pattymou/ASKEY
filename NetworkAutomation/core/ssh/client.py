#!/usr/bin/env python3
# -*- coding: utf-8 -*-
"""
core/ssh/client.py

Generic SSH/SFTP client for any network test environment.
Core must not contain Amarisoft-specific operations.
"""

from __future__ import annotations

import posixpath
from dataclasses import dataclass
from datetime import datetime
from pathlib import Path
from typing import Any, Iterable


def shell_quote(value: str) -> str:
    """Quote a string for POSIX shell usage."""
    return "'" + value.replace("'", "'\\''") + "'"


def import_paramiko():
    try:
        import paramiko  # type: ignore
        return paramiko
    except ModuleNotFoundError as exc:
        raise RuntimeError(
            "Python package 'paramiko' is required for SSH/SFTP.\n"
            "Install it on the Callbox control PC with:\n"
            "    python -m pip install paramiko"
        ) from exc


@dataclass(frozen=True)
class RemoteCommandResult:
    command: str
    exit_code: int
    stdout: str
    stderr: str

    @property
    def success(self) -> bool:
        return self.exit_code == 0

    def to_dict(self) -> dict[str, Any]:
        return {
            "command": self.command,
            "exit_code": self.exit_code,
            "stdout": self.stdout,
            "stderr": self.stderr,
            "success": self.success,
        }


class SSHClient:
    """Thin controller around Paramiko SSH/SFTP."""

    def __init__(
        self,
        host: str,
        port: int = 22,
        username: str = "root",
        password: str = "",
        ssh_timeout_sec: int = 30,
        command_timeout_sec: int = 120,
    ) -> None:
        self.host = host
        self.port = int(port)
        self.username = username
        self.password = password
        self.ssh_timeout_sec = int(ssh_timeout_sec)
        self.command_timeout_sec = int(command_timeout_sec)
        self._client = None

    @classmethod
    def from_callbox_settings(cls, settings: Any) -> "SSHClient":
        """Build client from any settings object with host/port/username/password fields."""
        return cls(
            host=settings.host,
            port=settings.port,
            username=settings.username,
            password=settings.password,
            ssh_timeout_sec=settings.ssh_timeout_sec,
            command_timeout_sec=settings.command_timeout_sec,
        )

    def connect(self) -> "SSHClient":
        if self._client is not None:
            return self
        paramiko = import_paramiko()
        client = paramiko.SSHClient()
        client.set_missing_host_key_policy(paramiko.AutoAddPolicy())
        client.connect(
            hostname=self.host,
            port=self.port,
            username=self.username,
            password=self.password,
            timeout=self.ssh_timeout_sec,
            look_for_keys=False,
            allow_agent=False,
        )
        self._client = client
        return self

    def close(self) -> None:
        if self._client is not None:
            self._client.close()
            self._client = None

    def __enter__(self) -> "SSHClient":
        return self.connect()

    def __exit__(self, exc_type, exc, tb) -> None:
        self.close()

    @property
    def client(self):
        if self._client is None:
            self.connect()
        return self._client

    def execute(self, command: str, timeout_sec: int | None = None) -> RemoteCommandResult:
        timeout = self.command_timeout_sec if timeout_sec is None else int(timeout_sec)
        stdin, stdout, stderr = self.client.exec_command(command, timeout=timeout)
        exit_code = stdout.channel.recv_exit_status()
        out = stdout.read().decode("utf-8", errors="replace")
        err = stderr.read().decode("utf-8", errors="replace")
        return RemoteCommandResult(command=command, exit_code=exit_code, stdout=out, stderr=err)

    def mkdir_p(self, remote_dir: str) -> RemoteCommandResult:
        return self.execute(f"mkdir -p {shell_quote(remote_dir)}")

    def upload(self, local_path: str | Path, remote_path: str) -> dict[str, Any]:
        local = Path(local_path)
        if not local.exists():
            raise FileNotFoundError(f"local file not found: {local}")

        remote_dir = posixpath.dirname(remote_path)
        mkdir_result = self.mkdir_p(remote_dir)
        if not mkdir_result.success:
            raise RuntimeError(
                f"remote mkdir failed, exit code {mkdir_result.exit_code}: {mkdir_result.stderr}"
            )

        sftp = self.client.open_sftp()
        try:
            sftp.put(str(local), remote_path)
        finally:
            sftp.close()

        return {
            "success": True,
            "local_path": str(local),
            "remote_path": remote_path,
            "remote_dir": remote_dir,
        }

    def backup_file(self, remote_file: str, backup_dir: str) -> dict[str, Any]:
        timestamp = datetime.now().strftime("%Y%m%d_%H%M%S")
        remote_name = posixpath.basename(remote_file)
        backup_path = posixpath.join(backup_dir, f"{remote_name}.{timestamp}.bak")

        cmd = (
            f"mkdir -p {shell_quote(backup_dir)} && "
            f"if [ -f {shell_quote(remote_file)} ]; then "
            f"cp {shell_quote(remote_file)} {shell_quote(backup_path)}; "
            f"else echo 'WARN: remote cfg not found, skip backup'; fi"
        )
        result = self.execute(cmd)
        if not result.success:
            raise RuntimeError(f"remote backup failed, exit code {result.exit_code}: {result.stderr}")

        return {
            "success": True,
            "remote_file": remote_file,
            "backup_dir": backup_dir,
            "backup_path": backup_path,
            "command": result.to_dict(),
            "warning": result.stdout.strip() if "WARN:" in result.stdout else None,
        }

    def run_commands(self, commands: Iterable[str]) -> list[dict[str, Any]]:
        results: list[dict[str, Any]] = []
        for command in commands:
            result = self.execute(command)
            results.append(result.to_dict())
            if not result.success:
                raise RuntimeError(f"remote command failed, exit code {result.exit_code}: {command}")
        return results


# Backward-compatible alias for existing Amarisoft code.
SSHController = SSHClient
