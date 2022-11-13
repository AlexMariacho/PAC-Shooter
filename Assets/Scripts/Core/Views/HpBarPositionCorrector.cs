using UnityEngine;

namespace Shooter.Core
{
    public class HpBarPositionCorrector : MonoBehaviour
    {
        [SerializeField] private Transform _rootTransform;
        
        private void Update()
        {
            transform.rotation = Quaternion.Euler(new Vector3(90, -_rootTransform.rotation.y, 0));
        }
    }
}