using System;
using UnityEngine;

namespace Shooter.Core
{
    [RequireComponent(typeof(Animator))]
    public class PlayerAnimationController : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        private Player _player;
        
        private static readonly int FireRate = Animator.StringToHash("FireRate");
        private static readonly int IsMove = Animator.StringToHash("IsMove");
        private static readonly int IsAttack = Animator.StringToHash("IsAttack");
        private static readonly int IsDeath = Animator.StringToHash("IsDeath");
        
        private static readonly int IdleState = Animator.StringToHash("Idle");
        
        public void Initialize(Player player)
        {
            _player = player;
            _player.ChangeState += OnChangePlayerState;
        }

        public void Reset()
        {
            _animator.SetBool(IsMove, false);
            _animator.SetBool(IsAttack, false);
            _animator.SetBool(IsDeath, false);
            
            _animator.Play(IdleState);
        }

        public void SetFireRate(float fireRate)
        {
            _animator.SetFloat(FireRate, fireRate);
        }

        private void OnChangePlayerState(PlayerState state)
        {
            switch (state)
            {
                case PlayerState.Idle:
                    _animator.SetBool(IsMove, false);
                    _animator.SetBool(IsAttack, false);
                    break;
                case PlayerState.Move:
                    _animator.SetBool(IsMove, true);
                    _animator.SetBool(IsAttack, false);
                    break;
                case PlayerState.Attack:
                    _animator.SetBool(IsMove, false);
                    _animator.SetBool(IsAttack, true);
                    break;
                case PlayerState.Death:
                    _animator.SetBool(IsDeath, true);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }
        }

        private void OnDestroy()
        {
            if (_player != null)
            {
                _player.ChangeState -= OnChangePlayerState;
            }
        }
    }
}