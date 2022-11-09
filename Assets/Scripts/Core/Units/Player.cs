using System;
using Core.Configurations;
using Core.Input;
using Core.Views;
using UnityEngine;

namespace Shooter.Core
{
    public class Player : BaseUnit, IDisposable
    {
        public IMove MoveComponent;
        public IWeapon Weapon;
        public IUnitInput Input;

        private StateMachine _stateMachine;
        private MoveInformation _moveInformation;
        private TargetInformation _targetInformation;
        
        public Player(IUnitInput input, UnitView view, PlayerConfiguration configuration)
        {
            View = view;
            
            Weapon = new RifleWeapon(view, configuration.FireRate, configuration.Distance, configuration.Damage);
            Destroyable = new PlayerDestroyable();
            MoveComponent = new PlayerNavigation(configuration.MoveSpeed, configuration.AngularSpeed, view.transform, view.NavAgent);
            Input = input;
            
            Input.Attack += OnAttack;
            Input.Move += OnMove;

            _moveInformation = new MoveInformation();
            _targetInformation = new TargetInformation();
            _stateMachine = new StateMachine(this, _moveInformation, _targetInformation, configuration);
            _stateMachine.Enable();
        }

        private void OnAttack(UnitView obj)
        {
            
        }

        private void OnMove(Vector3 targetPosition)
        {
            _moveInformation.TargetPoint = targetPosition;
        }
        
        public void Dispose()
        {
            _stateMachine.Disable();
            Input.Attack -= OnAttack;
            Input.Move -= OnMove;
        }

    }

}