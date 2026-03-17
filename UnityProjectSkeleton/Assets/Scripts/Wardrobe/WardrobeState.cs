using System;
using UnityEngine;

namespace TrainerAvatarSimulator.Wardrobe
{
    [Serializable]
    public class WardrobeState
    {
        public WardrobeSlotState Top = new WardrobeSlotState();
        public WardrobeSlotState Bottom = new WardrobeSlotState();
        public WardrobeSlotState Shoes = new WardrobeSlotState();
        public WardrobeSlotState Socks = new WardrobeSlotState();
    }

    [Serializable]
    public class WardrobeSlotState
    {
        public bool IsAvailable = true;
        public bool IsWorn = true;
        public string ItemId = string.Empty;
    }
}
