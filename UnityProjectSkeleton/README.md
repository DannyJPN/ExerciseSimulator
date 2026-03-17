# Unity Project Skeleton

Tato slozka obsahuje implementacni kostru pro `Trainer-Avatar Simulator`.

Ucel:

- zafixovat zakladni adresarovou strukturu
- pripravit jadrove datove typy pro `Unity`
- vytvorit startovni body pro vertical slice `wall sit + sankce + prosba o ulevu`

Predpokladane umisteni v Unity projektu:

```text
Assets/
  Art/
    Characters/
    Clothing/
    Environment/
    Lighting/
  Data/
    Behavior/
    Commands/
    Dialogue/
    Exercises/
    Interactions/
    Sanctions/
    Wardrobe/
  Prefabs/
    Avatar/
    Environment/
    UI/
  Scenes/
  Scripts/
    Actions/
    Behavior/
    Commands/
    Core/
    Environment/
    Exercises/
    Fatigue/
    Sanctions/
    UI/
    Validation/
    Wardrobe/
  UI/
```

Soucasti teto kostry jsou navic:

- `UI Toolkit` placeholdery pro debug panel a stavovy panel
- authoring typy pro behavior, dialogue, interactions, wardrobe a modifiers
- `asmdef` soubory pro runtime a UI vrstvu
- `BootstrapConfiguration` a `AvatarSimulationController` pro prvni scene setup

Klicove vstupni body:

- `Assets/Scripts/Core/SceneBootstrap.cs`
- `Assets/Scripts/Core/BootstrapConfiguration.cs`
- `Assets/Scripts/Core/AvatarSimulationController.cs`
- `Assets/Scripts/Commands/CommandDispatcher.cs`
- `Assets/Scripts/Core/AvatarStateMachine.cs`
- `Assets/UI/DebugCommandPanel.uxml`
- `Assets/UI/AvatarStateDebugView.uxml`

Prvni doporuceny implementacni krok:

1. Vytvorit novy `Unity 6 LTS HDRP` projekt.
2. Zkopirovat celou slozku `UnityProjectSkeleton/Assets/` do Unity projektu.
3. Vytvorit prvni `ScriptableObject` assety:
   - `BootstrapConfiguration` pro prvni scenu
   - `ExerciseDefinition` pro `Wall Sit`
   - `CommandDefinition` pro spusteni cviku
   - `SanctionDefinition` pro `RepeatExercise` a `ExtendHold`
   - `BehaviorProfileDefinition` pro `Disciplined`
4. Pripojit `AvatarStateMachine`, `AvatarRuntimeEvents`, `AvatarSimulationController`, `CommandDispatcher` a `CommandHistoryBuffer` na testovaci prefab avatara.
5. Vlozit `SceneBootstrap` do sceny a referencovat na nem `BootstrapConfiguration`.
6. Pripojit `UIDocument` s `DebugCommandPanel.uxml` a `AvatarStateDebugView.uxml`.
7. Implementovat prvni tok: `wall sit -> vyhodnoceni -> zadost o ulevu nebo sankce`.
