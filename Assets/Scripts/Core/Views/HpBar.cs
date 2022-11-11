using System;
using Shooter.Core;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Views
{
    public class HpBar : MonoBehaviour
    {
        [SerializeField] private Transform _rootTransform;
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

        private void Update()
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, -_rootTransform.rotation.y, 0));
        }

    }
}