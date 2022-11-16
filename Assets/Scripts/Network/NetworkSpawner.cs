using System;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

namespace Network
{
    public class NetworkSpawner : InterestManagement
    {
        public event Action<GameObject> Spawn;
        public event Action<GameObject> DeSpawn;
        
        public override bool OnCheckObserver(NetworkIdentity identity, NetworkConnectionToClient newObserver)
        {
            return true;
        }

        public override void OnRebuildObservers(NetworkIdentity identity, HashSet<NetworkConnectionToClient> newObservers)
        {
        }

        public override void OnSpawned(NetworkIdentity identity)
        {
            Spawn?.Invoke(identity.gameObject);
            Debug.Log($"|NetworkFactory| Create object");
        }

        public override void OnDestroyed(NetworkIdentity identity)
        {
            DeSpawn?.Invoke(identity.gameObject);
            Debug.Log($"|NetworkFactory| Destroy object");
        }
    }
}