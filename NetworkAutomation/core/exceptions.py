from __future__ import annotations


class NetworkAutomationError(Exception):
    """Base exception for NetworkAutomation."""


class ConfigurationError(NetworkAutomationError):
    pass


class ToolExecutionError(NetworkAutomationError):
    pass


class ResultWriteError(NetworkAutomationError):
    pass
