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
- `defaultPriority`
- `allowInterrupt`
- `uiCategory`

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

Kazda sablona muze mit:

- `templateId`
- `category`
- `toneTag`
- `text`

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

Poznamka:

- nesundatelne sortky mohou byt technicky oddelene od `WardrobeItemDefinition`
- jde spis o cast bazoveho visualu postavy

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
- `targetDuration`
- `targetRepetitions`
- `completedRepetitions`
- `wasInterrupted`
- `wasPhysicallyFailed`
- `activeModifierIds`
- `currentValidationResult`

### FatigueState

Pole:

- `legsFatigue`
- `armsFatigue`
- `coreFatigue`
- `cardioFatigue`
- `globalFatigue`

### PainState

Pole:

- `legsPain`
- `armsPain`
- `corePain`
- `backPain`
- `painDecayTimer`
- `lastPainSource`

`lastPainSource`:

- `Technique`
- `LongFatigue`
- `Clothing`
- `Unknown`

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

### BodyConditionState

Ucel:

- drzi dlouhodobejsi fyzicky stav v ramci sezeni i mezi sezenimi

Pole:

- `legsCondition`
- `armsCondition`
- `coreCondition`
- `cardioCondition`
- `activeMinorInjuries`

Poznamka:

- toto neni aktualni unava, ale delsi fyzicky stav avatara

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
- `finalAccepted`
- `blockedReason`
- `resolvedModifierIds`

## Save Data

Save data nesmi primo drzet reference na `Unity` objekty.

### AvatarProfileSave

Pole:

- `avatarProfileId`
- `displayName`
- `appearancePresetId`
- `behaviorProfileId`
- `lastSceneId`
- `lastPosition`
- `lastRotation`
- `wardrobeState`
- `bodyConditionState`
- `painState`
- `lastUpdatedUtc`

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
- lokalizaci
- live patching dialogovych sad z externiho zdroje
- databazi misto `JSON`
