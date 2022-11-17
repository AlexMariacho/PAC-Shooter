namespace Shooter.Core
{
    public class PlayerModel
    {
        public BaseWeapon Weapon { get; set; }
        public IMove Mover { get; set; }
        public IUnitInput Input { get; set; }
    }
}