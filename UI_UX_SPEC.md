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
- UI je `cesky`, vsechny texty pres lokalizacni klice (pripraveno na dalsi jazyk)
- UI trenerovi `nezobrazuje chyby provedeni` – trener je musi sam odhalit pozorovanim

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
- `Wardrobe` (sundat / obleci zpet pro kazdy slot)
- `Set Control` (`Dalsi serie`, `Dost`)
- `Trainer Responses` (`Prijmout`, `Zamitnout`, `Odpustit`)
- `Sanctions`

### 1b. Zadani parametru cviku

Pri volbe cviku trener:

- vybere ulozeny `preset`, nebo
- zada parametry rucne: pocet serii, opakovani nebo delka vydrze, u cviku se stranami `strana` (`avatar voli` / `stridave` / `jen leva` / `jen prava`), nebo
- spusti cviceni `bez cile`

Rucne zadane parametry lze ulozit jako novy preset.

Pauza mezi seriemi trva do povelu `Dalsi serie`.

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
- `Serie` (aktualni / celkem, `pauza` mezi seriemi)
- `Pokusy` (provedena opakovani nebo uplynuly cas – bez hodnoceni spravnosti)
- `Busy / Available`

Poznamka:

- `Counted / Not Counted` se trenerovi nezobrazuje; o nezapocitani rozhoduje trener

### 3. Fatigue and Pain Panel

Obsah:

- segmentova unava
- segmentova bolest
- globalni stav

Segmenty:

- jednotlive svaly, zvlast leva a prava strana (viz `FUNCTIONAL_SPEC_V2.md`, `Segmenty tela`)
- kardio (jen unava)

Doporucene zobrazeni:

- mapa tela (predni a zadni pohled) s obarvenymi svaly
- pod ni souhrnne bary po skupinach (nohy, ruce, trup, kardio) s moznosti rozbaleni na jednotlive svaly
- barvy pro warning a critical
- indikace `pretizeni` (avatar muze odmitnout cviky zatezujici dany sval)

### 4. Wardrobe Panel

Obsah:

- top (mikina / bunda)
- bottom (teplaky)
- shoes
- socks

U kazde polozky:

- obleceno / neobleceno
- zda je slot dostupny
- tlacitko `sundat` / `obleci zpet`

Poznamka:

- nesundatelna zakladni vrstva (tricko/tilko + sortky / sukne s vsitymi sortkami) se v panelu nezobrazuje jako slot

## Skryte nebo sekundarni panely

### 5. History Panel

Ma byt dostupny, ale ne trvale otevreny.

Obsah:

- zadane prikazy
- dokoncene cviky
- nedokoncene cviky
- nezapocitani a sankce udelene trenerem
- priznani chyb avatarem (jen to, co avatar sam rekl)
- prosby avatara a odpovedi trenera (prijato / zamitnuto / odpusteno / bez odpovedi)

Mozne formy:

- pravostranny drawer
- collapsible panel
- samostatna overlay karta

### 6. Detail Evaluation Panel

Pouze `debug` panel pro vyvojare, ve hre pro trenera nedostupny (trener chyby nevidi).

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
- `New Avatar` (predloha + upravy vzhledu, profil osobnosti, jmeno)
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

1. pokud avatar prave necvici, otoci se do kamery; behem cviku muze kratce zaujmout prosebnou pozici (pauza bez trestu)
2. avatar zobrazi zpravu v `Dialogue Strip`
3. trener odpovi: `Prijmout`, `Zamitnout`, u zadosti o sankci zvoli sankci nebo `Odpustit`
4. pokud trener neodpovi, avatar pokracuje ve cviceni
5. po zamitnuti se dialog strip aktualizuje a avatar pokracuje

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
- rest between sets (ceka na `Dalsi serie`)
- open-ended exercise (ceka na `Dost`)
- plea pause (prosebna pozice)
- interrupted exercise
- not counted (rozhodnuti trenera)
- relief requested
- intensity requested
- sanction requested by avatar
- sanction assigned
- sanction blocked (vcetne duvodu: fyzicke selhani / preruseni trenerem / neni co sundat)
- forgiven
- overload refusal
- physical failure (ochranna poloha do odezneni bolesti)

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
- dalsi jazyky krome cestiny (lokalizacni klice ale ano)
- nastavitelne theme profily
