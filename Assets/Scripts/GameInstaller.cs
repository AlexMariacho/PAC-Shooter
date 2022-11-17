using Cinemachine;
using Core;
using Mirror;
using Network;
using Shooter.Core;
using UnityEngine;
using Zenject;

namespace Shooter
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private RootObjects _rootObjects;
        [SerializeField] private NetworkSpawner _networkSpawner;
        [SerializeField] private CinemachineVirtualCamera _virtualCamera;
        [SerializeField] private PlayerConfiguration _playerConfiguration;
        [SerializeField] private NetworkManager _networkManager;
        
        public override void InstallBindings()
        {
            Container.Bind<Camera>().FromInstance(_camera).NonLazy();
            Container.Bind<CinemachineVirtualCamera>().FromInstance(_virtualCamera).NonLazy();
            Container.Bind<PlayerConfiguration>().FromInstance(_playerConfiguration).NonLazy();
            Container.Bind<RootObjects>().FromInstance(_rootObjects).NonLazy();
            Container.Bind<NetworkSpawner>().FromInstance(_networkSpawner).AsSingle();

            Container.Bind<PlayerSpawner>().AsSingle().NonLazy();
            Container.Bind<NetworkManager>().FromInstance(_networkManager).AsSingle().NonLazy();
        }

    }
}