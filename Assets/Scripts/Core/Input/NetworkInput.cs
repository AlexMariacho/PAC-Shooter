using System;
using Mirror;
using UnityEngine;

namespace Shooter.Core
{
    public class NetworkInput : NetworkBehaviour, IUnitInput
    {
        public event Action<Vector3> Move;
        public event Action<BaseUnit> Attack;
        
        
    }
}