using UnityEngine;
using UnityEngine.AI;

namespace Shooter.Core
{
    public class PlayerNavigation : IMove
    {
        private Transform _selfTransform;
        private NavMeshAgent _agent;

        public PlayerNavigation(float speed, float angularSpeed, Transform selfTransform, NavMeshAgent agent)
        {
            _selfTransform = selfTransform;
            _agent = agent;

            _agent.speed = speed;
            _agent.angularSpeed = angularSpeed;
        }

        public void Move(Vector3 point)
        {
            _agent.SetDestination(point);
        }

        public void Stop()
        {
            _agent.SetDestination(_selfTransform.position);
        }
    }
}