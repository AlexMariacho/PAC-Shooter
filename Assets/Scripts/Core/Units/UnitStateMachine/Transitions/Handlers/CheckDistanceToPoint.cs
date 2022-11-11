using System;
using UnityEngine;

namespace Shooter.Core.Handlers
{
    public class CheckDistanceToPoint : ITransitionEventDeliver
    {
        public event Action<TransitionEventType> TransitionEvent;

        private Transform _selfTransform;
        private MoveInformation _information;
        private float _minDistance;

        public CheckDistanceToPoint(Transform selfTransform, MoveInformation information, float minDistance)
        {
            _selfTransform = selfTransform;
            _information = information;
            _minDistance = minDistance;
        }

        public void Update()
        {
            var distance = Vector3.Distance(_selfTransform.position, _information.TargetPoint);
            TransitionEvent?.Invoke(distance > _minDistance ? TransitionEventType.StartMove : TransitionEventType.StopMove);
        }
    }
}