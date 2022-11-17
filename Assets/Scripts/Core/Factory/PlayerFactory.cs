using Cinemachine;
using Core;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Shooter.Core.Factory
{
    public class PlayerFactory : PlaceholderFactory<Player>
    {
        private RootObjects _root;
        private PlayerConfiguration _configuration;
        private Camera _camera;
        private CinemachineVirtualCamera _virtualCamera;

        [Inject]
        private void Construct(RootObjects root, PlayerConfiguration configuration, Camera camera, CinemachineVirtualCamera virtualCamera)
        {
            _root = root;
            _configuration = configuration;
            _camera = camera;
            _virtualCamera = virtualCamera;
        }

        public Player Create(PlayerView view)
        {
            UnitModel unitModel = new UnitModel();
            unitModel.Destroyable = new PlayerDestroyable(_configuration.Hp);
            unitModel.Transform = view.transform;
            
            PlayerModel playerModel = new PlayerModel();
            NavMeshAgent navMeshAgent = view.gameObject.AddComponent<NavMeshAgent>();
            playerModel.Mover = new PlayerNavigation(_configuration.MoveSpeed, _configuration.AngularSpeed, view.transform,
                navMeshAgent);
            playerModel.Weapon = view.GetComponent<BaseWeapon>();
            playerModel.Weapon.Initialize(view.transform, view.PlayerAnimationController, view.AnimatorEventHandler);
            if (view.isLocalPlayer)
            {
                playerModel.Input = new UiInput(_camera);
                _virtualCamera.Follow = view.transform;
                _virtualCamera.LookAt = view.transform;
            }
            else
            {
                playerModel.Input = new NetworkInput();
            }

            playerModel.View = view;
            view.transform.SetParent(_root.Units);
            
            Player player = new Player(_configuration, unitModel, playerModel);
            Debug.Log("|PlayerFactory| Create");

            return player;
        }
    }
}