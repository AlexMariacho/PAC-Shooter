using Mirror;
using Network;
using UnityEngine;

namespace Shooter.Core
{
    public class PlayerView : NetworkBehaviour
    {
        [field: SerializeField] public PlayerAnimationController PlayerAnimationController { get; private set; }
        [field: SerializeField] public AnimatorEventHandler AnimatorEventHandler { get; private set; }
        [field: SerializeField] public HpBarView HpBarView { get; private set; }

        public void Initialize(Player player)
        {
            PlayerAnimationController.Initialize(player);
            HpBarView.Initialize(player.DestroyableComponent);
        }
        
        public override void OnStartClient()
        {
            NetworkSpawner.Instance.RegisterSpawn(this.gameObject);
        }

        public override void OnStopClient()
        {
            NetworkSpawner.Instance.RegisterDeSpawn(this.gameObject);
        }
    }
}