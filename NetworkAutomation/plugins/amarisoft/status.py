import argparse,json,sys
from pathlib import Path
ROOT=Path(__file__).resolve().parents[2]
if str(ROOT) not in sys.path: sys.path.insert(0,str(ROOT))
from core.ssh import SSHClient
from plugins.amarisoft.apply_lte_to_callbox import load
def main():
 a=argparse.ArgumentParser(); a.add_argument('--settings',required=True); x=a.parse_args()
 try:
  s=load(Path(x.settings)); sp=ROOT/'state/callbox_state.json'; state=json.loads(sp.read_text(encoding='utf-8')) if sp.exists() else None
  with SSHClient.from_callbox_settings(s.callbox) as ssh:r=ssh.execute('service lte status',s.callbox.command_timeout_sec)
  o={'success':r.success,'service_running':'active (running)' in (r.stdout+r.stderr),'state':state,'service':r.to_dict()}; print(json.dumps(o,ensure_ascii=False,indent=2)); return 0 if o['success'] else 1
 except Exception as e: print(json.dumps({'success':False,'message':str(e)},ensure_ascii=False)); return 1
if __name__=='__main__': raise SystemExit(main())
