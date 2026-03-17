using System;
using TrainerAvatarSimulator.Core;

namespace TrainerAvatarSimulator.Sanctions
{
    public static class SanctionEngine
    {
        public static SanctionApplicationResult Apply(
            SanctionDefinition sanction,
            AvatarRuntimeState runtimeState,
            SanctionContext context)
        {
            if (sanction == null)
            {
                return SanctionApplicationResult.Blocked("Missing sanction definition.");
            }

            if (runtimeState == null || context == null || context.ExerciseState == null || context.FormResult == null)
            {
                return SanctionApplicationResult.Blocked("Missing sanction context.");
            }

            if (!CanApply(sanction, runtimeState, context, out var blockReason))
            {
                return SanctionApplicationResult.Blocked(blockReason);
            }

            var result = new SanctionApplicationResult
            {
                WasApplied = true,
                AppliedSanctionType = sanction.SanctionType,
                CountAttempt = sanction.SanctionType != SanctionType.DoNotCountRep,
                UpdatedDurationSeconds = context.ExerciseState.TargetDurationSeconds,
                UpdatedRepetitions = context.ExerciseState.TargetRepetitions,
                UpdatedRestSeconds = context.ExerciseState.PendingRestSeconds
            };

            switch (sanction.SanctionType)
            {
                case SanctionType.DoNotCountRep:
                    context.ExerciseState.CountCurrentResult = false;
                    result.CountAttempt = false;
                    break;

                case SanctionType.RepeatExercise:
                    context.ExerciseState.CompletedRepetitions = 0;
                    context.ExerciseState.CountCurrentResult = false;
                    result.CountAttempt = false;
                    break;

                case SanctionType.ExtendHold:
                    context.ExerciseState.TargetDurationSeconds += Math.Max(1f, context.ExerciseState.TargetDurationSeconds * sanction.Intensity);
                    result.UpdatedDurationSeconds = context.ExerciseState.TargetDurationSeconds;
                    break;

                case SanctionType.ExtraRepetitions:
                    context.ExerciseState.TargetRepetitions += Math.Max(1, (int)Math.Ceiling(Math.Max(1f, context.ExerciseState.TargetRepetitions) * sanction.Intensity));
                    result.UpdatedRepetitions = context.ExerciseState.TargetRepetitions;
                    break;

                case SanctionType.ShortenRest:
                    context.ExerciseState.PendingRestSeconds = Math.Max(0f, context.ExerciseState.PendingRestSeconds - context.ExerciseState.PendingRestSeconds * sanction.Intensity);
                    result.UpdatedRestSeconds = context.ExerciseState.PendingRestSeconds;
                    break;

                case SanctionType.ExtraSet:
                    if (context.ExerciseState.ExerciseKind == ExerciseKind.TimedHold)
                    {
                        context.ExerciseState.TargetDurationSeconds += Math.Max(1f, context.ExerciseState.TargetDurationSeconds);
                        result.UpdatedDurationSeconds = context.ExerciseState.TargetDurationSeconds;
                    }
                    else
                    {
                        context.ExerciseState.TargetRepetitions += Math.Max(1, context.ExerciseState.TargetRepetitions);
                        result.UpdatedRepetitions = context.ExerciseState.TargetRepetitions;
                    }
                    break;

                case SanctionType.StricterVariation:
                    context.ExerciseState.ActiveModifierId = sanction.SanctionId;
                    break;

                case SanctionType.LessClothing:
                    var slot = ResolveClothingSlot(runtimeState, sanction.PreferredClothingSlot);
                    if (!runtimeState.Wardrobe.TryRemove(slot))
                    {
                        return SanctionApplicationResult.Blocked("No removable clothing item is currently worn.");
                    }

                    result.RemovedClothingSlot = slot;
                    break;
            }

            return result;
        }

        private static bool CanApply(
            SanctionDefinition sanction,
            AvatarRuntimeState runtimeState,
            SanctionContext context,
            out string blockReason)
        {
            blockReason = string.Empty;

            if (sanction.BlockOnPhysicalFailure && context.IsPhysicalFailure)
            {
                blockReason = "Sanction blocked by physical failure.";
                return false;
            }

            if (context.FormResult.FailureSeverity < sanction.MinFormFailure)
            {
                blockReason = "Form failure is below sanction threshold.";
                return false;
            }

            if (sanction.SanctionType == SanctionType.LessClothing && !runtimeState.Wardrobe.HasRemovableWornItem())
            {
                blockReason = "No removable clothing item is available.";
                return false;
            }

            return true;
        }

        private static ClothingSlot ResolveClothingSlot(AvatarRuntimeState runtimeState, ClothingSlot preferredSlot)
        {
            if (runtimeState.Wardrobe.CanRemove(preferredSlot))
            {
                return preferredSlot;
            }

            return runtimeState.Wardrobe.GetNextRemovalCandidate();
        }
    }
}
