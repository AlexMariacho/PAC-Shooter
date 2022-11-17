using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Shooter.Ui
{
    public delegate void MessageClickHandler(MessageDialogView sender);
    public class MessageDialogView : SimpleView
    {
        [SerializeField] private TMP_Text _header;
        [SerializeField] private TMP_Text _text;
        [SerializeField] private Button _okButton;
        [SerializeField] private Button _cancelButton;

        public event MessageClickHandler Ok;
        public event MessageClickHandler Cancel;

        public void InitMessage(string header, string text, bool isCancel)
        {
            _header.text = header;
            _text.text = text;
            _cancelButton.gameObject.SetActive(isCancel);
        }
        
        private void OnCancelClick()
        {
            Cancel?.Invoke(this);
        }

        private void OnOkClick()
        {
            Ok?.Invoke(this);
        }

        protected override void Init()
        {
            _okButton.onClick.AddListener(OnOkClick);
            _cancelButton.onClick.AddListener(OnCancelClick);
        }

        protected override void Dispose()
        {
            _okButton.onClick.RemoveListener(OnOkClick);
            _cancelButton.onClick.RemoveListener(OnCancelClick);
        }
    }
}