from __future__ import annotations

import argparse
import json
import subprocess
import sys
from pathlib import Path


ROOT = Path(__file__).resolve().parent
WORKSPACE_ROOT = ROOT.parent
GENIMAGE_ROOT = WORKSPACE_ROOT.parent / "genimage"
GENIMAGE_SCRIPT = GENIMAGE_ROOT / "genimage.py"
VENV_PYTHON = WORKSPACE_ROOT.parent / "venv" / "Scripts" / "python.exe"
OUTPUT_DIR = GENIMAGE_ROOT / "generated" / "images" / "exercise_simulator"
MANIFEST_PATH = ROOT / "manifest.json"

BASE_PROMPT = (
    "Photoreal documentary-style indoor gym instructional scene. "
    "Exactly one adult human subject only. "
    "Full body visible. Realistic human anatomy, natural facial features, normal body proportions, "
    "five visible fingers on each hand when hands are shown, no stylization, no cartoon look, no mannequins, "
    "no duplicated limbs, no extra fingers, no text, no watermark. "
    "Respectful non-sexual non-violent tone. "
    "Modern small gym or training studio interior with realistic wall, floor, mat or bench only when relevant. "
    "Eye-level camera, clear readable pose, natural indoor lighting. "
)

OUTFIT_RULE = (
    "The subject wears modest neutral sportswear appropriate for exercise. "
    "If relevant, include non-removable compression shorts under training shorts for discreet presentation. "
)

UNIQUE_PROFILES = [
    "One adult woman around 18 years old, East Asian, slim build, long dark ponytail, youthful but adult face, attentive expression.",
    "One adult man around 19 years old, Black, lean athletic build, close-cropped hair, clear youthful but adult facial structure, calm expression.",
    "One adult woman around 20 years old, White Central European, petite build, chin-length brown hair, fresh adult facial features, focused look.",
    "One adult man around 21 years old, South Asian, compact build, short wavy hair, clean-shaven, alert adult expression.",
    "One adult woman around 22 years old, Latina, toned build, dark braid, energetic but realistic face, composed expression.",
    "One adult man around 23 years old, Middle Eastern, lean muscular build, short dark hair, faint stubble, serious expression.",
    "One adult woman around 24 years old, Black, athletic build, short natural curls, steady posture, realistic adult face.",
    "One adult man around 25 years old, East Asian, average build, neat straight hair, rectangular glasses, neutral expression.",
    "One adult woman around 26 years old, South Asian, average build, low braid, expressive eyes, attentive adult face.",
    "One adult man around 27 years old, White, stockier build, short light brown hair, freckles, focused expression.",
    "One adult woman around 28 years old, Middle Eastern, sturdy build, dark hair in a bun, clear cheekbones, composed expression.",
    "One adult man around 29 years old, Black, tall athletic build, close-cropped hair, broad shoulders, relaxed serious look.",
    "One adult woman around 30 years old, East Asian, average build, shoulder-length straight hair, calm mature face.",
    "One adult man around 31 years old, Latino, lean build, short curly hair, trimmed stubble, observant expression.",
    "One adult woman around 32 years old, White Central European, powerful lower body, auburn ponytail, practical expression.",
    "One adult man around 33 years old, South Asian, average build, side-parted dark hair, trimmed beard, calm eyes.",
    "One adult woman around 34 years old, Black, curvy athletic build, tightly coiled short hair, self-possessed expression.",
    "One adult man around 35 years old, East Asian, compact sturdy build, short hair, faint smile lines, concentrated face.",
    "One adult woman around 36 years old, Latina, average sturdy build, thick dark braid, realistic mature expression.",
    "One adult man around 37 years old, White, broad build, short sandy hair, trimmed beard, disciplined look.",
    "One adult woman around 38 years old, Middle Eastern, lean build, dark shoulder-length hair, serious watchful expression.",
    "One adult man around 39 years old, Black, muscular build, shaved head, realistic facial lines, composed demeanor.",
    "One adult woman around 40 years old, East Asian, average build, short bob haircut, determined face.",
    "One adult man around 41 years old, South Asian, sturdy build, salt-and-pepper beard beginning, deep calm expression.",
    "One adult woman around 42 years old, White, strong build, dark blonde ponytail, natural facial lines, focused face.",
    "One adult man around 43 years old, Latino, average build, curly dark hair, trimmed moustache and beard, grounded expression.",
    "One adult woman around 44 years old, Black, athletic build, braided hair tied back, attentive face with subtle age lines.",
    "One adult man around 45 years old, Middle Eastern, stocky build, receding hairline, neat beard, firm expression.",
    "One adult woman around 46 years old, South Asian, sturdy build, dark hair in a bun, clearly mature facial features.",
    "One adult man around 47 years old, East Asian, slim build, short grey-black hair, glasses, thoughtful face.",
    "One adult woman around 48 years old, Latina, average build, shoulder-length dark hair, realistic smile lines, calm expression.",
    "One adult man around 49 years old, White Central European, broad build, salt-and-pepper hair, trimmed beard, strict expression.",
    "One adult woman around 50 years old, Black, sturdy build, cropped natural hair, confident composed face.",
    "One adult man around 51 years old, South Asian, average build, short silvering hair, moustache, observant look.",
    "One adult woman around 52 years old, East Asian, lean mature build, grey-streaked bob haircut, disciplined expression.",
    "One adult man around 53 years old, Latino, compact build, short grey hair, lined realistic face, calm posture.",
    "One adult woman around 54 years old, White, sturdy build, light brown hair tied back, mature focused face.",
    "One adult man around 55 years old, Black, wiry athletic build, short twists with grey at the temples, determined expression.",
    "One adult woman around 57 years old, Middle Eastern, slim build, curly dark hair tied high with some grey strands, alert face.",
    "One adult man around 59 years old, East Asian, average build, brushed-back greying hair, clean-shaven, measured expression.",
    "One adult woman around 61 years old, South Asian, curvy strong build, long braid with grey strands, warm but disciplined face.",
    "One adult man around 63 years old, White, lean build, close-cropped grey hair, realistic facial lines, intense expression.",
    "One adult woman around 65 years old, Latina, athletic build for her age, shoulder-length wavy hair with grey, ambitious focused face."
]

OUTFIT_TOPS = [
    "charcoal short-sleeve performance tee",
    "navy sleeveless training top",
    "forest green fitted athletic tee",
    "burgundy lightweight long-sleeve base layer",
    "slate quarter-zip training top",
    "sand beige plain workout tee",
    "deep blue compression top",
    "dark olive loose training jersey",
    "rust red fitted sports top",
    "light grey breathable gym shirt",
    "black minimalist athletic top",
    "dark teal plain training shirt",
    "maroon compression tee"
]

OUTFIT_BOTTOMS = [
    "black training shorts over non-removable compression shorts",
    "navy tapered training pants",
    "graphite three-quarter leggings",
    "dark olive jogger pants",
    "charcoal training shorts",
    "black cropped gym pants",
    "midnight blue stretch track pants",
    "dark grey shorts over compression shorts",
    "deep green fitted training bottoms",
    "black full-length leggings",
    "charcoal slim joggers"
]

OUTFIT_FOOTWEAR = [
    "white trainers and black socks",
    "dark trainers and ankle socks",
    "grey trainers and crew socks",
    "navy trainers and white socks",
    "black trainers and grey socks",
    "light trainers and black socks"
]


def load_manifest() -> dict:
    return json.loads(MANIFEST_PATH.read_text(encoding="utf-8"))


def build_prompt(profile_text: str, outfit_text: str, scene: dict) -> str:
    return (
        BASE_PROMPT
        + f"Subject: {profile_text} "
        + OUTFIT_RULE
        + f"Outfit for this scene: {outfit_text}. "
        + f"Scene: {scene['description']}"
    ).strip()


def generate_scene(scene: dict, profile_text: str, outfit_text: str, dry_run: bool) -> None:
    prompt = build_prompt(profile_text, outfit_text, scene)
    out_path = OUTPUT_DIR / f"{scene['id']}.png"

    command = [
        str(VENV_PYTHON),
        str(GENIMAGE_SCRIPT),
        "image",
        "--model",
        "gpt-image-1.5",
        "--size",
        "1024x1536",
        "--quality",
        "high",
        "--output-format",
        "png",
        "--background",
        "opaque",
        "--prompt",
        prompt,
        "--out",
        str(out_path),
    ]

    print(f"[scene] {scene['id']}")
    print(f"[out]   {out_path}")
    if dry_run:
        print(f"[prompt] {prompt}\n")
        return

    OUTPUT_DIR.mkdir(parents=True, exist_ok=True)

    subprocess.run(command, check=True)


def build_unique_outfit(index: int, scene: dict) -> str:
    scene_id = scene["id"]
    if scene_id == "EX_WallSit_LessClothing_ShoesOffSocksOff":
        top = OUTFIT_TOPS[index % len(OUTFIT_TOPS)]
        return f"{top}, dark training shorts over non-removable compression shorts, barefoot with shoes and socks removed"

    if scene_id == "CMD_RemoveShoes":
        top = OUTFIT_TOPS[index % len(OUTFIT_TOPS)]
        bottom = OUTFIT_BOTTOMS[(index * 3) % len(OUTFIT_BOTTOMS)]
        footwear = OUTFIT_FOOTWEAR[(index * 5) % len(OUTFIT_FOOTWEAR)]
        return f"{top}, {bottom}, {footwear}, one shoe being removed by hand in a seated posture"

    if scene_id == "CMD_RemoveSocks":
        top = OUTFIT_TOPS[index % len(OUTFIT_TOPS)]
        bottom = OUTFIT_BOTTOMS[(index * 3) % len(OUTFIT_BOTTOMS)]
        return f"{top}, {bottom}, barefoot with one sock partly removed by hand"

    top = OUTFIT_TOPS[index % len(OUTFIT_TOPS)]
    bottom = OUTFIT_BOTTOMS[(index * 3) % len(OUTFIT_BOTTOMS)]
    footwear = OUTFIT_FOOTWEAR[(index * 5) % len(OUTFIT_FOOTWEAR)]
    return f"{top}, {bottom}, {footwear}"


def main() -> int:
    parser = argparse.ArgumentParser(description="Generate the Exercise Simulator image set.")
    parser.add_argument("--dry-run", action="store_true", help="Print prompts without calling genimage.")
    parser.add_argument("--only", nargs="*", help="Optional list of scene ids to generate.")
    args = parser.parse_args()

    manifest = load_manifest()
    scenes = manifest["scenes"]
    wanted = set(args.only or [])

    for index, scene in enumerate(scenes):
        if wanted and scene["id"] not in wanted:
            continue

        profile_text = UNIQUE_PROFILES[index % len(UNIQUE_PROFILES)]
        outfit_text = build_unique_outfit(index, scene)
        generate_scene(scene, profile_text, outfit_text, args.dry_run)

    return 0


if __name__ == "__main__":
    sys.exit(main())
