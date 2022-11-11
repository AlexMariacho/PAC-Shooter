using System;
using Core.Views;
using UnityEngine;

namespace Core.Input
{
    public class DummyInput : IUnitInput
    {
        public event Action<Vector3> Move;
        public event Action<UnitView> Attack;
    }
}