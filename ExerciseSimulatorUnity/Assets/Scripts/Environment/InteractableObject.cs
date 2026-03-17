using System;
using System.Collections.Generic;
using UnityEngine;
using TrainerAvatarSimulator.Core;

namespace TrainerAvatarSimulator.Environment
{
    public class InteractableObject : MonoBehaviour
    {
        [SerializeField] private InteractionType interactionType = InteractionType.None;
        [SerializeField] private List<InteractionPoint> interactionPoints = new List<InteractionPoint>();

        public InteractionType InteractionType => interactionType;
        public IReadOnlyList<InteractionPoint> InteractionPoints => interactionPoints;

        public bool TryGetPrimaryPoint(out InteractionPoint point)
        {
            point = interactionPoints.Count > 0 ? interactionPoints[0] : null;
            return point != null;
        }
    }

    [Serializable]
    public class InteractionPoint
    {
        public string PointId = "primary";
        public Transform Anchor;
        public AvatarPostureState RequiredPosture = AvatarPostureState.Standing;
    }
}
