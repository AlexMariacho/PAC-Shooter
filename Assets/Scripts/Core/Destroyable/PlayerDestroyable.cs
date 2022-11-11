using System;

namespace Shooter.Core
{
    public class PlayerDestroyable : IDestroyable
    {
        public event Action Death;
        public event Action<int> ChangeHp;
        public int Hp { get; private set; }
        public int MaxHp { get; private set; }

        public PlayerDestroyable(int maxHp)
        {
            MaxHp = maxHp;
            Hp = maxHp;
        }

        public void TakeDamage(int damage)
        {
            Hp -= damage;
            
            
            if (Hp <= 0)
            {
                Hp = 0;
                Death?.Invoke();
            }
        }
    }
}