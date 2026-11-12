# Functional Specification v2

## Projekt

Pracovni nazev: `Trainer-Avatar Simulator`

Typ produktu:
`PC-first sandbox simulator`, ve kterem uzivatel vystupuje vyhradne jako trener a ridi humanoidniho avatara pri cviceni, korekcich, sankcich a prevlekani.

Tento dokument uzavira funkcni zadani pro MVP na zaklade upresnenych rozhodnuti.
Pri rozporu s ostatnimi dokumenty ma prednost tento dokument.

## Zakladni role a rezim

- Uzivatel ma jedinou roli: `trener`
- Rezim aplikace je `sandbox`
- Neexistuje povinna struktura lekce
- Uzivatel muze zadavat povely v libovolnem poradi
- V MVP se povely zadavaji pres `UI panely`
- Do budoucna se pocita i s `volnym textem`

## Autorita trenera

- Trener ma v zasade volnou ruku v ramci definovaneho katalogu povelu a sankci
- System blokuje pouze:
  - fyzicky neproveditelne situace
  - sankce, ktere uz nelze dal aplikovat
  - sankce za situace, ktere se netrestaji (viz `Sankce`)
  - akce, ktere nemaji dostupnou realizovatelnou polohu nebo kontext

Avatar povel zasadne neodmita. Muze:

- pozadat o zmirneni povelu
- pozadat o zruseni povelu
- pozadat o zmirneni sankce

Pokud trener prosbu zamitne:

- avatar se musi podridit
- povel nebo sankce zustavaji v puvodni podobe

Jedina vyjimka je `pretizeni` (viz `Pretizeni a odmitnuti cviku`).

## Kamera a ovladani

- Kamera bude `volne ovladatelna`
- Zaroven budou existovat `rychle pohledy`

Doporucene rychle pohledy pro MVP:

- cela postava
- celni pohled
- bocni pohled
- detail nohou
- detail trupu

Kamera zastupuje pohled trenera. Kdyz se avatar obraci na trenera (prosba, zadost o sankci), obraci se do kamery.

Ovladani avatara:

- pouze pres `UI`
- bez primeho klikani na objekty ve scene

## Prostredi MVP

Prvni scena obsahuje pouze:

- `zed`
- `lavicku`
- `podlozku`

Avatar se pohybuje `realne v prostoru`, nikoli teleportem mezi body.

Interakce s prostredim probiha pres:

- dojit na misto
- zaujmout vhodnou polohu
- provest cvik nebo akci

## Tvorba avatara

Novy avatar vznika z `predlohy s upravami`:

- vyber predlohy (pohlavi, zakladni vzhled)
- uprava zakladnich parametru: vlasy, postava, odstin pleti, obleceni
- volba profilu osobnosti
- jmeno

Plny editor vzhledu neni soucasti MVP.

## Obleceni a diskretnost

Grafika musi byt za vsech okolnosti `SFW`.

### Nesundatelna zakladni vrstva

Kazdy avatar ma vzdy na sobe:

- `tricko nebo tilko`
- `sportovni sortky`
  - zeny mohou mit misto sortek `sukni s vsitymi sortkami` (zvenku sukne, zespodu jsou videt vsite sortky)

Zakladni vrstva neni slot, nelze ji sundat ani sankci, ani povelem.

### Sundavaci sloty

- `top` – vrchni vrstva (mikina, bunda)
- `bottom` – vrchni spodek (teplaky)
- `shoes`
- `socks`

Aplikace musi zustat zobrazitelna i ve stavu, kdy jsou vsechny 4 sloty sundane.

Prevlekani musi byt:

- detailne animovane
- diskretni
- provedene v nejblizsi mozne poloze, ve ktere to jde

Priklady vhodnych poloh:

- sed na lavicce
- sed se skrcenymi nohami
- klek

### Oblekani zpet

Ke kazdemu sundavacimu povelu existuje opacny povel (`obuj se`, `vezmi si ponozky`, `oblekni si mikinu`, `oblekni si teplaky`).

- avatar muze sam pozadat o vraceni obleceni (prosba `upravit odev`)
- bez souhlasu trenera se avatar neoblece
- stav obleceni se uklada i mezi sezenimi

### Vliv obleceni

Pro cviky neexistuji pevna globalni pravidla obleceni.
Obleceni ale ovlivnuje:

- komfort
- techniku
- vykon
- pravdepodobnost prosby o ulevu

Cviceni naboso:

- nezpusobuje bolest
- je primarne esteticka zmena

### Ponizeni

Sundane obleceni nasazene `za trest` (sankce `mene obleceni`) pusobi na avatara jako `ponizujici faktor`:

- snizuje sebevedomi a zvysuje pokoru (psychicke promenne)
- tim ovlivnuje vykon, sklon prosit a formulace reakci
- projevuje se navenek: vyraz, gesto, poznamka
- sila efektu zavisi na profilu (citlivy vice, disciplinovany mene)

Plati pro vsechny sundavaci sloty, nejen pro boty a ponozky.

## Dilci povely MVP

### Polohy

- `stoupni si`
- `sedni si`
- `klekni si`
- `sedni si na paty`
- `lehni si na bricho`
- `lehni si na zada`
- `drepni si`

Poznamka:
`sedni si` zde znamena bezne sezeni s pokrcenyma nohama, ne sed na lavicce, pokud to nebude explicitne zadano jinak.

### Ruce

- `predpaz`
- `rozpaz`
- `vzpaz`
- `dej ruce za hlavu`

### Obleceni

- `sundej si boty` / `obuj se`
- `sundej si ponozky` / `vezmi si ponozky`
- `sundej si mikinu` / `oblekni si mikinu`
- `sundej si teplaky` / `oblekni si teplaky`

### Rizeni serie

- `dalsi serie` – ukonci pauzu a spusti dalsi serii
- `dost` – ukonci cviceni bez cile

## Cviky MVP

Pevna pocatecni sada cviku:

- `wall sit`
- `plank`
- `drepy`
- `kliky`
- `divci kliky`
- `surrenders`
- `sedy lehy`
- `vypady`

### Definice surrenders

Jedno opakovani:

- stoj
- klek na jedno koleno
- klek na obe kolena
- klek na opacne koleno
- stoj

Pri cviku `surrenders` ma avatar:

- ruce za hlavou

### Strany

U cviku se stranami (`surrenders`, `vypady`):

- nohu si voli avatar sam
- trener muze urcit jinak: `stridave`, `jen leva`, `jen prava`

### Definice sedu-lehu

Pro MVP jde o `plne sedy lehy`, ne o `crunch`.

`Crunch` muze byt pozdeji pouzit jako:

- lehci varianta cviku
- nebo projev bolesti brisnich svalu

## Zadani cviku: serie, opakovani, vydrz

Cvik se deli na `serie`.

Parametry cviku:

- pocet serii
- pocet opakovani v serii (opakovaci cviky)
- delka vydrze (vydrzove cviky)

Trener muze:

- pouzit vlastni ulozeny `preset` (napr. `drepy 3x10`)
- zadat parametry pokazde znovu
- spustit `cviceni bez cile` – bezi, dokud trener nerekne `dost`

Pauza mezi seriemi:

- trva, dokud trener neda povel `dalsi serie`

## Spousteni a prerusovani povelu

- Dilci povely je mozne zadavat i `behem cviku`
- Novy povel se ma plnit `ihned`
- System tedy musi podporovat `okamzite preruseni aktualni cinnosti`

Pokud je cvik prerusen povelem trenera:

- cvik se povazuje za `nedokonceny`
- cvik se `nezapocitava`
- za preruseni nelze udelit sankci

## Hodnoceni provedeni

Serie konci po zadanem poctu pokusu. Spatne opakovani serii automaticky neprodluzuje.

Nesplneni hlida `trener` (hrac):

- trener sam sleduje provedeni (kamera, rychle pohledy)
- system trenerovi chyby provedeni v UI nezobrazuje
- nesplneni trener oznami az po serii nebo po cviku (nezapocitani, sankce)

System pritom chyby rozpoznava `jako svedomi avatara`:

- avatar vi, ktera opakovani byla spatne a proc
- podle profilu se muze sam priznat

Minimalni typy chyb pro MVP:

- `nedostatecny rozsah`
- `nezadouci pohyb casti tela, ktera se hybat nema`
- `predcasne ukonceni`

Priklad:

- u drepu muze jit o zvednuti pat

## Sebehodnoceni

Avatar musi umet:

- rozpoznat vlastni chybu
- priznat ji
- konkretne ji pojmenovat
- navrhnout sankci
- nebo obecne pozadat o sankci a ponechat vyber na trenerovi

Samo od sebe nesplneni oznamuji hlavne profily `sebekriticky` a `ambiciozni`.

Sebehodnoceni ma byt ovlivneno:

- profilem osobnosti
- aktualni unavou
- mirou bolesti
- sebekritikou

### Zadost o sankci

Kdyz avatar zada o sankci:

- pokud prave necvici, otoci se do kamery
- podle povahy pouzije mene ci vice prosebne gesto
- vyslovi zadost
- trener zvoli sankci, nebo `odpusti`

`Odpustit` je samostatna akce trenera.

## Sankce

Pro MVP jsou povolene tyto typy sankci:

- `opakovani cviku`
- `delsi serie nebo vetsi vydrz` – prodlouzi probihajici serii (uprava)
- `extra serie` – prida k cviku novou serii (pridani)
- `nezapocitani cviku`
- `zprisneni cviku`
- `provedeni cviku s mene oblecenim`

`Zkraceni pauzy` neni sankce.

Pravidla:

- fyzicke selhani se netresta – system takovou sankci blokuje
- preruseni cviku povelem trenera se netresta – system takovou sankci blokuje
- neprimerene nebo neproveditelne sankce system blokuje

### Sankce mene obleceni

- kus obleceni urci trener
- pokud ho trener neurci, vybere ho avatar
- pokud uz nelze zadny sundavaci slot sundat, je sankce blokovana

Avatar muze:

- sam navrhnout konkretni sankci
- obecne poprosit o sankci a nechat rozhodnout trenera
- pri udelene sankci poprosit o mirnejsi variantu

Pokud trener zadost o zmirneni zamitne:

- avatar sankci pokorne prijme

## Prosby o ulevu

Avatar muze pozadat o ulevu pri:

- unave
- bolesti
- nevhodnem odevu nebo obuti

Typy proseb o ulevu pro MVP:

- kratsi doba cviku
- mensi pocet opakovani
- zruseni cviku
- zmirneni cviku
- zmirneni sankce
- zruseni sankce
- moznost upravit odev

Prubeh prosby behem cviku:

- avatar muze pri prosbe pokracovat ve cviceni
- nebo muze kratce zaujmout `prosebnou pozici`
  - prosebna pozice je `pauza bez trestu`: cas i pocitani se zastavi a po navratu pokracuji
- pokud trener neodpovi, avatar ve cviceni pokracuje

Pokud je prosba zamitnuta:

- avatar musi pokracovat podle puvodniho prikazu

## Zadosti o ztizeni

Avatar muze pozadat o ztizeni kdyz:

- zatez je nizsi nez jeho aktualni kondice
- chce napravit predchozi chybu

Typy zadosti o ztizeni pro MVP:

- vice opakovani
- delsi vydrz
- zapojeni bezne nezapojene casti tela
- sundani casti odevu

Priklad:

- pri drepu muze avatar navrhnout `predpazit`

## Fyzicky stav, kondice a ukladani

Fyzicky stav avatara se mezi sezeni `pamatuje`.
Nepamatuje se pouze `vztah k trenerovi`.

Mezi sezeni se uklada:

- vzhled avatara
- obleceni
- kondice po segmentech tela
- unava a bolest po segmentech tela
- posledni umisteni ve scene

System ma podporovat `vice ulozenych avataru`.

Ukladani:

- manualni ulozeni a ukonceni
- prubezne automaticke ukladani

## Segmenty tela

Unava, bolest i kondice se vedou `po svalech`, `zvlast pro levou a pravou stranu`.
Sleduji se pouze svaly (ne klouby, ne chodidla).

Pracovni seznam svalovych segmentu (kazdy L/P):

- stehna (predni)
- hamstringy
- hyzde
- lytka
- ramena
- prsa
- triceps
- predlokti
- bricho
- spodni zada
- horni zada

Navic:

- `kardio` – pouze unava a kondice, neboli

## Kondice

Kondice je vedena `po segmentech tela`, ne jednou globalni hodnotou.

Kondice:

- roste pouze za `dokonale provedene cviky`
- roste pomalu
- ovlivnuje, co avatar vnima jako lehkou nebo tezsi zatez

## Unava, bolest a selhani

`Unava` a `bolest` jsou samostatne veliciny.

V MVP neexistuji zraneni. Jedinym dlouhodobym nasledkem pretizeni je `bolest svalu`.

Bolest muze vzniknout:

- spatnou technikou
- dlouhodobym unavenim
- nevhodnym odevem nebo obutim (krome cviceni naboso)

Regenerace:

- unava i bolest ubyvaji v `realnem case`, tedy i mezi sezenimi

Kdyz se zhorsuje stav, plati toto poradi projevu:

1. zhorseni techniky
2. prosba o ulevu
3. zpomaleni
4. fyzicke selhani

Pokud dojde k fyzickemu selhani:

- avatar cvik uz nedokaze dokoncit
- fyzicke selhani se netresta
- avatar zaujme ochrannou polohu
- drzi se za bolavy sval
- verbalne sdeli, co ho boli
- prosi o odpusteni a dava najevo neschopnost pokracovat
- v ochranne poloze zustava, dokud bolest neodezni natolik, aby mohl pokracovat
- na povely v teto dobe reaguje prosenim

Bolest:

- casem postupne ustupuje
- pri dalsim komandovani v bolesti muze avatar hlavne prosit a verbalne davat najevo bolest
- po urcitem case bolesti muze pri dalsim komandovani reagovat prevazne verbalne typu `au` a prosbami

Trener muze zatizit i bolavy sval, pokud to jeste neni fyzicky nemozne.

## Pretizeni a odmitnuti cviku

Avatar smi odmitnout cvik pouze pri `pretizeni`, tj. pri bolesti svalu zatezovanych cvikem.

Postup:

1. avatar nejdriv prosi o ulevu
2. pokud trener na cviku trva a bolest je nad `tvrdym prahem`, avatar cvik odmitne
3. pretizeny avatar musi odpocivat, dokud bolest neklesne pod prah

Polohy a ostatni dilci povely avatar plni i pri pretizeni.

## Psychika a vykon

Psychika ovlivnuje vykon.

System musi pocitat aspon s vlivem:

- citu pro spravedlnost
- discipliny
- sebekritiky
- citlivosti
- ambice
- sebevedomi a pokory (vcetne ponizeni, viz `Obleceni a diskretnost`)

Psychicke faktory ovlivnuji:

- pravdepodobnost prosby o ulevu
- pravdepodobnost zadosti o ztizeni
- sklon priznat chybu
- formulaci reakci

## Profily osobnosti

MVP ma obsahovat 4 zakladni profily:

- `disciplinovany`
- `citlivy`
- `sebekriticky`
- `ambiciozni`

Tyto profily se maji lisit:

- vnitrnimi parametry rozhodovani
- stylem verbalnich reakci
- mirou prosebnosti gest

## Dialog a projev

Avatar ma byt v MVP `spise strucny`.

Pravidla:

- po spravne provedenem cviku muze komentovat vysledek az `po cviceni`
- pri chybe ma chybu `konkretne pojmenovat`
- pro ruzne typy proseb maji existovat samostatne formulace
- zadost o ztizeni muze byt vecna i motivovana napravou predchozi chyby

V MVP:

- reakce budou `textove`
- bez hlasu
- jazyk `cestina`
- texty ulozene v datech tak, aby slo pozdeji pridat dalsi jazyk

## Vizualni reakce

Avatar musi mit i neverbalni projevy.

Minimalni vizualni projevy pro MVP:

- vyraz obliceje
- zrychlene dychani
- tres
- zpomaleni pohybu
- ochranne drzeni tela pri bolesti
- prosebna gesta (sila podle profilu)
- rozpaky pri ponizeni

## UI MVP

Trvale viditelne oblasti UI:

- panel povelu
- stav tela
- unava a bolest
- stav obleceni

Historie muze byt:

- schovana ve vedlejsim panelu nebo draweru

Historie ma obsahovat:

- zadane povely
- dokoncene a prerusene cviky
- nezapocitani a sankce udelene trenerem
- priznani chyb avatarem
- prosby avatara a odpovedi trenera

UI jazyk: `cestina`, pripravena na lokalizaci.

## Co neni soucast MVP

- hlasove ovladani
- runtime generativni dialog
- vice postav najednou v jedne scene
- otevrena rozsahla telocvicna
- vztahovy model mezi trenerem a avatarem napric sezenimi
- zraneni
- plny editor vzhledu avatara

## Doporuceny prvni vertical slice

Prvni overovaci implementace ma pokryt:

- `wall sit`
- preruseni cviku dilcim povelem
- automaticke nezapocitani pri preruseni
- rozpoznani chyby avatarem a jeho priznani
- prosbu o ulevu vcetne prosebne pozice
- zamitnuti prosby trenerem
- fyzicke selhani bez sankce
- sankci `delsi vydrz`
- sankci `mene obleceni`
- textovou i vizualni reakci bolesti

## Otevrene body mimo MVP

Tyto body nejsou nutne blokujici, ale budou vyzadovat pozdejsi rozhodnuti:

- potvrzeni presneho seznamu svalovych segmentu
- hodnoty prahu bolesti (prosba, tvrdy prah odmitnuti, konec ochranne polohy)
- presny katalog formulaci pro jednotlive osobnostni profily
- pravidla pro budouci parser volneho textu
