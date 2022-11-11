using System;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Views
{
    public class HpBar : MonoBehaviour
    {
        [SerializeField] private Transform _rootTransform;
        [SerializeField] private Image _image;
        
        private void Update()
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, -_rootTransform.rotation.y, 0));
        }

        public void SetHp(float hp)
        {
            _image.fillAmount = Math.Clamp(hp, 0, 1);
        }
    }
}