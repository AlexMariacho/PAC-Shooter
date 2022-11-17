using System;
using Mirror;

namespace Shooter.Ui.Menu.Controllers
{
    public class StartMenuController : IController
    {
        public event Action HostGame;
        public event Action JoinGame;
        
        private StartMenuView _view;
        private NetworkManager _networkManager;
        
        public StartMenuController(StartMenuView view, NetworkManager networkManager)
        {
            _view = view;
            _networkManager = networkManager;
        }

        public void Activate()
        {
            _view.Show();
            _view.CreateGame += OnCreateGame;
            _view.JoinGame += OnJoinGame;
        }

        public void Deactivate()
        {
            _view.Hide();
            _view.CreateGame -= OnCreateGame;
            _view.JoinGame -= OnJoinGame;
        }

        private void OnCreateGame()
        {
            _networkManager.StartHost();
            HostGame?.Invoke();
            Deactivate();
        }

        private void OnJoinGame()
        {
            JoinGame?.Invoke();
            Deactivate();
        }
    }
}