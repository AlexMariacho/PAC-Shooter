using System;
using System.Threading;
using Mirror;
using Shooter.Core;
using Shooter.Ui;
using Shooter.Ui.Menu.Controllers;
using UnityEngine;
using Zenject;

namespace Shooter
{
    public class GameManager : MonoBehaviour
    {
        private UiContext _uiContext;
        private PlayerSpawner _playerSpawner;
        private NetworkManager _networkManager;
        
        private StartMenuController _startMenu;
        private JoinMenuController _joinMenu;
        private HudController _gameHud;

        [Inject]
        private void Construct(UiContext uiContext, PlayerSpawner spawner, NetworkManager networkManager)
        {
            _uiContext = uiContext;
            _playerSpawner = spawner;
            _networkManager = networkManager;

            _startMenu = new StartMenuController(_uiContext.StartMenuView, _networkManager);
            _joinMenu = new JoinMenuController(_uiContext.JoinGameMenuView, _networkManager);
            _gameHud = new HudController(_uiContext.GameHudView, _networkManager);
        }

        private void OnEnable()
        {
            _startMenu.HostGame += OnHostGame;
            _startMenu.JoinGame += OnShowJoinGame;
            _joinMenu.Cancel += OnJoinCancel;
            _joinMenu.Join += OnJoinGame;
            _gameHud.BackToMenu += OnDisconnect;
        }

        private void OnHostGame()
        {
            StartGame();
            _gameHud.Activate();
        }

        private void OnShowJoinGame()
        {
            _joinMenu.Activate();
        }

        private void OnJoinGame()
        {
            StartGame();
            _gameHud.Activate();
        }

        private void OnJoinCancel()
        {
            _startMenu.Activate();
        }

        private void OnDisconnect()
        {
            _startMenu.Activate();
        }

        private void OnDisable()
        {
            _startMenu.HostGame -= OnHostGame;
            _startMenu.JoinGame -= OnShowJoinGame;
            _joinMenu.Cancel -= OnJoinCancel;
            _joinMenu.Join -= OnJoinGame;
            _gameHud.BackToMenu -= OnDisconnect;
        }

        public void Start()
        {
            _startMenu.Activate();
        }

        private async void StartGame()
        {
            using (GameController controller = new GameController(_playerSpawner))
            {
                var clt = new CancellationTokenSource();
                _gameHud.BackToMenu += () => clt.Cancel();
                
                try
                {
                    await controller.StartGame(clt.Token);
                }
                catch (Exception e)
                {
                    if (!(e is OperationCanceledException))
                    {
                        Console.WriteLine(e);
                        throw;
                    }
                }
                finally
                {
                    Debug.Log("End Game");
                }
            }
        }

    }
}