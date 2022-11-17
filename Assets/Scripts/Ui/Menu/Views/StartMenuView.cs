using System;
using UnityEngine;
using UnityEngine.UI;

namespace Shooter.Ui
{
    public class StartMenuView : SimpleView
    {
        [SerializeField] private Button _createButton;
        [SerializeField] private Button _joinButton;

        public event Action CreateGame;
        public event Action JoinGame;

        protected override void Init()
        {
            _createButton.onClick.AddListener(OnCreateClick);
            _joinButton.onClick.AddListener(OnJoinClick);
        }

        protected override void Dispose()
        {
            _createButton.onClick.RemoveListener(OnCreateClick);
            _joinButton.onClick.RemoveListener(OnJoinClick);
        }

        private void OnJoinClick()
        {
           JoinGame?.Invoke();
        }

        private void OnCreateClick()
        {
            CreateGame?.Invoke();
        }
    }
}