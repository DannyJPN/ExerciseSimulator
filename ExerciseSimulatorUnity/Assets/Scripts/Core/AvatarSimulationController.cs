using UnityEngine;
using TrainerAvatarSimulator.Behavior;
using TrainerAvatarSimulator.Wardrobe;

namespace TrainerAvatarSimulator.Core
{
    public class AvatarSimulationController : MonoBehaviour
    {
        [SerializeField] private AvatarStateMachine avatarStateMachine;
        [SerializeField] private BootstrapConfiguration bootstrapConfiguration;

        [SerializeField] private string activeBehaviorProfileId = string.Empty;
        [SerializeField] private string activeDialogueSetId = string.Empty;

        public string ActiveBehaviorProfileId => activeBehaviorProfileId;
        public string ActiveDialogueSetId => activeDialogueSetId;

        private void Awake()
        {
            if (avatarStateMachine == null)
            {
                avatarStateMachine = GetComponent<AvatarStateMachine>();
            }

            if (bootstrapConfiguration != null)
            {
                ApplyBootstrapConfiguration(bootstrapConfiguration);
            }
        }

        public void ApplyBootstrapConfiguration(BootstrapConfiguration configuration)
        {
            if (configuration == null || avatarStateMachine == null)
            {
                return;
            }

            avatarStateMachine.RuntimeState.CurrentPosture = configuration.InitialPosture;
            ApplyBehaviorProfile(configuration.DefaultBehaviorProfile, configuration.DefaultDialogueTemplateSet);

            foreach (var wardrobeItem in configuration.StartingWardrobeItems)
            {
                EquipStartingItem(wardrobeItem);
            }
        }

        public void ApplyBehaviorProfile(
            BehaviorProfileDefinition profile,
            DialogueTemplateSet dialogueSet = null)
        {
            if (avatarStateMachine == null || profile == null)
            {
                return;
            }

            var behaviorState = avatarStateMachine.RuntimeState.Behavior;
            behaviorState.Motivation = profile.MotivationBase;
            behaviorState.Compliance = profile.ComplianceBase;
            behaviorState.Confidence = profile.ConfidenceBase;
            behaviorState.SelfDiscipline = profile.SelfDisciplineBase;
            behaviorState.SelfCriticism = profile.SelfCriticismBase;
            behaviorState.FairnessPerception = 1f - profile.FairnessSensitivity;
            behaviorState.PerceivedLeniency = profile.LeniencySensitivity;
            behaviorState.Humility = Mathf.Clamp01((profile.ComplianceBase + profile.SelfCriticismBase) * 0.5f);
            behaviorState.PainTolerance = 1f - profile.PainSensitivity;
            behaviorState.Ambition = profile.AmbitionBase;
            behaviorState.PerfectExecutionCount = 0;

            activeBehaviorProfileId = profile.ProfileId;
            activeDialogueSetId = dialogueSet != null ? dialogueSet.DialogueSetId : profile.DialogueSetId;
        }

        private void EquipStartingItem(WardrobeItemDefinition item)
        {
            if (item == null || avatarStateMachine == null)
            {
                return;
            }

            var slotState = GetSlotState(item.Slot);
            if (slotState == null)
            {
                return;
            }

            slotState.IsAvailable = true;
            slotState.IsWorn = true;
            slotState.IsRemovable = item.IsRemovable;
            slotState.ItemId = item.WardrobeItemId;
        }

        private WardrobeSlotState GetSlotState(ClothingSlot slot)
        {
            var wardrobe = avatarStateMachine.RuntimeState.Wardrobe;

            switch (slot)
            {
                case ClothingSlot.Top:
                    return wardrobe.Top;
                case ClothingSlot.Bottom:
                    return wardrobe.Bottom;
                case ClothingSlot.Shoes:
                    return wardrobe.Shoes;
                case ClothingSlot.Socks:
                    return wardrobe.Socks;
                default:
                    return null;
            }
        }
    }
}
