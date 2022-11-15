using Mirror;
using UnityEngine;
using Zenject.SpaceFighter;

namespace Network
{
    public delegate void ConnectionPlayerHandler(NetworkConnectionToClient conn);
    
    public class MirrorNetworkManager : NetworkManager
    {
        public event ConnectionPlayerHandler ConnectNewPlayer;

        public override void OnServerAddPlayer(NetworkConnectionToClient conn)
        {
            ConnectNewPlayer?.Invoke(conn);
            Debug.Log($"Create player");

            var player = Instantiate(playerPrefab);
            player.name = $"{playerPrefab.name} [connId={conn.connectionId}]";
            // NetworkServer.Spawn(player.gameObject, conn.);
            NetworkServer.AddPlayerForConnection(conn, player.gameObject);
        }   

        public void AddNewPlayer(NetworkConnectionToClient conn, GameObject player)
        {
            player.name = $"{playerPrefab.name} [connId={conn.connectionId}]";
            NetworkServer.AddPlayerForConnection(conn, player.gameObject); ;
        }

    }
}