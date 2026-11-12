# Exercise Simulator Image Generation

Tato slozka obsahuje manifest a generator promptu pro obrazovy katalog `Exercise Simulator`.

Obsah:

- `manifest.json`
  - seznam vsech scen
- `generate_exercise_reference.py`
  - slozi finalni prompty a zavola `..\genimage\genimage.py`

Vystupy:

- obrazky se ukladaji do `..\genimage\generated\images\exercise_simulator`
- metadata a raw API odpovedi uklada `genimage` do `..\genimage\generated\json`

Pouziti:

```powershell
C:\Users\Krupa\Desktop\Private\venv\Scripts\python.exe .\image_generation\generate_exercise_reference.py
```

Jen vypsat finalni prompty bez generace:

```powershell
C:\Users\Krupa\Desktop\Private\venv\Scripts\python.exe .\image_generation\generate_exercise_reference.py --dry-run
```
