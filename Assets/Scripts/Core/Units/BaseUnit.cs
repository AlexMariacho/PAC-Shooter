using Mirror;

namespace Shooter.Core
{
    public abstract class BaseUnit
    {
        public UnitModel Model { get; set; }
        public abstract void Reset();
    }
}