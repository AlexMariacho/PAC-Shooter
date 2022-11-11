using System;
using Core.Views;
using Shooter.Simple.Units;
using UnityEngine;

namespace Core.Input
{
    public class NetworkInput : IUnitInput
    {
        public event Action<Vector3> Move;
        public event Action<BaseUnit> Attack;
    }
}