using System;

namespace Shooter.Core
{
    public class PlayerDestroyable : IDestroyable
    {
        public event Action Death;
        public int Hp { get; private set; }
        
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