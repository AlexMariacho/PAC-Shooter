using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Mirror;
using UnityEngine;
using UnityEngine.AI;
using Zenject;
using Random = UnityEngine.Random;

namespace Shooter.Core
{
    public class GameController : IDisposable
    {
        private PlayerSpawner _playerSpawner;
        private Dictionary<IDestroyable, Player> _players = new Dictionary<IDestroyable, Player>();

        public GameController(PlayerSpawner spawner)
        {
            _playerSpawner = spawner;

            _playerSpawner.Spawn += OnSpawnNewPlayer;
            _playerSpawner.DeSpawn += OnDeSpawnPlayer;
        }
        
        public async UniTask StartGame(CancellationToken cancellationToken)
        {
            //some game rules
            await UniTask.WaitUntil(() => false, cancellationToken: cancellationToken);
        }
        
        private void OnSpawnNewPlayer(Player player)
        {
            if (!_players.ContainsKey(player.DestroyableComponent))
            {
                _players[player.DestroyableComponent] = player;
                player.DestroyableComponent.Death += OnDeathPlayer;
                SetRandomPosition(player);
            }
        }

        [Command]
        private void SetRandomPosition(Player player)
        {
            float randomRadius = 5f;
            Vector3 randomPosition = new Vector3(Random.Range(-randomRadius, randomRadius), 0,
                Random.Range(-randomRadius, randomRadius));
            NavMeshHit hit;
            NavMesh.SamplePosition(randomPosition, out hit, 15, 1);
            Vector3 finalPosition = hit.position;

            player.transform.position = finalPosition;
            player.Reset();
        }
        
        private void OnDeSpawnPlayer(Player player)
        {
            if (_players.ContainsKey(player.DestroyableComponent))
            {
                _players.Remove(player.DestroyableComponent);
                player.DestroyableComponent.Death -= OnDeathPlayer;
                player.Dispose();
            }
        }

        private void OnDeathPlayer(IDestroyable destroyable)
        {
            RespawnPlayer(_players[destroyable]).Forget();
        }

        private async UniTask RespawnPlayer(Player player)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(3));
            SetRandomPosition(player);
        }

        public void Dispose()
        {
            _playerSpawner.Spawn -= OnSpawnNewPlayer;
            _playerSpawner.DeSpawn -= OnDeSpawnPlayer;
            
            foreach (var player in _players.Values)
            {
                player.DestroyableComponent.Death -= OnDeathPlayer;
                player.Dispose();
            }
            
            _players.Clear();
        }
    }
}