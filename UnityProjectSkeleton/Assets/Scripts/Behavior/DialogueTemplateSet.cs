using System;
using System.Collections.Generic;
using UnityEngine;

namespace TrainerAvatarSimulator.Behavior
{
    [CreateAssetMenu(menuName = "Trainer Avatar Simulator/Behavior/Dialogue Template Set")]
    public class DialogueTemplateSet : ScriptableObject
    {
        [SerializeField] private string dialogueSetId = "DLG_Default";
        [SerializeField] private List<DialogueTemplateEntry> entries = new List<DialogueTemplateEntry>();

        public string DialogueSetId => dialogueSetId;
        public IReadOnlyList<DialogueTemplateEntry> Entries => entries;
    }

    [Serializable]
    public class DialogueTemplateEntry
    {
        public string TemplateId = string.Empty;
        public string Category = string.Empty;
        public string ToneTag = string.Empty;

        [TextArea(2, 4)]
        public string Text = string.Empty;
    }
}
