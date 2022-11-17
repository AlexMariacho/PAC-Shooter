using System;
using Mirror;

namespace Shooter.Ui.Menu.Controllers
{
    public class JoinMenuController : IController
    {
        public event Action Join;
        public event Action Cancel;
        
        private JoinGameMenuView _view;
        private NetworkManager _networkManager;

        public JoinMenuController(JoinGameMenuView view, NetworkManager networkManager)
        {
            _view = view;
            _networkManager = networkManager;
        }

        public void Activate()
        {
            _view.Show();
            _view.Join += OnJoinGame;
            _view.Cancel += OnCancel;
        }

        public void Deactivate()
        {
            _view.Hide();
            _view.Join -= OnJoinGame;
            _view.Cancel -= OnCancel;
        }

        private void OnJoinGame(string ipaddress)
        {
            _networkManager.networkAddress = ipaddress;
            _networkManager.StartClient();
            Join?.Invoke();
            Deactivate();
        }

        private void OnCancel()
        {
            Cancel?.Invoke();
            Deactivate();
        }
    }
}