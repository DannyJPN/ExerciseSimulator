using UnityEngine;
using TrainerAvatarSimulator.Commands;

namespace TrainerAvatarSimulator.Core
{
    public class AvatarStateMachine : MonoBehaviour
    {
        [SerializeField] private AvatarRuntimeState runtimeState = new AvatarRuntimeState();
        [SerializeField] private AvatarRuntimeEvents runtimeEvents;

        public AvatarRuntimeState RuntimeState => runtimeState;
        public AvatarPostureState CurrentState => runtimeState.CurrentPosture;

        private void Awake()
        {
            if (runtimeEvents == null)
            {
                runtimeEvents = GetComponent<AvatarRuntimeEvents>();
            }
        }

        public bool TrySetState(AvatarPostureState nextState)
        {
            var previousState = runtimeState.CurrentPosture;

            if (previousState == nextState)
            {
                return true;
            }

            if (!IsTransitionAllowed(previousState, nextState))
            {
                return false;
            }

            runtimeState.CurrentPosture = nextState;
            runtimeEvents?.RaiseStateChanged(previousState, nextState);
            return true;
        }

        public void AssignCommandContext(CommandExecutionContext commandContext)
        {
            runtimeState.ActiveCommandContext = commandContext ?? new CommandExecutionContext();
            runtimeState.ActiveCommandId = runtimeState.ActiveCommandContext.CommandId;
            runtimeState.ActiveExerciseId = runtimeState.ActiveCommandContext.LinkedExerciseId;
        }

        public void ClearActiveIntent()
        {
            runtimeState.ActiveCommandId = string.Empty;
            runtimeState.ActiveExerciseId = string.Empty;
            runtimeState.ActiveCommandContext = new CommandExecutionContext();
            runtimeState.CurrentExercise = new Exercises.ExerciseExecutionState();
        }

        public bool IsTransitionAllowed(AvatarPostureState fromState, AvatarPostureState toState)
        {
            switch (fromState)
            {
                case AvatarPostureState.Idle:
                    return toState == AvatarPostureState.Standing ||
                           toState == AvatarPostureState.Sitting ||
                           toState == AvatarPostureState.Kneeling ||
                           toState == AvatarPostureState.Lying ||
                           toState == AvatarPostureState.Moving ||
                           toState == AvatarPostureState.WardrobeChange;
                case AvatarPostureState.Standing:
                    return toState == AvatarPostureState.Moving ||
                           toState == AvatarPostureState.Sitting ||
                           toState == AvatarPostureState.Kneeling ||
                           toState == AvatarPostureState.Lying ||
                           toState == AvatarPostureState.WardrobeChange ||
                           toState == AvatarPostureState.Negotiating;
                case AvatarPostureState.Sitting:
                    return toState == AvatarPostureState.Standing ||
                           toState == AvatarPostureState.Kneeling ||
                           toState == AvatarPostureState.WardrobeChange ||
                           toState == AvatarPostureState.Negotiating;
                case AvatarPostureState.Kneeling:
                    return toState == AvatarPostureState.Standing ||
                           toState == AvatarPostureState.Sitting ||
                           toState == AvatarPostureState.Lying ||
                           toState == AvatarPostureState.Negotiating;
                case AvatarPostureState.Lying:
                    return toState == AvatarPostureState.Standing ||
                           toState == AvatarPostureState.Sitting ||
                           toState == AvatarPostureState.Recovering;
                case AvatarPostureState.Moving:
                    return toState == AvatarPostureState.Aligning ||
                           toState == AvatarPostureState.Standing ||
                           toState == AvatarPostureState.WardrobeChange;
                case AvatarPostureState.Aligning:
                    return toState == AvatarPostureState.Exercising ||
                           toState == AvatarPostureState.HoldingPose ||
                           toState == AvatarPostureState.Standing;
                case AvatarPostureState.Exercising:
                case AvatarPostureState.HoldingPose:
                    return toState == AvatarPostureState.Standing ||
                           toState == AvatarPostureState.Recovering ||
                           toState == AvatarPostureState.Negotiating ||
                           toState == AvatarPostureState.Sanctioned;
                case AvatarPostureState.WardrobeChange:
                    return toState == AvatarPostureState.Sitting ||
                           toState == AvatarPostureState.Standing;
                case AvatarPostureState.Recovering:
                    return toState == AvatarPostureState.Standing ||
                           toState == AvatarPostureState.Sitting ||
                           toState == AvatarPostureState.Idle;
                case AvatarPostureState.Negotiating:
                    return toState == AvatarPostureState.Standing ||
                           toState == AvatarPostureState.Sanctioned ||
                           toState == AvatarPostureState.HoldingPose;
                case AvatarPostureState.Sanctioned:
                    return toState == AvatarPostureState.Standing ||
                           toState == AvatarPostureState.HoldingPose ||
                           toState == AvatarPostureState.Exercising ||
                           toState == AvatarPostureState.WardrobeChange;
                default:
                    return false;
            }
        }
    }
}
