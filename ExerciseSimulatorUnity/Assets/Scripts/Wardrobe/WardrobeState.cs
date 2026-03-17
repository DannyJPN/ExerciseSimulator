using System;
using UnityEngine;
using TrainerAvatarSimulator.Core;

namespace TrainerAvatarSimulator.Wardrobe
{
    [Serializable]
    public class WardrobeState
    {
        public WardrobeSlotState Top = new WardrobeSlotState();
        public WardrobeSlotState Bottom = new WardrobeSlotState();
        public WardrobeSlotState Shoes = new WardrobeSlotState();
        public WardrobeSlotState Socks = new WardrobeSlotState();

        public WardrobeSlotState GetSlot(ClothingSlot slot)
        {
            switch (slot)
            {
                case ClothingSlot.Top:
                    return Top;
                case ClothingSlot.Bottom:
                    return Bottom;
                case ClothingSlot.Shoes:
                    return Shoes;
                case ClothingSlot.Socks:
                    return Socks;
                default:
                    return null;
            }
        }

        public bool HasRemovableWornItem()
        {
            return CanRemove(ClothingSlot.Shoes) ||
                CanRemove(ClothingSlot.Socks) ||
                CanRemove(ClothingSlot.Top) ||
                CanRemove(ClothingSlot.Bottom);
        }

        public ClothingSlot GetNextRemovalCandidate()
        {
            if (CanRemove(ClothingSlot.Shoes))
            {
                return ClothingSlot.Shoes;
            }

            if (CanRemove(ClothingSlot.Socks))
            {
                return ClothingSlot.Socks;
            }

            if (CanRemove(ClothingSlot.Top))
            {
                return ClothingSlot.Top;
            }

            if (CanRemove(ClothingSlot.Bottom))
            {
                return ClothingSlot.Bottom;
            }

            return ClothingSlot.Top;
        }

        public bool CanRemove(ClothingSlot slot)
        {
            var slotState = GetSlot(slot);
            return slotState != null &&
                slotState.IsAvailable &&
                slotState.IsWorn &&
                slotState.IsRemovable;
        }

        public bool TryRemove(ClothingSlot slot)
        {
            if (!CanRemove(slot))
            {
                return false;
            }

            GetSlot(slot).IsWorn = false;
            return true;
        }
    }

    [Serializable]
    public class WardrobeSlotState
    {
        public bool IsAvailable = true;
        public bool IsWorn = true;
        public bool IsRemovable = true;
        public string ItemId = string.Empty;
    }
}
