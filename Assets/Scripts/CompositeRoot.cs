using Cinemachine;
using Core;
using Network;
using Shooter.Core;
using UnityEngine;
using Zenject;
using Player = Shooter.Core.Player;

namespace Shooter
{
    public class CompositeRoot : MonoInstaller
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private RootObjects _rootObjects;
        [SerializeField] private Player _playerPrefab;
        [SerializeField] private NetworkSpawner _networkSpawner;
        [SerializeField] private CinemachineVirtualCamera _virtualCamera;

        public override void InstallBindings()
        {
            var factory = new PlayerSpawner(_rootObjects.Units.transform, _networkSpawner, _camera, _virtualCamera);
            Container.Bind<PlayerSpawner>().FromInstance(factory).AsSingle();
            
            //Container.BindInterfacesAndSelfTo<MirrorNetworkManager>().FromInstance(_networkManager).AsSingle();
            

        }
        
    }
}