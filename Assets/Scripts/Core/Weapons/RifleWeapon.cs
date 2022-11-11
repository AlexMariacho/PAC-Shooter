using Shooter.Simple.Units;
using UnityEngine;

namespace Shooter.Core
{
    public class RifleWeapon : IWeapon
    {
        private float _fireRate;
        private float _distance;
        private int _damage;
        
        private float _currentReloadTime;
        private Player _target;

        // public RifleWeapon(UnitView selfView, float fireRate, float distance, int damage)
        // {
        //     _selfView = selfView;
        //     _fireRate = fireRate;
        //     _distance = distance;
        //     _damage = damage;
        //
        //     _selfView.AnimatorEventHandler.FireMoment += OnTakeDamage;
        // }

        public void Fire()
        {
            
        }

        // public void Fire(Player target)
        // {
        //     _target = target;
        //     if (_currentReloadTime < _fireRate)
        //     {
        //         _currentReloadTime += Time.deltaTime;
        //     }
        //
        //     if (Vector3.Distance(_selfView.transform.position, target.transform.position) > _distance)
        //     {
        //         return;
        //     }
        //     
        //     _selfView.transform.LookAt(target.transform.position);
        // }
        //
        // private void OnTakeDamage()
        // {
        //     _target.Model.Destroyable.TakeDamage(_damage);
        // }
    }
}