using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using Zenject;

namespace DefaultNamespace
{
    public class TestCallback : MonoBehaviour
    {
        private Dictionary<uint, GameObject> _clientPrefabs = new Dictionary<uint, GameObject>();

        public delegate void SpawnDelegate(Vector3 position, System.Guid assetId);
        public delegate void UnSpawnDelegate(GameObject spawned);

        private SpawnDelegate _spawnDelegate;
        private UnSpawnDelegate _unSpawnDelegate;
        
        private void Start()
        {
            
        }
        

        
    }
}