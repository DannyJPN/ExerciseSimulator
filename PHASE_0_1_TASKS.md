# Phase 0-1 Task Breakdown

## Ucel

Tento dokument rozepisuje `Fazi 0` a `Fazi 1` z [IMPLEMENTATION_PLAN.md](C:\Users\Krupa\Desktop\Private\surrendering\IMPLEMENTATION_PLAN.md) do konkretni sady ukolu pro prvni skutecny `Unity` setup.

Cil teto etapy:

- zalozit kompilujici `Unity 6 LTS HDRP` projekt
- prenest a stabilizovat skeleton
- zprovoznit prvni centralni runtime model
- overit tok `UI -> command -> avatar state`

## Scope teto etapy

Etapa neresi:

- realny pohyb ke zdi
- plne cviky
- validaci formy
- unavu, bolest a sankce jako hotovy gameplay

Etapa musi dodat:

- projekt
- scenu
- avatar prefab placeholder
- interaktivni placeholder objekty
- runtime state model
- command dispatch
- jednoduche UI test tlacitko nebo panel

## Vystupy etapy

Na konci musi existovat:

- funkcni `Unity` projekt
- scena `Gym_MVP`
- prefab avatara s runtime komponentami
- pripraveny panel pro odeslani prvniho prikazu
- schopnost zmenit stav avatara z UI
- logovatelny command flow

## Adresarovy cil v Unity projektu

```text
Assets/
  Art/
    Characters/
    Environments/
    Props/
    Materials/
  Data/
    Commands/
    Exercises/
    Sanctions/
    Dialogue/
    Wardrobe/
  Prefabs/
    Avatar/
    Environment/
    UI/
  Scenes/
    Gym_MVP.unity
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
```

## Faze 0: Project Bootstrap

### Task 0.1: Zalozit Unity projekt

Popis:
Vytvorit novy projekt `Unity 6 LTS` s `HDRP`.

Hotovo kdyz:

- projekt jde otevrit
- scena se spusti
- HDRP je aktivni

Poznamky:

- neinstalovat predcasne zbytecne baliky
- drzet setup minimalisticky

### Task 0.2: Zalozit repozitarovou strukturu

Popis:
Pripravit adresare pro `Assets`, data, prefab slozky a skripty.

Hotovo kdyz:

- struktura odpovida technicke specifikaci
- existuji prazdne slozky pro data, prefaby a sceny

### Task 0.3: Prenest skeleton skriptu

Zdroj:
[UnityProjectSkeleton](C:\Users\Krupa\Desktop\Private\surrendering\UnityProjectSkeleton)

Popis:
Zkopirovat kostru skriptu do `Assets/Scripts`.

Soubory, ktere maji byt preneseny jako prvni:

- [AvatarEnums.cs](C:\Users\Krupa\Desktop\Private\surrendering\UnityProjectSkeleton\Assets\Scripts\Core\AvatarEnums.cs)
- [AvatarRuntimeState.cs](C:\Users\Krupa\Desktop\Private\surrendering\UnityProjectSkeleton\Assets\Scripts\Core\AvatarRuntimeState.cs)
- [AvatarStateMachine.cs](C:\Users\Krupa\Desktop\Private\surrendering\UnityProjectSkeleton\Assets\Scripts\Core\AvatarStateMachine.cs)
- [CommandDefinition.cs](C:\Users\Krupa\Desktop\Private\surrendering\UnityProjectSkeleton\Assets\Scripts\Commands\CommandDefinition.cs)
- [CommandDispatcher.cs](C:\Users\Krupa\Desktop\Private\surrendering\UnityProjectSkeleton\Assets\Scripts\Commands\CommandDispatcher.cs)
- [ExerciseDefinition.cs](C:\Users\Krupa\Desktop\Private\surrendering\UnityProjectSkeleton\Assets\Scripts\Exercises\ExerciseDefinition.cs)
- [InteractableObject.cs](C:\Users\Krupa\Desktop\Private\surrendering\UnityProjectSkeleton\Assets\Scripts\Environment\InteractableObject.cs)
- [BehaviorState.cs](C:\Users\Krupa\Desktop\Private\surrendering\UnityProjectSkeleton\Assets\Scripts\Behavior\BehaviorState.cs)
- [FatigueState.cs](C:\Users\Krupa\Desktop\Private\surrendering\UnityProjectSkeleton\Assets\Scripts\Fatigue\FatigueState.cs)
- [WardrobeState.cs](C:\Users\Krupa\Desktop\Private\surrendering\UnityProjectSkeleton\Assets\Scripts\Wardrobe\WardrobeState.cs)
- [SanctionDefinition.cs](C:\Users\Krupa\Desktop\Private\surrendering\UnityProjectSkeleton\Assets\Scripts\Sanctions\SanctionDefinition.cs)
- [FormEvaluationResult.cs](C:\Users\Krupa\Desktop\Private\surrendering\UnityProjectSkeleton\Assets\Scripts\Validation\FormEvaluationResult.cs)

Hotovo kdyz:

- skripty jsou v Unity importovane
- nejsou compile errors z namespace nebo chybejicich typu

### Task 0.4: Nastavit zakladni baliky

Baliky:

- `Input System`
- `Cinemachine`
- `AI Navigation`
- `Animation Rigging`

Hotovo kdyz:

- baliky jsou nainstalovane
- projekt po instalaci stale kompiluje

Poznamka:

- `Addressables` lze nainstalovat uz zde, ale nebude hned vyuzito

### Task 0.5: Vytvorit scenu Gym_MVP

Popis:
Pripravit prvni hlavni scenu s neutralnim osvetlenim a placeholder prostredim.

Minimalni obsah sceny:

- rovna podlaha
- jedna zed
- jedna lavicka placeholder
- jedna podlozka placeholder
- kamera
- smerove svetlo

Hotovo kdyz:

- scena existuje jako `Assets/Scenes/Gym_MVP.unity`
- scena jde otevrit a spustit

### Task 0.6: Vytvorit placeholder prefaby prostredi

Prefaby:

- `Wall_Interactable`
- `Bench_Interactable`
- `Mat_Interactable`

Komponenty:

- `InteractableObject`
- aspon jeden `InteractionPoint`

Hotovo kdyz:

- vsechny tri objekty existuji jako prefaby
- jdou vlozit do sceny
- maji konzistentni naming

### Task 0.7: Vytvorit avatar prefab placeholder

Popis:
Pripravit testovaci humanoidni prefab i za cenu provizorni grafiky.

Minimalni komponenty:

- `Animator`
- `AvatarStateMachine`
- `CommandDispatcher`
- placeholder presenter pro debug stav

Hotovo kdyz:

- prefab jde vlozit do sceny
- ma reference mezi stavovymi komponentami

### Task 0.8: Git a projektova hygiena

Popis:
Nastavit `gitignore`, text serialization a stabilni asset workflow.

Hotovo kdyz:

- Unity ma `Force Text`
- meta soubory jsou verzovane
- repozitar je pripraveny na dalsi iterace

## Faze 1: Core Runtime and State Model

### Task 1.1: Stabilizovat AvatarRuntimeState

Popis:
Udelat z `AvatarRuntimeState` centralni zdroj pravdy o stavu avatara.

Musi obsahovat:

- current posture
- active command id
- active exercise id
- fatigue state
- wardrobe state
- behavior state

Hotovo kdyz:

- vsechny runtime podsystemy ctou z jednoho mista
- nevznika duplikace stavu napric komponentami

### Task 1.2: Dopsat pravidla prechodu stavu

Popis:
`AvatarStateMachine` zatim jen nastavuje stav. To nestaci.

Je potreba:

- nadefinovat povolene prechody
- zavest guardy
- priprava pro preruseni prikazem s vyssi prioritou

Minimalni stavy pro tuto etapu:

- `Idle`
- `Standing`
- `Sitting`
- `Kneeling`
- `Lying`
- `Moving`
- `WardrobeChange`
- `Negotiating`

Hotovo kdyz:

- neplatne prechody jsou blokovane
- stav se nemeni "natvrdo" bez validace

### Task 1.3: Zaviest command execution context

Popis:
Udelat objekt nebo strukturu, ktera nese data o prave bezicim prikazu.

Minimalni obsah:

- command id
- command type
- timestamp start
- interrupted flag
- linked exercise id
- source ui action

Doporucene soubory:

- `Assets/Scripts/Commands/CommandExecutionContext.cs`

Hotovo kdyz:

- dispatcher nedrzi jen holy string
- command life-cycle je sledovatelny

### Task 1.4: Rozsirit CommandDispatcher

Popis:
`CommandDispatcher` musi byt vic nez prepinac enumu.

Musi umet:

- prijmout prikaz z UI
- overit, ze command muze bezet
- zapsat context
- prerusit predchozi command
- spustit zmenu stavu
- vyemitovat udalost do debug vrstvy

Hotovo kdyz:

- opakovane kliknuti na ruzne prikazy nezpusobi nekonzistentni stav
- posledni prikaz je vzdy znamy

### Task 1.5: Pridat runtime udalosti

Popis:
Pripravit jednoduchy event layer pro dalsi moduly.

Minimalni udalosti:

- `OnCommandStarted`
- `OnCommandInterrupted`
- `OnStateChanged`
- `OnExerciseAssigned`
- `OnWardrobeCommandStarted`

Doporucene soubory:

- `Assets/Scripts/Core/AvatarRuntimeEvents.cs`

Hotovo kdyz:

- debug UI umi zobrazit, co se deje
- dalsi moduly nemusi byt tvrde napojene na dispatcher

### Task 1.6: Prvni UI panel pro debug commandy

Popis:
Pripravit minimalni `UI Toolkit` panel s par tlacitky.

Tlacitka pro tuto etapu:

- `Standing`
- `Sitting`
- `Kneeling`
- `Lie On Back`
- `Remove Shoes`
- `Wall Sit Placeholder`

Doporucene soubory:

- `Assets/Scripts/UI/DebugCommandPanelController.cs`
- `Assets/UI/DebugCommandPanel.uxml`
- `Assets/UI/DebugCommandPanel.uss`

Hotovo kdyz:

- kliknuti meni runtime stav nebo spousti placeholder command

### Task 1.7: Debug state presenter

Popis:
Potrebujeme videt, co si system mysli, ze se deje.

Minimalni zobrazeni:

- current posture
- active command
- active exercise
- is busy

Doporucene soubory:

- `Assets/Scripts/UI/AvatarStateDebugView.cs`

Hotovo kdyz:

- na obrazovce je videt aktualni runtime stav

### Task 1.8: Placeholder command assets

Vytvorit prvni `ScriptableObject` assety:

- `CMD_Stand`
- `CMD_Sit`
- `CMD_Kneel`
- `CMD_LieBack`
- `CMD_RemoveShoes`
- `CMD_WallSit`

Umisteni:

- `Assets/Data/Commands/`

Hotovo kdyz:

- UI nepracuje s hardcoded stringy
- prikazy lze menit pres assety

### Task 1.9: Placeholder exercise asset

Vytvorit:

- `EX_WallSit`

Minimalni data:

- exercise id
- display name
- interaction type `Wall`
- default duration
- entry posture
- active posture

Umisteni:

- `Assets/Data/Exercises/`

Hotovo kdyz:

- `CMD_WallSit` odkazuje na realny asset

### Task 1.10: Placeholder sanction assets

Vytvorit:

- `SAN_ExtendHold`
- `SAN_LessClothing`

Umisteni:

- `Assets/Data/Sanctions/`

Hotovo kdyz:

- existuje pripraveny asset zakladnich sankci pro dalsi etapy

### Task 1.11: Avatar bootstrap wiring

Popis:
Pripravit start scene hookup bez manualniho dohledavani referenci pri kazdem testu.

Musi zajistit:

- avatar prefab ma vsechny reference
- UI vidi dispatcher
- scene boot neni zavisly na manualnim drag-drop chaosu

Doporucene soubory:

- `Assets/Scripts/Core/SceneBootstrap.cs`

Hotovo kdyz:

- otevreni sceny a stisk play staci k testu command flow

### Task 1.12: Logging a historie commandu

Popis:
I v teto fazi ma smysl logovat udalosti do jednoducheho bufferu.

Minimalni zaznam:

- cas
- command id
- akce `started/interrupted/completed`

Doporucene soubory:

- `Assets/Scripts/Core/CommandHistoryBuffer.cs`

Hotovo kdyz:

- posledni prikazy jsou viditelne v debug vrstve

## Prvni nove soubory k vytvoreni

Tyto soubory zatim v skeletonu chybi a jsou vhodnym prvnim rozsireni:

- `Assets/Scripts/Commands/CommandExecutionContext.cs`
- `Assets/Scripts/Core/AvatarRuntimeEvents.cs`
- `Assets/Scripts/Core/SceneBootstrap.cs`
- `Assets/Scripts/Core/CommandHistoryBuffer.cs`
- `Assets/Scripts/UI/DebugCommandPanelController.cs`
- `Assets/Scripts/UI/AvatarStateDebugView.cs`

## Prvni ScriptableObject assety

### Commands

- `CMD_Stand`
- `CMD_Sit`
- `CMD_Kneel`
- `CMD_LieBack`
- `CMD_RemoveShoes`
- `CMD_WallSit`

### Exercises

- `EX_WallSit`

### Sanctions

- `SAN_ExtendHold`
- `SAN_LessClothing`

## Prvni MonoBehaviour komponenty

Na avatar prefab:

- `Animator`
- `AvatarStateMachine`
- `CommandDispatcher`
- `CommandHistoryBuffer`

Na scene bootstrap objekt:

- `SceneBootstrap`

Na UI root:

- `DebugCommandPanelController`
- `AvatarStateDebugView`

Na interaktivni objekty:

- `InteractableObject`

## Zavislosti mezi tasky

Poradi s minimalnim rizikem:

1. `0.1` az `0.5`
2. `0.6` a `0.7`
3. `1.1`
4. `1.2`
5. `1.3`
6. `1.4`
7. `1.5`
8. `1.8`, `1.9`, `1.10`
9. `1.6` a `1.7`
10. `1.11`
11. `1.12`

## Acceptance testy pro konec Faze 0-1

### Test A: Projekt a scena

Postup:

1. Otevrit `Gym_MVP`
2. Stisknout play

Ocekavany vysledek:

- zadna compile error
- scena bezi
- avatar je ve scene
- UI je viditelne

### Test B: Stand command

Postup:

1. Kliknout `Standing`

Ocekavany vysledek:

- v debug view se zmeni `CurrentPosture`
- aktivni command se zapise

### Test C: Sit command

Postup:

1. Kliknout `Sitting`

Ocekavany vysledek:

- `CurrentPosture` prejde do `Sitting`
- historie commandu dostane novy zaznam

### Test D: Interrupt behavior

Postup:

1. Kliknout `Wall Sit Placeholder`
2. Okamzite kliknout `Kneeling`

Ocekavany vysledek:

- prvni command je oznacen jako preruseny
- druhy command se stane aktivnim
- runtime stav zustane konzistentni

### Test E: Wardrobe placeholder command

Postup:

1. Kliknout `Remove Shoes`

Ocekavany vysledek:

- command se zapise jako wardrobe command
- avatar prejde do stavu `WardrobeChange` nebo odpovidajiciho placeholder stavu

## Definition of Done pro Fazi 0-1

Faze 0-1 je hotova pouze tehdy, pokud:

- projekt kompiluje
- scena `Gym_MVP` existuje a je testovatelna
- avatar prefab existuje
- existuji command assety
- UI umi poslat command bez hardcodovanych stringu
- runtime stav je viditelny v debug vrstve
- command flow podporuje preruseni

## Co jeste vedome chybi po Fazi 0-1

Po teto etape stale nebude hotove:

- realne locomotion
- interaction anchor workflow
- wall sit jako skutecny cvik
- validace formy
- unava a bolest jako gameplay system
- relief request
- sankce execution

To je v poradku. Tyto casti patri do dalsich fazi.

## Doporuceny navazujici krok

Po dokonceni teto etapy ma nasledovat:

`Faze 2 + Faze 4 v minimalnim rozsahu pro Wall Sit Vertical Slice`

To znamena:

- pohyb ke zdi
- zarovnani
- hold-based exercise session
- placeholder timer
- priprava na unavu a selhani
