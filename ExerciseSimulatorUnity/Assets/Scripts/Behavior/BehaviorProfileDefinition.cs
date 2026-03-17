using UnityEngine;

namespace TrainerAvatarSimulator.Behavior
{
    [CreateAssetMenu(menuName = "Trainer Avatar Simulator/Behavior/Behavior Profile Definition")]
    public class BehaviorProfileDefinition : ScriptableObject
    {
        [SerializeField] private string profileId = "BHP_Profile";
        [SerializeField] private string displayName = "Profile";
        [SerializeField, Range(0f, 1f)] private float motivationBase = 0.5f;
        [SerializeField, Range(0f, 1f)] private float complianceBase = 0.7f;
        [SerializeField, Range(0f, 1f)] private float confidenceBase = 0.5f;
        [SerializeField, Range(0f, 1f)] private float selfDisciplineBase = 0.7f;
        [SerializeField, Range(0f, 1f)] private float selfCriticismBase = 0.6f;
        [SerializeField, Range(0f, 1f)] private float fairnessSensitivity = 0.5f;
        [SerializeField, Range(0f, 1f)] private float painSensitivity = 0.5f;
        [SerializeField, Range(0f, 1f)] private float leniencySensitivity = 0.5f;
        [SerializeField, Range(0f, 1f)] private float ambitionBase = 0.55f;
        [SerializeField] private string dialogueSetId = string.Empty;

        public string ProfileId => profileId;
        public string DisplayName => displayName;
        public float MotivationBase => motivationBase;
        public float ComplianceBase => complianceBase;
        public float ConfidenceBase => confidenceBase;
        public float SelfDisciplineBase => selfDisciplineBase;
        public float SelfCriticismBase => selfCriticismBase;
        public float FairnessSensitivity => fairnessSensitivity;
        public float PainSensitivity => painSensitivity;
        public float LeniencySensitivity => leniencySensitivity;
        public float AmbitionBase => ambitionBase;
        public string DialogueSetId => dialogueSetId;
    }
}
