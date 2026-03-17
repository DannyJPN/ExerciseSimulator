using TrainerAvatarSimulator.Core;
using TrainerAvatarSimulator.Exercises;
using TrainerAvatarSimulator.Fatigue;
using TrainerAvatarSimulator.Sanctions;
using TrainerAvatarSimulator.Validation;
using TrainerAvatarSimulator.Wardrobe;

namespace TrainerAvatarSimulator.Behavior
{
    public static class BehaviorDecisionEngine
    {
        public static BehaviorDecision EvaluateReliefRequest(
            BehaviorState behavior,
            FatigueState fatigue,
            WardrobeState wardrobe)
        {
            if (behavior == null || fatigue == null)
            {
                return BehaviorDecision.None();
            }

            if (fatigue.HasPhysicalFailureRisk())
            {
                return new BehaviorDecision
                {
                    Kind = BehaviorDecisionKind.ReportPhysicalFailure,
                    MessageTag = "physical_failure",
                    Reason = fatigue.GetDominantPainSegment().ToString(),
                    Severity = fatigue.MaxPain,
                    RequiresTrainerApproval = false,
                    BlocksCurrentExercise = true
                };
            }

            if (wardrobe != null && wardrobe.HasRemovableWornItem() && fatigue.Discomfort >= 0.55f)
            {
                return new BehaviorDecision
                {
                    Kind = BehaviorDecisionKind.RequestClothingAdjustment,
                    MessageTag = "adjust_clothing",
                    Reason = "wardrobe_discomfort",
                    SuggestedClothingSlot = wardrobe.GetNextRemovalCandidate(),
                    Severity = fatigue.Discomfort,
                    RequiresTrainerApproval = true
                };
            }

            var reliefPressure = fatigue.MaxPain * (1f - behavior.PainTolerance) + fatigue.MaxLocalFatigue * behavior.Compliance;
            if (reliefPressure >= 0.58f || fatigue.IsOverloaded(0.78f))
            {
                return new BehaviorDecision
                {
                    Kind = BehaviorDecisionKind.RequestRelief,
                    MessageTag = "request_relief",
                    Reason = fatigue.IsInPain() ? "pain" : "fatigue",
                    Severity = reliefPressure,
                    RequiresTrainerApproval = true
                };
            }

            return BehaviorDecision.None();
        }

        public static BehaviorDecision EvaluateIntensityRequest(
            BehaviorState behavior,
            FatigueState fatigue,
            bool correctingPreviousFailure)
        {
            if (behavior == null || fatigue == null)
            {
                return BehaviorDecision.None();
            }

            var readiness = behavior.Motivation * 0.4f +
                behavior.Ambition * 0.35f +
                behavior.PerceivedLeniency * 0.25f;

            readiness += correctingPreviousFailure ? 0.1f : 0f;

            if (behavior.PerfectExecutionCount < 1 || fatigue.IsOverloaded(0.45f) || fatigue.IsInPain(0.35f))
            {
                return BehaviorDecision.None();
            }

            if (readiness < 0.6f)
            {
                return BehaviorDecision.None();
            }

            var sanctionType = behavior.PerfectExecutionCount >= 4
                ? SanctionType.StricterVariation
                : SanctionType.ExtraRepetitions;

            return new BehaviorDecision
            {
                Kind = BehaviorDecisionKind.RequestMoreIntensity,
                MessageTag = correctingPreviousFailure ? "request_intensity_redemption" : "request_intensity",
                Reason = correctingPreviousFailure ? "redemption" : "underloaded",
                SuggestedSanctionType = sanctionType,
                Severity = readiness,
                RequiresTrainerApproval = true
            };
        }

        public static BehaviorDecision EvaluateSelfAssessment(
            BehaviorState behavior,
            FormEvaluationResult evaluation,
            ExerciseExecutionState exerciseState)
        {
            if (behavior == null || evaluation == null)
            {
                return BehaviorDecision.None();
            }

            if (evaluation.Passed)
            {
                behavior.PerfectExecutionCount++;
                return BehaviorDecision.None();
            }

            behavior.PerfectExecutionCount = 0;
            var willingnessToConfess = behavior.SelfCriticism * 0.65f + behavior.SelfDiscipline * 0.35f;
            if (willingnessToConfess < 0.5f)
            {
                return BehaviorDecision.None();
            }

            var suggestedSanction = evaluation.Failed
                ? SanctionType.RepeatExercise
                : SanctionType.DoNotCountRep;

            if (exerciseState != null && exerciseState.ExerciseKind == ExerciseKind.TimedHold)
            {
                suggestedSanction = SanctionType.ExtendHold;
            }

            return new BehaviorDecision
            {
                Kind = BehaviorDecisionKind.ConfessPoorForm,
                MessageTag = "confess_poor_form",
                Reason = evaluation.PrimaryIssue.ToString(),
                SuggestedSanctionType = suggestedSanction,
                Severity = 1f - evaluation.OverallScore,
                RequiresTrainerApproval = true
            };
        }

        public static BehaviorDecision EvaluateMercyPlea(
            BehaviorState behavior,
            FatigueState fatigue,
            SanctionDefinition sanction)
        {
            if (behavior == null || fatigue == null || sanction == null || !sanction.AllowNegotiation)
            {
                return BehaviorDecision.None();
            }

            var pleaPressure = fatigue.MaxPain * 0.45f +
                fatigue.MaxLocalFatigue * 0.3f +
                behavior.Humility * 0.15f +
                (1f - behavior.FairnessPerception) * 0.1f;

            if (pleaPressure < 0.55f)
            {
                return BehaviorDecision.None();
            }

            return new BehaviorDecision
            {
                Kind = BehaviorDecisionKind.PleadForMercy,
                MessageTag = "plead_for_mercy",
                Reason = sanction.SanctionType.ToString(),
                SuggestedSanctionType = GetReducedSanctionType(sanction.SanctionType),
                Severity = pleaPressure,
                RequiresTrainerApproval = true
            };
        }

        private static SanctionType GetReducedSanctionType(SanctionType sanctionType)
        {
            switch (sanctionType)
            {
                case SanctionType.ExtraSet:
                    return SanctionType.ExtraRepetitions;
                case SanctionType.ExtraRepetitions:
                case SanctionType.ExtendHold:
                case SanctionType.StricterVariation:
                    return SanctionType.DoNotCountRep;
                case SanctionType.LessClothing:
                    return SanctionType.DoNotCountRep;
                default:
                    return sanctionType;
            }
        }
    }
}
