using UnityEngine;
using TrainerAvatarSimulator.Core;

namespace TrainerAvatarSimulator.Wardrobe
{
    [CreateAssetMenu(menuName = "Trainer Avatar Simulator/Wardrobe/Wardrobe Item Definition")]
    public class WardrobeItemDefinition : ScriptableObject
    {
        [SerializeField] private string wardrobeItemId = "WRD_Item";
        [SerializeField] private string displayName = "Wardrobe Item";
        [SerializeField] private ClothingSlot slot = ClothingSlot.Top;
        [SerializeField] private bool isRemovable = true;
        [SerializeField] private string visualPrefabPath = string.Empty;
        [SerializeField, Range(-1f, 1f)] private float comfortModifier = 0f;
        [SerializeField, Range(-1f, 1f)] private float painModifier = 0f;
        [SerializeField] private string animationTag = string.Empty;

        public string WardrobeItemId => wardrobeItemId;
        public string DisplayName => displayName;
        public ClothingSlot Slot => slot;
        public bool IsRemovable => isRemovable;
        public string VisualPrefabPath => visualPrefabPath;
        public float ComfortModifier => comfortModifier;
        public float PainModifier => painModifier;
        public string AnimationTag => animationTag;
    }
}
