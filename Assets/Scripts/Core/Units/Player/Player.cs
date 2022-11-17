using System;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Shooter.Core
{
    public class Player : BaseUnit, IDisposable
    {
        public PlayerConfiguration Configuration { get; private set; }
        public PlayerView View { get => PlayerModel.View; }

        public event Action<PlayerState> ChangeState;

        public PlayerModel PlayerModel { get; private set; }
        public PlayerState CurrentState { get; private set; }
        
        private Vector3 _targetMovePosition;
        private IEnumerator _moveCoroutine;

        private BaseUnit _targetAttack;
        private IEnumerator _attackCoroutine;

        public Player(PlayerConfiguration configuration, PlayerModel playerModel, IDestroyable destroyableComponent, Transform transform)
        {
            Configuration = configuration;
            PlayerModel = playerModel;
            DestroyableComponent = destroyableComponent;
            Transform = transform;

            DestroyableComponent.Death += OnDeath;
            View.Initialize(this);

            PlayerModel.Input.Attack += OnAttack;
            PlayerModel.Input.Move += OnMove;
            
            //SetState(PlayerState.Idle);
        }

        public override void Reset()
        {
            _targetAttack = null;
            int maxHp = DestroyableComponent.MaxHp;
            DestroyableComponent = new PlayerDestroyable(maxHp);
            View.Initialize(this);
            
            //SetState(PlayerState.Idle);
        }

        private void OnAttack(BaseUnit targetView)
        {
            if (targetView != this &&
                Vector3.Distance(Transform.position, targetView.Transform.position) < PlayerModel.Weapon.Configuration.Distance)
            {
                _targetAttack = targetView;
                //SetState(PlayerState.Attack);
            }
        }

        private void OnMove(Vector3 point)
        {
            _targetMovePosition = point;
            //SetState(PlayerState.Move);
        }

        private void OnDeath()
        {
            //SetState(PlayerState.Death);
        }

        // private void SetState(PlayerState state)
        // {
        //     StopAllCoroutines();
        //     switch (state)
        //     {
        //         case PlayerState.Idle:
        //             CurrentState = PlayerState.Idle;
        //             break;
        //         case PlayerState.Move:
        //             _moveCoroutine = MoveToPoint(_targetMovePosition);
        //             StartCoroutine(_moveCoroutine);
        //             CurrentState = PlayerState.Move;
        //             break;
        //         case PlayerState.Attack:
        //             _attackCoroutine = AttackTarget(_targetAttack);
        //             _checkAttackDistanceCoroutine = CheckAttackDistance(_targetAttack);
        //             StartCoroutine(_attackCoroutine);
        //             StartCoroutine(_checkAttackDistanceCoroutine);
        //             CurrentState = PlayerState.Attack;
        //             break;
        //         case PlayerState.Death:
        //             CurrentState = PlayerState.Death;
        //             Dispose();
        //             break;
        //         default:
        //             throw new ArgumentOutOfRangeException(nameof(state), state, null);
        //     }
        //     ChangeState?.Invoke(CurrentState);
        // }
        //
        // private IEnumerator MoveToPoint(Vector3 point)
        // {
        //     PlayerModel.Mover.Move(point);
        //     yield return new WaitUntil(() => Vector3.Distance(transform.position, point) < 0.1f);
        //     SetState(PlayerState.Idle);
        // }
        //
        // private IEnumerator AttackTarget(BaseUnit target)
        // {
        //     PlayerModel.Mover.Move(transform.position);
        //     while (target.Model.Destroyable.Hp > 0)
        //     {
        //         PlayerModel.Weapon.Fire(target);
        //         yield return null;
        //     }
        //     SetState(PlayerState.Idle);
        // }
        //
        // private IEnumerator CheckAttackDistance(BaseUnit target)
        // {
        //     yield return new WaitUntil(() =>
        //         Vector3.Distance(transform.position, target.transform.position) > PlayerModel.Weapon.Configuration.Distance);
        //     SetState(PlayerState.Idle);
        // }

        public void Dispose()
        {
            PlayerModel.Input.Attack -= OnAttack;
            PlayerModel.Input.Move -= OnMove;
            DestroyableComponent.Death -= OnDeath;
        }
    }
    
}