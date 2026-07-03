
#!/usr/bin/env python3
import argparse,subprocess,sys
from pathlib import Path
BASE=Path(__file__).parent
def main():
 p=argparse.ArgumentParser()
 sub=p.add_subparsers(dest="cmd",required=True)
 b=sub.add_parser("set-band")
 b.add_argument("--cell",type=int,required=True)
 b.add_argument("--band",type=int,required=True)
 b.add_argument("--bandwidth",type=float,required=True)
 b.add_argument("--dry-run",action="store_true")
 a=p.parse_args()
 if a.cmd=="set-band":
  cmd=[sys.executable,str(BASE/"apply_lte_to_callbox.py"),"--settings",str(BASE/"callbox_settings.json"),"--cell",str(a.cell),"--band",str(a.band),"--bandwidth",str(a.bandwidth)]
  if a.dry_run: cmd.append("--dry-run")
  raise SystemExit(subprocess.call(cmd))
if __name__=="__main__":
 main()
