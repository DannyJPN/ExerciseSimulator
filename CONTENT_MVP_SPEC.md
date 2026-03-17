# Content MVP Specification

## Ucel

Tento dokument fixuje konkretni MVP obsah.

Smysl:

- jasne rict, co je `v prvni verzi`
- neplest MVP s pozdejsim rozsireni
- dat jasny authoring backlog pro content assety

## MVP prikazove skupiny

### Exercises

- `Wall Sit`
- `Drep`
- `Klik`
- `Divci Klik`
- `Surrenders`
- `Sedy Lehy`
- `Vypady`

### Postures

- `Stoupni si`
- `Sedni si`
- `Klekni si`
- `Sedni si na paty`
- `Lehni si na bricho`
- `Lehni si na zada`
- `Drepni si`

### Arms

- `Predpaz`
- `Rozpaz`
- `Vzpaz`
- `Dej ruce za hlavu`

### Wardrobe

- `Sundej si boty`
- `Sundej si ponozky`

### Sanctions

- `Opakuj cvik`
- `Delsi vydrz`
- `Nezapocitat`
- `Zprisnit cvik`
- `Mene obleceni`

## Detail cviku MVP

### 1. Wall Sit

Typ:

- hold based

Interakce:

- zed

Minimalni chyby:

- ztrata vysky pozice
- ztrata kontaktu se zdi
- predcasne ukonceni

Mozne zprisneni:

- delsi vydrz
- mene obleceni

### 2. Drep

Typ:

- repetition based

Minimalni chyby:

- nedostatecny rozsah
- zvednuti pat
- predcasne ukonceni

Mozne zprisneni:

- vice opakovani
- predpaz
- mene obleceni

### 3. Klik

Typ:

- repetition based

Minimalni chyby:

- nedostatecny rozsah
- ztrata rovne linie tela
- predcasne ukonceni

Mozne zprisneni:

- vice opakovani
- delsi drzeni spodku

### 4. Divci Klik

Typ:

- repetition based

Poznamka:

- samostatny cvik, ne jen fallback bez definice

Minimalni chyby:

- nedostatecny rozsah
- predcasne ukonceni

### 5. Surrenders

Typ:

- repetition based

Povinna pozice rukou:

- ruce za hlavou

Minimalni chyby:

- poruseni poradi poloh
- ztrata rukou za hlavou
- predcasne ukonceni

### 6. Sedy Lehy

Typ:

- repetition based

Poznamka:

- plny sed leh
- `crunch` neni samostatny MVP cvik

Minimalni chyby:

- nedostatecne zvednuti
- predcasne ukonceni

### 7. Vypady

Typ:

- repetition based

Minimalni chyby:

- nedostatecny rozsah
- spatna stabilita
- predcasne ukonceni

## Typy chyb MVP

Globalni katalog chyb:

- `InsufficientRange`
- `ForbiddenBodyMovement`
- `EarlyTermination`
- `LostContact`
- `SequenceViolation`

Mapovani:

- `SequenceViolation` je hlavne pro `surrenders`
- `LostContact` je hlavne pro `wall sit`

## Typy proseb o ulevu

- `shorter_duration`
- `fewer_repetitions`
- `cancel_exercise`
- `soften_exercise`
- `soften_sanction`
- `cancel_sanction`
- `adjust_clothing`

## Typy zadosti o ztizeni

- `more_repetitions`
- `longer_hold`
- `add_arm_modifier`
- `remove_clothing`

## Typy sankci MVP

### 1. RepeatExercise

Pouziti:

- kdyz cvik nebyl splnen korektne a trener chce opakovani

### 2. ExtendHold

Pouziti:

- hold cviky jako `wall sit`

### 3. DoNotCount

Pouziti:

- cvik se nezapocita

Poznamka:

- system uz automaticky umi nezapocitani pri chybe
- zde jde o explicitni rozhodnuti trenera

### 4. StricterVariation

Pouziti:

- pridani arm modifieru
- zvyseni narocnosti v ramci tohotez cviku

### 5. LessClothing

Pouziti:

- odebrani casti obleceni

Blokace:

- pokud uz neni co sundat

## Osobnostni profily MVP

### 1. Disciplined

Chovani:

- vysoka compliance
- nizsi tendence protestovat
- castejsi prijeti sankce bez odporu

Styl mluvy:

- vecny
- strohy
- pokorny

### 2. Sensitive

Chovani:

- vysoka citlivost na bolest
- drivejsi prosby o ulevu
- castejsi verbalni reakce na diskomfort

Styl mluvy:

- opatrny
- citelnejsi diskomfort

### 3. SelfCritical

Chovani:

- casto sam priznava chyby
- sam navrhuje sankci
- nizsi prah pro sebehodnoceni fail

Styl mluvy:

- sebekriticky
- presny

### 4. Ambitious

Chovani:

- casteji zadosti o ztizeni
- snaha napravit chybu vyssi zatezi
- nizsi tolerance benevolence

Styl mluvy:

- energicky
- orientovany na vykon

## Dialogove kategorie MVP

Minimalni textove kategorie:

- `Success`
- `PoorFormConfession`
- `PainReport`
- `ReliefRequest`
- `IntensityRequest`
- `SanctionAcceptance`
- `MercyRequest`
- `ClothingAdjustmentRequest`
- `ExerciseNotCounted`

## Vizualni projevy MVP

Minimalni sada:

- bezny neutralni stav
- namaha
- vyrazna unava
- bolest
- ochranne drzene tela
- tres

## Authoring backlog pro content

### Nutne command assety

- `CMD_WallSit`
- `CMD_Squat`
- `CMD_PushUp`
- `CMD_KneePushUp`
- `CMD_Surrenders`
- `CMD_SitUp`
- `CMD_Lunge`
- `CMD_Stand`
- `CMD_Sit`
- `CMD_Kneel`
- `CMD_SitOnHeels`
- `CMD_LieFront`
- `CMD_LieBack`
- `CMD_SquatDown`
- `CMD_ArmsFront`
- `CMD_ArmsSide`
- `CMD_ArmsUp`
- `CMD_HandsBehindHead`
- `CMD_RemoveShoes`
- `CMD_RemoveSocks`

### Nutne exercise assety

- `EX_WallSit`
- `EX_Squat`
- `EX_PushUp`
- `EX_KneePushUp`
- `EX_Surrenders`
- `EX_SitUp`
- `EX_Lunge`

### Nutne sanction assety

- `SAN_RepeatExercise`
- `SAN_ExtendHold`
- `SAN_DoNotCount`
- `SAN_StricterVariation`
- `SAN_LessClothing`

### Nutne behavior profile assety

- `BHP_Disciplined`
- `BHP_Sensitive`
- `BHP_SelfCritical`
- `BHP_Ambitious`

## Co do MVP nepatri

- dalsi cviky mimo tento seznam
- dalsi sloty obleceni
- hlasove reakce
- parser volneho textu
- vztahovy model s trenerem mezi sezenimi
