using Core.Input;
using Shooter.Core;

namespace Shooter.Simple.Units
{
    public class PlayerModel
    {
        public IWeapon Weapon { get; set; }
        public IMove Mover { get; set; }
        public IUnitInput Input { get; set; }
    }
}