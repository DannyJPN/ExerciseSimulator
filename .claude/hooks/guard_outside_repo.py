"""PreToolUse hook: require user confirmation ("ask") for anything touching paths outside the repo.

- Write / Edit / NotebookEdit: asks when the target file lies outside the project directory.
- Bash / PowerShell: asks when the command references a path outside the project directory
  (absolute Windows/Git-Bash paths, ~, $HOME, %USERPROFILE%, or relative paths with "..").

Claude Code's own session files (memory under ~/.claude/projects, scratchpad under %TEMP%/claude)
are exempt.
"""

import json
import os
import re
import sys
import tempfile


def norm(path: str, base: str) -> str:
    path = re.sub(r"\$env:(\w+)", lambda m: os.environ.get(m.group(1), m.group(0)), path.strip("\"'"))
    path = os.path.expandvars(os.path.expanduser(path))
    m = re.match(r"^/([a-zA-Z])(/.*)?$", path)  # Git Bash form /c/Users/...
    if m:
        path = f"{m.group(1)}:{m.group(2) or '/'}"
    if not os.path.isabs(path):
        path = os.path.join(base, path)
    return os.path.normcase(os.path.realpath(path))


def is_under(path: str, root: str) -> bool:
    try:
        return os.path.commonpath([path, root]) == root
    except ValueError:  # different drives
        return False


def main() -> None:
    data = json.load(sys.stdin)
    tool = data.get("tool_name", "")
    tool_input = data.get("tool_input", {}) or {}
    cwd = data.get("cwd") or os.getcwd()

    root = norm(os.environ.get("CLAUDE_PROJECT_DIR") or cwd, cwd)
    exempt = [
        norm(os.path.join("~", ".claude", "projects"), cwd),
        norm(os.path.join(tempfile.gettempdir(), "claude"), cwd),
    ]

    def outside(p: str) -> bool:
        n = norm(p, cwd)
        return not is_under(n, root) and not any(is_under(n, e) for e in exempt)

    offending = []
    if tool in ("Write", "Edit", "NotebookEdit"):
        target = tool_input.get("file_path") or tool_input.get("notebook_path")
        if target and outside(target):
            offending.append(target)
    elif tool in ("Bash", "PowerShell"):
        cmd = tool_input.get("command", "")
        stop = r"[^\s\"'|;&<>()`]*"
        patterns = [
            r"(?<![A-Za-z])[A-Za-z]:[\\/]" + stop,         # C:\... or C:/... (not https://)
            r"(?<![\w:/.-])/[A-Za-z]/" + stop,             # /c/...
            r"(?<![\w/])~[\\/]?" + stop,                   # ~/...
            r"\$HOME" + stop, r"\$env:USERPROFILE" + stop, r"%USERPROFILE%" + stop,
            r"(?<![\w/\\])\.\.(?:[\\/]" + stop + r")?",   # ../...
        ]
        for pat in patterns:
            for tok in re.findall(pat, cmd):
                if tok and outside(tok):
                    offending.append(tok)

    if offending:
        shown = ", ".join(sorted(set(offending))[:5])
        print(json.dumps({
            "hookSpecificOutput": {
                "hookEventName": "PreToolUse",
                "permissionDecision": "ask",
                "permissionDecisionReason": f"Akce sahá mimo repo ({root}): {shown}",
            }
        }))


if __name__ == "__main__":
    main()
