using UnityEngine;

namespace Shooter.Ui
{
    public class SimpleView : MonoBehaviour
    {
        [SerializeField] private GameObject _root;
        public void SetActive(bool active)
        {
            _root.SetActive(active);
        }

        protected virtual void Init()
        {
        }

        protected virtual void Dispose()
        {
        }
    }
}