using Core.Configurations;
using Core.Views;
using UnityEngine;

namespace Shooter.Core
{
    public class AttackState : BaseState
    {
        private IWeapon _weapon;
        private TargetInformation _targetInformation;
        private Animator _animator;

        public AttackState(IStateMachine stateMachine, TargetInformation targetInformation, IWeapon weapon, Animator animator) : base(stateMachine)
        {
            _weapon = weapon;
            _targetInformation = targetInformation;
            _animator = animator;
        }

        public override void UpdateState()
        {
            _weapon.Fire(_targetInformation.Target);
        }

        public override void EnterState()
        {
            Debug.Log("Enter Attack state");
            _animator.SetBool(AnimatorConstantNames.Attack, true);
        }

        public override void ExitState()
        {
            Debug.Log("Enter Attack state");
            _animator.SetBool(AnimatorConstantNames.Attack, false);
        }
    }
}