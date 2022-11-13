using System;
using UnityEngine;

namespace Shooter.Core
{
    public class AnimatorEventHandler : MonoBehaviour
    {
        public event Action FireMoment;
        public event Action Death;

        private void FireEvent()
        {
            FireMoment?.Invoke();
        }

        private void DeathEvent()
        {
            Death?.Invoke();
        }
    }
}