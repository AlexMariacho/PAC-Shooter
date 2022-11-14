using System;
using UnityEngine;
using UnityEngine.UI;

namespace Shooter.Ui
{
    public class GameHudView : SimpleView
    {
        [SerializeField] private Button _exitButton;
        public event Action Exit;
        
        protected override void Init()
        {
            _exitButton.onClick.AddListener(OnExitClick);
        }

        protected override void Dispose()
        {
            _exitButton.onClick.RemoveListener(OnExitClick);
        }

        private void OnExitClick()
        {
            Exit?.Invoke();
        }
    }
}