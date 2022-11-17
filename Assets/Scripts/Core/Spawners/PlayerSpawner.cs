using System;
using Network;
using UnityEngine;
using Zenject;

namespace Shooter.Core
{
    public sealed class PlayerSpawner
    {
        public event Action<Player> Spawn;
        public event Action<Player> DeSpawn;

        private NetworkSpawner _networkSpawner;
        private PlayerFactory _playerFactory;

        [Inject]
        public void Construct(NetworkSpawner networkServer, PlayerFactory factory)
        {
            _networkSpawner = networkServer;
            _playerFactory = factory;

            _networkSpawner.Spawn += OnSpawn;
            _networkSpawner.DeSpawn += OnDeSpawn;
        }

        private void OnSpawn(GameObject gameObject)
        {
            if (gameObject.TryGetComponent(out Player spawnedPlayer))
            {
                Player player = _playerFactory.Initialize(spawnedPlayer);
                Debug.Log("|Player spawner| Spawn");
                Spawn?.Invoke(player);
            }
        }

        private void OnDeSpawn(GameObject obj)
        {
            if (obj.TryGetComponent(out Player deSpawnPlayer))
            {
                Debug.Log("|Player spawner| Despawn");
                DeSpawn?.Invoke(deSpawnPlayer);
            }
        }

    }
}