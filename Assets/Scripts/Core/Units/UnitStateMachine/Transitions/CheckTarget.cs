namespace Shooter.Core
{
    public class CheckTarget : ITransition
    {
        private TargetInformation _targetInformation;

        public CheckTarget(TargetInformation targetInformation)
        {
            _targetInformation = targetInformation;
        }

        public bool CheckCondition()
        {
            return _targetInformation.Target != null;
        }
    }
}