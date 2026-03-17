using UnityEngine;

namespace TrainerAvatarSimulator.Exercises
{
    [CreateAssetMenu(menuName = "Trainer Avatar Simulator/Exercises/Exercise Modifier Definition")]
    public class ExerciseModifierDefinition : ScriptableObject
    {
        [SerializeField] private string modifierId = "MOD_Modifier";
        [SerializeField] private string displayName = "Modifier";
        [SerializeField] private string dialogueTag = string.Empty;
        [SerializeField, Range(0.1f, 3f)] private float fatigueMultiplier = 1f;
        [SerializeField, Range(-1f, 1f)] private float validationAdjustment = 0f;

        public string ModifierId => modifierId;
        public string DisplayName => displayName;
        public string DialogueTag => dialogueTag;
        public float FatigueMultiplier => fatigueMultiplier;
        public float ValidationAdjustment => validationAdjustment;
    }
}
