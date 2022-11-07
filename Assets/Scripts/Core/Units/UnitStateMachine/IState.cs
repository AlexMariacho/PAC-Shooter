using System;

namespace Shooter.Core
{
    public interface IState
    {
        event Action<IState> Finish;
        void EnterState();
        void ExitState();
        void Update();
    }
}