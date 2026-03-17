using System;
using UnityEngine;
using TrainerAvatarSimulator.Core;

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
        [Range(0f, 1f)] public float GlobalPain;
        [Range(0f, 1f)] public float LegsPain;
        [Range(0f, 1f)] public float ArmsPain;
        [Range(0f, 1f)] public float CorePain;

        public float MaxLocalFatigue => Mathf.Max(LegsFatigue, ArmsFatigue, CoreFatigue);
        public float MaxLocalPain => Mathf.Max(LegsPain, ArmsPain, CorePain);
        public float MaxPain => Mathf.Max(GlobalPain, MaxLocalPain);

        public bool IsOverloaded(float threshold = 0.85f)
        {
            return GlobalFatigue >= threshold || Discomfort >= threshold || MaxLocalFatigue >= threshold;
        }

        public bool IsInPain(float threshold = 0.65f)
        {
            return MaxPain >= threshold || Discomfort >= threshold;
        }

        public bool HasPhysicalFailureRisk(float threshold = 0.95f)
        {
            return GlobalFatigue >= threshold ||
                MaxLocalFatigue >= threshold ||
                MaxPain >= threshold;
        }

        public float GetFatigue(BodySegment segment)
        {
            switch (segment)
            {
                case BodySegment.Cardio:
                    return CardioFatigue;
                case BodySegment.Legs:
                    return LegsFatigue;
                case BodySegment.Arms:
                    return ArmsFatigue;
                case BodySegment.Core:
                    return CoreFatigue;
                default:
                    return GlobalFatigue;
            }
        }

        public float GetPain(BodySegment segment)
        {
            switch (segment)
            {
                case BodySegment.Legs:
                    return LegsPain;
                case BodySegment.Arms:
                    return ArmsPain;
                case BodySegment.Core:
                    return CorePain;
                default:
                    return GlobalPain;
            }
        }

        public BodySegment GetDominantPainSegment()
        {
            var maxPain = MaxPain;
            if (maxPain <= 0f)
            {
                return BodySegment.None;
            }

            if (LegsPain >= ArmsPain && LegsPain >= CorePain && LegsPain >= GlobalPain)
            {
                return BodySegment.Legs;
            }

            if (ArmsPain >= LegsPain && ArmsPain >= CorePain && ArmsPain >= GlobalPain)
            {
                return BodySegment.Arms;
            }

            if (CorePain >= LegsPain && CorePain >= ArmsPain && CorePain >= GlobalPain)
            {
                return BodySegment.Core;
            }

            return BodySegment.Cardio;
        }

        public FatigueApplicationResult ApplyLoad(
            FatigueLoadProfile profile,
            float intensityScale = 1f,
            float painSensitivity = 1f,
            float clothingDiscomfort = 0f)
        {
            if (profile == null)
            {
                return FatigueApplicationResult.Empty;
            }

            var result = new FatigueApplicationResult();
            var scaledIntensity = Mathf.Max(0f, intensityScale);
            var painScale = Mathf.Clamp(painSensitivity, 0.25f, 2f);

            GlobalFatigue = Clamp01(GlobalFatigue + profile.GlobalFatigue * scaledIntensity);
            CardioFatigue = Clamp01(CardioFatigue + profile.CardioFatigue * scaledIntensity);
            LegsFatigue = Clamp01(LegsFatigue + profile.LegsFatigue * scaledIntensity);
            ArmsFatigue = Clamp01(ArmsFatigue + profile.ArmsFatigue * scaledIntensity);
            CoreFatigue = Clamp01(CoreFatigue + profile.CoreFatigue * scaledIntensity);
            Discomfort = Clamp01(Discomfort + profile.Discomfort * scaledIntensity + clothingDiscomfort);

            GlobalPain = Clamp01(GlobalPain + (profile.GlobalPain + profile.FatigueDrivenPain * GlobalFatigue) * painScale);
            LegsPain = Clamp01(LegsPain + (profile.LegsPain + profile.FatigueDrivenPain * LegsFatigue) * painScale);
            ArmsPain = Clamp01(ArmsPain + (profile.ArmsPain + profile.FatigueDrivenPain * ArmsFatigue) * painScale);
            CorePain = Clamp01(CorePain + (profile.CorePain + profile.FatigueDrivenPain * CoreFatigue) * painScale);

            result.GlobalFatigue = GlobalFatigue;
            result.MaxLocalFatigue = MaxLocalFatigue;
            result.MaxPain = MaxPain;
            result.DominantPainSegment = GetDominantPainSegment();
            result.ShouldRequestRelief = IsOverloaded(0.75f) || IsInPain(0.7f);
            result.ShouldTriggerPhysicalFailure = HasPhysicalFailureRisk();
            return result;
        }

        public void Recover(
            float fatigueRecovery = 0.1f,
            float painRecovery = 0.08f,
            float discomfortRecovery = 0.12f)
        {
            GlobalFatigue = Clamp01(GlobalFatigue - fatigueRecovery);
            CardioFatigue = Clamp01(CardioFatigue - fatigueRecovery);
            LegsFatigue = Clamp01(LegsFatigue - fatigueRecovery);
            ArmsFatigue = Clamp01(ArmsFatigue - fatigueRecovery);
            CoreFatigue = Clamp01(CoreFatigue - fatigueRecovery);

            GlobalPain = Clamp01(GlobalPain - painRecovery);
            LegsPain = Clamp01(LegsPain - painRecovery);
            ArmsPain = Clamp01(ArmsPain - painRecovery);
            CorePain = Clamp01(CorePain - painRecovery);
            Discomfort = Clamp01(Discomfort - discomfortRecovery);
        }

        private static float Clamp01(float value)
        {
            return Mathf.Clamp01(value);
        }
    }
}
