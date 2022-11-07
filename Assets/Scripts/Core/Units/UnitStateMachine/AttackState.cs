using System;

namespace Shooter.Core
{
    public class AttackState : IState
    {
        public event Action<IState> Finish;

        private BaseUnit _target;
        private IWeapon _weapon;
        
        public void EnterState()
        {
            
        }

        public void ExitState()
        {

        }

        public void Update()
        {

        }
    }
}