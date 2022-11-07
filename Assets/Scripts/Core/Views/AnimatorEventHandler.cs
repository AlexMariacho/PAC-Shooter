using System;
using UnityEngine;

namespace Core.Views
{
    public class AnimatorEventHandler : MonoBehaviour
    {
        public event Action FireMoment;
        public event Action Death;

        private void FireEvent()
        {
            FireMoment?.Invoke();
            Debug.Log("FIRE");
        }

        private void DeathEvent()
        {
            Death?.Invoke();
            Debug.Log("Death");
        }
    }
}