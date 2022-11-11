using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core;
using Core.Configurations;
using Core.Views;
using Shooter.Core.Handlers;
using UnityEngine;
using Zenject;

namespace Shooter.Core
{
    public class PlayerStateMachine : IStateMachine, IDisposable
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

        private Dictionary<TransitionEventType, List<TransitionInformation>> _transitions =
            new Dictionary<TransitionEventType, List<TransitionInformation>>();
        private List<ITransitionEventDeliver> _eventDelivers = new List<ITransitionEventDeliver>();

        private MoveInformation _moveInformation;
        private TargetInformation _targetInformation;
        
        public PlayerStateMachine(Player player, PlayerConfiguration configuration, WorldContainer container)
        {
            _worldContainer = container;
            _configuration = configuration;
            player.Input.Move += OnChangeMovePoint;
            player.Input.Attack += OnSetAttackTarget;
            
            _moveInformation = new MoveInformation(player.View.transform.position);
            _targetInformation = new TargetInformation();
            
            CreateStates(player);
            CreateEventDelivers(player);
            CreateTransitions();

            _activeState = _idleState;
        }
        
        private void CreateStates(Player player)
        {
            _anyState = new IdleState(this);
            _idleState = new IdleState(this);
            _deathState = new DeathState(this);
            _moveState = new MoveState(this, player.View.Animator, player.MoveComponent, _moveInformation);
            _attackState = new AttackState(this, _targetInformation, player.Weapon, player.View.Animator);
        }

        private void CreateEventDelivers(Player player)
        {
            _eventDelivers.Add(new CheckDistanceToPoint(player.View.transform, _moveInformation, 0.2f));
            _eventDelivers.Add(new CheckAttackDistanceToUnit(player.View.transform, _targetInformation, _configuration.Distance));
            _eventDelivers.Add(new CheckAlivePlayer(player.Destroyable));
            
            foreach (var eventDeliver in _eventDelivers)
            {
                eventDeliver.TransitionEvent += OnCheckTransition;
            }
        }

        private void CreateTransitions()
        {
            CreateTransition(_anyState, _deathState, TransitionEventType.Death);
            
            CreateTransition(_idleState, _moveState, TransitionEventType.StartMove);
            CreateTransition(_idleState, _attackState, TransitionEventType.PossibleDistanceForAttack);

            CreateTransition(_moveState, _idleState, TransitionEventType.StopMove);
            CreateTransition(_moveState, _attackState, TransitionEventType.PossibleDistanceForAttack);
            
            CreateTransition(_attackState, _moveState, TransitionEventType.StartMove);
            CreateTransition(_attackState, _idleState, TransitionEventType.ImpossibleDistanceForAttack);
        }

        private void CreateTransition(BaseState from, BaseState to, TransitionEventType eventType)
        {
            var transitionInformation = new TransitionInformation(eventType, from, to);
            if (!_transitions.ContainsKey(eventType))
            {
                _transitions.Add(eventType, new List<TransitionInformation>());
            }
            _transitions[eventType].Add(transitionInformation);
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
                foreach (var eventDeliver in _eventDelivers)
                {
                    eventDeliver.Update();
                }
                _activeState.UpdateState();
                await Task.Delay(100);
            }
        }

        public void Disable()
        {
            _isEnabled = false;
        }

        private void OnCheckTransition(TransitionEventType transitionEvent)
        {
            if (!_transitions.ContainsKey(transitionEvent)) return;
            foreach (var transitionInformation in _transitions[transitionEvent])
            {
                if (transitionInformation.FromState == _activeState ||
                    transitionInformation.FromState == _anyState)
                {
                    SetState(transitionInformation.ToState);
                    return;
                }
            }
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

        public void Dispose()
        {
            foreach (var eventDeliver in _eventDelivers)
            {
                eventDeliver.TransitionEvent -= OnCheckTransition;
            }
        }
    }
}