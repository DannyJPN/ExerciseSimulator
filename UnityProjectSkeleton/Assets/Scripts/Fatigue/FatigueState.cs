using System;
using UnityEngine;

namespace TrainerAvatarSimulator.Fatigue
{
    [Serializable]
    public class FatigueState
    {
        [Range(0f, 1f)] public float GlobalFatigue;
        [Range(0f, 1f)] public float CardioFatigue;
        [Range(0f, 1f)] public float LegsFatigue;
        [Range(0f, 1f)] public float ArmsFatigue;
        [Range(0f, 1f)] public float CoreFatigue;
        [Range(0f, 1f)] public float Discomfort;

        public float MaxLocalFatigue => Mathf.Max(LegsFatigue, ArmsFatigue, CoreFatigue);

        public bool IsOverloaded(float threshold = 0.85f)
        {
            return GlobalFatigue >= threshold || Discomfort >= threshold || MaxLocalFatigue >= threshold;
        }
    }
}
