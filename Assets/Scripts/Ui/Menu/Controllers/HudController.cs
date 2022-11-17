using System;
using Mirror;

namespace Shooter.Ui.Menu.Controllers
{
    public class HudController : IController
    {
        public event Action BackToMenu;
        private GameHudView _view;
        private NetworkManager _networkManager;

        public HudController(GameHudView view, NetworkManager networkManager)
        {
            _view = view;
            _networkManager = networkManager;
        }

        public void Activate()
        {
            _view.Show();
            _view.Exit += OnExitClick;
        }

        public void Deactivate()
        {
            _view.Hide();
            _view.Exit -= OnExitClick;
        }

        private void OnExitClick()
        {
            if (NetworkServer.active && NetworkClient.isConnected)
            {
                _networkManager.StopHost();
            }
            else if (NetworkClient.isConnected)
            {
                _networkManager.StopClient();
            }
            BackToMenu?.Invoke();
            Deactivate();
        }
    }
}