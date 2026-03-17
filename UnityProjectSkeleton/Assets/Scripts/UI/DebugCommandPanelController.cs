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

                button.clicked += () => dispatcher.TryDispatch(binding.Command, CommandSource.DebugUI);
            }
        }
    }

    [System.Serializable]
    public class CommandButtonBinding
    {
        public string ButtonName = string.Empty;
        public CommandDefinition Command;
    }
}
