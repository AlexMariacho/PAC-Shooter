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
            Container.Bind<WorldContainer>().FromInstance( new WorldContainer());
            
            Container.BindInterfacesAndSelfTo<GameManager>().AsSingle();
            
            Container.BindFactory<IUnitInput, PlayerConfiguration, WorldContainer, Player, PlayerFactory>().AsSingle();

        }
        
    }
}