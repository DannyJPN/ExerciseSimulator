using UnityEngine;
using TrainerAvatarSimulator.Core;

namespace TrainerAvatarSimulator.Environment
{
    [CreateAssetMenu(menuName = "Trainer Avatar Simulator/Environment/Interaction Preset Definition")]
    public class InteractionPresetDefinition : ScriptableObject
    {
        [SerializeField] private string interactionId = "INT_Preset";
        [SerializeField] private InteractionType interactionType = InteractionType.None;
        [SerializeField] private AvatarPostureState requiredPosture = AvatarPostureState.Standing;
        [SerializeField] private Vector3 alignmentOffset = Vector3.zero;
        [SerializeField] private Vector3 alignmentEuler = Vector3.zero;
        [SerializeField] private string ikHintTag = string.Empty;

        public string InteractionId => interactionId;
        public InteractionType InteractionType => interactionType;
        public AvatarPostureState RequiredPosture => requiredPosture;
        public Vector3 AlignmentOffset => alignmentOffset;
        public Vector3 AlignmentEuler => alignmentEuler;
        public string IkHintTag => ikHintTag;
    }
}
