using Cinemachine;
using Core;
using Shooter.Core;
using UnityEngine;

namespace Shooter
{
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] private RootObjects _rootObjects;
        [SerializeField] private Camera _camera;
        [SerializeField] private CinemachineVirtualCamera _virtualCamera;
        [SerializeField] private Player _playerPrefab;
        private PlayerSpawner _playerSpawner;

        private async void Start()
        {
            // _playerFactory = new PlayerFactory(_playerPrefab, _rootObjects.Units);
            // var player = _playerFactory.Create(new UiInput(_camera));
            // _virtualCamera.Follow = player.transform;
            // _virtualCamera.LookAt = player.transform;
            //
            // var ctl = new CancellationTokenSource();
            // using (GameController controller = new GameController(_playerFactory))
            // {
            //     try
            //     {
            //         await controller.StartGame(ctl.Token);
            //     }
            //     catch (Exception e)
            //     {
            //         if (!(e is OperationCanceledException))
            //         {
            //             Console.WriteLine(e);
            //             throw;
            //         }
            //     }
            //     finally
            //     {
            //         Debug.Log("End Game");
            //     }
            // }
            //

        }
    }
}