using Mirror;
using UnityEngine;

namespace Shooter.Core
{
    public abstract class BaseUnit
    {
        public IDestroyable DestroyableComponent { get; protected set; }
        public Transform Transform { get; protected set; }
        public abstract void Reset();
    }
}