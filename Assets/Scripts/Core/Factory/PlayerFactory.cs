using Core.Input;
using Shooter.Core;
using Shooter.Simple.Units;
using UnityEngine;
using UnityEngine.AI;

namespace Core.Factory
{
    public class PlayerFactory : BaseFactory<Player>
    {
        private RootObjects _rootObjects;
        private Player _playerPrefab;
        private IUnitInput _input;
        
        public PlayerFactory(Player playerPrefab, RootObjects rootObjects)
        {
            _rootObjects = rootObjects;
            _playerPrefab = playerPrefab;
            _input = new DummyInput();
        }

        public override Player Create()
        {
            Player player = GameObject.Instantiate(_playerPrefab, _rootObjects.Units);
            PlayerConfiguration configuration = player.Configuration;
            UnitModel unitModel = new UnitModel();
            unitModel.Destroyable = new PlayerDestroyable(configuration.Hp);

            PlayerModel playerModel = new PlayerModel();
            NavMeshAgent navMeshAgent = player.gameObject.AddComponent<NavMeshAgent>();
            playerModel.Mover = new PlayerNavigation(configuration.MoveSpeed, configuration.AngularSpeed, player.transform,
                navMeshAgent);
            playerModel.Weapon = new RifleWeapon();
            playerModel.Input = _input;
            
            player.Initialize(unitModel, playerModel);
            return player;
        }

        public Player Create(IUnitInput input)
        {
            _input = input;
            return Create();
        }
        
    }
}