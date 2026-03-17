using System;
using UnityEngine;
using TrainerAvatarSimulator.Fatigue;

namespace TrainerAvatarSimulator.Behavior
{
    [Serializable]
    public class BehaviorState
    {
        [Range(0f, 1f)] public float Motivation = 0.5f;
        [Range(0f, 1f)] public float Compliance = 0.7f;
        [Range(0f, 1f)] public float Confidence = 0.5f;
        [Range(0f, 1f)] public float PerceivedLeniency = 0.5f;
        [Range(0f, 1f)] public float SelfDiscipline = 0.7f;
        [Range(0f, 1f)] public float SelfCriticism = 0.6f;
        [Range(0f, 1f)] public float FairnessPerception = 0.5f;
        [Range(0f, 1f)] public float Humility = 0.7f;

        public bool ShouldRequestRelief(FatigueState fatigue)
        {
            return fatigue.IsOverloaded() && Compliance > 0.3f;
        }

        public bool ShouldRequestMoreIntensity(FatigueState fatigue)
        {
            return !fatigue.IsOverloaded(0.45f) && PerceivedLeniency > 0.65f && Motivation > 0.6f;
        }

        public bool ShouldConfessPoorForm(float formScore)
        {
            return formScore < 0.65f && SelfCriticism > 0.55f;
        }

        public bool ShouldPleadForMercy(FatigueState fatigue)
        {
            return Humility > 0.5f && fatigue.IsOverloaded(0.75f);
        }
    }
}
