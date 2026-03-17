using System;
using UnityEngine;
using TrainerAvatarSimulator.Commands;

namespace TrainerAvatarSimulator.Core
{
    public class AvatarRuntimeEvents : MonoBehaviour
    {
        public event Action<CommandExecutionContext> CommandStarted;
        public event Action<CommandExecutionContext> CommandInterrupted;
        public event Action<AvatarPostureState, AvatarPostureState> StateChanged;
        public event Action<string> ExerciseAssigned;
        public event Action<string, string> WardrobeCommandStarted;

        public void RaiseCommandStarted(CommandExecutionContext context)
        {
            CommandStarted?.Invoke(context);
        }

        public void RaiseCommandInterrupted(CommandExecutionContext context)
        {
            CommandInterrupted?.Invoke(context);
        }

        public void RaiseStateChanged(AvatarPostureState previousState, AvatarPostureState nextState)
        {
            StateChanged?.Invoke(previousState, nextState);
        }

        public void RaiseExerciseAssigned(string exerciseId)
        {
            ExerciseAssigned?.Invoke(exerciseId);
        }

        public void RaiseWardrobeCommandStarted(string commandId, string wardrobeTargetId)
        {
            WardrobeCommandStarted?.Invoke(commandId, wardrobeTargetId);
        }
    }
}
