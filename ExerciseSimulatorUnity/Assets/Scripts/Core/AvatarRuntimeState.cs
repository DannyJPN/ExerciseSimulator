using System;
using TrainerAvatarSimulator.Commands;
using TrainerAvatarSimulator.Behavior;
using TrainerAvatarSimulator.Fatigue;
using TrainerAvatarSimulator.Exercises;
using TrainerAvatarSimulator.Wardrobe;
using TrainerAvatarSimulator.Validation;

namespace TrainerAvatarSimulator.Core
{
    [Serializable]
    public class AvatarRuntimeState
    {
        public AvatarPostureState CurrentPosture = AvatarPostureState.Idle;
        public string ActiveCommandId = string.Empty;
        public string ActiveExerciseId = string.Empty;
        public CommandExecutionContext ActiveCommandContext = new CommandExecutionContext();

        public FatigueState Fatigue = new FatigueState();
        public WardrobeState Wardrobe = new WardrobeState();
        public BehaviorState Behavior = new BehaviorState();
        public ExerciseExecutionState CurrentExercise = new ExerciseExecutionState();
        public FormEvaluationResult LastFormEvaluation = new FormEvaluationResult();
        public FormIssueType LastReportedIssue = FormIssueType.None;
        public bool IsPhysicalFailureActive;

        public bool HasActiveCommand =>
            ActiveCommandContext != null &&
            ActiveCommandContext.Status != CommandExecutionStatus.None &&
            ActiveCommandContext.Status != CommandExecutionStatus.Completed &&
            ActiveCommandContext.Status != CommandExecutionStatus.Failed &&
            ActiveCommandContext.Status != CommandExecutionStatus.Interrupted;

        public bool IsBusy =>
            CurrentPosture == AvatarPostureState.Moving ||
            CurrentPosture == AvatarPostureState.Aligning ||
            CurrentPosture == AvatarPostureState.Exercising ||
            CurrentPosture == AvatarPostureState.HoldingPose ||
            CurrentPosture == AvatarPostureState.WardrobeChange ||
            CurrentPosture == AvatarPostureState.Negotiating;
    }
}
