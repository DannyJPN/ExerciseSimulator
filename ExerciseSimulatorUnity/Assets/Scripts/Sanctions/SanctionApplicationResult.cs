using System;
using TrainerAvatarSimulator.Core;

namespace TrainerAvatarSimulator.Sanctions
{
    [Serializable]
    public class SanctionApplicationResult
    {
        public bool WasApplied;
        public bool CountAttempt = true;
        public string BlockReason = string.Empty;
        public SanctionType AppliedSanctionType = SanctionType.None;
        public ClothingSlot RemovedClothingSlot = ClothingSlot.Top;
        public int UpdatedRepetitions;
        public float UpdatedDurationSeconds;
        public float UpdatedRestSeconds;

        public static SanctionApplicationResult Blocked(string reason)
        {
            return new SanctionApplicationResult
            {
                WasApplied = false,
                BlockReason = reason ?? string.Empty
            };
        }
    }
}
