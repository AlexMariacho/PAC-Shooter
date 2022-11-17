using Mirror;
using UnityEngine;

namespace Network
{
    public struct MoveMessage : NetworkMessage
    {
        public uint NetId;
        public Vector3 MovePosition;
    }

    public struct AttackMessage : NetworkMessage
    {
        public uint NetId;
        public uint TargetNetId;
    }

    public struct DeathPlayerMessage : NetworkMessage
    {
        public uint NetId;
    }
}