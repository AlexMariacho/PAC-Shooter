using UnityEngine;

namespace Shooter.Core
{
    public class RifleWeapon : IWeapon
    {
        private Transform _selfTransform;
        private IMove _selfMovable;
        private float _fireRate;
        private float _distance;
        private int _damage;
        
        private float _currentReloadTime;
        
        public void Fire(BaseUnit target)
        {
            
        }
        
        
    }
}