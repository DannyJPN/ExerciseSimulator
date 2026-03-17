using System.Collections.Generic;
using TrainerAvatarSimulator.Commands;
using TrainerAvatarSimulator.Exercises;
using TrainerAvatarSimulator.Sanctions;

namespace TrainerAvatarSimulator.Validation
{
    public static class ContentDefinitionValidator
    {
        public static IReadOnlyList<string> ValidateCommands(IEnumerable<CommandDefinition> commands)
        {
            var messages = new List<string>();
            var ids = new HashSet<string>();

            if (commands == null)
            {
                messages.Add("Command collection is missing.");
                return messages;
            }

            foreach (var command in commands)
            {
                if (command == null)
                {
                    messages.Add("Command collection contains a null entry.");
                    continue;
                }

                if (string.IsNullOrWhiteSpace(command.CommandId))
                {
                    messages.Add("Command definition has an empty id.");
                }
                else if (!ids.Add(command.CommandId))
                {
                    messages.Add($"Duplicate command id: {command.CommandId}");
                }

                if (command.CommandType == Core.CommandType.Exercise && command.LinkedExercise == null)
                {
                    messages.Add($"Exercise command {command.CommandId} is missing linked exercise.");
                }
            }

            return messages;
        }

        public static IReadOnlyList<string> ValidateExercises(IEnumerable<ExerciseDefinition> exercises)
        {
            var messages = new List<string>();
            var ids = new HashSet<string>();

            if (exercises == null)
            {
                messages.Add("Exercise collection is missing.");
                return messages;
            }

            foreach (var exercise in exercises)
            {
                if (exercise == null)
                {
                    messages.Add("Exercise collection contains a null entry.");
                    continue;
                }

                if (string.IsNullOrWhiteSpace(exercise.ExerciseId))
                {
                    messages.Add("Exercise definition has an empty id.");
                }
                else if (!ids.Add(exercise.ExerciseId))
                {
                    messages.Add($"Duplicate exercise id: {exercise.ExerciseId}");
                }

                if (exercise.ExerciseKind == Core.ExerciseKind.TimedHold && exercise.DefaultDurationSeconds <= 0f)
                {
                    messages.Add($"Timed exercise {exercise.ExerciseId} must define positive duration.");
                }

                if (exercise.ExerciseKind == Core.ExerciseKind.Repetition && exercise.DefaultRepetitions <= 0)
                {
                    messages.Add($"Repetition exercise {exercise.ExerciseId} must define positive repetitions.");
                }
            }

            return messages;
        }

        public static IReadOnlyList<string> ValidateSanctions(IEnumerable<SanctionDefinition> sanctions)
        {
            var messages = new List<string>();
            var ids = new HashSet<string>();

            if (sanctions == null)
            {
                messages.Add("Sanction collection is missing.");
                return messages;
            }

            foreach (var sanction in sanctions)
            {
                if (sanction == null)
                {
                    messages.Add("Sanction collection contains a null entry.");
                    continue;
                }

                if (string.IsNullOrWhiteSpace(sanction.SanctionId))
                {
                    messages.Add("Sanction definition has an empty id.");
                }
                else if (!ids.Add(sanction.SanctionId))
                {
                    messages.Add($"Duplicate sanction id: {sanction.SanctionId}");
                }

                if (sanction.SanctionType == Core.SanctionType.None)
                {
                    messages.Add($"Sanction {sanction.SanctionId} uses None type.");
                }
            }

            return messages;
        }
    }
