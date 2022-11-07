using System;
using UnityEngine;
using UnityEngine.AI;

namespace Shooter.Core
{
    public class MoveState : IState
    {
        public event Action<IState> Finish;
        
        private NavMeshAgent _agent;
        private float _minimumDistance;
        private Vector3 _targetPoint;
        
        public MoveState(NavMeshAgent agent, Vector3 targetPoint, float minimumDistance)
        {
            _agent = agent;
            _minimumDistance = minimumDistance;
            _targetPoint = targetPoint;
        }

        public void EnterState()
        {
            _agent.SetDestination(_targetPoint);
            _agent.stoppingDistance = _minimumDistance;
        }

        public void ExitState()
        {

        }

        public void Update()
        {
            if (_agent.isStopped)
            {
                ExitState();
            }
        }
    }
}