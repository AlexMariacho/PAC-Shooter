using System;
using UnityEngine;
using UnityEngine.UI;

namespace Shooter.Ui
{
    public class CreateGameMenuView : DialogView
    {
        [SerializeField] private Button _cancelButton;
        public event Action Cancel;
        protected override void Init()
        {
            _cancelButton.onClick.AddListener(OnCancelClick);
        }
        protected override void Dispose()
        {
            _cancelButton.onClick.RemoveListener(OnCancelClick);
        }

        private void OnCancelClick()
        {
            Cancel?.Invoke();
        }
    }
}