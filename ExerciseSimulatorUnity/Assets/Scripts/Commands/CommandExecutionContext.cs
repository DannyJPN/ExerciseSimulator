using System;
using TrainerAvatarSimulator.Core;

namespace TrainerAvatarSimulator.Commands
{
    [Serializable]
    public class CommandExecutionContext
    {
        public string CommandId = string.Empty;
        public string DisplayName = string.Empty;
        public CommandType CommandType = CommandType.Exercise;
        public CommandSource Source = CommandSource.Unknown;
        public string LinkedExerciseId = string.Empty;
        public string WardrobeTargetId = string.Empty;
        public int Priority = 0;
        public bool AllowInterrupt = true;
        public CommandExecutionStatus Status = CommandExecutionStatus.None;
        public string FailureReason = string.Empty;
        public string InterruptedByCommandId = string.Empty;
        public string StartedAtIsoUtc = string.Empty;

        public static CommandExecutionContext Create(CommandDefinition definition, CommandSource source)
        {
            var context = new CommandExecutionContext
            {
                CommandId = definition.CommandId,
                DisplayName = definition.DisplayName,
                CommandType = definition.CommandType,
                Source = source,
                LinkedExerciseId = definition.LinkedExercise != null ? definition.LinkedExercise.ExerciseId : string.Empty,
                WardrobeTargetId = definition.WardrobeTargetId,
                Priority = definition.DefaultPriority,
                AllowInterrupt = definition.AllowInterrupt
            };

            context.MarkRunning();
            return context;
        }

        public void MarkRunning()
        {
            Status = CommandExecutionStatus.Running;
            StartedAtIsoUtc = DateTime.UtcNow.ToString("O");
            FailureReason = string.Empty;
            InterruptedByCommandId = string.Empty;
        }

        public void MarkInterrupted(string nextCommandId)
        {
            Status = CommandExecutionStatus.Interrupted;
            InterruptedByCommandId = nextCommandId ?? string.Empty;
        }

        public void MarkCompleted()
        {
            Status = CommandExecutionStatus.Completed;
        }

        public void MarkFailed(string reason)
        {
            Status = CommandExecutionStatus.Failed;
            FailureReason = reason ?? string.Empty;
        }
    }
}
