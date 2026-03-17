using System;
using UnityEngine;

namespace TrainerAvatarSimulator.Validation
{
    [Serializable]
    public class FormEvaluationResult
    {
        [Range(0f, 1f)] public float OverallScore = 1f;
        [Range(0f, 1f)] public float PostureScore = 1f;
        [Range(0f, 1f)] public float TimingScore = 1f;
        [Range(0f, 1f)] public float ContactScore = 1f;

        public bool Passed => OverallScore >= 0.85f;
        public bool PartialPass => OverallScore >= 0.6f && OverallScore < 0.85f;
        public bool Failed => OverallScore < 0.6f;
    }
}
