using System;

namespace Shooter.Core.Handlers
{
    public interface ITransitionEventDeliver
    {
        public event Action<TransitionEventType> TransitionEvent;

        void Update();
    }
}