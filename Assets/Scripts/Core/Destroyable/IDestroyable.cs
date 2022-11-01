using System;

namespace Shooter.Core
{
    public interface IDestroyable
    {
        event Action Death;
        int Hp { get; }
        void TakeDamage(int damage);
    }
}