using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;
using TrainerAvatarSimulator.Core;

namespace TrainerAvatarSimulator.UI
{
    public class AvatarStateDebugView : MonoBehaviour
    {
        [SerializeField] private UIDocument document;
        [SerializeField] private AvatarStateMachine avatarStateMachine;
        [SerializeField] private AvatarSimulationController simulationController;
        [SerializeField] private CommandHistoryBuffer commandHistoryBuffer;

        private Label postureLabel;
        private Label commandLabel;
        private Label exerciseLabel;
        private Label busyLabel;
        private Label profileLabel;
        private Label fatigueLabel;
        private Label discomfortLabel;
        private Label historyLabel;

        private void Awake()
        {
            if (document == null)
            {
                document = GetComponent<UIDocument>();
            }

            if (avatarStateMachine == null)
            {
                avatarStateMachine = FindFirstObjectByType<AvatarStateMachine>();
            }

            if (simulationController == null)
            {
                simulationController = FindFirstObjectByType<AvatarSimulationController>();
            }

            if (commandHistoryBuffer == null)
            {
                commandHistoryBuffer = FindFirstObjectByType<CommandHistoryBuffer>();
            }
        }

        private void OnEnable()
        {
            if (document?.rootVisualElement == null)
            {
                return;
            }

            postureLabel = document.rootVisualElement.Q<Label>("PostureValue");
            commandLabel = document.rootVisualElement.Q<Label>("CommandValue");
            exerciseLabel = document.rootVisualElement.Q<Label>("ExerciseValue");
            busyLabel = document.rootVisualElement.Q<Label>("BusyValue");
            profileLabel = document.rootVisualElement.Q<Label>("ProfileValue");
            fatigueLabel = document.rootVisualElement.Q<Label>("FatigueValue");
            discomfortLabel = document.rootVisualElement.Q<Label>("DiscomfortValue");
            historyLabel = document.rootVisualElement.Q<Label>("HistoryValue");
        }

        private void Update()
        {
            if (avatarStateMachine == null || document?.rootVisualElement == null)
            {
                return;
            }

            var runtime = avatarStateMachine.RuntimeState;

            if (postureLabel != null)
            {
                postureLabel.text = runtime.CurrentPosture.ToString();
            }

            if (commandLabel != null)
            {
                commandLabel.text = string.IsNullOrWhiteSpace(runtime.ActiveCommandId) ? "None" : runtime.ActiveCommandId;
            }

            if (exerciseLabel != null)
            {
                exerciseLabel.text = string.IsNullOrWhiteSpace(runtime.ActiveExerciseId) ? "None" : runtime.ActiveExerciseId;
            }

            if (busyLabel != null)
            {
                busyLabel.text = runtime.IsBusy ? "Busy" : "Available";
            }

            if (profileLabel != null)
            {
                profileLabel.text = simulationController == null ||
                    string.IsNullOrWhiteSpace(simulationController.ActiveBehaviorProfileId)
                    ? "None"
                    : simulationController.ActiveBehaviorProfileId;
            }

            if (fatigueLabel != null)
            {
                fatigueLabel.text = runtime.Fatigue.GlobalFatigue.ToString("0.00");
            }

            if (discomfortLabel != null)
            {
                discomfortLabel.text = runtime.Fatigue.Discomfort.ToString("0.00");
            }

            if (historyLabel != null && commandHistoryBuffer != null)
            {
                historyLabel.text = string.Join("\n", commandHistoryBuffer.Entries.Take(5).Select(entry =>
                    $"{entry.Action}: {entry.DisplayName}"));
            }
        }
    }
}
