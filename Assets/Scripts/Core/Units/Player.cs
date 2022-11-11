using System;
using System.Collections;
using Core.Views;
using UnityEngine;

namespace Shooter.Simple.Units
{
    public class Player : BaseUnit
    {
        [field: SerializeField] public PlayerConfiguration Configuration { get; private set; }
        [SerializeField] private PlayerView _playerView;
        
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
            _playerView.Initialize(this);
            
            PlayerModel.Input.Attack += OnAttack;
            PlayerModel.Input.Move += OnMove;
            Model.Destroyable.Death += OnDeath;

            SetState(PlayerState.Idle);
        }

        private void OnAttack(BaseUnit targetView)
        {
            if (targetView != this &&
                Vector3.Distance(transform.position, targetView.transform.position) < Configuration.Distance)
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
            Debug.Log($"Set state {state.ToString()}");
        }

        private IEnumerator MoveToPoint(Vector3 point)
        {
            PlayerModel.Mover.Move(point);
            yield return new WaitUntil(() => Vector3.Distance(transform.position, point) < 0.1f);
            SetState(PlayerState.Idle);
        }

        private IEnumerator AttackTarget(BaseUnit target)
        {
            PlayerModel.Weapon.Fire();
            yield return new WaitUntil(() => target.Model.Destroyable.Hp > 0);
            SetState(PlayerState.Idle);
        }

        private IEnumerator CheckAttackDistance(BaseUnit target)
        {
            yield return new WaitUntil(() =>
                Vector3.Distance(transform.position, target.transform.position) < Configuration.Distance);
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
        
        public float FireRate;
        public int Damage;
        public float Distance;
    }

    public enum PlayerState
    {
        Idle,
        Move,
        Attack,
        Death
    }
}