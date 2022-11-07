using System;
using Core.Views;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Core.Input
{
    public class UiInput : IUnitInput, IDisposable
    {
        public event Action<Vector3> Move;
        public event Action<UnitView> Attack;

        private Camera _camera;
        private UnityInputSystem _input;

        public UiInput(Camera camera)
        {
            _camera = camera;
            _input = new UnityInputSystem();
            _input.Player.Click.performed += OnScreenClick;
            _input.Enable();
        }

        private void OnScreenClick(InputAction.CallbackContext clickPosition)
        {
            RaycastHit hit;
            Ray ray = _camera.ScreenPointToRay(_input.Player.MousePosition.ReadValue<Vector2>());
        
            if (Physics.Raycast(ray, out hit)) 
            {
                if (hit.transform.TryGetComponent(out UnitView view))
                {
                    Attack?.Invoke(view);
                    return;
                }

                Move?.Invoke(_camera.ScreenToWorldPoint(_input.Player.MousePosition.ReadValue<Vector2>()));
            }
        }

        public void Dispose()
        {
            _input.Player.Click.performed -= OnScreenClick;
            _input?.Dispose();
        }
    }
}