using Core.Configurations;
using Core.Input;
using Shooter.Core;
using UnityEngine;
using Zenject;

namespace Core
{
    public class PlayerFactory : PlaceholderFactory<IUnitInput, PlayerConfiguration, WorldContainer, Player>
    {
        private RootObjects _rootObjects;

        [Inject]
        private void Construct(RootObjects rootObjects)
        {
            _rootObjects = rootObjects;
        }

        public Player Create(IUnitInput input, PlayerConfiguration configuration, WorldContainer container)
        {
            var view = GameObject.Instantiate(configuration.View, _rootObjects.Units, true);
            Player player = new Player(input, view, configuration, container);
            container.RegisterUnit(player);
            return player;
        }
    }
}