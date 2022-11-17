using UnityEngine;

namespace Shooter.Ui
{
    public class SimpleView : MonoBehaviour
    {
        [SerializeField] private GameObject _root;
        protected void SetActive(bool active)
        {
            _root.SetActive(active);
        }

        protected virtual void Init()
        {
        }

        protected virtual void Dispose()
        {
        }
        
        public virtual void Show()
        {
            SetActive(true);
            Init();
        }

        public virtual void Hide()
        {
            Dispose();
            SetActive(false);
        }
    }
}