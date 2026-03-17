# ExerciseSimulatorUnity

Tato slozka je `Unity`-ready projektovy scaffold odvozeny ze `UnityProjectSkeleton`.

Co je uvnitr:

- `Assets/` zkopirovane ze skeletonu
- `Packages/manifest.json` s minimalnim manifestem
- `ProjectSettings/ProjectVersion.txt` jako startovni placeholder pro `Unity 6`
- `Assets/Data/Seed/` s prvnim MVP seed katalogem pro asset authoring

Co zatim chybi:

- skutecna `.unity` scena vytvorena editorem
- `.meta` soubory vygenerovane `Unity`
- `HDRP` pipeline assety
- importovane 3D assety avatara a prostredi

Prvni krok na PC s `Unity`:

1. Otevrete tuto slozku jako projekt v `Unity 6 LTS`.
2. Nechte editor dogenerovat `.meta`, `Library/` a zbytek `ProjectSettings`.
3. Pres `Package Manager` doplnte:
   - `High Definition RP`
   - `Cinemachine`
   - `Animation Rigging`
   - `AI Navigation`
4. Vytvorte scenu `Assets/Scenes/Gym_MVP.unity`.
5. Podle `Assets/Scenes/README.md` zalozte `SceneBootstrap`, `AvatarRoot`, `Environment` a `UI`.
6. Vytvorte prvni `ScriptableObject` assety podle `UnityProjectSkeleton/README.md`.
7. Jako konkretni zdroj hodnot pouzijte `Assets/Data/Seed/`.

Poznamka:

- `ProjectVersion.txt` je placeholder. Pokud editor bude chtit upgrade nebo pouzivate jinou patch verzi `Unity 6`, potvrdit upgrade je v poradku.
