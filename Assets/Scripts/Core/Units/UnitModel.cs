using UnityEngine;

namespace Shooter.Core
{
    public class UnitModel
    {
        public IDestroyable Destroyable { get; set; }
        public Transform Transform { get; set; }
    }
}