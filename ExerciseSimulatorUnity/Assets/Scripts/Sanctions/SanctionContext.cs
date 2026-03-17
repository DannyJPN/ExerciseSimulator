using System;
using TrainerAvatarSimulator.Exercises;
using TrainerAvatarSimulator.Validation;

namespace TrainerAvatarSimulator.Sanctions
{
    [Serializable]
    public class SanctionContext
    {
        public ExerciseExecutionState ExerciseState = new ExerciseExecutionState();
        public FormEvaluationResult FormResult = new FormEvaluationResult();
        public bool IsPhysicalFailure;
        public bool RequestedByAvatar;
    }
}
