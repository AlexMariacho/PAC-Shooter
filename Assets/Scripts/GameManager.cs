using Core;
using Core.Configurations;
using Core.Input;
using Shooter.Core;
using UnityEngine;
using Zenject;

namespace Shooter
{
    public class GameManager : IInitializable
    {
        private Camera _camera;
        private PlayerConfiguration _configuration;
        private WorldContainer _worldContainer;

        private PlayerFactory _playerFactory;

        [Inject]
        public void Initialize()
        {
        }

        [Inject]
        private void Construct(Camera camera, PlayerConfiguration configuration, WorldContainer worldContainer, PlayerFactory playerFactory)
        {
            _camera = camera;
            _configuration = configuration;
            _worldContainer = worldContainer;
            _playerFactory = playerFactory;
        }

        public void Start()
        {
            var player = _playerFactory.Create(new UiInput(_camera), _configuration, _worldContainer);
            var bot =_playerFactory.Create(new DummyInput(), _configuration, _worldContainer);
            bot.View.transform.position = new Vector3(5, 0, 0);
        }
    }
}