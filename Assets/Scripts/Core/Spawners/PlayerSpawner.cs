using System;
using System.Collections.Generic;
using Cinemachine;
using Cysharp.Threading.Tasks;
using Network;
using Network.Extensions;
using Shooter.Core.Factory;
using UnityEngine;
using UnityEngine.AI;
using Zenject;
using Zenject.SpaceFighter;

namespace Shooter.Core
{
    public sealed class PlayerSpawner : IDisposable
    {
        public event Action<Player> Spawn;
        public event Action<Player> DeSpawn;

        private NetworkSpawner _networkSpawner;
        private PlayerFactory _playerFactory;

        private Dictionary<PlayerView, Player> _viewToPlayer = new Dictionary<PlayerView, Player>();

        [Inject]
        public void Construct(NetworkSpawner networkServer, PlayerFactory factory)
        {
            _networkSpawner = networkServer;
            _playerFactory = factory;

            _networkSpawner.Spawn += OnSpawn;
            _networkSpawner.DeSpawn += OnDeSpawn;
        }

        private async void OnSpawn(GameObject gameObject)
        {
            if (gameObject.TryGetComponent(out PlayerView playerView))
            {
                Player player = _playerFactory.Create(playerView);
                _viewToPlayer[playerView] = player;
                Debug.Log("|Player spawner| Spawn");
                Spawn?.Invoke(player);
            }
        }

        private void OnDeSpawn(GameObject obj)
        {
            if (obj.TryGetComponent(out PlayerView playerView))
            {
                if (_viewToPlayer.ContainsKey(playerView))
                {
                    //add remove player
                    Debug.Log("|Player spawner| Despawn");
                    DeSpawn?.Invoke(_viewToPlayer[playerView]);
                }
            }
        }
        
        public void Dispose()
        {
            //todo:
        }
    }
}