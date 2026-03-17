# Implementation Plan

## Ucel dokumentu

Tento dokument prevadi [MVP_TECH_SPEC.md](C:\Users\Krupa\Desktop\Private\surrendering\MVP_TECH_SPEC.md) a [FUNCTIONAL_SPEC_V2.md](C:\Users\Krupa\Desktop\Private\surrendering\FUNCTIONAL_SPEC_V2.md) do konkretniho realizacniho planu pro `Unity 6 LTS`.

Cil:

- urcit poradi implementace
- rozdelit system na pracovni baliky
- popsat zavislosti mezi moduly
- definovat vystupy jednotlivych fazi
- nastavit realisticky prvni `vertical slice`

## Zakladni strategicke rozhodnuti

Projekt se nebude stavet "po funkcich v menu", ale `po vykonatelnych tocich`.

To znamena:

- nejdriv se musi dokazat kompletni cesta `povel -> pohyb -> cvik -> vyhodnoceni -> reakce`
- teprve potom ma smysl rozsirovat pocet cviku, sankci a profilu

Prvni cil implementace:

`Wall Sit Vertical Slice`

Tento slice musi pokryt:

- vyber povelu pres UI
- realny presun avatara ke zdi
- zarovnani na interakcni bod
- drzeni pozice
- sledovani unavy a bolesti
- prosbu o ulevu
- zamitnuti prosby
- fyzicke selhani bez sankce
- sankci `delsi vydrz`
- sankci `mene obleceni`
- textovou a vizualni reakci

## Pracovni proudy

Projekt rozdelit do techto hlavnich proudu:

1. `Core Runtime`
2. `Avatar Motion and Interaction`
3. `Exercise Execution and Validation`
4. `Fatigue, Pain and Capability`
5. `Sanctions, Negotiation and Self-Assessment`
6. `Wardrobe and Discrete Clothing Changes`
7. `UI and Camera`
8. `Persistence and Multi-Avatar Profiles`
9. `Content Pipeline`
10. `Animation, IK and Presentation`
11. `Visual Production and Rendering`
12. `Testing, QA and Build Hygiene`

Ne vsechny proudy mohou bezet od prvniho dne stejne.
Kriticka cesta pro prvni vertical slice je:

- `Core Runtime`
- `Avatar Motion and Interaction`
- `Exercise Execution and Validation`
- `Fatigue, Pain and Capability`
- `Sanctions, Negotiation and Self-Assessment`
- `UI and Camera`

Podpurne proudy, ktere nejsou na kriticke ceste prvniho slice, ale musi bezet vcas:

- `Content Pipeline`
- `Animation, IK and Presentation`
- `Visual Production and Rendering`
- `Testing, QA and Build Hygiene`

## Cross-Cutting Implementation Rules

Tato pravidla plati napric vsemi fazemi:

1. `Data over hardcode`

- nove cviky, sankce, wardrobe pravidla a behavior profily se maji navrhovat jako data-first
- v kodu se maji objevit jen obecne vykonavaci a validacni mechanismy

2. `Vertical slice before breadth`

- dokud neni kompletni `Wall Sit` slice, nepridavat dalsi cviky jen proto, aby byl obsah "sirsI"

3. `Visual target is PC-first photoreal`

- vsechny volby v renderingu, materialech a osvetleni se maji rozhodovat ve prospech `PC HDRP`
- multiplatformni kompromisy jsou az druha vlna

4. `Animation quality gates`

- novy pohyb nesmi jit do hlavni sceny, pokud nema:
  - definovany vstupni a vystupni stav
  - jasny fallback pri preruseni
  - aspon zakladni kontakt s povrchem a objekty

5. `Test what is systemic`

- ciste datove a rozhodovaci casti se maji testovat mimo scenu
- scene-only chovani testovat v izolovanych testovacich scenach

## Missing-Until-Now Areas Explicitly Added

Tento plan novE explicitne pokryva i:

- test strategy
- asset production pipeline
- animation and IK backlog
- visual rendering milestone
- content authoring workflow
- build and release hygiene

## Faze realizace

### Faze 0: Project Bootstrap

Cil:
Zalozit skutecny `Unity` projekt a pripravit technicky zaklad bez herni logiky.

Hlavni ukoly:

- vytvorit `Unity 6 LTS HDRP` projekt
- importovat zakladni skeleton ze [UnityProjectSkeleton](C:\Users\Krupa\Desktop\Private\surrendering\UnityProjectSkeleton)
- nastavit `Input System`
- nastavit `Cinemachine`
- zalozit hlavni scenu `Gym_MVP`
- pripravit adresarovou strukturu podle specifikace
- nastavit `Addressables`
- zalozit zakladni assembly definitions pro skripty, pokud budou davat smysl
- vytvorit git ignore a potvrdit strukturu repozitare
- nastavit `Force Text` a `Visible Meta Files`
- zalozit `Dev` a `Review` build profil konvenci
- vytvorit testovaci scenu `AnimationSandbox`
- vytvorit testovaci scenu `ValidationSandbox`

Deliverables:

- kompilujici prazdny projekt
- scena `Gym_MVP`
- scena `AnimationSandbox`
- scena `ValidationSandbox`
- avatar prefab placeholder
- placeholder interaktivni zed, lavicka a podlozka

Acceptance criteria:

- projekt se otevre bez compile errors
- scena se spusti
- v scene jsou pripraveny tri interaktivni objekty
- `meta` soubory a scene assety jsou stabilne verzovane

### Faze 1: Core Runtime and State Model

Cil:
Zprovoznit sdileny runtime stav a kostru strojove logiky.

Hlavni ukoly:

- dokoncit `AvatarRuntimeState`
- rozdelit runtime stav na:
  - posture/state
  - fatigue/pain
  - wardrobe
  - behavior
  - active command/exercise
- zavest jednoznacny `AvatarStateMachine`
- nadefinovat povolene a nepovolene prechody mezi stavy
- vytvorit system preruseni aktualni akce novym prikazem
- dopsat centralni `CommandDispatcher`
- zavest `CommandExecutionContext`
- pripravit eventy nebo signalizaci pro:
  - command started
  - command interrupted
  - exercise started
  - exercise finished
  - sanction proposed
  - relief requested
  - physical failure

Deliverables:

- stabilni runtime state model
- centralni dispatcher prikazu
- jednotny zdroj pravdy o stavu avatara

Acceptance criteria:

- avatar umi prijmout prikaz z UI
- system vidi, co je aktivni command a co aktivni exercise
- novy prikaz umi prerusit stary bez rozbiti stavu

Zavislosti:

- Faze 0

### Faze 2: World Navigation and Interaction Anchors

Cil:
Umoznit avatarovi pohybovat se v prostoru a pracovat s interakcnimi body.

Hlavni ukoly:

- integrovat `NavMesh`
- vytvorit `InteractableObject` a `InteractionPoint` workflow
- zavest vyhledani nejblizsiho validniho interakcniho bodu
- implementovat `MoveToInteractionAction`
- implementovat `AlignToInteractionAction`
- pripravit rezervaci interakcniho bodu pro budouci rozsirovani
- nastavit root motion nebo hybridni pohyb podle kvality animaci

Deliverables:

- avatar dojde ke zdi
- avatar se zarovna na spravne misto
- avatar umi dojit k lavicce a podlozce

Acceptance criteria:

- avatar neskace teleportem
- po vydani prikazu se presune na nejblizsi validni misto
- po presunu konci v predvidatelne orientaci
- interaction point lze debugove zobrazit a vizualne zkontrolovat ve scene

Zavislosti:

- Faze 1

### Faze 3: Action System and Atomic Commands

Cil:
Zprovoznit zakladni dilci povely jako prerusitelne akce.

Hlavni ukoly:

- vytvorit `ActionRunner`
- implementovat zakladni akce:
  - stand
  - sit
  - kneel
  - sit on heels
  - lie on stomach
  - lie on back
  - squat down
  - arms front
  - arms side
  - arms up
  - hands behind head
  - remove shoes
  - remove socks
- vytvorit pravidla, z jake polohy lze akci spustit
- zavest fallback "nejblizsi mozna poloha" pro prevlekani
- napojit akce na animator controller
- zavest per-action interrupt policy
- zavest timeout nebo fail-safe pro akce, ktere nedobehnou do konce

Deliverables:

- avatar umi reagovat na dilci povely z panelu
- dilci povel jde zadat i behem cviku
- aktualni cinnost se korektne prerusi

Acceptance criteria:

- prikazy `sedni si`, `klekni si`, `predpaz`, `sundej si boty` realne meni stav avatara
- odebirani obleceni ma animovany prubeh, ne jen skryti meshe
- prerusena akce vrati avatar do konzistentniho runtime stavu

Zavislosti:

- Faze 1
- Faze 2

### Faze 4: Exercise Framework

Cil:
Zavest jednotny framework pro cviky, opakovani, vydrze a kroky cviku.

Hlavni ukoly:

- rozsirit `ExerciseDefinition`
- zavedeni `ExerciseSession`
- zavedeni `ExerciseStep`
- podpora dvou typu cviku:
  - repetition based
  - hold based
- podpora preruseni cviku bez automaticke sankce
- podpora nezapocitani pri preruseni nebo spatnem provedeni
- podpora exercise modifiers:
  - arms position
  - longer hold
  - reduced clothing

Deliverables:

- engine pro spousteni cviku z dat
- jednotne zpracovani cviku i sankcnich variant

Acceptance criteria:

- `wall sit` funguje jako hold based cvik
- `drep` funguje jako repetition based cvik
- system umi poznat, ze cvik byl prerusen a nema byt zapocitan

Zavislosti:

- Faze 2
- Faze 3

### Faze 5: Form Validation

Cil:
Vyhodnocovat kvalitu provedeni a identifikovat typ chyby.

Hlavni ukoly:

- rozsirit `FormEvaluationResult`
- zavest pravidla validace:
  - rozsah pohybu
  - zakazany pohyb casti tela
  - predcasne ukonceni
  - kontakt s objektem
- pripravit monitorovani kostry a referencnich bodu tela
- vytvorit system mapovani chyb na verbalni hlasky
- nastavit pro kazdy cvik minimalni sadu validacnich pravidel
- vytvorit debug vizualizaci validacnich hranic a referencnich bodu

Deliverables:

- cvik vraci:
  - pass
  - fail
  - typ chyby

Acceptance criteria:

- u `wall sit` jde rozpoznat ztrata spravne pozice
- u `drepu` jde rozpoznat nedostatecny rozsah a zvednuti pat
- spatne provedeni zpusobi nezapocitani
- debug overlay umi ukazat alespon posledni `PrimaryIssue`

Zavislosti:

- Faze 4

### Faze 6: Fatigue, Pain and Physical Failure

Cil:
Modelovat unavu, bolest a fyzicke selhani ve spravnem poradi.

Hlavni ukoly:

- rozsirit `FatigueState` o bolest a segmenty tela
- nadefinovat zatizeni segmentu pro kazdy cvik
- zavest update model:
  - spravne provedeni
  - spatne provedeni
  - bolest z techniky
  - bolest z dlouhe unavy
  - bolest z nevhodneho obleceni
- implementovat poradi projevu:
  - horsi technika
  - prosba o ulevu
  - zpomaleni
  - fyzicke selhani
- zavest ochrannou polohu a oznameni bolave casti tela
- zavest casovy ustup bolesti
- logovat tuning telemetry pro prahy unavy, bolesti a selhani

Deliverables:

- avatar se neunavi jen kosmeticky, ale meni vykon
- existuje fyzicke selhani bez sankce

Acceptance criteria:

- pri dlouhem `wall sit` avatar umozni relief request
- po zamitnuti prosby umi dojit k fyzickemu selhani
- po selhani zaujme ochrannou polohu a hlasi bolest
- tuning dat lze menit bez zasahu do exekucni logiky

Zavislosti:

- Faze 4
- Faze 5

### Faze 7: Behavior, Self-Assessment and Negotiation

Cil:
Zprovoznit prosby o ulevu, zadosti o ztizeni a sebekriticke reakce.

Hlavni ukoly:

- rozsirit `BehaviorState` do utility modelu
- vytvorit evaluaci:
  - should request relief
  - should request intensity
  - should confess poor form
  - should plead for mercy
- zavest typy proseb:
  - shorter duration
  - fewer reps
  - cancel or soften exercise
  - soften sanction
  - cancel sanction
  - adjust clothing
- zavest typy zadosti o ztizeni:
  - more reps
  - longer hold
  - add arm position
  - remove clothing
- zavest sebehodnoceni po cviku
- zavest moznost:
  - navrhnout konkretni sankci
  - obecne poprosit o sankci
- zavest stav `Negotiating`
- pripravit oddelene `dialogue tags` od finalnich textovych sablon

Deliverables:

- avatar umi aktivne reagovat na zatez
- avatar umi sam popsat chybu a navrhnout dalsi postup

Acceptance criteria:

- avatar umi pozadat o ulevu pri unave, bolesti a nevhodnem obleceni
- avatar umi pozadat o ztizeni pri nizke zatezi
- avatar umi pri spatnem provedeni sam navrhnout sankci
- behavior rozhodnuti lze traceovat v debug logu nebo inspectoru

Zavislosti:

- Faze 5
- Faze 6

### Faze 8: Sanction Execution

Cil:
Implementovat katalog sankci a jejich aplikaci na cviky a dilci povely.

Hlavni ukoly:

- rozsirit `SanctionDefinition`
- zavest `SanctionRequest`
- zavest `SanctionResolution`
- implementovat sankce:
  - repeat exercise
  - longer hold
  - not counted
  - stricter variation
  - less clothing
- zavest kontrolu blokovanych sankci
- nadefinovat, ktere sankce se na ktere cviky hodi
- integrovat sankce s UI a historii
- zavest audit trail sankce:
  - kdo ji navrhl
  - kdo ji potvrdil
  - proc byla blokovana nebo provedena

Deliverables:

- sankce lze pridelit a provest
- system umi blokovat nerealizovatelne sankce

Acceptance criteria:

- `less clothing` nelze ulozit, kdyz uz neni co sundat
- `longer hold` umi prodlouzit `wall sit`
- `stricter variation` umi pridat napr. ruce pred telo tam, kde je to podporovano
- fyzicke selhani blokuje sankce, ktere tak maji byt definovane

Zavislosti:

- Faze 4
- Faze 7

### Faze 9: Wardrobe System

Cil:
Dovest obleceni z pouheho stavu do plnohodnotneho gameplay systemu.

Hlavni ukoly:

- rozsirit `WardrobeState`
- vytvorit `WardrobeItemDefinition`
- implementovat vazbu mezi oblecenim a:
  - komfortem
  - bolesti
  - technikou
  - vykonem
- dodelat animovane zouvani a sundavani ponozek
- priprava slotu top a bottom
- zachovat nesundatelne sortky v renderer pipeline

Deliverables:

- obleceni realne ovlivnuje simulaci
- prevlekani je soucast systemu sankci a proseb

Acceptance criteria:

- odebrani bot nebo ponozek meni runtime stav i vizual
- nevhodny odev umi spustit prosbu o upravu odevu

Zavislosti:

- Faze 3
- Faze 6
- Faze 8

### Faze 10: UI, Camera and Session History

Cil:
Vytvorit skutecne pouzitelne rozhrani trenera.

Hlavni ukoly:

- implementovat `UI Toolkit` layout
- vytvorit panely:
  - commands
  - current body state
  - fatigue and pain
  - wardrobe state
  - hidden history
- zavest session history:
  - commands
  - completed exercises
  - failed exercises
  - sanctions
  - requests
- integrovat kameru:
  - free camera
  - quick views
- doplnit indikatory aktivniho stavu a aktivni bolesti
- zavest `review mode` overlay pro kontrolu techniky z boku a zepredu

Deliverables:

- trener umi realne ovladat avatar pres UI
- historie je pristupna bez zahlceni obrazovky

Acceptance criteria:

- z UI jde spustit cvik, dilci povel i sankce
- kamera umi prepinat rychle pohledy
- historie zachycuje klicove udalosti
- UI zustava citelne i pri otevrene historii a aktivni bolesti

Zavislosti:

- Faze 1
- Faze 3
- Faze 7
- Faze 8

### Faze 11: Persistence and Multi-Avatar Profiles

Cil:
Ukladat fyzicky stav a podporit vice avataru.

Hlavni ukoly:

- navrhnout save format
- vytvorit `AvatarProfile`
- ulozit:
  - vzhled
  - obleceni
  - segmentovou kondici
  - historii bolesti nebo zraneni
  - posledni umisteni
- implementovat autosave
- implementovat manual save
- implementovat prepinani vice avataru

Deliverables:

- vice persistentnich avataru
- konzistentni navazani mezi sezenimi

Acceptance criteria:

- po restartu aplikace se vrati fyzicky stav avatara
- mezi sezenimi se neprenasi vztah k trenerovi, ale fyzicky stav ano

Zavislosti:

- Faze 1
- Faze 6
- Faze 9
- Faze 10

### Faze 12: Content Expansion for MVP Completion

Cil:
Doplnit zbytek MVP obsahu po overeni systemu.

Hlavni ukoly:

- pridat vsechny pocatecni cviky
- pridat vsechny dilci povely
- pridat 4 osobnostni profily
- doplnit textove formulace reakci
- doladit vizualni reakce:
  - dech
  - tres
  - facial cues
  - protective pose
- doladit pravidla pro arm modifiers a stricter variants
- prevest seed katalog do realnych `ScriptableObject` assetu a zkontrolovat je validatorem

Deliverables:

- plny MVP feature set podle specifikace

Acceptance criteria:

- vsechny MVP cviky jsou datove definovane a spustitelne
- vsechny MVP profily meni rozhodovani i textove reakce
- seed katalog a realne assety se nerozchazeji v ID a vazbach

### Faze 13: Animation, IK and Presentation Pass

Cil:
Stabilizovat pohybovou prezentaci tak, aby slice nepusobil roboticky a aby fotorealisticke assety mely smysl.

Hlavni ukoly:

- vytvorit prvni `Animator Controller` backlog po statech:
  - locomotion
  - sit
  - kneel
  - wall sit entry
  - wall sit hold
  - wardrobe change
  - protective pose
- zavest `IK` vrstvu pro:
  - kontakt zad se zdi
  - kontakt chodidel s podlahou
  - ruce za hlavou
  - sed a klek na povrchu
- definovat pravidla pro root motion vs scripted motion
- vytvorit animation review checklist
- vytvorit `AnimationSandbox` test flow

Deliverables:

- stabilni animator kostra
- prvni kontaktni `IK` setup
- overeny pohybovy debug flow mimo hlavni scenu

Acceptance criteria:

- wall sit entry a hold neobsahuji zjevny foot slide
- protective pose jde prehrat bez rozpadu postavy
- wardrobe akce ma korektni vstupni a vystupni stav

Zavislosti:

- Faze 2
- Faze 3
- Faze 4

### Faze 14: Visual Production and HDRP Pass

Cil:
Uzavrit vizualni baseline pro PC-first fotorealistickou verzi.

Hlavni ukoly:

- aktivovat a stabilizovat `HDRP` pipeline assety
- nastavit lighting baseline:
  - hlavni directional light
  - interior fill
  - reflection probes
  - post process baseline
- vytvorit materialovy standard pro:
  - kuzi
  - latku
  - obuv
  - beton
  - podlahu
- importovat a zkontrolovat prvni realisticke assety:
  - avatar
  - clothing
  - wall
  - bench
  - mat
- vytvorit visual review checklist pro:
  - proporce
  - material response
  - shadow quality
  - color consistency
  - contact believability

Deliverables:

- prvni HDRP visual baseline
- import checklist a schvalena sada prvnich assetu
- scena, na ktere jde hodnotit realismus, ne jen logiku

Acceptance criteria:

- avatar vypada jako clovek a ne jako placeholder
- scena ma stabilni osvetleni pro review screenshoty
- materialy nepusobi jako nahodna smes free assetu bez sjednoceni

Zavislosti:

- Faze 0
- Faze 13

### Faze 15: Testing, QA and Build Hygiene

Cil:
Udelat z prototypu neco, co se da bezpecne iterovat bez chaosu.

Hlavni ukoly:

- vymezit test vrstvy:
  - pure runtime tests
  - scene integration tests
  - manual review checklist
- vytvorit minimalni test sady pro:
  - behavior decisions
  - sanction blocking
  - fatigue progression
  - content ID consistency
- zavest build konvence:
  - `Dev`
  - `Review`
  - `Milestone`
- zavest save compatibility pravidla
- zavest crash/logging strategii pro debug buildy
- vytvorit regression checklist pro vertical slice

Deliverables:

- test matrix
- regression checklist
- build hygiene pravidla

Acceptance criteria:

- systemicke chyby jdou overit i bez manualniho prehravani cele sceny
- existuje jednotny postup pro review build
- save data schema ma verzi nebo jasnou upgrade strategii

Zavislosti:

- Faze 1
- Faze 6
- Faze 8
- Faze 10

## Navrhovane milestones

### Milestone A: Running Prototype

Obsah:

- projekt existuje
- avatar se umi pohybovat
- UI umi poslat prikaz
- lze spustit jednoduchy pohyb ke zdi

Minimalni hotove faze:

- Faze 0
- Faze 1
- Faze 2

### Milestone B: Vertical Slice

Obsah:

- `wall sit`
- relief request
- sanction flow
- physical failure
- history logging

Minimalni hotove faze:

- Faze 3
- Faze 4
- Faze 5
- Faze 6
- Faze 7
- Faze 8
- cast Faze 10

### Milestone C: MVP Sandbox

Obsah:

- vice cviku
- vice dilcich povelu
- wardrobe gameplay
- quick camera views
- vice avataru
- persistentni stav

Minimalni hotove faze:

- Faze 9
- Faze 10
- Faze 11
- Faze 12

### Milestone D: Visual Review Build

Obsah:

- HDRP baseline
- prvni realisticky avatar
- prvni importovana gym scena nebo jeji cast
- animator a IK pass pro hlavni slice

Minimalni hotove faze:

- Faze 13
- Faze 14

### Milestone E: Stable Internal Build

Obsah:

- regression checklist
- build hygiene
- otestovany vertical slice
- stabilni save schema

Minimalni hotove faze:

- Faze 15

## Co lze delat paralelne

Po dokonceni Faze 1 lze paralelne rozjet:

- tvorbu prostredi a interakcnich anchoru
- pripravu animator controlleru
- navrh UI layoutu
- authoring `ScriptableObject` assetu

Po dokonceni Faze 4 lze paralelne rozjet:

- validacni pravidla pro dalsi cviky
- obsah sankci
- dialog templates
- profily osobnosti

Po dokonceni Faze 8 lze paralelne rozjet:

- rozsireni wardrobe obsahu
- persistence
- pridani dalsich cviku
- QA checklisty a regression sady

Po dokonceni Faze 10 lze paralelne rozjet:

- visual polish pass
- animation cleanup
- import a review realnych assetu
- internal review build pipeline

## Doporucene poradi implementace v praxi

Pokud se ma zacit okamzite, doporucuji tento konkretni sled:

1. Zalozit `Unity` projekt a prenest skeleton.
2. Rozbehat `AvatarStateMachine`, `CommandDispatcher` a jednoduche UI tlacitko.
3. Udelat chod ke zdi a zarovnani.
4. Udelat `wall sit` bez validace, jen jako vykonatelnou sekvenci.
5. Dodat validaci drzeni pozice.
6. Dodat unavu, bolest a relief request.
7. Dodat fyzicke selhani.
8. Dodat sankce `delsi vydrz` a `mene obleceni`.
9. Dodat session history.
10. Udelat animation/IK cleanup pro hlavni flow.
11. Udelat HDRP visual baseline.
12. Teprve potom pridavat dalsi cviky a dilci povely.

## Hlavni rizika implementace

### 1. Animace a prechody

Riziko:
system bude funkcni, ale avatar bude pusobit roboticky nebo neprirozene.

Mitigace:

- investovat brzy do kvalitnich locomotion a hold animaci
- neodkladat animator controller az na konec
- drzet maly pocet cviku v prvnich iteracich

### 2. Prilis obecny command system

Riziko:
snaha navrhnout uz ted parser pro budouci volny text zkomplikuje MVP.

Mitigace:

- pro MVP drzet pevne definovane prikazy a menu
- text parser resit az po stabilizaci runtime modelu

### 3. Nevyvazeny model bolesti

Riziko:
avatar bude bud selhavat moc brzy, nebo naopak prosby nikdy neprijdou.

Mitigace:

- ladit na jednom cviku
- sbirat debug telemetry
- oddelit tuning dat od kodu

### 4. Obleceni jako jen kosmetika

Riziko:
system `less clothing` bude pusobit umele, pokud nebude propojen s comfort a negotiation.

Mitigace:

- od zacatku vest clothing state jako gameplay data
- napojit ho na fatigue, pain a behavior

## Doplnujici technicke ukoly

Tyto ukoly nejsou prve na kriticke ceste, ale maji byt pripraveny vcas:

- debug overlay pro runtime state
- tuning inspector pro fatigue and pain
- editor tooling pro vytvareni exercise definitions
- jednotne ID schema pro commands, exercises, sanctions a wardrobe items
- testovaci scena pro validaci animator prechodu
- importer nebo poloautomaticky prepis seed JSON do `ScriptableObject` assetu
- screenshot baseline pro visual review
- naming convention check pro assety a prefaby
- save data version field

## Content Authoring Workflow

Obsah MVP se nema vytvaret nahodne v editoru.

Doporuceny postup:

1. Nejdriv upravit seed data nebo referencni katalog.
2. Potom vytvorit nebo aktualizovat `ScriptableObject` asset.
3. Spustit obsahovou validaci:
   - ID unikaty
   - reference na existujici assety
   - typ cviku vs. validni sankce
4. Teprve potom asset pripojit do sceny nebo bootstrapu.

Prakticke pravidlo:

- `seed JSON` je zdroj pravdy pro prvni authoring
- `ScriptableObject` je vykonavaci forma v Unity
- oboji se musi udrzovat konzistentni

## Test Strategy

Testovani rozdelit do tri vrstev:

### 1. Pure Runtime Tests

Co sem patri:

- fatigue and pain progression
- behavior decisions
- sanction blocking
- content validation

Vhodne pro:

- rychle spusteni bez sceny
- ladeni prahu a rozhodovacich pravidel

### 2. Scene Integration Tests

Co sem patri:

- command to movement flow
- interaction anchors
- hold timer
- wardrobe transitions
- camera quick views

Vhodne pro:

- `Gym_MVP`
- `AnimationSandbox`
- `ValidationSandbox`

### 3. Manual Review Checklists

Co sem patri:

- vizualni realismus
- citelnost UI
- prirozenost animaci
- konzistence historie a dialogu

Pravidlo:

- novy vertical slice se nepovazuje za hotovy bez alespon jedne manualni review checklist session

## Asset and Import Governance

Protoze projekt stoji na realistu, asset pipeline neni vedlejsi tema.

Kazdy importovany asset ma projit:

1. `intake` kontrolou
2. naming a folder kontrolou
3. material review
4. scale a rig review
5. scene smoke testem

Blokacni chyby:

- spatna mierka
- nekompatibilni rig
- materialy mimo HDRP workflow
- nejednotne naming schema
- asset bez jasne licence nebo puvodu

## Build and Release Hygiene

Minimalni standard pred prvnimi review buildy:

- build profily `Dev`, `Review`, `Milestone`
- crash/log soubor nebo aspon debug log export
- verze save dat
- changelog k milestone buildum
- jednotna konvence screenshotu a videi pro review

## Doporuceni k dalsimu kroku

Nejlepsi navazujici krok po tomto planu je:

`Rozpracovat Fazi 0 a Fazi 1 do konkretni task breakdown struktury`

To znamena:

- konkretni soubory a komponenty
- seznam prvnich `ScriptableObject` assetu
- seznam prvnich `MonoBehaviour` komponent
- navrh prvni sceny
- definice prvnich acceptance testu pro `wall sit`
