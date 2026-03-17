# UI UX Specification MVP

## Ucel

Tento dokument definuje rozhrani trenera pro MVP.

Cile:

- umoznit rychle zadavani povelu
- drzet trvale viditelne kriticke informace
- nezahltit obrazovku historii
- podporit kontrolu techniky pres kameru a stavove panely

## Zasady UI

UI ma pusobit jako:

- vecny trenerky panel
- ne jako hra s preplacanym HUD

Pravidla:

- trvale viditelne jen dulezite informace
- historie a detail lze schovat
- ovladani pres klikaci panely, ne pres svet ve scene
- textova komunikace avatara ma byt cistitelna bez prekryti hlavni akce

## Hlavni layout

Doporuceny desktop layout:

- leva strana: `Command Panel`
- prava strana: `Avatar Status Panel` + `Wardrobe Panel`
- dolni cast: `Dialogue Strip` a kratke systemove zpravy
- horni cast: kamera, quick views, session controls
- skryty drawer nebo panel: `History`

## Trvale viditelne panely

### 1. Command Panel

Obsah:

- skupiny povelu
- cviky
- dilci povely
- sankce
- rychle korekce

Skupiny:

- `Exercises`
- `Postures`
- `Arms`
- `Wardrobe`
- `Sanctions`

Priklad tlacitek:

- `Wall Sit`
- `Drep`
- `Sedni si`
- `Klekni si`
- `Predpaz`
- `Sundej si boty`

### 2. Avatar Status Panel

Obsah:

- aktualni stav postavy
- aktivni prikaz
- aktivni cvik
- zda je cvik pocitan
- aktualni interakce

Zobrazeni:

- `Posture`
- `Command`
- `Exercise`
- `Counted / Not Counted`
- `Busy / Available`

### 3. Fatigue and Pain Panel

Obsah:

- segmentova unava
- segmentova bolest
- globalni stav

Segmenty:

- nohy
- ruce
- stred tela
- kardio

Doporucene zobrazeni:

- horizontalni bary
- barvy pro warning a critical

### 4. Wardrobe Panel

Obsah:

- top
- bottom
- shoes
- socks

U kazde polozky:

- obleceno / neobleceno
- zda je slot dostupny

Poznamka:

- nesundatelne sortky se v panelu nemusi chovat jako editable slot

## Skryte nebo sekundarni panely

### 5. History Panel

Ma byt dostupny, ale ne trvale otevreny.

Obsah:

- zadane prikazy
- dokoncene cviky
- nedokoncene cviky
- chyby provedeni
- sankce
- prosby avatara

Mozne formy:

- pravostranny drawer
- collapsible panel
- samostatna overlay karta

### 6. Detail Evaluation Panel

Nepovinne pro uplne prvni build, ale doporucene pro ladeni.

Obsah:

- posledni chyba
- primary error type
- proc cvik nebyl zapocitan

## Dialogue Strip

Ucel:

- zobrazovat kratke textove reakce avatara

Vlastnosti:

- jedna az dve kratke vety
- automaticke mizenI po case
- dulezite reakce lze pripnout do historie

Typy zprav:

- zadost o ulevu
- zadost o ztizeni
- priznani chyby
- hlaseni bolesti
- prijeti sankce

## Kamera

Kamera ma dva rezimy:

- `free camera`
- `quick views`

### Free Camera

Umoznuje:

- orbit
- zoom
- pan

### Quick Views

Pevne pohledy pro kontrolu techniky:

- cela postava
- celni pohled
- bocni pohled
- detail nohou
- detail trupu

Poznamka:

- quick views nesmi resetovat session stav
- maji byt dostupne jednim klikem

## Session Controls

Horni lista nebo horni roh ma obsahovat:

- `Save`
- `Autosave indicator`
- `Select Avatar`
- `Load Avatar`
- `Session Menu`

## Interakcni pravidla

### Zadani prikazu

Tok:

1. trener klikne na tlacitko
2. system zobrazi aktivni command
3. pokud je prikaz okamzity, nahradi bezici cinnost
4. zmena se projevi v `Avatar Status Panel`

### Zadani sankce

Tok:

1. trener vybere sankci
2. system overi proveditelnost
3. pokud je sankce blokovana, UI ukaze duvod
4. pokud je mozna, sankce se aplikuje nebo prejde do vyjednavani

### Prosba avatara

Tok:

1. avatar zobrazi zpravu v `Dialogue Strip`
2. pokud jde o volbu trenera, v panelu sankci nebo cviku se ukaze odpovidajici kontext
3. po zamitnuti se dialog strip aktualizuje a avatar pokracuje

## Priorita informaci

Nejvyssi prioritu na obrazovce maji:

- aktivni cvik
- bolest
- unava
- stav obleceni
- aktualni prosba avatara

Nizsi priorita:

- historie
- starsi udalosti

## UI stavy, ktere musi MVP umet

- idle
- active command
- active exercise
- interrupted exercise
- not counted
- relief requested
- intensity requested
- sanction assigned
- sanction blocked
- physical failure

## Vizuani komunikace stavu

Doporucene konvence:

- modra nebo neutralni pro bezny stav
- zluta pro upozorneni
- cervena pro bolest nebo kriticky stav
- seda pro blokovanou volbu

## Doporucene MVP komponenty v UI Toolkit

- `VisualElement` root layout
- `ListView` nebo vlastni tlacitkove seznamy pro commandy
- `ProgressBar` pro unavu a bolest
- `Label` pro dialog strip
- `Foldout` nebo drawer pro history

## Co nemusi byt v prvni verzi UI

- drag and drop
- vlastni komplexni timeline
- pokrocile filtry historie
- lokalizacni system
- nastavitelne theme profily
