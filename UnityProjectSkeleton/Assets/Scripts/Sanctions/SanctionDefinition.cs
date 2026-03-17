using UnityEngine;
using TrainerAvatarSimulator.Core;

namespace TrainerAvatarSimulator.Sanctions
{
    [CreateAssetMenu(menuName = "Trainer Avatar Simulator/Sanctions/Sanction Definition")]
    public class SanctionDefinition : ScriptableObject
    {
        [SerializeField] private string sanctionId = "sanction";
        [SerializeField] private string displayName = "Sanction";
        [SerializeField] private SanctionType sanctionType = SanctionType.None;
        [SerializeField] private float intensity = 0.25f;
        [SerializeField] private float minFormFailure = 0.15f;
        [SerializeField] private bool allowNegotiation = true;

        public string SanctionId => sanctionId;
        public string DisplayName => displayName;
        public SanctionType SanctionType => sanctionType;
        public float Intensity => intensity;
        public float MinFormFailure => minFormFailure;
        public bool AllowNegotiation => allowNegotiation;
    }
}
