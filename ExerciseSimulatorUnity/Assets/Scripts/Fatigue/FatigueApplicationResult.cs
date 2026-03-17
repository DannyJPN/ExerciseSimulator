using System;
using TrainerAvatarSimulator.Core;

namespace TrainerAvatarSimulator.Fatigue
{
    [Serializable]
    public class FatigueApplicationResult
    {
        public static readonly FatigueApplicationResult Empty = new FatigueApplicationResult();

        public float GlobalFatigue;
        public float MaxLocalFatigue;
        public float MaxPain;
        public BodySegment DominantPainSegment = BodySegment.None;
        public bool ShouldRequestRelief;
        public bool ShouldTriggerPhysicalFailure;
    }
}
