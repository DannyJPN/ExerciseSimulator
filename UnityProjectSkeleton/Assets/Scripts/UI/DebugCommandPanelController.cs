using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using TrainerAvatarSimulator.Commands;
using TrainerAvatarSimulator.Core;

namespace TrainerAvatarSimulator.UI
{
    public class DebugCommandPanelController : MonoBehaviour
    {
        [SerializeField] private UIDocument document;
        [SerializeField] private CommandDispatcher dispatcher;
        [SerializeField] private List<CommandButtonBinding> commandBindings = new List<CommandButtonBinding>();

        private readonly List<ButtonRegistration> activeRegistrations = new List<ButtonRegistration>();

        private void Awake()
        {
            if (document == null)
            {
                document = GetComponent<UIDocument>();
            }

            if (dispatcher == null)
            {
                dispatcher = FindFirstObjectByType<CommandDispatcher>();
            }
        }

        private void OnEnable()
        {
            RegisterButtons();
        }

        private void OnDisable()
        {
            UnregisterButtons();
        }

        public void Configure(CommandDispatcher commandDispatcher)
        {
            dispatcher = commandDispatcher;
            RegisterButtons();
        }

        private void RegisterButtons()
        {
            UnregisterButtons();

            if (document?.rootVisualElement == null || dispatcher == null)
            {
                return;
            }

            foreach (var binding in commandBindings)
            {
                if (binding.Command == null || string.IsNullOrWhiteSpace(binding.ButtonName))
                {
                    continue;
                }

                var button = document.rootVisualElement.Q<Button>(binding.ButtonName);
                if (button == null)
                {
                    continue;
                }

                System.Action handler = () => dispatcher.TryDispatch(binding.Command, CommandSource.DebugUI);
                button.clicked += handler;
                activeRegistrations.Add(new ButtonRegistration(button, handler));
            }
        }

        private void UnregisterButtons()
        {
            foreach (var registration in activeRegistrations)
            {
                registration.Button.clicked -= registration.Handler;
            }

            activeRegistrations.Clear();
        }
    }

    [System.Serializable]
    public class CommandButtonBinding
    {
        public string ButtonName = string.Empty;
        public CommandDefinition Command;
    }

    internal readonly struct ButtonRegistration
    {
        public ButtonRegistration(Button button, System.Action handler)
        {
            Button = button;
            Handler = handler;
        }

        public Button Button { get; }
        public System.Action Handler { get; }
    }
}
