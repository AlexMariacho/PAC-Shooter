using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Shooter.Core
{
    public class UiInput : IUnitInput, IDisposable
    {
        public event Action<Vector3> Move;
        public event Action<BaseUnit> Attack;

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
                if (hit.transform.TryGetComponent(out BaseUnit unit))
                {
                    Attack?.Invoke(unit);
                    return;
                }

                if (hit.collider != null)
                {
                    Move?.Invoke(hit.point);
                }
            }
        }

        private void SendMove(Vector3 move)
        {
            
        }

        public void Dispose()
        {
            _input.Player.Click.performed -= OnScreenClick;
            _input?.Dispose();
        }
    }
}