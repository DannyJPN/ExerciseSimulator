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
        [Range(0f, 1f)] public float PainTolerance = 0.5f;
        [Range(0f, 1f)] public float Ambition = 0.55f;
        public int PerfectExecutionCount;
    }
}
