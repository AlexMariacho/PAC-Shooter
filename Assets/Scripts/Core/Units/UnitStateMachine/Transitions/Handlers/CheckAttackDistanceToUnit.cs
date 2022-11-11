using System;
using UnityEngine;

namespace Shooter.Core.Handlers
{
    public class CheckAttackDistanceToUnit : ITransitionEventDeliver
    {
        public event Action<TransitionEventType> TransitionEvent;

        private Transform _selfTransform;
        private TargetInformation _targetInformation;
        private float _minDistance;

        public CheckAttackDistanceToUnit(Transform selfTransform, TargetInformation targetInformation, float minDistance)
        {
            _selfTransform = selfTransform;
            _targetInformation = targetInformation;
            _minDistance = minDistance;
        }

        public void Update()
        {
            if (_targetInformation.Target == null) return;

            var distance = Vector3.Distance(_selfTransform.position, _targetInformation.Target.View.transform.position);
            TransitionEvent?.Invoke(distance > _minDistance
                ? TransitionEventType.ImpossibleDistanceForAttack
                : TransitionEventType.PossibleDistanceForAttack);
        }
    }
}