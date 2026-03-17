using System;
using System.Collections.Generic;
using UnityEngine;
using TrainerAvatarSimulator.Commands;

namespace TrainerAvatarSimulator.Core
{
    public class CommandHistoryBuffer : MonoBehaviour
    {
        [SerializeField] private AvatarRuntimeEvents runtimeEvents;
        [SerializeField] private int maxEntries = 20;
        [SerializeField] private List<CommandHistoryEntry> entries = new List<CommandHistoryEntry>();

        public IReadOnlyList<CommandHistoryEntry> Entries => entries;

        private void Awake()
        {
            if (runtimeEvents == null)
            {
                runtimeEvents = GetComponent<AvatarRuntimeEvents>();
            }
        }

        private void OnEnable()
        {
            if (runtimeEvents == null)
            {
                return;
            }

            runtimeEvents.CommandStarted += HandleCommandStarted;
            runtimeEvents.CommandInterrupted += HandleCommandInterrupted;
        }

        private void OnDisable()
        {
            if (runtimeEvents == null)
            {
                return;
            }

            runtimeEvents.CommandStarted -= HandleCommandStarted;
            runtimeEvents.CommandInterrupted -= HandleCommandInterrupted;
        }

        public void Configure(AvatarRuntimeEvents events)
        {
            runtimeEvents = events;
        }

        private void HandleCommandStarted(CommandExecutionContext context)
        {
            Append("started", context.CommandId, context.DisplayName);
        }

        private void HandleCommandInterrupted(CommandExecutionContext context)
        {
            Append("interrupted", context.CommandId, context.DisplayName);
        }

        private void Append(string action, string commandId, string displayName)
        {
            entries.Insert(0, new CommandHistoryEntry
            {
                TimestampIsoUtc = DateTime.UtcNow.ToString("O"),
                Action = action,
                CommandId = commandId ?? string.Empty,
                DisplayName = displayName ?? string.Empty
            });

            if (entries.Count > maxEntries)
            {
                entries.RemoveRange(maxEntries, entries.Count - maxEntries);
            }
        }
    }

    [Serializable]
    public class CommandHistoryEntry
    {
        public string TimestampIsoUtc = string.Empty;
        public string Action = string.Empty;
        public string CommandId = string.Empty;
        public string DisplayName = string.Empty;
    }
}
