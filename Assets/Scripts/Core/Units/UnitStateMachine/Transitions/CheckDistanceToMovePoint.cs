using System.Runtime.InteropServices.ComTypes;
using UnityEngine;

namespace Shooter.Core
{
    public class CheckDistanceToMovePoint : ITransition
    {
        private Transform _selfTransform;
        private MoveInformation _moveInformation;

        private bool _inverse;

        public CheckDistanceToMovePoint(Transform selfTransform, MoveInformation moveInformation, bool inverse)
        {
            _selfTransform = selfTransform;
            _moveInformation = moveInformation;

            _inverse = inverse;
        }

        public bool CheckCondition()
        {
            if (_inverse)
            {
                return Vector3.Distance(_selfTransform.position, _moveInformation.TargetPoint) > 0.1f;
            }
            return Vector3.Distance(_selfTransform.position, _moveInformation.TargetPoint) < 0.1f;

        }
    }
}