using System;
using System.Threading;
using Network;
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
        private NetworkSpawner _networkSpawner;
        
        private StartMenuController _startMenu;

        [Inject]
        private void Construct(UiContext uiContext, PlayerSpawner playerSpawner, NetworkSpawner networkSpawner)
        {
            _uiContext = uiContext;
            _playerSpawner = playerSpawner;
            _networkSpawner = networkSpawner;
        }

        public async void Start()
        {
            var ctl = new CancellationTokenSource();
            using (GameController controller = new GameController(_playerSpawner))
            {
                try
                {
                    await controller.StartGame(ctl.Token);
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