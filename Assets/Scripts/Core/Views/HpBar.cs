using Shooter.Core;
using UnityEngine;
using UnityEngine.UI;

namespace Shooter.Core
{
    public class HpBar : MonoBehaviour
    {
        [SerializeField] private Image _image;
        private IDestroyable _destroyable;
        private int _maxHp;

        public void Initialize(IDestroyable destroyable)
        {
            _destroyable = destroyable;
            _maxHp = _destroyable.MaxHp;
            _destroyable.ChangeHp += OnChangeHp;
        }

        private void OnChangeHp(int hp)
        {
            float currentPercent = (float) hp / _maxHp;
            _image.fillAmount = currentPercent;
        }
        
    }
}