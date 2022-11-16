using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Shooter.Ui
{
    public class GameHudView : SimpleView
    {
        [SerializeField] private Button _exitButton;
        [SerializeField] private TMP_Text _addressLabel;
        public event Action Exit;

        public void ShowAddress(string address)
        {
            _addressLabel.text = address;
        }

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