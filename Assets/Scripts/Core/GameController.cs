using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Shooter.Core
{
    public class GameController : IDisposable
    {
        private PlayerSpawner _playerSpawner;
        private Dictionary<IDestroyable, Player> _players = new Dictionary<IDestroyable, Player>();

        public GameController(PlayerSpawner playerSpawner)
        {
            _playerSpawner = playerSpawner;
        }

        public async UniTask StartGame(CancellationToken cancellationToken)
        {
            await UniTask.WaitUntil(() => false, cancellationToken: cancellationToken);
        }
        
        public void Dispose()
        {
            foreach (var playersValue in _players.Values)
            {
                playersValue.Dispose();
            }
            _playerSpawner.Dispose();
            Debug.Log("Game manager DISPOSE");
        }
    }
}