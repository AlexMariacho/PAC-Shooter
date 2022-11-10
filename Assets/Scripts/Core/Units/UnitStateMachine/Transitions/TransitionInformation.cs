using System;

namespace Shooter.Core
{
    [Serializable]
    public class TransitionInformation
    {
        public Func<bool> Condition;
        public BaseState NextState;

        public TransitionInformation(Func<bool> condition, BaseState nextState)
        {
            Condition = condition;
            NextState = nextState;
        }
    }
}