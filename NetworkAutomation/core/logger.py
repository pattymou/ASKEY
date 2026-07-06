#!/usr/bin/env python3
# -*- coding: utf-8 -*-
from __future__ import annotations
import sys

def log(message: str) -> None:
    print(message, file=sys.stderr)
