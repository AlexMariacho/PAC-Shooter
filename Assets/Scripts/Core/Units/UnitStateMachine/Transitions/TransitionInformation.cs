using System;

namespace Shooter.Core
{
    [Serializable]
    public class TransitionInformation
    {
        public ITransition Condition;
        public BaseState NextState;

        public TransitionInformation(ITransition condition, BaseState nextState)
        {
            Condition = condition;
            NextState = nextState;
        }
    }
}