using System;
using UnityEngine;
using TrainerAvatarSimulator.Core;

namespace TrainerAvatarSimulator.Validation
{
    [Serializable]
    public class FormEvaluationResult
    {
        [Range(0f, 1f)] public float OverallScore = 1f;
        [Range(0f, 1f)] public float PostureScore = 1f;
        [Range(0f, 1f)] public float TimingScore = 1f;
        [Range(0f, 1f)] public float ContactScore = 1f;
        public FormIssueType PrimaryIssue = FormIssueType.None;

        public bool Passed => OverallScore >= 0.85f;
        public bool PartialPass => OverallScore >= 0.6f && OverallScore < 0.85f;
        public bool Failed => OverallScore < 0.6f;
        public float FailureSeverity => 1f - OverallScore;
    }
}
