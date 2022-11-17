using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class TestCallback : NetworkBehaviour
    {
        private Vector3 _position;
        
        private void Update()
        {
            if (isLocalPlayer && Input.GetKey(KeyCode.Space))
            {
                Hola(Vector3.down);
                Debug.Log("Send Hola to server");
            }
        }

        [Command]
        private void Hola(Vector3 position)
        {
            Debug.Log($"Recive Hola from client {position}");
        }

        [TargetRpc]
        private void GetNewPosition(NetworkConnection conn, Vector3 position)
        {
            Debug.Log($"Recieve NEW position {position}");
        }
        
        

        private NetworkConnection GetNetwork()
        {
            if (connectionToServer != null)
                return connectionToServer;

            if (connectionToClient != null)
                return connectionToClient;

            return null;
        }

    }
}