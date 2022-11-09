using UnityEngine;

namespace Shooter.Core
{
    public class CheckDistanceToTarget : ITransition
    {
        private Transform _selfTransform;
        private TargetInformation _targetInformation;

        private bool _inverse;
        private float _distance;

        public CheckDistanceToTarget(Transform selfTransform, TargetInformation targetInformation, float distance, bool inverse = false)
        {
            _selfTransform = selfTransform;
            _targetInformation = targetInformation;

            _inverse = inverse;
            _distance = distance;
        }

        public bool CheckCondition()
        {
            if (_selfTransform == null || _targetInformation == null)
                return false;
            
            if (_inverse)
            {
                return Vector3.Distance(_selfTransform.position, _targetInformation.Target.View.transform.position) > _distance;
            }
            return Vector3.Distance(_selfTransform.position, _targetInformation.Target.View.transform.position) < _distance;
        }
    }
}