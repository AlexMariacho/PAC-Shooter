using UnityEngine;

namespace Shooter.Core
{
    public class DeathState : BaseState
    {
        public DeathState(IStateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void EnterState()
        {
            Debug.Log("Enter DEATH state");
        }

        public override void ExitState()
        {
            Debug.Log("Enter DEATH state");
        }

        public override void UpdateState()
        {
           
        }
    }
}