using Core.Views;
using UnityEngine;

namespace Shooter.Core
{
    public class MoveState : BaseState
    {
        private Animator _animator;
        private IMove _mover;
        private MoveInformation _moveInformation;

        public MoveState(IStateMachine stateMachine, MoveInformation moveInformation, IMove mover, Animator animator) : base(stateMachine)
        {
            _moveInformation = moveInformation;
            _mover = mover;
            _animator = animator;
        }

        public override void UpdateState()
        {
            base.UpdateState();
            _mover.Move(_moveInformation.TargetPoint);
        }

        public override void EnterState()
        {
            _animator.SetBool(AnimatorConstantNames.Move, true);
        }

        public override void ExitState()
        {
            _animator.SetBool(AnimatorConstantNames.Move, false);
        }

    }
}