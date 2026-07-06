# NetworkAutomation V5

## New direction
This version separates generic platform components from Amarisoft-specific logic.

## Keep
- `callbox_agent.py` as the single CLI entry point.
- `callbox_settings.json` for Amarisoft settings.
- `plugins/amarisoft/apply_lte_to_callbox.py`
- `plugins/amarisoft/lte_config_modifier.py`
- `plugins/amarisoft/AutoConfig.cfg`
- `plugins/amarisoft/Earfcn_LTE.json`
- `plugins/amarisoft/update_settings.py`

## New
- `core/result.py`
- `core/logger.py`
- `core/ssh/client.py`
- `plugins/amarisoft/controller.py`
- `tools/iperf/runner.py`

## Delete after test passes
- root `controller/` folder
- root `controller.py`
- root `apply_lte_to_callbox.py`
- root `lte_config_modifier.py`
- root `update_settings.py`
- root `AutoConfig.cfg`
- root `Earfcn_LTE.json`

Keep those root files temporarily until dry-run works.

## Test commands

Dry-run Amarisoft LTE config generation:

```bash
python callbox_agent.py amarisoft set-band --cell 1 --band 5 --bandwidth 10 --dry-run
```

Legacy compatible command:

```bash
python callbox_agent.py set-band --cell 1 --band 5 --bandwidth 10 --dry-run
```

Generic iperf client:

```bash
python callbox_agent.py iperf client --server 192.168.1.100 --time 10 --parallel 1
```
