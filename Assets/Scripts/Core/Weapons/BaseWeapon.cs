using System;
using Core.Views;
using Shooter.Simple.Units;
using UnityEngine;

namespace Shooter.Core
{
    public abstract class BaseWeapon : MonoBehaviour
    {
        [field: SerializeField] public WeaponConfiguration Configuration { get; protected set; }
        public abstract void Fire(BaseUnit target);

        public abstract void Initialize(Transform selfView, PlayerAnimation playerAnimation,
            AnimatorEventHandler eventHandler);
    }

    [Serializable]
    public class WeaponConfiguration
    {
        public float FireRate;
        public int Damage;
        public float Distance;
    }
}