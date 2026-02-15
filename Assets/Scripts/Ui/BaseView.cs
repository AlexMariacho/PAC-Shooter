using UnityEngine;

namespace Shooter.Ui
{
    public class BaseView : MonoBehaviour
    {
        [SerializeField] private GameObject _root;

        public void Show()
        {
            Init();
            _root.SetActive(true);
            OnShow();
        }

        public void Hide()
        {
            Dispose();
            _root.SetActive(false);
            OnHide();
        }
        
        protected virtual void Init() { }
        protected virtual void Dispose() { }
        
        protected virtual void OnShow() { }
        protected virtual void OnHide() { }
    }
}