# Unity Project Skeleton

Tato slozka obsahuje implementacni kostru pro `Trainer-Avatar Simulator`.

Ucel:

- zafixovat zakladni adresarovou strukturu
- pripravit jadrove datove typy pro `Unity`
- vytvorit startovni body pro vertical slice `wall sit + sankce + prosba o ulevu`

Predpokladane umisteni v Unity projektu:

```text
Assets/
  Scripts/
    Actions/
    Behavior/
    Commands/
    Core/
    Environment/
    Exercises/
    Fatigue/
    Sanctions/
    Validation/
    Wardrobe/
```

Prvni doporuceny implementacni krok:

1. Vytvorit novy `Unity 6 LTS HDRP` projekt.
2. Zkopirovat obsah `Assets/Scripts/` do Unity projektu.
3. Vytvorit prvni `ScriptableObject` assety:
   - `ExerciseDefinition` pro `Wall Sit`
   - `CommandDefinition` pro spusteni cviku
   - `SanctionDefinition` pro `RepeatExercise` a `ExtendHold`
4. Pripojit `AvatarStateMachine` a `CommandDispatcher` na testovaci prefab avatara.
5. Implementovat prvni tok: `wall sit -> vyhodnoceni -> zadost o ulevu nebo sankce`.
