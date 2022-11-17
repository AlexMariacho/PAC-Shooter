using Cinemachine;
using Core;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Shooter.Core
{
    public class PlayerFactory : PlaceholderFactory<Player>
    {
        private RootObjects _root;
        private PlayerConfiguration _configuration;
        private Camera _camera;
        private CinemachineVirtualCamera _virtualCamera;
        
        [Inject]
        private void Construct(RootObjects root, PlayerConfiguration configuration, Camera camera, CinemachineVirtualCamera virtualCamera)
        {
            _root = root;
            _configuration = configuration;
            _camera = camera;
            _virtualCamera = virtualCamera;
        }

        public Player Initialize(Player spawnedPlayer)
        {
            PlayerModel playerModel = new PlayerModel();
            NavMeshAgent navMeshAgent = spawnedPlayer.gameObject.AddComponent<NavMeshAgent>();
            playerModel.Mover = new PlayerNavigation(_configuration.MoveSpeed, _configuration.AngularSpeed, spawnedPlayer.transform,
                navMeshAgent);
            playerModel.Weapon = spawnedPlayer.GetComponent<BaseWeapon>();
            playerModel.Weapon.Initialize(
                spawnedPlayer.transform, 
                spawnedPlayer.View.PlayerAnimationController, 
                spawnedPlayer.View.AnimatorEventHandler);
            if (spawnedPlayer.isLocalPlayer)
            {
                playerModel.Input = new UiInput(_camera);
                _virtualCamera.Follow = spawnedPlayer.transform;
                _virtualCamera.LookAt = spawnedPlayer.transform;
            }
            else
            {
                playerModel.Input = new NetworkInput();
            }
            
            spawnedPlayer.transform.SetParent(_root.Units);
            spawnedPlayer.Initialize(_configuration, playerModel, new PlayerDestroyable(_configuration.Hp));
            Debug.Log("|PlayerFactory| Create");

            return spawnedPlayer;
        }
    }
}