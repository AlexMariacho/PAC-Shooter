using UnityEngine;

namespace Shooter.Core
{
    public class PlayerView : MonoBehaviour
    {
        [field: SerializeField] public PlayerAnimation PlayerAnimation { get; private set; }
        [field: SerializeField] public AnimatorEventHandler AnimatorEventHandler { get; private set; }
        [field: SerializeField] public HpBar HpBar { get; private set; }

        public void Initialize(Player player)
        {
            PlayerAnimation.Initialize(player);
            HpBar.Initialize(player.Model.Destroyable);
        }
    }
}