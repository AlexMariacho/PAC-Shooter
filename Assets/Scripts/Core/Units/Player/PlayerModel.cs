namespace Shooter.Core
{
    public class PlayerModel
    {
        public PlayerView View { get; set; }
        public BaseWeapon Weapon { get; set; }
        public IMove Mover { get; set; }
        public IUnitInput Input { get; set; }
    }
}