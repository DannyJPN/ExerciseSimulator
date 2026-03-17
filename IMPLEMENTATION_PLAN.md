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

Ne vsechny proudy mohou bezet od prvniho dne stejne.
Kriticka cesta pro prvni vertical slice je:

- `Core Runtime`
- `Avatar Motion and Interaction`
- `Exercise Execution and Validation`
- `Fatigue, Pain and Capability`
- `Sanctions, Negotiation and Self-Assessment`
- `UI and Camera`

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

Deliverables:

- kompilujici prazdny projekt
- scena `Gym_MVP`
- avatar prefab placeholder
- placeholder interaktivni zed, lavicka a podlozka

Acceptance criteria:

- projekt se otevre bez compile errors
- scena se spusti
- v scene jsou pripraveny tri interaktivni objekty

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

Deliverables:

- avatar umi reagovat na dilci povely z panelu
- dilci povel jde zadat i behem cviku
- aktualni cinnost se korektne prerusi

Acceptance criteria:

- prikazy `sedni si`, `klekni si`, `predpaz`, `sundej si boty` realne meni stav avatara
- odebirani obleceni ma animovany prubeh, ne jen skryti meshe

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

Deliverables:

- cvik vraci:
  - pass
  - fail
  - typ chyby

Acceptance criteria:

- u `wall sit` jde rozpoznat ztrata spravne pozice
- u `drepu` jde rozpoznat nedostatecny rozsah a zvednuti pat
- spatne provedeni zpusobi nezapocitani

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

Deliverables:

- avatar se neunavi jen kosmeticky, ale meni vykon
- existuje fyzicke selhani bez sankce

Acceptance criteria:

- pri dlouhem `wall sit` avatar umozni relief request
- po zamitnuti prosby umi dojit k fyzickemu selhani
- po selhani zaujme ochrannou polohu a hlasi bolest

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

Deliverables:

- avatar umi aktivne reagovat na zatez
- avatar umi sam popsat chybu a navrhnout dalsi postup

Acceptance criteria:

- avatar umi pozadat o ulevu pri unave, bolesti a nevhodnem obleceni
- avatar umi pozadat o ztizeni pri nizke zatezi
- avatar umi pri spatnem provedeni sam navrhnout sankci

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

Deliverables:

- sankce lze pridelit a provest
- system umi blokovat nerealizovatelne sankce

Acceptance criteria:

- `less clothing` nelze ulozit, kdyz uz neni co sundat
- `longer hold` umi prodlouzit `wall sit`
- `stricter variation` umi pridat napr. ruce pred telo tam, kde je to podporovano

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

Deliverables:

- trener umi realne ovladat avatar pres UI
- historie je pristupna bez zahlceni obrazovky

Acceptance criteria:

- z UI jde spustit cvik, dilci povel i sankce
- kamera umi prepinat rychle pohledy
- historie zachycuje klicove udalosti

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

Deliverables:

- plny MVP feature set podle specifikace

Acceptance criteria:

- vsechny MVP cviky jsou datove definovane a spustitelne
- vsechny MVP profily meni rozhodovani i textove reakce

Zavislosti:

- Faze 7
- Faze 8
- Faze 9
- Faze 10
- Faze 11

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
10. Teprve potom pridavat dalsi cviky a dilci povely.

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

## Doporuceni k dalsimu kroku

Nejlepsi navazujici krok po tomto planu je:

`Rozpracovat Fazi 0 a Fazi 1 do konkretni task breakdown struktury`

To znamena:

- konkretni soubory a komponenty
- seznam prvnich `ScriptableObject` assetu
- seznam prvnich `MonoBehaviour` komponent
- navrh prvni sceny
- definice prvnich acceptance testu pro `wall sit`
