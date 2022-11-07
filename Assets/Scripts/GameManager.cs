using Core;
using Core.Configurations;
using Core.Input;
using Shooter.Core;
using UnityEngine;

namespace Shooter
{
    public class GameManager
    {
        private Camera _camera;
        private PlayerConfiguration _configuration;
        private RootObjects _rootObjects;

        public GameManager(Camera camera, PlayerConfiguration configuration, RootObjects rootObjects)
        {
            _camera = camera;
            _configuration = configuration;
            _rootObjects = rootObjects;
        }

        public void Start()
        {
            var player = CreatePlayer(new UiInput(_camera), _configuration);
            player.View.transform.SetParent(_rootObjects.Units);
        }

        private Player CreatePlayer(IUnitInput input, PlayerConfiguration configuration)
        {
            var view = GameObject.Instantiate(configuration.View);
            Player player = new Player(input, view, configuration);
            return player;
        }

    }
}