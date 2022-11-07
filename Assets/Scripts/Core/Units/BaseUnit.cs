using System;
using Core.Views;

namespace Shooter.Core
{
    [Serializable]
    public class BaseUnit
    {
        public IDestroyable Destroyable { get; protected set; }
        public UnitView View;
    }


}