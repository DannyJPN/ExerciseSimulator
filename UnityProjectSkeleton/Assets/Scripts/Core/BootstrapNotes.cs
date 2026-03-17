using UnityEngine;

namespace TrainerAvatarSimulator.Core
{
    public class BootstrapNotes : MonoBehaviour
    {
        [TextArea(5, 12)]
        [SerializeField] private string notes =
            "Attach this component only during prototyping.\n" +
            "Recommended first scene flow:\n" +
            "1. Spawn avatar prefab with AvatarStateMachine and CommandDispatcher.\n" +
            "2. Place a wall InteractableObject with one interaction point.\n" +
            "3. Create Wall Sit exercise and linked command assets.\n" +
            "4. Drive execution with a temporary UI button panel.\n" +
            "5. Add form evaluation and sanction negotiation after pose hold works.";
    }
}
