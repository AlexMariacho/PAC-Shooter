using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Configurations;

namespace Shooter.Core
{
    public class StateMachine : IStateMachine
    {
        private BaseState _activeState;

        private IdleState _idleState;
        private MoveState _moveState;
        private AttackState _attackState;
        private ChaseState _chaseState;
        private DeathState _deathState;
        
        public StateMachine(Player player, MoveInformation moveInformation, TargetInformation targetInformation,
            PlayerConfiguration playerConfiguration)
        {
            //create states
            _idleState = new IdleState(this);
            _moveState = new MoveState(this, moveInformation, player.MoveComponent, player.View.Animator);
            _chaseState = new ChaseState(this, moveInformation, player.MoveComponent, player.View.Animator);
            _attackState = new AttackState(this, targetInformation, player.Weapon);
            
            
            //create transitions
            _idleState.AddTransition(new TransitionInformation(
                new CheckDistanceToMovePoint(player.View.transform, moveInformation, true), 
                _moveState));
            _idleState.AddTransition(new TransitionInformation(
                new CheckTarget(targetInformation), 
                _chaseState));
            
            
            _moveState.AddTransition(new TransitionInformation(
                new CheckDistanceToMovePoint(player.View.transform, moveInformation, false), 
                _idleState));
            _moveState.AddTransition(new TransitionInformation(
                new CheckTarget(targetInformation), 
                _chaseState));


            _chaseState.AddTransition(new TransitionInformation(
                new CheckDistanceToTarget(player.View.transform, targetInformation, playerConfiguration.Distance), 
                _attackState));


            _attackState.AddTransition(new TransitionInformation(
                new CheckDistanceToTarget(player.View.transform, targetInformation, playerConfiguration.Distance, true), 
                _idleState));
            _attackState.AddTransition(new TransitionInformation(
                new CheckTarget(targetInformation),
                _idleState));

            _activeState = _idleState;
        }

        public void SetState(BaseState state)
        {
            _activeState.ExitState();

            _activeState = state;
            _activeState.EnterState();
        }

        public async void Enable()
        {
            while (true)
            {
                _activeState.UpdateState();
                await Task.Delay(100);
            }
        }

        public void Disable()
        {
            
        }

    }
}