using UnityEngine;

namespace Shooter.Core
{
    public abstract class BaseUnit : MonoBehaviour
    {
        public UnitModel Model { get; protected set; }
        public abstract void Reset();
    }
}