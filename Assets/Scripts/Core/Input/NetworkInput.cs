using System;
using UnityEngine;

namespace Shooter.Core
{
    public class NetworkInput : IUnitInput
    {
        public event Action<Vector3> Move;
        public event Action<BaseUnit> Attack;
        
        
    }
}