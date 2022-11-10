using Core;
using Core.Configurations;
using Core.Input;
using Shooter.Core;
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
            Container.Bind<Camera>().FromInstance(_camera).NonLazy();
            Container.Bind<PlayerConfiguration>().FromInstance(_playerConfiguration).NonLazy();
            Container.Bind<RootObjects>().FromInstance(_rootObjects).NonLazy();

            var worldContainer = new WorldContainer();
            Container.Bind<WorldContainer>().FromInstance(worldContainer);
            Container.BindInterfacesAndSelfTo<GameManager>().AsSingle();
            
            //Container.Bind<IInitializable>().To<Player>().AsSingle();
            //Container.BindInterfacesAndSelfTo<Player>();
            //Container.Bind<IInitializable>().To<TestInject>().AsSingle();
            //Container.BindFactory<IUnitInput, TestInject, TestFactory>().AsSingle();

            Container.BindFactory<IUnitInput, PlayerConfiguration, WorldContainer, Player, PlayerFactory>().AsSingle();

        }
        
    }
}