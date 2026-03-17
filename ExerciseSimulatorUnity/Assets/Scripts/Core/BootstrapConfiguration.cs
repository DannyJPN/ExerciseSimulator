using System.Collections.Generic;
using UnityEngine;
using TrainerAvatarSimulator.Behavior;
using TrainerAvatarSimulator.Commands;
using TrainerAvatarSimulator.Exercises;
using TrainerAvatarSimulator.Sanctions;
using TrainerAvatarSimulator.Wardrobe;

namespace TrainerAvatarSimulator.Core
{
    [CreateAssetMenu(menuName = "Trainer Avatar Simulator/Core/Bootstrap Configuration")]
    public class BootstrapConfiguration : ScriptableObject
    {
        [SerializeField] private AvatarPostureState initialPosture = AvatarPostureState.Standing;
        [SerializeField] private BehaviorProfileDefinition defaultBehaviorProfile;
        [SerializeField] private DialogueTemplateSet defaultDialogueTemplateSet;
        [SerializeField] private List<WardrobeItemDefinition> startingWardrobeItems = new List<WardrobeItemDefinition>();
        [SerializeField] private List<CommandDefinition> debugCommands = new List<CommandDefinition>();
        [SerializeField] private List<ExerciseDefinition> registeredExercises = new List<ExerciseDefinition>();
        [SerializeField] private List<SanctionDefinition> registeredSanctions = new List<SanctionDefinition>();

        public AvatarPostureState InitialPosture => initialPosture;
        public BehaviorProfileDefinition DefaultBehaviorProfile => defaultBehaviorProfile;
        public DialogueTemplateSet DefaultDialogueTemplateSet => defaultDialogueTemplateSet;
        public IReadOnlyList<WardrobeItemDefinition> StartingWardrobeItems => startingWardrobeItems;
        public IReadOnlyList<CommandDefinition> DebugCommands => debugCommands;
        public IReadOnlyList<ExerciseDefinition> RegisteredExercises => registeredExercises;
        public IReadOnlyList<SanctionDefinition> RegisteredSanctions => registeredSanctions;
    }
}
