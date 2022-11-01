namespace Shooter.Core
{
    public class BaseUnit
    {
        public IDestroyable Destroyable;
        public IMove MoveComponent;
        public IWeapon Weapon;
    }
}