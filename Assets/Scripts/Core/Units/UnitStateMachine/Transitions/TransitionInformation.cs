using System;

namespace Shooter.Core
{
    [Serializable]
    public class TransitionInformation
    {
        public TransitionEventType TransitionEvent { get; private set; }
        public BaseState FromState { get; private set; }
        public BaseState ToState { get; private set; }

        public TransitionInformation(TransitionEventType transitionEvent, BaseState fromState, BaseState toState)
        {
            TransitionEvent = transitionEvent;
            FromState = fromState;
            ToState = toState;
        }
    }
}