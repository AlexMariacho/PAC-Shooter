using System;
using UnityEngine;

namespace Shooter.Core.Handlers
{
    public class CheckAlivePlayer : ITransitionEventDeliver
    {
        public event Action<TransitionEventType> TransitionEvent;

        private IDestroyable _selfDestroyable;

        public CheckAlivePlayer(IDestroyable selfDestroyable)
        {
            _selfDestroyable = selfDestroyable;
            _selfDestroyable.Death += OnDeathEvent;
        }

        public void Update()
        {
        }

        private void OnDeathEvent()
        {
            TransitionEvent?.Invoke(TransitionEventType.Death);
        }
    }
}