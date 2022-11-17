using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Shooter.Ui
{
    public delegate void IpJoinHandler(string ipAddress);
    public class JoinGameMenuView : SimpleView
    {
        [SerializeField] private TMP_InputField _input;
        [SerializeField] private Button _joinButton;
        [SerializeField] private Button _cancelButton;

        public event Action Cancel;
        public event IpJoinHandler Join;
        protected override void Init()
        {
            _joinButton.onClick.AddListener(OnJoinClick);
            _cancelButton.onClick.AddListener(OnCancelClick);
        }


        protected override void Dispose()
        {
            _joinButton.onClick.RemoveListener(OnJoinClick);
            _cancelButton.onClick.RemoveListener(OnCancelClick);
        }

        private void OnCancelClick()
        {
            Cancel?.Invoke();
        }

        private void OnJoinClick()
        {
            if (!string.IsNullOrEmpty(_input.text))
            {
                Join?.Invoke(_input.text);
            }
        }

    }
}