using System;

namespace Shooter.Core
{
    public interface IDestroyable
    {
        event Action Death;
        event Action<int> ChangeHp;
        int Hp { get; }
        int MaxHp { get; }
        void TakeDamage(int damage);
    }
}