using System;
using Cinemachine;
using Cysharp.Threading.Tasks;
using Network;
using Network.Extensions;
using UnityEngine;
using UnityEngine.AI;

namespace Shooter.Core
{
    public sealed class PlayerSpawner : IDisposable
    {
        public event Action<Player> Spawn;
        public event Action<Player> DeSpawn;

        private NetworkSpawner _networkSpawner;
        private Transform _rootObject;
        private Camera _camera;
        private CinemachineVirtualCamera _virtualCamera;

        public PlayerSpawner(Transform rootObject, NetworkSpawner networkServer, Camera camera, CinemachineVirtualCamera virtualCamera)
        {
            _rootObject = rootObject;
            _networkSpawner = networkServer;
            _camera = camera;
            _virtualCamera = virtualCamera;

            _networkSpawner.Spawn += OnSpawn;
            _networkSpawner.DeSpawn += OnDeSpawn;
        }

        private async void OnSpawn(GameObject gameObject)
        {
            if (gameObject.TryGetComponent(out Player player))
            {
                await UniTask.WaitUntil(() => player.IsReady());
                PlayerConfiguration configuration = player.Configuration;
                UnitModel unitModel = new UnitModel();
                unitModel.Destroyable = new PlayerDestroyable(configuration.Hp);

                PlayerModel playerModel = new PlayerModel();
                NavMeshAgent navMeshAgent = player.gameObject.AddComponent<NavMeshAgent>();
                playerModel.Mover = new PlayerNavigation(configuration.MoveSpeed, configuration.AngularSpeed, player.transform,
                    navMeshAgent);
                playerModel.Weapon = player.GetComponent<BaseWeapon>();
                playerModel.Weapon.Initialize(player.transform, player.View.PlayerAnimationController, player.View.AnimatorEventHandler);
                if (player.isLocalPlayer)
                {
                    playerModel.Input = new UiInput(_camera);
                    _virtualCamera.Follow = player.transform;
                    _virtualCamera.LookAt = player.transform;
                }
                else
                {
                    playerModel.Input = new NetworkInput();
                }
                
                Debug.Log("|PlayerSpawner| Create");
                player.InitializeModels(unitModel, playerModel);
                player.transform.SetParent(_rootObject);
            }
        }

        private void OnDeSpawn(GameObject obj)
        {
            if (obj.TryGetComponent(out Player player))
            {
                DeSpawn?.Invoke(player);
            }
        }
        
        public void Dispose()
        {
            
        }
    }
}