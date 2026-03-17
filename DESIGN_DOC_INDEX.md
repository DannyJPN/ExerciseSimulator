# Design Document Index

## Ucel

Tento dokument slouzi jako rozcestnik kompletni navrhove dokumentace pro projekt `Trainer-Avatar Simulator`.

Smysl sady dokumentu:

- uzavrit funkcni zadani
- uzavrit technicky smer
- uzavrit MVP obsah
- uzavrit datovy model
- uzavrit prvni vertical slice
- pripravit podklady pro implementaci v `Unity`

## Hlavni dokumenty

### 1. Technicka specifikace

Soubor:
[MVP_TECH_SPEC.md](C:\Users\Krupa\Desktop\Private\surrendering\MVP_TECH_SPEC.md)

Obsah:

- doporuceny stack
- systemova architektura
- moduly a zodpovednosti
- stavovy model
- navrh slozek
- technicka rizika

### 2. Funkcni specifikace

Soubor:
[FUNCTIONAL_SPEC_V2.md](C:\Users\Krupa\Desktop\Private\surrendering\FUNCTIONAL_SPEC_V2.md)

Obsah:

- role trenera
- pravidla chovani avatara
- povely
- cviky
- sankce
- unava, bolest a selhani
- UI principy
- perzistence a profily

### 3. Implementacni plan

Soubor:
[IMPLEMENTATION_PLAN.md](C:\Users\Krupa\Desktop\Private\surrendering\IMPLEMENTATION_PLAN.md)

Obsah:

- implementacni faze
- milestone
- zavislosti
- acceptance criteria
- kriticka cesta

### 4. Startovni backlog

Soubor:
[PHASE_0_1_TASKS.md](C:\Users\Krupa\Desktop\Private\surrendering\PHASE_0_1_TASKS.md)

Obsah:

- konkretni tasky pro `Fazi 0` a `Fazi 1`
- prvni soubory
- prvni assety
- prvni acceptance testy

### 5. Datovy model

Soubor:
[DATA_MODEL_SPEC.md](C:\Users\Krupa\Desktop\Private\surrendering\DATA_MODEL_SPEC.md)

Obsah:

- authoring data
- runtime data
- save data
- ID schema
- vazby mezi typy

### 6. UI a UX MVP

Soubor:
[UI_UX_SPEC.md](C:\Users\Krupa\Desktop\Private\surrendering\UI_UX_SPEC.md)

Obsah:

- rozlozeni obrazovky
- trvale viditelne panely
- skryta historie
- kamera
- ovladani trenera
- stavy zpetne vazby

### 7. MVP obsah

Soubor:
[CONTENT_MVP_SPEC.md](C:\Users\Krupa\Desktop\Private\surrendering\CONTENT_MVP_SPEC.md)

Obsah:

- katalog povelu
- katalog cviku
- katalog sankci
- typy proseb
- osobnostni profily
- zakladni dialogove kategorie

### 8. Prvni vertical slice

Soubor:
[VERTICAL_SLICE_WALL_SIT.md](C:\Users\Krupa\Desktop\Private\surrendering\VERTICAL_SLICE_WALL_SIT.md)

Obsah:

- scope prvniho slice
- presny tok `wall sit`
- edge cases
- acceptance criteria
- minimalni assety a komponenty

### 9. Photoreal Unity setup

Soubor:
[PHOTOREAL_UNITY_SETUP.md](C:\Users\Krupa\Desktop\Private\surrendering\PHOTOREAL_UNITY_SETUP.md)

Obsah:

- HDRP startovni setup
- lighting a camera baseline
- avatar a clothing pipeline
- materialovy standard
- visual acceptance criteria

### 10. Asset intake pipeline

Soubor:
[ASSET_INTAKE_PIPELINE.md](C:\Users\Krupa\Desktop\Private\surrendering\ASSET_INTAKE_PIPELINE.md)

Obsah:

- intake pravidla
- naming convention
- review checklist
- import pravidla
- blokacni chyby

## Stav navrhove sady

Aktualni stav:

- technicke zadani: hotovo
- funkcni zadani: hotovo
- implementacni plan: hotovo
- startovni backlog: hotovo
- datovy model: hotovo
- UI a UX MVP: hotovo
- MVP obsah: hotovo
- vertical slice navrh: hotovo
- photoreal unity setup: hotovo
- asset intake pipeline: hotovo

## Co uz neni navrhova prace

Po dokonceni teto sady uz dalsi hlavni krok neni dokumentace, ale implementace:

- zalozeni skutecneho `Unity` projektu
- preneseni skeletonu
- rozbeh prvniho vertical slice

## Doporucene poradi cteni

1. [FUNCTIONAL_SPEC_V2.md](C:\Users\Krupa\Desktop\Private\surrendering\FUNCTIONAL_SPEC_V2.md)
2. [MVP_TECH_SPEC.md](C:\Users\Krupa\Desktop\Private\surrendering\MVP_TECH_SPEC.md)
3. [DATA_MODEL_SPEC.md](C:\Users\Krupa\Desktop\Private\surrendering\DATA_MODEL_SPEC.md)
4. [UI_UX_SPEC.md](C:\Users\Krupa\Desktop\Private\surrendering\UI_UX_SPEC.md)
5. [CONTENT_MVP_SPEC.md](C:\Users\Krupa\Desktop\Private\surrendering\CONTENT_MVP_SPEC.md)
6. [VERTICAL_SLICE_WALL_SIT.md](C:\Users\Krupa\Desktop\Private\surrendering\VERTICAL_SLICE_WALL_SIT.md)
7. [PHOTOREAL_UNITY_SETUP.md](C:\Users\Krupa\Desktop\Private\surrendering\PHOTOREAL_UNITY_SETUP.md)
8. [ASSET_INTAKE_PIPELINE.md](C:\Users\Krupa\Desktop\Private\surrendering\ASSET_INTAKE_PIPELINE.md)
9. [IMPLEMENTATION_PLAN.md](C:\Users\Krupa\Desktop\Private\surrendering\IMPLEMENTATION_PLAN.md)
10. [PHASE_0_1_TASKS.md](C:\Users\Krupa\Desktop\Private\surrendering\PHASE_0_1_TASKS.md)
