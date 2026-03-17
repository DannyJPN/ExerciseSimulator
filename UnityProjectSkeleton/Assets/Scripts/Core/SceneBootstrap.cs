using UnityEngine;
using TrainerAvatarSimulator.Commands;

namespace TrainerAvatarSimulator.Core
{
    public class SceneBootstrap : MonoBehaviour
    {
        [SerializeField] private AvatarStateMachine avatarStateMachine;
        [SerializeField] private CommandDispatcher commandDispatcher;
        [SerializeField] private AvatarRuntimeEvents runtimeEvents;
        [SerializeField] private CommandHistoryBuffer commandHistoryBuffer;

        private void Awake()
        {
            if (avatarStateMachine == null)
            {
                avatarStateMachine = FindFirstObjectByType<AvatarStateMachine>();
            }

            if (commandDispatcher == null)
            {
                commandDispatcher = FindFirstObjectByType<CommandDispatcher>();
            }

            if (runtimeEvents == null)
            {
                runtimeEvents = FindFirstObjectByType<AvatarRuntimeEvents>();
            }

            if (commandHistoryBuffer == null)
            {
                commandHistoryBuffer = FindFirstObjectByType<CommandHistoryBuffer>();
            }

            if (commandDispatcher != null)
            {
                commandDispatcher.Configure(avatarStateMachine, runtimeEvents);
            }

            if (commandHistoryBuffer != null && runtimeEvents != null)
            {
                commandHistoryBuffer.Configure(runtimeEvents);
            }
        }
    }
}
