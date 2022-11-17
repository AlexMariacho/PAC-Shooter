using System.Collections;
using UnityEngine;

namespace Shooter.Core
{
    public class RifleWeapon : BaseWeapon
    {
        [SerializeField] private LineRenderer _fireLine;
        [SerializeField] private Transform _firePoint;
        private PlayerAnimationController _playerAnimationController;
        
        private BaseUnit _target;
        private Transform _selfTransform;
        private AnimatorEventHandler _eventHandler;

        public override void Initialize(Transform selfView, PlayerAnimationController playerAnimationController, AnimatorEventHandler eventHandler)
        {
            _selfTransform = selfView;
            
            _playerAnimationController = playerAnimationController;
            _playerAnimationController.SetFireRate(Configuration.FireRate);

            _eventHandler = eventHandler;
            _eventHandler.FireMoment += OnTakeDamage;
        }

        public override void Fire(BaseUnit target)
        {
            _target = target;
            
            if (Vector3.Distance(_selfTransform.position, target.transform.position) > Configuration.Distance)
            {
                return;
            }
            
            _selfTransform.LookAt(target.transform.position);
        }
        
        private void OnTakeDamage()
        {
            _target.DestroyableComponent.TakeDamage(Configuration.Damage);
            StartCoroutine(ShowFireLine(_target.transform.position));
        }

        private IEnumerator ShowFireLine(Vector3 targetPosition)
        {
            _fireLine.gameObject.SetActive(true);
            _fireLine.SetPosition(0, _firePoint.transform.position);
            _fireLine.SetPosition(1, targetPosition);
            yield return new WaitForSeconds(0.1f);
            _fireLine.gameObject.SetActive(false);
        }

    } 
}