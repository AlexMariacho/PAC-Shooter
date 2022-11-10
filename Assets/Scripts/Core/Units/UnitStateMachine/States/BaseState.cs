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

        public void CheckTransitions()
        {
            for (int i = 0; i < _transitions.Count; i++)
            {
                if (_transitions[i].Condition.Invoke())
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