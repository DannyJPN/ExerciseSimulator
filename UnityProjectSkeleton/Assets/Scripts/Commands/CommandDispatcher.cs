using UnityEngine;
using TrainerAvatarSimulator.Core;

namespace TrainerAvatarSimulator.Commands
{
    public class CommandDispatcher : MonoBehaviour
    {
        [SerializeField] private AvatarStateMachine avatarStateMachine;
        [SerializeField] private AvatarRuntimeEvents runtimeEvents;

        private void Awake()
        {
            if (avatarStateMachine == null)
            {
                avatarStateMachine = GetComponent<AvatarStateMachine>();
            }

            if (runtimeEvents == null)
            {
                runtimeEvents = GetComponent<AvatarRuntimeEvents>();
            }
        }

        public void Configure(AvatarStateMachine stateMachine, AvatarRuntimeEvents events)
        {
            avatarStateMachine = stateMachine;
            runtimeEvents = events;
        }

        public bool TryDispatch(CommandDefinition command)
        {
            return TryDispatch(command, CommandSource.DebugUI);
        }

        public bool TryDispatch(CommandDefinition command, CommandSource source)
        {
            if (avatarStateMachine == null || command == null)
            {
                return false;
            }

            var runtimeState = avatarStateMachine.RuntimeState;
            var previousContext = runtimeState.ActiveCommandContext;

            if (runtimeState.HasActiveCommand)
            {
                if (!command.AllowInterrupt)
                {
                    return false;
                }

                InterruptActiveCommand(previousContext, command.CommandId);
            }

            var nextContext = CommandExecutionContext.Create(command, source);
            avatarStateMachine.AssignCommandContext(nextContext);
            runtimeEvents?.RaiseCommandStarted(nextContext);

            if (command.LinkedExercise != null)
            {
                runtimeEvents?.RaiseExerciseAssigned(command.LinkedExercise.ExerciseId);
                return TryEnterState(nextContext, AvatarPostureState.Moving, "Could not enter moving state.");
            }

            if (command.CommandType == CommandType.AtomicPosture)
            {
                return TryEnterState(nextContext, command.TargetPosture, "Could not enter target posture state.");
            }

            if (command.CommandType == CommandType.Wardrobe)
            {
                runtimeEvents?.RaiseWardrobeCommandStarted(command.CommandId, command.WardrobeTargetId);
                return TryEnterState(nextContext, AvatarPostureState.WardrobeChange, "Could not enter wardrobe state.");
            }

            if (command.CommandType == CommandType.Discipline)
            {
                return TryEnterState(nextContext, AvatarPostureState.Sanctioned, "Could not enter sanctioned state.");
            }

            nextContext.MarkFailed("Unsupported command type.");
            avatarStateMachine.ClearActiveIntent();
            return false;
        }

        private void InterruptActiveCommand(CommandExecutionContext activeContext, string nextCommandId)
        {
            if (activeContext == null || activeContext.Status != CommandExecutionStatus.Running)
            {
                return;
            }

            activeContext.MarkInterrupted(nextCommandId);
            runtimeEvents?.RaiseCommandInterrupted(activeContext);
        }

        private bool TryEnterState(CommandExecutionContext context, AvatarPostureState targetState, string failureReason)
        {
            if (avatarStateMachine.TrySetState(targetState))
            {
                return true;
            }

            context.MarkFailed(failureReason);
            avatarStateMachine.ClearActiveIntent();
            return false;
        }
    }
}
