using System.Collections.Generic;
using Core.Views;
using Shooter.Core;

namespace Core
{
    public class WorldContainer
    {
        private Dictionary<UnitView, BaseUnit> _viewToUnit = new Dictionary<UnitView, BaseUnit>();

        public void RegisterUnit(BaseUnit unit)
        {
            _viewToUnit[unit.View] = unit;
        }

        public BaseUnit GetUnitByView(UnitView view)
        {
            return _viewToUnit.ContainsKey(view) ? _viewToUnit[view] : null;
        }

    }
}