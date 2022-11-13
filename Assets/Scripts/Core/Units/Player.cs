using System;
using System.Collections;
using UnityEngine;

namespace Shooter.Core
{
    [RequireComponent(typeof(BaseWeapon))]
    public class Player : BaseUnit
    {
        [field: SerializeField] public PlayerConfiguration Configuration { get; private set; }
        [field: SerializeField] public PlayerView View { get; private set; }

        public event Action<PlayerState> ChangeState;

        public PlayerModel PlayerModel { get; private set; }
        public PlayerState CurrentState { get; private set; }
        
        private Vector3 _targetMovePosition;
        private IEnumerator _moveCoroutine;

        private BaseUnit _targetAttack;
        private IEnumerator _attackCoroutine;
        private IEnumerator _checkAttackDistanceCoroutine;

        public void Initialize(UnitModel model, PlayerModel playerModel)
        {
            Model = model;
            PlayerModel = playerModel;
            View.Initialize(this);
            
            PlayerModel.Input.Attack += OnAttack;
            PlayerModel.Input.Move += OnMove;
            Model.Destroyable.Death += OnDeath;

            SetState(PlayerState.Idle);
        }

        private void OnAttack(BaseUnit targetView)
        {
            if (targetView != this &&
                Vector3.Distance(transform.position, targetView.transform.position) < PlayerModel.Weapon.Configuration.Distance)
            {
                _targetAttack = targetView;
                SetState(PlayerState.Attack);
            }
        }

        private void OnMove(Vector3 point)
        {
            _targetMovePosition = point;
            SetState(PlayerState.Move);
        }

        private void OnDeath()
        {
            SetState(PlayerState.Death);
        }

        private void SetState(PlayerState state)
        {
            StopAllCoroutines();
            switch (state)
            {
                case PlayerState.Idle:
                    CurrentState = PlayerState.Idle;
                    break;
                case PlayerState.Move:
                    _moveCoroutine = MoveToPoint(_targetMovePosition);
                    StartCoroutine(_moveCoroutine);
                    CurrentState = PlayerState.Move;
                    break;
                case PlayerState.Attack:
                    _attackCoroutine = AttackTarget(_targetAttack);
                    _checkAttackDistanceCoroutine = CheckAttackDistance(_targetAttack);
                    StartCoroutine(_attackCoroutine);
                    StartCoroutine(_checkAttackDistanceCoroutine);
                    CurrentState = PlayerState.Attack;
                    break;
                case PlayerState.Death:
                    CurrentState = PlayerState.Death;
                    Dispose();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }
            ChangeState?.Invoke(CurrentState);
        }

        private IEnumerator MoveToPoint(Vector3 point)
        {
            PlayerModel.Mover.Move(point);
            yield return new WaitUntil(() => Vector3.Distance(transform.position, point) < 0.1f);
            SetState(PlayerState.Idle);
        }

        private IEnumerator AttackTarget(BaseUnit target)
        {
            PlayerModel.Mover.Move(transform.position);
            while (target.Model.Destroyable.Hp > 0)
            {
                PlayerModel.Weapon.Fire(target);
                yield return null;
            }
            SetState(PlayerState.Idle);
        }

        private IEnumerator CheckAttackDistance(BaseUnit target)
        {
            yield return new WaitUntil(() =>
                Vector3.Distance(transform.position, target.transform.position) > PlayerModel.Weapon.Configuration.Distance);
            SetState(PlayerState.Idle);
        }
        
        private void Dispose()
        {
            PlayerModel.Input.Attack -= OnAttack;
            PlayerModel.Input.Move -= OnMove;
            Model.Destroyable.Death -= OnDeath;
        }

    }

    [Serializable]
    public class PlayerConfiguration
    {
        public int Hp;
        public float MoveSpeed;
        public float AngularSpeed;
    }

    public enum PlayerState
    {
        Idle,
        Move,
        Attack,
        Death
    }
}