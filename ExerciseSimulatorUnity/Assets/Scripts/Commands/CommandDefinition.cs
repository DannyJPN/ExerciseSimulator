using UnityEngine;
using TrainerAvatarSimulator.Core;
using TrainerAvatarSimulator.Exercises;

namespace TrainerAvatarSimulator.Commands
{
    [CreateAssetMenu(menuName = "Trainer Avatar Simulator/Commands/Command Definition")]
    public class CommandDefinition : ScriptableObject
    {
        [SerializeField] private string commandId = "command";
        [SerializeField] private string displayName = "Command";
        [SerializeField] private CommandType commandType = CommandType.Exercise;
        [SerializeField] private ExerciseDefinition linkedExercise;
        [SerializeField] private AvatarPostureState targetPosture = AvatarPostureState.Idle;
        [SerializeField] private string wardrobeTargetId = string.Empty;
        [SerializeField] private int defaultPriority = 100;
        [SerializeField] private bool allowInterrupt = true;

        public string CommandId => commandId;
        public string DisplayName => displayName;
        public CommandType CommandType => commandType;
        public ExerciseDefinition LinkedExercise => linkedExercise;
        public AvatarPostureState TargetPosture => targetPosture;
        public string WardrobeTargetId => wardrobeTargetId;
        public int DefaultPriority => defaultPriority;
        public bool AllowInterrupt => allowInterrupt;
    }
}
