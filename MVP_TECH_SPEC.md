# MVP Technical Specification

## Projekt

Pracovni nazev: `Trainer-Avatar Simulator`

Cil projektu:
Vytvorit realisticky PC-first 3D simulator, ve kterem uzivatel vystupuje jako trener a ridi humanoidniho avatara pri cviceni. Avatar musi umet:

- provadet cviky v interaktivnim prostredi
- reagovat na presne i dilci povely
- hodnotit kvalitu vlastniho provedeni
- prijimat i navrhovat sankce
- zadat o ulevu nebo naopak o zvyseni obtiznosti
- menit casti obleceni podle pozadavku cviku nebo povelu

MVP nema byt medicinsky presny simulator. Cilem je `realisticky pusobici interaktivni simulator chovani a treningu`.

## Cilova platforma

- Primarni platforma: `Windows PC`
- Sekundarni potencial: `macOS`, pozdeji pripadne konzole
- MVP se navrhuje jako `single-player offline desktop aplikace`

## Doporuceny technologicky stack

### Core runtime

- Engine: `Unity 6 LTS`
- Jazyk: `C#`
- Render pipeline: `HDRP`
- Input: `Unity Input System`
- Kamera: `Cinemachine`
- Navigace: `Unity NavMesh`
- Fyzika: vestavena `PhysX`
- UI: `UI Toolkit`
- Animace: `Animator (Mecanim)` + `Animation Rigging`
- Data definice: `ScriptableObject`
- Ukladani: `JSON` do lokalniho profilu
- Asset loading: `Addressables`
- Audio: `Unity Audio Mixer`

### Doporucene externi nastroje

- Avatar pipeline: `Character Creator 4` + `iClone`
- Alternativa pro obsah: `Blender`
- Zdroj pohybovych dat: `Rokoko`, `ActorCore`, pripadne jina mocap knihovna
- Prototyp animaci: `Mixamo` pouze pro interni blokaci, ne pro finalni realistickou verzi
- Version control: `Git`

### Proc tento stack

- `Unity` je vhodnejsi nez `Unreal` pro rychlejsi iteraci simulacni logiky, UI panelu, datove definovanych pravidel a multiplatformni potencial.
- `HDRP` je vhodne pro realisticky vizual na PC.
- `ScriptableObject` umozni drzet cviky, sankce, povely a pravidla jako data misto hardcodu.
- `Animation Rigging` a `IK` jsou nutne pro kontakt se zdi, lavickou, podlozkou a pro dilci pozice tela.

## Rozsah MVP

### Obsah, ktery do MVP patri

- 1 realisticky avatar
- 1 mensi scena telocvicny
- 3 interaktivni objekty:
  - zed
  - podlozka
  - lavicka
- 8-12 dilcich povelu
- 4-6 cviku
- zakladni model unavy
- zakladni system sankci
- system sebehodnoceni
- system vyjednavani o uleve nebo ztizeni
- system obleceni a zouvani

### Co do MVP vedome nepatri

- multiplayer
- hlasove ovladani
- plne proceduralni animace
- otevreny rozsah telocvicny s desitkami objektu
- pokrocila medicinska biomechanika
- generativni AI dialog bez omezeni
- vice avataru najednou

## Vysoko-urovnova architektura

System bude rozdelen na 9 hlavnich modulu:

1. `Command System`
2. `Action Execution System`
3. `Exercise System`
4. `Environment Interaction System`
5. `Form Validation System`
6. `Fatigue and Capability System`
7. `Wardrobe System`
8. `Sanction, Self-Assessment and Negotiation System`
9. `Behavior and Dialogue System`

Tyto moduly pobezi nad sdilenym `Avatar Runtime State`, ktery ponese aktualni stav tela, unavy, vybaveni a psychologickych promennych.

## Hlavni systemy

### 1. Command System

Ucel:
Prevest uzivatelsky povel na vykonatelny zamer.

Typy povelu:

- `exercise commands`
  - "udelej wall sit"
  - "3 serie drepu"
- `atomic posture commands`
  - "sedni si"
  - "klekni si"
  - "lehni si"
  - "predpaz"
- `wardrobe commands`
  - "sundej si ponozky"
  - "zuj si boty"
- `discipline commands`
  - "opakuj to"
  - "dostanes sankci"
  - "pauza"

Implementace:

- `CommandDefinition : ScriptableObject`
- `CommandDispatcher`
- `CommandContext`
- `CommandResult`

Poznamka:
Pro MVP neni nutne delat volny text parser. Staci tlacitka, menu a pevne mapovane textove povely.

### 2. Action Execution System

Ucel:
Provadet atomicke akce avataru tak, aby slo z nich skladat cviky i male povely.

Priklady akci:

- `MoveToPoint`
- `AlignToInteraction`
- `SitDown`
- `Kneel`
- `StandUp`
- `RaiseArmsFront`
- `RemoveShoes`
- `RemoveSocks`
- `LieOnMat`
- `HoldPose`

Implementace:

- `IAvatarAction`
- `ActionRunner`
- `ActionQueue`
- `Animator` state machine pro telesne stavy
- `Animation Rigging` pro dotahy kontaktu

Poznamka:
Akce musi podporovat:

- preruseni
- fail state
- timeout
- overeni vstupnich podminek

### 3. Exercise System

Ucel:
Definovat cvik jako datovy objekt s pravidly, prostredim a validaci.

Kazdy cvik obsahuje:

- nazev
- cilove interaction pointy
- vstupni podminky
- seznam kroku
- pravidla spravneho provedeni
- unavovy profil
- seznam moznych sankci
- lehci a tezsi varianty

Implementace:

- `ExerciseDefinition : ScriptableObject`
- `ExerciseSession`
- `ExerciseStep`
- `ExerciseEvaluator`

Prvni doporucene cviky pro MVP:

- `Wall Sit`
- `Squat`
- `Push-Up`
- `Plank`
- `Lunge`
- `Seated Forward Arm Raise` nebo podobny staticky posturalni cvik

### 4. Environment Interaction System

Ucel:
Umoznit avatarovi pouzivat objekty telocvicny realisticky a opakovatelne.

Kazdy interaktivni objekt ma:

- `InteractionPoint`
- cilovou pozici tela
- cilovou rotaci
- typ podpory
- povolene cviky
- pripadne kontaktni body pro IK

Objekty pro MVP:

- `WallInteraction`
- `BenchInteraction`
- `MatInteraction`

Implementace:

- `InteractableObject`
- `InteractionPoint`
- `InteractionReservation`

### 5. Form Validation System

Ucel:
Vyhodnocovat, zda cvik probehl spravne.

Sledovane metriky:

- uhly kloubu
- vyska panve
- hloubka drepu
- poloha kolen vuci chodidlum
- kontakt zad se zdi
- kontakt tela s lavickou nebo podlozkou
- doba ve spravne pozici
- tempo provedeni

Implementace:

- `PoseRule`
- `RangeRule`
- `TimingRule`
- `ContactRule`
- `FormScore`

Vystup:

- `pass`
- `partial pass`
- `fail`
- odchylky po kategoriich

### 6. Fatigue and Capability System

Ucel:
Modelovat schopnost avatara pokracovat a kvalitu jeho vykonu.

Doporucene runtime veliciny:

- `globalFatigue`
- `cardioFatigue`
- `localFatigueLegs`
- `localFatigueArms`
- `localFatigueCore`
- `discomfort`
- `motivation`
- `compliance`
- `confidence`
- `perceivedLeniency`

Pouziti:

- uprava rychlosti akci
- snizeni kvality techniky
- zvyseni pravdepodobnosti prosby o ulevu
- zvyseni pravdepodobnosti prosby o ztizeni
- omezeni dostupnych sankci

Poznamka:
MVP ma pouzit `deterministicky pravidlovy model`, ne slozitou fyziologickou simulaci.

### 7. Wardrobe System

Ucel:
Ridit obleceni jako herni stav, ne jen kosmetiku.

Sloty:

- `top`
- `bottom`
- `shoes`
- `socks`

Stavy:

- obleceno
- sundano
- dostupne
- vyzadovano cvikem
- zakazano cvikem

Implementace:

- `WardrobeItemDefinition : ScriptableObject`
- `WardrobeState`
- `WardrobeRule`

Priklady pravidel:

- cvik muze vyzadovat cviceni bez bot
- uzivatel muze vydat povel k sundani ponozek
- avatar muze odmitnout provest akci, pokud neni v korektnim stavu pro dany cvik

### 8. Sanction, Self-Assessment and Negotiation System

Ucel:
Umoznit sankce, sebekritiku a vyjednavani o jejich primerenosti.

#### Self-Assessment

Avatar po cviku vyhodnoti:

- jak moc se odchylil od spravne formy
- zda je vina jeho
- zda ma opakovani uznat
- zda ma sam navrhnout sankci

Implementace:

- `SelfAssessmentProfile`
- `SelfAssessmentResult`

Dulezite runtime vlastnosti:

- `selfDiscipline`
- `selfCriticism`
- `fairnessPerception`
- `humility`

#### Sanctions

Doporucene typy sankci pro MVP:

- nezapocitani opakovani
- opakovani cviku znovu
- prodlouzeni drzeni pozice
- extra opakovani
- zkraceni pauzy
- extra serie

Implementace:

- `SanctionDefinition : ScriptableObject`
- `SanctionRequest`
- `SanctionExecution`

#### Negotiation

Scenare:

- avatar sam navrhne sankci za spatne provedeni
- uzivatel udeli sankci
- avatar pozada o mirnejsi variantu
- avatar naopak pozada o prisnejsi rezim, pokud ma pocit prilisne benevolence

Implementace:

- `NegotiationRule`
- `NegotiationOutcome`

### 9. Behavior and Dialogue System

Ucel:
Rozhodovat, co avatar chce a jak to verbalizuje.

Pro MVP doporucuji:

- `Utility AI` pro vyber reakce
- `rule-based dialogue templates`

Nedoporucuji pro MVP:

- generativni LLM dialog v runtime

Duvod:

- horsi kontrola tonu a konzistence
- vyssi slozitost
- horsi testovatelnost

Druhy autonomnich reakci:

- prosba o ulevu
- prosba o ztizeni
- priznani chyby
- zadost o slitovani pri sankci
- prijeti sankce
- zadost o dalsi cvik nebo dalsi serii

## Stavovy model avatara

Minimalni FSM pro MVP:

- `Idle`
- `Moving`
- `Aligning`
- `Standing`
- `Sitting`
- `Kneeling`
- `Lying`
- `Exercising`
- `HoldingPose`
- `WardrobeChange`
- `Recovering`
- `Negotiating`
- `Sanctioned`

Pravidla:

- stav musi byt jednoznacny
- prechody musi byt validovane
- nektere akce jsou povolene jen z konkretnich stavu

Priklad:

- `RemoveSocks` lze spustit ze stavu `Sitting`
- `Wall Sit` lze spustit jen po `Moving` + `Aligning`

## Datovy model

### Authoring data

Pouzit `ScriptableObject` pro:

- `CommandDefinition`
- `ExerciseDefinition`
- `ExerciseVariantDefinition`
- `InteractionDefinition`
- `WardrobeItemDefinition`
- `SanctionDefinition`
- `DialogueTemplateSet`
- `BehaviorProfile`

### Runtime data

Pouzit bezne `C#` tridy nebo `struct` pro:

- `AvatarRuntimeState`
- `ExerciseSessionState`
- `FatigueState`
- `WardrobeState`
- `BehaviorState`
- `NegotiationState`
- `FormEvaluationResult`

### Save data

Ukladat pres `JSON`:

- preference uzivatele
- posledni stav avatara
- odemcene obleceni, pokud bude potreba
- zakladni treningovou historii

## Navrh UI

MVP UI ma byt funkcni a profesionalni, ne herne preplacane.

Hlavni panely:

- `Command Panel`
- `Exercise Panel`
- `Avatar Status Panel`
- `Sanction / Negotiation Panel`
- `Wardrobe Panel`

Zobrazovane informace:

- aktualni stav avatara
- unava po kategoriich
- aktivni cvik
- kvalita provedeni
- navrh sankce nebo prosba avatara

## Navrh sceny MVP

Jedna scena:

- maly funkcni treningovy prostor
- zed pro `wall sit`
- podlozka pro cviky na zemi
- lavicka pro sezeni a pomocne polohy
- neutralni realisticke osvetleni
- minimum rusivych objektu

## Referencni gameplay toky

### Tok 1: Wall Sit

1. Uzivatel vyda povel `wall sit 45 s`
2. `Command System` vybere spravny cvik
3. Avatar dojde ke zdi
4. Zarovna se do `InteractionPoint`
5. Prejde do drzeni pozice
6. `Form Validation` sleduje vysku panve, uhel kolen a kontakt zad
7. `Fatigue System` zveda zatez nohou a diskomfort
8. Avatar muze:
   - vydrzet
   - pozadat o zkraceni
   - priznat spatne provedeni
9. Po dokonceni se vyhodnoti skore a pripadne sankce

### Tok 2: Sankce se zadosti o slitovani

1. Uzivatel vyhodnoti cvik jako spatny
2. Zada sankci `extra 20 s wall sit`
3. System overi proveditelnost vzhledem k unave
4. Avatar:
   - sankci prijme
   - nebo pokorne pozada o mirnejsi variantu
5. `Negotiation System` vrati vysledek
6. Sankce se provede nebo nahradi jinou

### Tok 3: Avatar zada o ztizeni

1. Po serii drepu avatar vyhodnoti nizkou zatez
2. `Behavior System` rozhodne, ze rezim je prilis benevolentni
3. Avatar navrhne:
   - vice opakovani
   - kratsi pauzu
   - tezsi variantu
4. Uzivatel potvrdi nebo odmitne

### Tok 4: Dilci povel obleceni

1. Uzivatel vyda povel `sundej si ponozky`
2. System overi, ze avatar muze prejit do vhodne polohy
3. Avatar si sedne na lavicku nebo jinou vhodnou pozici
4. Provede sekvenci zmeny obleceni
5. `WardrobeState` se aktualizuje

## Doporucena adresarova struktura Unity projektu

```text
Assets/
  Art/
    Characters/
    Environments/
    Props/
    Animations/
  Audio/
  Prefabs/
    Avatar/
    Interactables/
    UI/
  Scenes/
    Gym_MVP.unity
  Scripts/
    Core/
    Commands/
    Actions/
    Exercises/
    Environment/
    Validation/
    Fatigue/
    Wardrobe/
    Behavior/
    Sanctions/
    UI/
  Data/
    Commands/
    Exercises/
    Interactions/
    Wardrobe/
    Sanctions/
    Dialogue/
```

## Implementacni faze

### Faze 1: Technicky vertical slice

Cil:
Overit, ze avatar umi prijmout povel, dojit k objektu, zaujmout pozici a byt vyhodnocen.

Obsah:

- 1 scena
- 1 avatar
- 1 interaktivni zed
- 1 cvik `wall sit`
- zakladni unava
- zakladni pass/fail validace

### Faze 2: Atomic commands

Obsah:

- sedni si
- klekni si
- stoupni si
- predpaz
- lehni si
- zuj boty
- sundej ponozky

### Faze 3: Sankce a sebehodnoceni

Obsah:

- self-assessment po cviku
- 3 typy sankci
- prijeti sankce
- prosba o mirnejsi sankci

### Faze 4: Autonomni vyjednavani zateze

Obsah:

- prosba o ulevu
- prosba o ztizeni
- navrh extra serie
- navrh lehci varianty

### Faze 5: Rozsireni obsahu

Obsah:

- dalsi cviky
- dalsi interaktivni objekty
- vice animacnich prechodu
- lepsi audio a prezentace

## Hlavni technicka rizika

### 1. Nekvalitni animace

Nejvetsi riziko projektu. Slaby pohyb rozbije realismus rychleji nez horsi grafika.

Mitigace:

- pouzit kvalitni mocap zdroje
- neprepalit pocet cviku v MVP
- investovat do prechodu a IK

### 2. Prilis velky scope

Riziko:
snaha pokryt mnoho cviku, sankci a interakci hned od zacatku.

Mitigace:

- drzet se pevneho MVP
- systemy delat datove a rozsiritelne
- obsah rozsirivat az po vertical slice

### 3. Neprirozene autonomni chovani

Riziko:
avatar bude pusobit bud roboticky, nebo nelogicky.

Mitigace:

- utility scoring s omezenym poctem pravidel
- jasne definovane stavy
- male mnozstvi dobre testovanych reakci

## Konkretni doporuceni

Pokud se ma projekt skutecne rozbehnout, prvni implementacni cil ma byt:

`Vertical slice: wall sit + sankce + prosba o ulevu + sebehodnoceni`

Tato varianta overi najednou:

- chuze k interaktivnimu objektu
- zarovnani a drzeni pozice
- validaci formy
- unavu
- dialogovou reakci
- sankcni logiku

Teprve po uspesnem vertical slice ma smysl pridavat dalsi cviky a sirsi povelovy slovnik.
