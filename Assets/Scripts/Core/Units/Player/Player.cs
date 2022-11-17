using System;
using System.Collections;
using Mirror;
using Network;
using UnityEngine;

namespace Shooter.Core
{
    public class Player : BaseUnit, IDisposable
    {
        [field: SerializeField] public PlayerView View { get; private set; }
        public PlayerModel PlayerModel { get; private set; }
        private PlayerConfiguration _configuration;


        public event Action<PlayerState> ChangeState;
        [field: SerializeField] public PlayerState CurrentState { get; private set; }
        
        [SerializeField] private Vector3 _targetMovePosition;
        private IEnumerator _moveCoroutine;

        private BaseUnit _targetAttack;
        private IEnumerator _attackCoroutine;

        private IEnumerator _checkAttackDistanceCoroutine;

        public void Initialize(PlayerConfiguration configuration, PlayerModel playerModel, IDestroyable destroyableComponent)
        {
            _configuration = configuration;
            PlayerModel = playerModel;
            DestroyableComponent = destroyableComponent;
        }

        public override void Init()
        {
            _targetAttack = null;
            View.PlayerAnimationController.Reset();
            
            DestroyableComponent.Reset();
            DestroyableComponent.Death += OnDeath;
            PlayerModel.Input.Attack += OnAttack;
            PlayerModel.Input.Move += OnMove;
            View.Initialize(this);
            
            SetState(PlayerState.Idle);
        }
        
        private void SetState(PlayerState state)
        {
            View.StopAllCoroutines();
            switch (state)
            {
                case PlayerState.Idle:
                    CurrentState = PlayerState.Idle;
                    break;
                case PlayerState.Move:
                    _moveCoroutine = MoveToPoint(_targetMovePosition);
                    View.StartCoroutine(_moveCoroutine);
                    CurrentState = PlayerState.Move;
                    break;
                case PlayerState.Attack:
                    _attackCoroutine = AttackTarget(_targetAttack);
                    _checkAttackDistanceCoroutine = CheckAttackDistance(_targetAttack);
                    View.StartCoroutine(_attackCoroutine);
                    View.StartCoroutine(_checkAttackDistanceCoroutine);
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
            while (target.DestroyableComponent.Hp > 0)
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

        public void Dispose()
        {
            PlayerModel.Input.Attack -= OnAttack;
            PlayerModel.Input.Move -= OnMove;
            DestroyableComponent.Death -= OnDeath;
        }
        
        #region Mirror network

        public override void OnStartClient()
        {
            NetworkSpawner.Instance.RegisterSpawn(this.gameObject);
        }

        public override void OnStopClient()
        {
            NetworkSpawner.Instance.RegisterDeSpawn(this.gameObject);
        }

        [Command]
        private void OnAttack(BaseUnit target)
        {
            if (target != this &&
                Vector3.Distance(transform.position, target.transform.position) < PlayerModel.Weapon.Configuration.Distance)
            {
                ReceiveAttackCommand(target);
            }
        }

        private void OnDeath(IDestroyable destroyable)
        {
            SendDeathCommand();
        }

        [Command]
        private void OnMove(Vector3 point)
        {
            ReceiveMoveCommand(point);
        }

        [Command]
        private void SendDeathCommand()
        {
            ReceiveDeathCommand();
        }


        [ClientRpc]
        private void ReceiveDeathCommand()
        {
            if (DestroyableComponent.Hp > 0)
                DestroyableComponent.TakeDamage(DestroyableComponent.Hp);
            SetState(PlayerState.Death);
        }

        [ClientRpc]
        private void ReceiveMoveCommand(Vector3 point)
        {
            _targetMovePosition = point;
            SetState(PlayerState.Move);
        }

        [ClientRpc]
        private void ReceiveAttackCommand(BaseUnit target)
        {
            _targetAttack = target;
            SetState(PlayerState.Attack);
        }
        
        
        
        #endregion
        
    }
    
}