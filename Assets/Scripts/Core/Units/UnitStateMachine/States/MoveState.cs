using Core.Input;
using Core.Views;
using UnityEngine;

namespace Shooter.Core
{
    public class MoveState : BaseState
    {
        private Animator _animator;
        private IMove _mover;
        private MoveInformation _moveInformation;

        public MoveState(IStateMachine stateMachine, Animator animator, IMove mover, MoveInformation moveInformation) : base(stateMachine)
        {
            _animator = animator;
            _mover = mover;
            _moveInformation = moveInformation;
        }

        public override void UpdateState()
        {
            _mover.Move(_moveInformation.TargetPoint);
        }

        public override void EnterState()
        {
            Debug.Log("Enter move state");
            _animator.SetBool(AnimatorConstantNames.Move, true);
        }

        public override void ExitState()
        {
            Debug.Log("Exit move state");
            _animator.SetBool(AnimatorConstantNames.Move, false);
        }

    }
}