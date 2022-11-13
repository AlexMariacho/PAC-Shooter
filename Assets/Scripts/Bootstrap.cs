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
        private PlayerFactory _playerFactory;

        private void Start()
        {
            _playerFactory = new PlayerFactory(_playerPrefab, _rootObjects.Units);
            var player = _playerFactory.Create(new UiInput(_camera));
            _virtualCamera.Follow = player.transform;
            _virtualCamera.LookAt = player.transform;
            var dummy = _playerFactory.Create(new DummyInput());
            dummy.transform.position = new Vector3(10, 0, 0);
        }
    }
}