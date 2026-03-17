# Scenes

Sem patri:

- `Gym_MVP.unity`
- pripadne pomocne testovaci sceny pro animation a validation debug

Prvni scena ma obsahovat:

- testovaciho avatara
- `SceneBootstrap`
- `UIDocument` pro debug command panel
- `UIDocument` pro runtime debug view
- placeholder zed, lavicku a podlozku

Doporucena hierarchie `Gym_MVP`:

```text
Gym_MVP
  Systems
    SceneBootstrap
    EventSystem
  AvatarRoot
    AvatarStateMachine
    AvatarRuntimeEvents
    AvatarSimulationController
    CommandDispatcher
    CommandHistoryBuffer
    BootstrapNotes
  Environment
    Wall_01
    Bench_01
    Mat_01
  UI
    DebugCommandPanel
    AvatarStateDebugView
```

Minimalni serializovane reference:

- `SceneBootstrap`
  - `Configuration`
  - `AvatarStateMachine`
  - `AvatarSimulationController`
  - `CommandDispatcher`
  - `AvatarRuntimeEvents`
  - `CommandHistoryBuffer`
- `DebugCommandPanelController`
  - `UIDocument`
  - `CommandDispatcher`
  - `CommandBindings`
- `AvatarStateDebugView`
  - `UIDocument`
  - `AvatarStateMachine`
  - `AvatarSimulationController`
  - `CommandHistoryBuffer`
