using System;
using UnityEngine;

namespace Shooter.Core
{
    public abstract class BaseWeapon : MonoBehaviour
    {
        [field: SerializeField] public WeaponConfiguration Configuration { get; protected set; }
        public abstract void Fire(BaseUnit target);
        public abstract void Initialize(Transform selfView, PlayerAnimationController playerAnimationController,
            AnimatorEventHandler eventHandler);
    }

    [Serializable]
    public class WeaponConfiguration
    {
        public float FireRate = 3;
        public int Damage = 1;
        public float Distance = 5;
    }
}