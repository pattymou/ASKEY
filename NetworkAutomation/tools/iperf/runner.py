#!/usr/bin/env python3
# -*- coding: utf-8 -*-
from __future__ import annotations

import json
import shutil
import subprocess
from dataclasses import dataclass
from typing import Any


@dataclass
class IperfRunner:
    iperf_bin: str = "iperf3"

    def _ensure_binary(self) -> None:
        if shutil.which(self.iperf_bin) is None:
            raise RuntimeError(f"iperf binary not found: {self.iperf_bin}. Install iperf3 or pass --bin path.")

    def client(
        self,
        server: str,
        port: int = 5201,
        duration: int = 10,
        parallel: int = 1,
        protocol: str = "tcp",
        reverse: bool = False,
        bandwidth: str | None = None,
    ) -> dict[str, Any]:
        self._ensure_binary()
        cmd = [self.iperf_bin, "-c", server, "-p", str(port), "-t", str(duration), "-P", str(parallel), "-J"]
        if protocol.lower() == "udp":
            cmd.append("-u")
            if bandwidth:
                cmd.extend(["-b", bandwidth])
        if reverse:
            cmd.append("-R")

        p = subprocess.run(cmd, capture_output=True, text=True)
        if p.returncode != 0:
            return {"success": False, "tool": "iperf3", "command": cmd, "returncode": p.returncode, "stdout": p.stdout, "stderr": p.stderr}

        try:
            raw = json.loads(p.stdout)
        except Exception as exc:
            return {"success": False, "tool": "iperf3", "error": f"INVALID_IPERF_JSON: {exc}", "stdout": p.stdout, "stderr": p.stderr}

        return {
            "success": True,
            "tool": "iperf3",
            "role": "client",
            "server": server,
            "port": port,
            "duration_sec": duration,
            "parallel": parallel,
            "protocol": protocol.lower(),
            "reverse": reverse,
            "summary": summarize_iperf_json(raw, protocol.lower()),
            "raw": raw,
        }


def summarize_iperf_json(raw: dict[str, Any], protocol: str) -> dict[str, Any]:
    end = raw.get("end", {})
    if protocol == "udp":
        udp = end.get("sum", {}) or end.get("sum_received", {}) or {}
        bps = float(udp.get("bits_per_second", 0) or 0)
        return {
            "throughput_mbps": round(bps / 1_000_000, 3),
            "jitter_ms": udp.get("jitter_ms"),
            "lost_percent": udp.get("lost_percent"),
            "lost_packets": udp.get("lost_packets"),
            "packets": udp.get("packets"),
            "retransmits": None,
        }
    tcp = end.get("sum_received") or end.get("sum_sent") or {}
    sent = end.get("sum_sent", {})
    bps = float(tcp.get("bits_per_second", 0) or 0)
    return {
        "throughput_mbps": round(bps / 1_000_000, 3),
        "retransmits": sent.get("retransmits"),
        "jitter_ms": None,
        "lost_percent": None,
    }
