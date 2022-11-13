using UnityEngine;
using UnityEngine.AI;

namespace Shooter.Core
{
    public sealed class PlayerFactory : BaseFactory<Player>
    {
        private Transform _rootObject;
        private Player _playerPrefab;
        private IUnitInput _input;
        
        public PlayerFactory(Player playerPrefab, Transform rootObject)
        {
            _rootObject = rootObject;
            _playerPrefab = playerPrefab;
            _input = new DummyInput();
        }

        public override Player Create()
        {
            Player player = GameObject.Instantiate(_playerPrefab, _rootObject);
            PlayerConfiguration configuration = player.Configuration;
            UnitModel unitModel = new UnitModel();
            unitModel.Destroyable = new PlayerDestroyable(configuration.Hp);

            PlayerModel playerModel = new PlayerModel();
            NavMeshAgent navMeshAgent = player.gameObject.AddComponent<NavMeshAgent>();
            playerModel.Mover = new PlayerNavigation(configuration.MoveSpeed, configuration.AngularSpeed, player.transform,
                navMeshAgent);
            playerModel.Weapon = player.GetComponent<BaseWeapon>();
            playerModel.Weapon.Initialize(player.transform, player.View.PlayerAnimation, player.View.AnimatorEventHandler);
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