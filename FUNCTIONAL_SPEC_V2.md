# Functional Specification v2

## Projekt

Pracovni nazev: `Trainer-Avatar Simulator`

Typ produktu:
`PC-first sandbox simulator`, ve kterem uzivatel vystupuje vyhradne jako trener a ridi humanoidniho avatara pri cviceni, korekcich, sankcich a prevlekani.

Tento dokument uzavira funkcni zadani pro MVP na zaklade upresnenych rozhodnuti.

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
  - akce, ktere nemaji dostupnou realizovatelnou polohu nebo kontext

Avatar nesmi prikaz primo odmitnout. Muze:

- pozadat o zmimeni povelu
- pozadat o zruseni povelu
- pozadat o zmimeni sankce

Pokud trener prosbu zamitne:

- avatar se musi podridit
- povel nebo sankce zustavaji v puvodni podobe

## Kamera a ovladani

- Kamera bude `volne ovladatelna`
- Zaroven budou existovat `rychle pohledy`

Doporucene rychle pohledy pro MVP:

- cela postava
- celni pohled
- bocni pohled
- detail nohou
- detail trupu

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

## Obleceni a diskretnost

Avatar ma tyto sloty obleceni:

- `top`
- `bottom`
- `shoes`
- `socks`

Soucasne plati:

- avatar ma `nesundatelne sortky`
- aplikace musi zustat zobrazitelna i ve stavu, kdy jsou vsechny 4 sloty sundane

Prevlekani musi byt:

- detailne animovane
- diskretni
- provedene v nejblizsi mozne poloze, ve ktere to jde

Priklady vhodnych poloh:

- sed na lavicce
- sed se skracenymi nohami
- klek

Pro cviky neexistuji pevna globalni pravidla obleceni.
Obleceni ale ovlivnuje:

- komfort
- techniku
- vykon
- pravdepodobnost prosby o ulevu

Sankce `mene obleceni` musi byt podporovana uz v MVP.
Pokud uz nelze zadnou dalsi cast obleceni sundat:

- tato sankce je blokovana

## Dilci povely MVP

Minimalni sada dilcich povelu pro MVP:

- `stoupni si`
- `sedni si`
- `klekni si`
- `sedni si na paty`
- `lehni si na bricho`
- `lehni si na zada`
- `predpaz`
- `rozpaz`
- `vzpaz`
- `dej ruce za hlavu`
- `sundej si boty`
- `sundej si ponozky`
- `drepni si`

Poznamka:
`sedni si` zde znamena bezne sezeni s pokrcenyma nohama, ne sed na lavicce, pokud to nebude explicitne zadano jinak.

## Cviky MVP

Pevna pocatecni sada cviku:

- `wall sit`
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

### Definice sedu-lehu

Pro MVP jde o `plne sedy lehy`, ne o `crunch`.

`Crunch` muze byt pozdeji pouzit jako:

- lehci varianta cviku
- nebo projev bolesti brisnich svalu

## Spousteni a prerusovani povelu

- Dilci povely je mozne zadavat i `behem cviku`
- Novy povel se ma plnit `ihned`
- System tedy musi podporovat `okamzite preruseni aktualni cinnosti`

Pokud je cvik prerusen:

- cvik se povazuje za `nedokonceny`
- cvik se `nezapocitava`
- z preruseni automaticky nevyplyva sankce

## Hodnoceni provedeni

Avatar musi umet rozpoznat konkretni typ chyby.

Minimalni typy chyb pro MVP:

- `nedostatecny rozsah`
- `nezadouci pohyb casti tela, ktera se hybat nema`
- `predcasne ukonceni`

Priklad:

- u drepu muze jit o zvednuti pat

Vyhodnoceni ma byt:

- automaticke
- po cviku nebo po opakovani dle typu cviku
- spojene s verbalni identifikaci chyby

Pokud avatar vyhodnoti provedeni jako spatne:

- cvik nebo opakovani se `nezapocitava`

## Sebehodnoceni

Avatar musi umet:

- rozpoznat vlastni chybu
- priznat ji
- konkretne ji pojmenovat
- navrhnout sankci
- nebo obecne pozadat o sankci a ponechat vyber na trenerovi

Sebehodnoceni ma byt ovlivneno:

- profilem osobnosti
- aktualni unavou
- mirou bolesti
- sebekritikou

## Sankce

Pro MVP jsou povolene tyto typy sankci:

- `opakovani cviku`
- `delsi serie nebo vetsi vydrz`
- `nezapocitani cviku`
- `zprisneni cviku`
- `provedeni cviku s mene oblecenim`

Pravidla:

- fyzicke selhani se netresta
- nedokonceni z duvodu preruseni se automaticky netresta
- neprimerene nebo neproveditelne sankce system blokuje

Avatar muze:

- sam navrhnout konkretni sankci
- obecne poprosit o sankci a nechat rozhodnout trenera
- pri udelene sankci poprosit o mirnejsi variantu

Pokud trener zadost o zmimeni zamitne:

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
- zmimeni cviku
- zmimeni sankce
- zruseni sankce
- moznost upravit odev

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
- historie bolesti nebo zraneni
- posledni umisteni ve scene

System ma podporovat `vice ulozenych avataru`.

Ukladani:

- manualni ulozeni a ukonceni
- prubezne automaticke ukladani

## Kondice

Kondice je vedena `po segmentech tela`, ne jednou globalni hodnotou.

Minimalni segmentace pro MVP:

- nohy
- ruce
- stred tela
- kardio

Kondice:

- roste pouze za `dokonale provedene cviky`
- roste pomalu
- ovlivnuje, co avatar vnima jako lehkou nebo tezsi zatez

## Unava, bolest a selhani

`Unava` a `bolest` jsou samostatne veliciny.

Bolest muze vzniknout:

- spatnou technikou
- dlouhodobym unavenim
- nevhodnym odevem nebo obutim

Kdyz se zhorsuje stav, plati toto poradi projevu:

1. zhorseni techniky
2. prosba o ulevu
3. zpomaleni
4. fyzicke selhani

Pokud dojde k fyzickemu selhani:

- avatar cvik uz nedokaze dokoncit
- fyzicke selhani se netresta
- avatar zaujme ochrannou polohu
- drzi se za bolavou cast tela
- verbalne sdeli, co ho boli

Bolest:

- casem postupne ustupuje
- pri dalsim komandovani v bolesti muze avatar hlavne prosit a verbalne davat najevo bolest
- po urcitem case bolesti muze pri dalsim komandovani reagovat prevazne verbalne typu `au` a prosbami

Trener muze zatizit i bolavou cast tela, pokud to jeste neni fyzicky nemozne.

## Psychika a vykon

Psychika ovlivnuje vykon.

System musi pocitat aspon s vlivem:

- citu pro spravedlnost
- discipliny
- sebekritiky
- citlivosti
- ambice

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

## Vizualni reakce

Avatar musi mit i neverbalni projevy.

Minimalni vizualni projevy pro MVP:

- vyraz obliceje
- zrychlene dychani
- tres
- zpomaleni pohybu
- ochranne drzeni tela pri bolesti

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
- splnene cviky
- chyby
- sankce
- prosby avatara

## Co neni soucast MVP

- hlasove ovladani
- runtime generativni dialog
- vice postav najednou v jedne scene
- otevrena rozsahla telocvicna
- vztahovy model mezi trenerem a avatarem napric sezenimi

## Doporuceny prvni vertical slice

Prvni overovaci implementace ma pokryt:

- `wall sit`
- preruseni cviku dilcim povelem
- automaticke nezapocitani pri preruseni
- rozpoznani chyby
- prosbu o ulevu
- zamitnuti prosby trenerem
- fyzicke selhani bez sankce
- sankci `delsi vydrz`
- sankci `mene obleceni`
- textovou i vizualni reakci bolesti

## Otevrene body mimo MVP

Tyto body nejsou nutne blokujici, ale budou vyzadovat pozdejsi rozhodnuti:

- presna granularita segmentu tela nad ramec nohy, ruce, stred tela a kardio
- presny katalog formulaci pro jednotlive osobnostni profily
- pravidla pro budouci parser volneho textu
