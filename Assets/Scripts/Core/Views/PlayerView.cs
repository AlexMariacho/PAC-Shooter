using UnityEngine;

namespace Shooter.Core
{
    public class PlayerView : MonoBehaviour
    {
        [field: SerializeField] public PlayerAnimationController PlayerAnimationController { get; private set; }
        [field: SerializeField] public AnimatorEventHandler AnimatorEventHandler { get; private set; }
        [field: SerializeField] public HpBarView HpBarView { get; private set; }

        public void Initialize(Player player)
        {
            PlayerAnimationController.Initialize(player);
            HpBarView.Initialize(player.DestroyableComponent);
        }
    }
}