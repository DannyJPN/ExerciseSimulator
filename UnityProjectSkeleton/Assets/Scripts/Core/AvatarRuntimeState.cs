using System;
using TrainerAvatarSimulator.Commands;
using TrainerAvatarSimulator.Behavior;
using TrainerAvatarSimulator.Fatigue;
using TrainerAvatarSimulator.Wardrobe;

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
