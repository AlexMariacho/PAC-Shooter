using System.Collections.Generic;
using System.Linq;

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

        public virtual void UpdateState()
        {
            for (int i = 0; i < _transitions.Count; i++)
            {
                if (_transitions[i].Condition.CheckCondition())
                {
                    _stateMachine.SetState(_transitions[i].NextState);
                    return;
                }
            }
        }

        public void AddTransition(TransitionInformation transitionInformation)
        {
            _transitions.Add(transitionInformation);
        }
    }
}