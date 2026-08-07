from __future__ import annotations
from dataclasses import asdict, dataclass
from pathlib import Path
from typing import Any
import paramiko
@dataclass
class CommandResult:
    success: bool; command: str; exit_code: int; stdout: str; stderr: str
    def to_dict(self)->dict[str,Any]: return asdict(self)
class SSHClient:
    def __init__(self,host,port,username,password,ssh_timeout_sec=30,command_timeout_sec=120):
        self.host=host; self.port=port; self.username=username; self.password=password; self.ssh_timeout_sec=ssh_timeout_sec; self.command_timeout_sec=command_timeout_sec; self._client=None
    @classmethod
    def from_callbox_settings(cls,s):
        return cls(str(s.host),int(getattr(s,'port',22)),str(s.username),str(getattr(s,'password','')),int(getattr(s,'ssh_timeout_sec',30)),int(getattr(s,'command_timeout_sec',120)))
    def __enter__(self):
        c=paramiko.SSHClient(); c.set_missing_host_key_policy(paramiko.AutoAddPolicy()); c.connect(self.host,port=self.port,username=self.username,password=self.password,timeout=self.ssh_timeout_sec,banner_timeout=self.ssh_timeout_sec,auth_timeout=self.ssh_timeout_sec); self._client=c; return self
    def __exit__(self,*a):
        if self._client: self._client.close(); self._client=None
    def execute(self,command,timeout_sec=None):
        if not self._client: raise RuntimeError('SSH not connected')
        _,o,e=self._client.exec_command(command,timeout=timeout_sec or self.command_timeout_sec); out=o.read().decode(errors='replace'); err=e.read().decode(errors='replace'); code=o.channel.recv_exit_status(); return CommandResult(code==0,command,code,out,err)
    def upload(self,local_path,remote_path):
        with self._client.open_sftp() as sftp: sftp.put(str(local_path),remote_path)
        return {'success':True,'local_path':str(local_path),'remote_path':remote_path}
    def backup_file(self,remote_path,backup_dir):
        cmd=f"mkdir -p {backup_dir} && if [ -f {remote_path} ]; then cp {remote_path} {backup_dir}/$(basename {remote_path}).$(date +%Y%m%d_%H%M%S).bak; fi"; r=self.execute(cmd); return {'success':r.success,'stdout':r.stdout,'stderr':r.stderr}
