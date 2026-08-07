import argparse,json,re,subprocess,sys
from pathlib import Path
ROOT=Path(__file__).resolve().parents[2]
def main():
 a=argparse.ArgumentParser(); a.add_argument('--settings',required=True); a.add_argument('text'); a.add_argument('--execute',action='store_true'); a.add_argument('--dry-run',action='store_true'); x=a.parse_args(); b=re.search(r'(?:band|b)\s*(\d+)',x.text,re.I); w=re.search(r'(\d+(?:\.\d+)?)\s*(?:mhz|m)\b',x.text,re.I); c=re.search(r'cell\s*(\d+)',x.text,re.I)
 if not b or not w: print(json.dumps({'success':False,'message':'請提供 Band 與 Bandwidth'},ensure_ascii=False)); return 1
 p={'success':True,'cell':int(c.group(1)) if c else 1,'band':int(b.group(1)),'bandwidth':float(w.group(1))}
 if not x.execute: print(json.dumps(p,ensure_ascii=False,indent=2)); return 0
 cmd=[sys.executable,str(ROOT/'callbox_agent.py'),'amarisoft','set-band','--cell',str(p['cell']),'--band',str(p['band']),'--bandwidth',str(p['bandwidth'])]
 if x.dry_run: cmd.append('--dry-run')
 r=subprocess.run(cmd,capture_output=True,text=True,cwd=str(ROOT)); print(r.stdout.strip()); return r.returncode
if __name__=='__main__': raise SystemExit(main())
