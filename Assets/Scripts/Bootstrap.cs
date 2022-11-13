using Core;
using Shooter.Core;
using UnityEngine;

namespace Shooter
{
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] public RootObjects _rootObjects;
        [SerializeField] public Camera _camera;
        [SerializeField] private Player _playerPrefab;
        private PlayerFactory _playerFactory;

        private void Start()
        {
            _playerFactory = new PlayerFactory(_playerPrefab, _rootObjects.Units);
            _playerFactory.Create(new UiInput(_camera));
            var dummy = _playerFactory.Create(new DummyInput());
            dummy.transform.position = new Vector3(10, 0, 0);
        }
    }
}