# Seed Data

Tato slozka obsahuje prvni obsahovy seed pro MVP.

Ucel:

- zafixovat konkretni startovni hodnoty
- zrychlit zakladani prvnich `ScriptableObject` assetu
- drzet konzistenci mezi dokumentaci a runtime typy

Pouziti v `Unity`:

1. Otevrit odpovidajici JSON.
2. Vytvorit `ScriptableObject` asset odpovidajiciho typu.
3. Prepsat hodnoty z JSON do inspectoru.
4. Ulozit asset do odpovidajici slozky pod `Assets/Data/`.

Mapovani souboru:

- `behavior-profiles.json`
  - `BehaviorProfileDefinition`
- `sanctions.json`
  - `SanctionDefinition`
- `exercises.json`
  - `ExerciseDefinition`
- `commands.json`
  - `CommandDefinition`
- `wardrobe-items.json`
  - `WardrobeItemDefinition`
- `interactions.json`
  - `InteractionPresetDefinition`
- `bootstrap-configuration.json`
  - `BootstrapConfiguration`

Poznamka:

- JSON je seed a referencni sablona.
- Neni tu zatim importer, proto jsou hodnoty urcene primarne k rucnimu prepisu do assetu.
