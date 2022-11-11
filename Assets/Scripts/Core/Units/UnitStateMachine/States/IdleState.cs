using UnityEngine;

namespace Shooter.Core
{
    public class IdleState : BaseState
    {
        public IdleState(IStateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void EnterState()
        {
            Debug.Log("Enter Idle");
        }

        public override void ExitState()
        {
            Debug.Log("Exit Idle");
        }

        public override void UpdateState()
        {
            
        }
    }
}