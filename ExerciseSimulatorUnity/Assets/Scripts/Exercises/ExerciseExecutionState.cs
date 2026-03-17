using System;
using UnityEngine;
using TrainerAvatarSimulator.Core;

namespace TrainerAvatarSimulator.Exercises
{
    [Serializable]
    public class ExerciseExecutionState
    {
        public string ExerciseId = string.Empty;
        public string DisplayName = string.Empty;
        public ExerciseKind ExerciseKind = ExerciseKind.Repetition;
        public float TargetDurationSeconds;
        public int TargetRepetitions;
        public float PendingRestSeconds;
        public int CompletedRepetitions;
        public bool CountCurrentResult = true;
        public string ActiveModifierId = string.Empty;

        public static ExerciseExecutionState FromDefinition(ExerciseDefinition definition)
        {
            if (definition == null)
            {
                return new ExerciseExecutionState();
            }

            return new ExerciseExecutionState
            {
                ExerciseId = definition.ExerciseId,
                DisplayName = definition.DisplayName,
                ExerciseKind = definition.ExerciseKind,
                TargetDurationSeconds = definition.DefaultDurationSeconds,
                TargetRepetitions = definition.DefaultRepetitions
            };
        }

        public void ApplyModifier(ExerciseModifierDefinition modifier)
        {
            if (modifier == null)
            {
                return;
            }

            ActiveModifierId = modifier.ModifierId;
            if (ExerciseKind == ExerciseKind.TimedHold)
            {
                TargetDurationSeconds = Mathf.Max(1f, TargetDurationSeconds * Mathf.Max(0.25f, modifier.FatigueMultiplier));
                return;
            }

            TargetRepetitions = Mathf.Max(1, Mathf.RoundToInt(TargetRepetitions * Mathf.Max(0.5f, modifier.FatigueMultiplier)));
        }
    }
}
