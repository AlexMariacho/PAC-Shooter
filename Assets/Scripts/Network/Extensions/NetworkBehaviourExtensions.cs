using Mirror;

namespace Network.Extensions
{
    public static class NetworkBehaviourExtensions
    {
        public static bool IsReady(this NetworkBehaviour behaviour)
        {
            if (behaviour.connectionToClient != null)
                return behaviour.connectionToClient.isReady;

            if (behaviour.connectionToServer != null)
                return behaviour.connectionToServer.isReady;

            return false;
        }
    }
}