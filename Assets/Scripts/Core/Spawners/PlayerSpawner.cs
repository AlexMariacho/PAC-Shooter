using System;
using Cinemachine;
using Core;
using Network;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Shooter.Core
{
    public sealed class PlayerSpawner
    {
        public event Action<Player> Spawn;
        public event Action<Player> DeSpawn;

        private NetworkSpawner _networkSpawner;

        private RootObjects _root;
        private PlayerConfiguration _configuration;
        private Camera _camera;
        private CinemachineVirtualCamera _virtualCamera;

        [Inject]
        public void Construct(NetworkSpawner networkSpawner, RootObjects root, PlayerConfiguration configuration, Camera camera, CinemachineVirtualCamera virtualCamera)
        {
            _networkSpawner = networkSpawner;
            _root = root;
            _configuration = configuration;
            _camera = camera;
            _virtualCamera = virtualCamera;
            
            _networkSpawner.Spawn += OnSpawn;
            _networkSpawner.DeSpawn += OnDeSpawn;
        }

        private void OnSpawn(GameObject gameObject)
        {
            if (gameObject.TryGetComponent(out Player spawnedPlayer))
            {
                Player player = Initialize(spawnedPlayer);
                Spawn?.Invoke(player);
            }
        }

        private void OnDeSpawn(GameObject obj)
        {
            if (obj.TryGetComponent(out Player deSpawnPlayer))
            {
                DeSpawn?.Invoke(deSpawnPlayer);
            }
        }

        private Player Initialize(Player spawnedPlayer)
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

            return spawnedPlayer;
        }
        
    }
}