using Mirror;

namespace Shooter.Core
{
    public abstract class BaseUnit : NetworkBehaviour
    {
        public UnitModel Model { get; protected set; }
        public abstract void Reset();
    }
}