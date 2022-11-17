using Mirror;

namespace Shooter.Core
{
    public abstract class BaseUnit : NetworkBehaviour
    {
        public IDestroyable DestroyableComponent { get; protected set; }
        public abstract void Reset();
    }
}