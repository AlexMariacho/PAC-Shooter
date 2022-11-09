namespace Shooter.Core
{
    public interface IStateMachine
    {
        void SetState(BaseState state);
        void Enable();
        void Disable();
    }
}