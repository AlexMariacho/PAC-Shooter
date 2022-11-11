using UnityEngine;

namespace Core.Views
{
    public class HpBarPositionCorrector : MonoBehaviour
    {
        [SerializeField] private Transform _rootTransform;
        
        private void Update()
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, -_rootTransform.rotation.y, 0));
        }
    }
}