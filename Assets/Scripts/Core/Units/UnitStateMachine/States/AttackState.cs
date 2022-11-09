using Core.Configurations;
using Core.Views;
using UnityEngine;

namespace Shooter.Core
{
    public class AttackState : BaseState
    {
        private IWeapon _weapon;
        private TargetInformation _targetInformation;

        public AttackState(IStateMachine stateMachine, TargetInformation targetInformation, IWeapon weapon) : base(stateMachine)
        {
            _weapon = weapon;
            _targetInformation = targetInformation;
        }

        public override void UpdateState()
        {
            _weapon.Fire(_targetInformation.Target);
            base.UpdateState();
        }

        public override void EnterState()
        {
        }

        public override void ExitState()
        {
        }
    }
}