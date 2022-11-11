using System;
using UnityEngine;

namespace Shooter.Simple.Units
{
    [RequireComponent(typeof(Player)),
    RequireComponent(typeof(Animator))]
    public class PlayerAnimation : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        private Player _player;
        
        private const string KeyMoveAnimation = "IsMove";
        private const string KeyAttackAnimation = "IsAttack";
        private const string KeyDeathAnimation = "IsDeath";

        public void Initialize(Player player)
        {
            _player = player;
            _player.ChangeState += OnChangePlayerState;
        }

        private void OnChangePlayerState(PlayerState state)
        {
            switch (state)
            {
                case PlayerState.Idle:
                    _animator.SetBool(KeyMoveAnimation, false);
                    _animator.SetBool(KeyAttackAnimation, false);
                    break;
                case PlayerState.Move:
                    _animator.SetBool(KeyMoveAnimation, true);
                    _animator.SetBool(KeyAttackAnimation, false);
                    break;
                case PlayerState.Attack:
                    _animator.SetBool(KeyMoveAnimation, false);
                    _animator.SetBool(KeyAttackAnimation, true);
                    break;
                case PlayerState.Death:
                    _animator.SetBool(KeyDeathAnimation, true);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }
        }

        private void OnDestroy()
        {
            _player.ChangeState -= OnChangePlayerState;
        }
    }
}