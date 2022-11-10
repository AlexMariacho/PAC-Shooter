using UnityEngine;

namespace Shooter.Core
{
    public class MoveInformation
    {
        public Vector3 TargetPoint;

        public MoveInformation(Vector3 targetPoint)
        {
            TargetPoint = targetPoint;
        }
    }
}