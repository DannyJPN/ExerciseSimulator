# Vertical Slice Wall Sit

## Ucel

Tento dokument presne definuje prvni vertical slice, podle ktereho se ma overit architektura projektu.

Cil slice:

- dokazat, ze navrzena architektura funguje v jedne plne herni situaci
- overit propojeni UI, pohybu, cviku, vyhodnoceni, unavy, bolesti a sankce

## Scope

Slice pokryva pouze:

- jednoho avatara
- jednu scenu
- jeden interaktivni objekt typu `zed`
- cvik `wall sit`
- dve sankce:
  - `ExtendHold`
  - `LessClothing`

## Minimalni tok

1. trener zvoli `Wall Sit`
2. system vybere `EX_WallSit`
3. avatar prijme command
4. avatar dojde ke zdi
5. avatar se zarovna
6. avatar prejde do drzeni wall sit pozice
7. bezi casovac cviku
8. system prubezne sleduje:
   - unavu nohou
   - bolest
   - kontakt se zdi
   - vysku pozice
9. pokud je zatez vysoka, avatar muze pozadat o ulevu
10. trener muze prosbu zamitnout
11. avatar pokracuje
12. pokud uz nezvlada, dojde k fyzickemu selhani
13. fyzicke selhani se netresta
14. pokud trener udeli sankci, system overi:
   - zda je `ExtendHold` platna
   - zda je `LessClothing` platna
15. historie zapise vysledek

## Explicitni chovani slice

### Pri startu cviku

System musi:

- nastavit `activeCommand`
- nastavit `activeExercise`
- obsadit interakcni bod
- prepnout stav avatara do pohyboveho modu

### Pri drzeni pozice

System musi:

- bezet hold timer
- vyhodnocovat kvalitu drzene pozice
- zvedat fatigue nohou
- zvedat pain pri chybe nebo dlouhem pretizeni

### Pri prosbe o ulevu

Avatar muze pozadat o:

- kratsi vydrz
- zruseni nebo zmimeni cviku
- upravu odevu

Pokud trener zamitne:

- avatar pokracuje
- komunikace se zapise do historie

### Pri fyzickem selhani

MusI nastat:

- ukonceni cviku jako `not counted`
- nastaveni stavu ochranneho drzeni tela
- verbalni hlaseni bolesti
- zadna automaticka sankce

### Pri sankci ExtendHold

MusI nastat:

- prodlouzeni cilove vydrze
- aktualizace session state
- zaznam do historie

### Pri sankci LessClothing

MusI nastat:

- overeni, ze existuje sundatelna cast odevu
- prechod do wardrobe flow
- animovane sundani zvolene casti odevu
- navrat do dalsi cinnosti

Pokud uz neni co sundat:

- sankce je blokovana
- UI ukaze duvod blokace

## Edge Cases

### 1. Okamzite preruseni cviku jinym povelem

Priklad:

- trener behem wall situ zada `Klekni si`

Vysledek:

- wall sit je prerusen
- wall sit je `not counted`
- bez automaticke sankce

### 2. Ztrata kontaktu se zdi

Vysledek:

- zhorseni evaluation score
- mozne nezapocitani
- mozna verbalni sebereflexe chyby

### 3. Vysoka bolest, ale stale ne totalni selhani

Vysledek:

- avatar zpomaluje
- prosby o ulevu jsou castejsi

### 4. Sankce LessClothing neni mozna

Vysledek:

- sankce blokovana
- system navrhne nic nebo nechava rozhodnuti na trenerovi

## Minimalni assety pro slice

### Scene

- `Gym_MVP`

### Interactables

- `Wall_Interactable`

### Commands

- `CMD_WallSit`
- `CMD_Kneel`
- `CMD_RemoveShoes`

### Exercises

- `EX_WallSit`

### Sanctions

- `SAN_ExtendHold`
- `SAN_LessClothing`

### Profiles

- minimalne `BHP_Disciplined`

## Minimalni komponenty pro slice

Na avatarovi:

- `AvatarStateMachine`
- `CommandDispatcher`
- `ActionRunner`
- `WallSitController` nebo ekvivalentni exercise executor
- `FatigueController`
- `PainController`
- `BehaviorController`
- `WardrobeController`

Na zdi:

- `InteractableObject`
- `InteractionPoint`

Na UI:

- `DebugCommandPanelController`
- `AvatarStateDebugView`
- `SanctionPanelController`
- `DialogueStripController`

## Acceptance Criteria

### AC1: Command to movement

- klik na `Wall Sit` zpusobi pohyb avatara ke zdi

### AC2: Movement to hold

- po prichodu ke zdi avatar zaujme wall sit pozici

### AC3: Relief request

- pri vysoke unave nebo bolesti avatar sam pozada o ulevu

### AC4: Rejected relief

- po zamitnuti prosby avatar pokracuje

### AC5: Physical failure

- pri dalsim pretizeni avatar fyzicky selze
- cvik neni zapocitan
- neni automaticka sankce

### AC6: Extend hold

- trener muze udalit `ExtendHold`
- cilova doba cviku se zvysi

### AC7: Less clothing

- trener muze udalit `LessClothing`
- system sunda platnou cast odevu
- pokud to nejde, sankci zablokuje

### AC8: History

- historie zachyti:
  - start cviku
  - prosbu o ulevu
  - zamitnuti
  - selhani nebo dokonceni
  - sankci

## Debug telemetry doporucena pro slice

Po dobu ladeni zobrazovat:

- current posture
- current command
- elapsed hold time
- target hold time
- legs fatigue
- legs pain
- last dialogue category
- counted / not counted

## Co tento slice vedome nepokriva

- ostatni cviky
- ostatni osobnostni profily
- plnou wardrobe logiku vsech slotu
- parser volneho textu
- persistenci mezi sezenimi

## Kdy je slice povazovan za uspesny

Slice je uspesny, kdyz:

- funguje od UI az po vysledek cviku
- obsahuje aspon jednu autonomni prosbu avatara
- obsahuje aspon jednu sankci
- obsahuje fyzicke selhani bez rozpadu stavu
- dokaze udrzet konzistentni historii udalosti
