from pathlib import Path

ROOT = Path(__file__).resolve().parents[1]

def test_agents_forbids_project_search_for_band_commands():
    text = (ROOT / 'workspace' / 'AGENTS.md').read_text(encoding='utf-8')
    assert 'Get-ChildItem' in text
    assert 'Select-String' in text
    assert '嚴禁' in text
    assert '請指定方向：DL、UL，或兩者。' in text
    assert '第一次工具呼叫' in text

def test_prompt_requires_direct_mcp_routing():
    text = (ROOT / 'workspace' / 'PROMPT.md').read_text(encoding='utf-8')
    assert '第一次動作就是呼叫該工具' in text
    assert 'networkautomation_execute_intent' in text
    assert '不得搜尋程式碼' in text
