using UnityEngine;
using TrainerAvatarSimulator.Commands;

namespace TrainerAvatarSimulator.Core
{
    public class SceneBootstrap : MonoBehaviour
    {
        [SerializeField] private BootstrapConfiguration configuration;
        [SerializeField] private AvatarStateMachine avatarStateMachine;
        [SerializeField] private AvatarSimulationController simulationController;
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

            if (simulationController == null)
            {
                simulationController = FindFirstObjectByType<AvatarSimulationController>();
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

            if (simulationController != null && configuration != null)
            {
                simulationController.ApplyBootstrapConfiguration(configuration);
            }
        }
    }
}
