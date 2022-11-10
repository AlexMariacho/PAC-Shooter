using System;
using System.Runtime.InteropServices;
using Core;
using Core.Configurations;
using Core.Input;
using Core.Views;
using Zenject;

namespace Shooter.Core
{
    public class Player : BaseUnit, IInitializable, IDisposable
    {
        public IMove MoveComponent;
        public IWeapon Weapon;
        public IUnitInput Input;

        private WorldContainer _worldContainer;
        private PlayerStateMachine _playerStateMachine;

        public Player(IUnitInput input, UnitView view, PlayerConfiguration configuration, WorldContainer worldContainer)
        {
            View = view;
            _worldContainer = worldContainer;
            
            Weapon = new RifleWeapon(view, configuration.FireRate, configuration.Distance, configuration.Damage);
            Destroyable = new PlayerDestroyable();
            MoveComponent = new PlayerNavigation(configuration.MoveSpeed, configuration.AngularSpeed, view.transform, view.NavAgent);
            Input = input;
            
            _playerStateMachine = new PlayerStateMachine(this, _worldContainer);
            _playerStateMachine.Enable();
        }

        public void Dispose()
        {
            _playerStateMachine.Disable();
        }

        [Inject]
        public void Initialize()
        {
            
        }
    }

}