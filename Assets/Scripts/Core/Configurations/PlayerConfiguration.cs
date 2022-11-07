using System;
using Core.Views;

namespace Core.Configurations
{
    [Serializable]
    public class PlayerConfiguration
    {
        public int Hp;
        public float MoveSpeed;
        public float AngularSpeed;

        public int Damage;
        public float FireRate;
        public float Distance;

        public UnitView View;
    }
}