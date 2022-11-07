using Core.Views;
using UnityEngine;

namespace Shooter.Core
{
    public class RifleWeapon : IWeapon
    {
        private UnitView _selfView;
        private float _fireRate;
        private float _distance;
        private int _damage;
        
        private float _currentReloadTime;
        private BaseUnit _target;

        public RifleWeapon(UnitView selfView, float fireRate, float distance, int damage)
        {
            _selfView = selfView;
            _fireRate = fireRate;
            _distance = distance;
            _damage = damage;

            _selfView.AnimatorEventHandler.FireMoment += OnTakeDamage;
        }

        public void Fire(BaseUnit target)
        {
            _target = target;
            if (_currentReloadTime < _fireRate)
            {
                _currentReloadTime += Time.deltaTime;
            }

            if (Vector3.Distance(_selfView.transform.position, target.View.transform.position) > _distance)
            {
                return;
            }
            
            _selfView.transform.Rotate(target.View.transform.position - _selfView.transform.position);
            _selfView.Animator.SetBool(AnimatorConstantNames.Attack, true);
        }

        private void OnTakeDamage()
        {
            _target.Destroyable.TakeDamage(_damage);
        }
    }
}