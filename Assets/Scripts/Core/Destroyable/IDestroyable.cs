using System;

namespace Shooter.Core
{
    public interface IDestroyable
    {
        event Action<IDestroyable> Death;
        event Action<int> ChangeHp;
        int Hp { get; }
        int MaxHp { get; }
        void TakeDamage(int damage);

        void Reset();
    }
}