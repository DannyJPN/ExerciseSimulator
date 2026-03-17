using System;
using UnityEngine;
using TrainerAvatarSimulator.Core;

namespace TrainerAvatarSimulator.Fatigue
{
    [Serializable]
    public class FatigueLoadProfile
    {
        [SerializeField] private ExerciseKind exerciseKind = ExerciseKind.Repetition;
        [SerializeField] private BodySegment primarySegment = BodySegment.None;
        [SerializeField, Range(0f, 1f)] private float globalFatigue = 0.08f;
        [SerializeField, Range(0f, 1f)] private float cardioFatigue = 0.05f;
        [SerializeField, Range(0f, 1f)] private float legsFatigue = 0f;
        [SerializeField, Range(0f, 1f)] private float armsFatigue = 0f;
        [SerializeField, Range(0f, 1f)] private float coreFatigue = 0f;
        [SerializeField, Range(0f, 1f)] private float discomfort = 0.03f;
        [SerializeField, Range(0f, 1f)] private float globalPain = 0f;
        [SerializeField, Range(0f, 1f)] private float legsPain = 0f;
        [SerializeField, Range(0f, 1f)] private float armsPain = 0f;
        [SerializeField, Range(0f, 1f)] private float corePain = 0f;
        [SerializeField, Range(0f, 1f)] private float fatigueDrivenPain = 0.02f;

        public ExerciseKind ExerciseKind => exerciseKind;
        public BodySegment PrimarySegment => primarySegment;
        public float GlobalFatigue => globalFatigue;
        public float CardioFatigue => cardioFatigue;
        public float LegsFatigue => legsFatigue;
        public float ArmsFatigue => armsFatigue;
        public float CoreFatigue => coreFatigue;
        public float Discomfort => discomfort;
        public float GlobalPain => globalPain;
        public float LegsPain => legsPain;
        public float ArmsPain => armsPain;
        public float CorePain => corePain;
        public float FatigueDrivenPain => fatigueDrivenPain;
    }
}
