using System;
using Shooter.Simple.Units;
using UnityEngine;

namespace Core.Input
{
    public class DummyInput : IUnitInput
    {
        public event Action<Vector3> Move;
        public event Action<BaseUnit> Attack;
    }
}