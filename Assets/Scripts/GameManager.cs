using System;
using System.Threading;
using Cysharp.Threading.Tasks;
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

        private StartMenuUiController _startMenuUi;
        private JoinMenuUiController _joinMenuUi;
        private HudUiController _gameHudUi;

        private CancellationToken _destroyToken;

        [Inject]
        private void Construct(
            UiContext uiContext,
            PlayerSpawner spawner,
            NetworkManager networkManager)
        {
            _uiContext = uiContext;
            _playerSpawner = spawner;
            _networkManager = networkManager;

            _startMenuUi = new StartMenuUiController(_uiContext.StartMenuView, _networkManager);
            _joinMenuUi = new JoinMenuUiController(_uiContext.JoinGameMenuView, _networkManager);
            _gameHudUi = new HudUiController(_uiContext.GameHudView, _networkManager);
        }

        private void Awake()
        {
            _destroyToken = this.GetCancellationTokenOnDestroy();
        }

        private void OnEnable()
        {
            _startMenuUi.HostGame += OnHostGame;
            _startMenuUi.JoinGame += OnShowJoinGame;
            _joinMenuUi.Cancel += OnJoinCancel;
            _joinMenuUi.Join += OnJoinGame;
            _gameHudUi.BackToMenu += OnDisconnect;
        }

        private void OnDisable()
        {
            _startMenuUi.HostGame -= OnHostGame;
            _startMenuUi.JoinGame -= OnShowJoinGame;
            _joinMenuUi.Cancel -= OnJoinCancel;
            _joinMenuUi.Join -= OnJoinGame;
            _gameHudUi.BackToMenu -= OnDisconnect;
        }

        private void Start()
        {
            _startMenuUi.Activate();
        }

        private void OnHostGame()
        {
            RunGameLoopAsync().Forget();
            _gameHudUi.Activate();
        }

        private void OnShowJoinGame()
        {
            _joinMenuUi.Activate();
        }

        private void OnJoinGame()
        {
            RunGameLoopAsync().Forget();
            _gameHudUi.Activate();
        }

        private void OnJoinCancel()
        {
            _startMenuUi.Activate();
        }

        private void OnDisconnect()
        {
            _startMenuUi.Activate();
        }



        private async UniTaskVoid RunGameLoopAsync()
        {
            using var gameCts = new CancellationTokenSource();

            using var linkedCts =
                CancellationTokenSource.CreateLinkedTokenSource(gameCts.Token, _destroyToken);

            void OnBackToMenu()
            {
                if (!gameCts.IsCancellationRequested)
                {
                    gameCts.Cancel();
                }
            }

            _gameHudUi.BackToMenu += OnBackToMenu;

            try
            {
                await StartGameInternalAsync(linkedCts.Token);
            }
            catch (OperationCanceledException)
            {
                Debug.Log("Game canceled.");
            }
            catch (Exception ex)
            {
                Debug.LogError($"Unhandled exception in game loop: {ex}");
            }
            finally
            {
                _gameHudUi.BackToMenu -= OnBackToMenu;
                Debug.Log("End Game");
            }
        }
        
        private async UniTask StartGameInternalAsync(CancellationToken token)
        {
            using var controller = new GameController(_playerSpawner);
            await controller.StartGame(token);
        }

    }
}