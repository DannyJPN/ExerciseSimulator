using System.Collections.Generic;
using UnityEngine;
using TrainerAvatarSimulator.Core;
using TrainerAvatarSimulator.Fatigue;
using TrainerAvatarSimulator.Sanctions;

namespace TrainerAvatarSimulator.Exercises
{
    [CreateAssetMenu(menuName = "Trainer Avatar Simulator/Exercises/Exercise Definition")]
    public class ExerciseDefinition : ScriptableObject
    {
        [SerializeField] private string exerciseId = "exercise";
        [SerializeField] private string displayName = "Exercise";
        [SerializeField] private ExerciseKind exerciseKind = ExerciseKind.Repetition;
        [SerializeField] private InteractionType requiredInteraction = InteractionType.None;
        [SerializeField] private AvatarPostureState entryPosture = AvatarPostureState.Standing;
        [SerializeField] private AvatarPostureState activePosture = AvatarPostureState.Exercising;
        [SerializeField] private float defaultDurationSeconds = 30f;
        [SerializeField] private int defaultRepetitions = 0;
        [SerializeField] private Vector3 alignmentOffset = Vector3.zero;
        [SerializeField] private FatigueLoadProfile loadProfile = new FatigueLoadProfile();
        [SerializeField] private List<ExerciseModifierDefinition> availableModifiers = new List<ExerciseModifierDefinition>();
        [SerializeField] private List<SanctionDefinition> recommendedSanctions = new List<SanctionDefinition>();

        public string ExerciseId => exerciseId;
        public string DisplayName => displayName;
        public ExerciseKind ExerciseKind => exerciseKind;
        public InteractionType RequiredInteraction => requiredInteraction;
        public AvatarPostureState EntryPosture => entryPosture;
        public AvatarPostureState ActivePosture => activePosture;
        public float DefaultDurationSeconds => defaultDurationSeconds;
        public int DefaultRepetitions => defaultRepetitions;
        public Vector3 AlignmentOffset => alignmentOffset;
        public FatigueLoadProfile LoadProfile => loadProfile;
        public IReadOnlyList<ExerciseModifierDefinition> AvailableModifiers => availableModifiers;
        public IReadOnlyList<SanctionDefinition> RecommendedSanctions => recommendedSanctions;
    }
}
