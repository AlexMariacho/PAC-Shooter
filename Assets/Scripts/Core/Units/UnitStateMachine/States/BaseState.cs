using System.Collections.Generic;

namespace Shooter.Core
{
    public abstract class BaseState
    {
        protected IStateMachine _stateMachine;
        protected List<TransitionInformation> _transitions = new List<TransitionInformation>();

        protected BaseState(IStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public abstract void EnterState();
        public abstract void ExitState();
        public abstract void UpdateState();
    }
}