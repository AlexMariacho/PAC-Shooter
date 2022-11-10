using System.Threading.Tasks;
using Core;
using Core.Configurations;
using Core.Views;
using UnityEngine;
using Zenject;

namespace Shooter.Core
{
    public class PlayerStateMachine : IStateMachine
    {
        private WorldContainer _worldContainer;
        private PlayerConfiguration _configuration;
        private bool _isEnabled;
        
        private BaseState _activeState;
        private BaseState _anyState;
        
        private IdleState _idleState;
        private MoveState _moveState;
        private AttackState _attackState;
        private ChaseState _chaseState;
        private DeathState _deathState;

        private MoveInformation _moveInformation;
        private TargetInformation _targetInformation;
        
        public PlayerStateMachine(Player player, WorldContainer container)
        {
            _worldContainer = container;
            player.Input.Move += OnChangeMovePoint;
            player.Input.Attack += OnSetAttackTarget;
            
            _moveInformation = new MoveInformation(player.View.transform.position);
            _targetInformation = new TargetInformation();
            
            CreateStates(player);
            CreateTransitions(player);

            _activeState = _idleState;
        }
        
        private void CreateStates(Player player)
        {
            _anyState = new IdleState(this);
            _idleState = new IdleState(this);
            _deathState = new DeathState(this);
            _moveState = new MoveState(this, player.View.Animator, player.MoveComponent, _moveInformation);
            _attackState = new AttackState(this, _targetInformation, player.Weapon);
        }

        private void CreateTransitions(Player player)
        {
            var playerPosition = player.View.transform.position;
            var attackTarget = _targetInformation.Target;
            
            _idleState.AddTransition(new TransitionInformation(
                () => Vector3.Distance(playerPosition, _moveInformation.TargetPoint) > 0.3f,
                _moveState));
            _idleState.AddTransition(new TransitionInformation(
                () =>
                {
                    return attackTarget != null &&
                           Vector3.Distance(playerPosition, attackTarget.View.transform.position) <
                           _configuration.Distance;
                },
                _attackState));
            
            
            _moveState.AddTransition(new TransitionInformation(
                () => Vector3.Distance(playerPosition, _moveInformation.TargetPoint) < 0.1f,
                _idleState));
            _moveState.AddTransition(new TransitionInformation(
                () =>
                {
                    return attackTarget != null &&
                           Vector3.Distance(playerPosition, attackTarget.View.transform.position) <
                           _configuration.Distance;
                },
                _attackState));
            
        }

        public void SetState(BaseState state)
        {
            _activeState.ExitState();

            _activeState = state;
            _activeState.EnterState();
        }

        public async void Enable()
        {
            _isEnabled = true;
            while (_isEnabled)
            {
                _activeState.UpdateState();
                _anyState.CheckTransitions();
                _activeState.CheckTransitions();
                await Task.Delay(100);
            }
        }

        public void Disable()
        {
            _isEnabled = false;
        }

        private void OnChangeMovePoint(Vector3 point)
        {
            _moveInformation.TargetPoint = point;
            _targetInformation.Target = null;
        }

        private void OnSetAttackTarget(UnitView unitView)
        {
            _targetInformation.Target = _worldContainer.GetUnitByView(unitView);
        }
    }
}