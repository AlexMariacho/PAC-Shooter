using Core;
using Core.Configurations;
using UnityEngine;
using Zenject;

namespace Shooter
{
    public class CompositeRoot : MonoInstaller
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private PlayerConfiguration _playerConfiguration;

        [SerializeField] private RootObjects _rootObjects;

        public override void InstallBindings()
        {
            Container.Bind<RootObjects>().FromInstance(_rootObjects).NonLazy();
            var manager = new GameManager(_camera, _playerConfiguration, _rootObjects);
            Container.Bind<GameManager>().FromInstance(manager).NonLazy();
        }
        
    }
}