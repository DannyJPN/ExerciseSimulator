# Data Model Specification

## Ucel

Tento dokument definuje datovy model projektu pro tri vrstvy:

- `authoring data`
- `runtime data`
- `save data`

Cil:

- oddelit data od kodu
- udrzet cviky, sankce, povely a profily jako upravitelna data
- zajistit konzistentni ID schema

## Datove principy

Plati:

- gameplay logika nema byt hardcoded po jednotlivych cvicich
- povely, cviky a sankce maji byt autorovatelne jako data
- runtime stav ma byt oddelen od authoring assetu
- save data ma byt serializovatelne bez prime zavislosti na `UnityEngine.Object`
- texty pro UI a dialogy se neukladaji primo, ale jako `textKey` do lokalizacni tabulky (MVP: `cs`)

## Segmenty tela

Unava, bolest a kondice se vedou po `BodySegmentId`.

Svalove segmenty (kazdy ve variante `_L` a `_R`):

- `Quadriceps`
- `Hamstrings`
- `Glutes`
- `Calves`
- `Shoulders`
- `Chest`
- `Triceps`
- `Forearms`
- `Abs`
- `LowerBack`
- `UpperBack`

Plus:

- `Cardio` – jen unava a kondice, bez bolesti

Pracovni seznam, k potvrzeni.

Datova reprezentace:

- `SegmentValues` = mapa `BodySegmentId -> float (0..1)`

## ID schema

Kazda datova entita musi mit stabilni string ID.

Format:

- commands: `CMD_<Name>`
- exercises: `EX_<Name>`
- sanctions: `SAN_<Name>`
- wardrobe items: `WRD_<Name>`
- interaction presets: `INT_<Name>`
- behavior profiles: `BHP_<Name>`
- dialogue sets: `DLG_<Name>`

Priklady:

- `CMD_WallSit`
- `EX_WallSit`
- `SAN_ExtendHold`
- `WRD_Shoes_Basic`
- `BHP_Disciplined`

## Authoring Data

Authoring data budou primarne `ScriptableObject` assety.

### CommandDefinition

Ucel:

- reprezentuje zadatelny povel v UI

Pole:

- `commandId`
- `displayName`
- `commandType`
- `linkedExerciseId` nebo reference
- `targetPosture`
- `wardrobeTargetSlot`
- `wardrobeAction` (`Remove` / `PutOn`)
- `defaultPriority`
- `allowInterrupt`
- `uiCategory`
- `displayNameKey`

Poznamka:

- text parser do budoucna bude mapovat input na `CommandDefinition`

### ExerciseDefinition

Ucel:

- popisuje cvik a jeho pravidla

Pole:

- `exerciseId`
- `displayName`
- `exerciseMode`
- `requiredInteractionType`
- `entryPosture`
- `activePosture`
- `defaultDurationSeconds`
- `defaultRepetitions`
- `defaultSets`
- `isSided` (cvik se stranami: `surrenders`, `vypady`)
- `loadedSegments` (ktere svaly cvik zatezuje a jak moc)
- `allowedModifiers`
- `fatigueProfileId`
- `validationProfileId`
- `recommendedSanctionIds`
- `dialogueTags`

`exerciseMode`:

- `Hold`
- `Repetition`

### ExerciseModifierDefinition

Ucel:

- reprezentuje zprisneni nebo upravu cviku bez tvorby uplne noveho cviku

Priklady:

- ruce pred telo
- ruce za hlavu
- delsi vydrz
- vice opakovani
- mene obleceni

Pole:

- `modifierId`
- `displayName`
- `affectedExerciseIds`
- `requiredPostureOverride`
- `fatigueMultiplier`
- `validationAdjustment`
- `dialogueTag`

### SanctionDefinition

Ucel:

- popisuje typ sankce

Pole:

- `sanctionId`
- `displayName`
- `sanctionType`
- `intensity`
- `allowNegotiation`
- `blockedIfNoValidTarget`
- `exerciseCompatibility`
- `requiresWardrobeChange`
- `humiliationFactor` (psychicky dopad, napr. u `LessClothing`)

`sanctionType`:

- `RepeatExercise`
- `ExtendHold` (prodlouzeni probihajici serie)
- `ExtraSet` (pridani nove serie)
- `DoNotCount`
- `StricterVariation`
- `LessClothing`

Sankce je vzdy blokovana, pokud se vztahuje k:

- fyzickemu selhani
- cviku prerusenemu povelem trenera

### BehaviorProfileDefinition

Ucel:

- urcuje osobnostni profil avatara

Pole:

- `profileId`
- `displayName`
- `motivationBase`
- `complianceBase`
- `confidenceBase`
- `selfDisciplineBase`
- `selfCriticismBase`
- `fairnessSensitivity`
- `painSensitivity`
- `leniencySensitivity`
- `humiliationSensitivity`
- `pleadingIntensity` (sila prosebnych gest)
- `selfReportFailureChance` (sklon sam ohlasit nesplneni; vysoky u `SelfCritical` a `Ambitious`)
- `dialogueSetId`

Pocatecni profily:

- `BHP_Disciplined`
- `BHP_Sensitive`
- `BHP_SelfCritical`
- `BHP_Ambitious`

### DialogueTemplateSet

Ucel:

- obsahuje textove sablony reakci

Kategorie:

- `success`
- `poor_form_confession`
- `relief_request`
- `intensity_request`
- `sanction_acceptance`
- `mercy_request`
- `pain_report`
- `clothing_adjustment_request`
- `sanction_request`
- `forgiveness_plea`
- `overload_refusal`
- `humiliation`

Kazda sablona muze mit:

- `templateId`
- `category`
- `toneTag`
- `textKey`

### WardrobeItemDefinition

Ucel:

- popisuje cast odevu

Pole:

- `wardrobeItemId`
- `displayName`
- `slot`
- `isRemovable`
- `visualPrefabReference`
- `comfortModifiers`
- `painModifiers`
- `animationTag`

Sloty:

- `top` – vrchni vrstva (mikina, bunda)
- `bottom` – vrchni spodek (teplaky)
- `shoes`
- `socks`

Poznamka:

- nesundatelna zakladni vrstva (tricko/tilko + sortky, u zen volitelne sukne s vsitymi sortkami) neni `WardrobeItemDefinition`
- jde o cast bazoveho visualu postavy a musi zarucit SFW zobrazeni ve vsech stavech
- `painModifiers` se pro bosou nohu neuplatnuji (cviceni naboso neboli)

### TrainerPresetDefinition

Ucel:

- trenerem ulozena kombinace parametru cviku

Pole:

- `presetId`
- `exerciseId`
- `sets`
- `repetitions` nebo `durationSeconds`
- `sideMode` (`AvatarChoice` / `Alternate` / `LeftOnly` / `RightOnly`)

Poznamka:

- presety vytvari uzivatel za behu, ukladaji se jako save data, ne jako authoring asset

### InteractionPresetDefinition

Ucel:

- definuje typ interakce s prostredim

Priklady:

- `INT_Wall_Default`
- `INT_Bench_Default`
- `INT_Mat_Default`

Pole:

- `interactionId`
- `interactionType`
- `requiredPosture`
- `alignmentOffset`
- `orientationRule`
- `ikHints`

## Runtime Data

Runtime data maji byt beznych `C#` typu, ne assety.

### AvatarRuntimeState

Hlavni korenovy stav.

Pole:

- `currentPosture`
- `activeCommand`
- `activeExercise`
- `fatigueState`
- `painState`
- `wardrobeState`
- `behaviorState`
- `bodyConditionState`
- `currentInteractionTarget`
- `isBusy`

### CommandExecutionContext

Pole:

- `commandId`
- `commandType`
- `source`
- `startTime`
- `wasInterrupted`
- `interruptedByCommandId`
- `linkedExerciseId`
- `targetSlot`

### ExerciseSessionState

Ucel:

- stav prave beziciho cviku

Pole:

- `exerciseId`
- `exerciseMode`
- `startTime`
- `elapsedTime`
- `targetSets`
- `currentSet`
- `setPhase` (`Active` / `RestUntilCommand`)
- `isOpenEnded` (cviceni `bez cile`, konci povelem `Dost`)
- `targetDuration`
- `targetRepetitions`
- `attemptedRepetitions`
- `sideMode`
- `isPausedForPlea` (prosebna pozice – cas i pocitani stoji)
- `wasInterrupted`
- `wasPhysicallyFailed`
- `activeModifierIds`
- `avatarKnownErrors` (chyby, o kterych vi avatar; trenerovi se nezobrazuji)

### FatigueState

Pole:

- `segments` (`SegmentValues`, vcetne `Cardio`)
- `globalFatigue`

### PainState

Pole:

- `segments` (`SegmentValues`, bez `Cardio`)
- `lastPainSource`
- `isOverloaded` (bolest zatezovanych svalu nad tvrdym prahem)
- `isInProtectivePose` (po fyzickem selhani, dokud bolest neklesne)

`lastPainSource`:

- `Technique`
- `LongFatigue`
- `Clothing` (ne bosa noha)
- `Unknown`

Regenerace:

- unava i bolest klesaji podle realneho casu
- pri nacteni savu se odecte cas od `lastUpdatedUtc`

### WardrobeState

Pole:

- `top`
- `bottom`
- `shoes`
- `socks`
- `effectiveComfortScore`

Kazdy slot obsahuje:

- `itemId`
- `isWorn`
- `isAvailable`

### BehaviorState

Pole:

- `motivation`
- `compliance`
- `confidence`
- `perceivedLeniency`
- `selfDiscipline`
- `selfCriticism`
- `fairnessPerception`
- `humility`
- `selfConfidence`
- `humiliation` (zvysuje se pri sankci `LessClothing`, ovlivnuje vykon a dialog)

### BodyConditionState

Ucel:

- drzi dlouhodobejsi fyzicky stav v ramci sezeni i mezi sezenimi

Pole:

- `segments` (`SegmentValues`, vcetne `Cardio`)

Poznamka:

- toto neni aktualni unava, ale delsi fyzicky stav avatara
- v MVP neexistuji zraneni; dlouhodobym nasledkem je jen bolest v `PainState`

### FormEvaluationResult

Pole:

- `overallScore`
- `rangeScore`
- `stabilityScore`
- `contactScore`
- `completionScore`
- `errorTypes`
- `primaryErrorType`
- `isCounted`

Poznamka:

- vysledek slouzi jako `svedomi avatara` (sebehodnoceni, priznani)
- trenerovi se v UI nezobrazuje, o nezapocitani rozhoduje trener

`errorTypes` muze obsahovat:

- `InsufficientRange`
- `ForbiddenBodyMovement`
- `EarlyTermination`
- `LostContact`

### SanctionResolutionState

Pole:

- `sanctionId`
- `source`
- `wasRequestedByAvatar`
- `wasAssignedByTrainer`
- `wasNegotiated`
- `wasForgiven` (trener na zadost avatara odpustil)
- `finalAccepted`
- `blockedReason`
- `wardrobeTargetSlot` (u `LessClothing`; prazdne = vybira avatar)
- `resolvedModifierIds`

## Save Data

Save data nesmi primo drzet reference na `Unity` objekty.

### AvatarProfileSave

Pole:

- `avatarProfileId`
- `displayName`
- `appearancePresetId`
- `appearanceAdjustments` (vlasy, postava, odstin pleti, obleceni)
- `behaviorProfileId`
- `lastSceneId`
- `lastPosition`
- `lastRotation`
- `wardrobeState`
- `bodyConditionState`
- `fatigueState`
- `painState`
- `lastUpdatedUtc` (zaklad pro regeneraci v realnem case)

### TrainerSettingsSave

Pole:

- `presets` (seznam `TrainerPresetDefinition`)
- `language` (MVP: `cs`)

### SessionHistorySave

Pole:

- `sessionId`
- `avatarProfileId`
- `eventLog`
- `completedExercises`
- `failedExercises`
- `sanctionsApplied`

### EventLogEntrySave

Pole:

- `timestamp`
- `eventType`
- `entityId`
- `summary`
- `metadata`

## Vazby mezi typy

Zakladni graf vztahu:

- `CommandDefinition` muze odkazovat na `ExerciseDefinition`
- `ExerciseDefinition` odkazuje na `SanctionDefinition`, `InteractionPresetDefinition`, `DialogueTemplateSet`
- `BehaviorProfileDefinition` odkazuje na `DialogueTemplateSet`
- `WardrobeItemDefinition` vstupuje do `WardrobeState`
- `AvatarProfileSave` uklada snapshot runtime a dlouhodobeho stavu

## Datove rozhodnuti pro MVP

Pro MVP plati:

- asset reference v editoru jsou v poradku
- runtime ma pracovat primarne se string ID plus nactenym assetem
- save system bude ukladat jen string ID a primitivni hodnoty

## Minimalni authoring assety pro prvni vertical slice

Nutne assety:

- `CMD_WallSit`
- `CMD_Stand`
- `CMD_RemoveShoes`
- `EX_WallSit`
- `SAN_ExtendHold`
- `SAN_LessClothing`
- `BHP_Disciplined`
- `DLG_Default_Disciplined`
- `INT_Wall_Default`

## Co zamerne neresime v MVP

- slozite dedeni assetu
- dalsi jazyky krome cestiny (struktura `textKey` je ale pripravena)
- zraneni
- live patching dialogovych sad z externiho zdroje
- databazi misto `JSON`
