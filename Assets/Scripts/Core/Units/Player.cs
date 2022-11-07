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

        private UnitState _currentState;
        private BaseUnit _target;
        private float _distance;

        public Player(IUnitInput input, UnitView view, PlayerConfiguration configuration)
        {
            View = view;
            
            //Weapon = new RifleWeapon();
            Destroyable = new PlayerDestroyable();
            MoveComponent = new PlayerNavigation(configuration.MoveSpeed, configuration.AngularSpeed, view.transform, view.NavAgent);
            Input = input;
            
            Input.Attack += OnAttack;
            Input.Move += OnMove;
        }

        private void OnAttack(UnitView obj)
        {
            SetState(Vector3.Distance(View.transform.position, View.transform.position) > _distance
                ? UnitState.MoveToAttack
                : UnitState.Attack);
        }

        private void OnMove(Vector3 obj)
        {
            SetState(UnitState.Move);
        }

        private void SetState(UnitState state)
        {
            
        }

        public void Dispose()
        {
            Input.Attack -= OnAttack;
            Input.Move -= OnMove;
        }
    }

    public enum UnitState
    {
        Idle,
        Move,
        MoveToAttack,
        Attack,
        Death
    }
}