using System;
using UnityEngine;

namespace Network
{
    public class NetworkSpawner : MonoBehaviour
    {
        //todo: Издержки использования Mirror network
        public static NetworkSpawner Instance
        {
            get
            {
                if (_instance == null)
                {
                    Debug.LogError("Instance not existing!");
                    return null;
                }

                return _instance;
            }
            private set
            {
                if (_instance == null)
                    _instance = value;
                else
                {
                    if (_instance != value)
                    {
                        Debug.LogError("NetworkSpawner exist more 1!");
                        Destroy(value.gameObject);
                    }
                }
            }
        }
        private static NetworkSpawner _instance;
        
        public event Action<GameObject> Spawn;
        public event Action<GameObject> DeSpawn;

        private void Awake()
        {
            Instance = this;
        }

        public void RegisterSpawn(GameObject gameObject)
        {
            Spawn?.Invoke(gameObject);
        }

        public void RegisterDeSpawn(GameObject gameObject)
        {
            DeSpawn?.Invoke(gameObject);
        }
    }
}