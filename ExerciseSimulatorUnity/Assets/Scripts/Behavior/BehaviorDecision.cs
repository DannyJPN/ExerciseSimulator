using System;
using TrainerAvatarSimulator.Core;

namespace TrainerAvatarSimulator.Behavior
{
    [Serializable]
    public class BehaviorDecision
    {
        public BehaviorDecisionKind Kind = BehaviorDecisionKind.None;
        public string MessageTag = string.Empty;
        public string Reason = string.Empty;
        public SanctionType SuggestedSanctionType = SanctionType.None;
        public ClothingSlot SuggestedClothingSlot = ClothingSlot.Top;
        public float Severity;
        public bool RequiresTrainerApproval;
        public bool BlocksCurrentExercise;

        public bool HasDecision => Kind != BehaviorDecisionKind.None;

        public static BehaviorDecision None()
        {
            return new BehaviorDecision();
        }
    }
}
