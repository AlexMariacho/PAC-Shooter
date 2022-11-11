using Core.Views;
using UnityEngine;

namespace Shooter.Simple.Units
{
    public class BaseUnit : MonoBehaviour
    {
        [SerializeField] protected HpBar _hpBarView;
        public UnitModel Model { get; protected set; }
        
    }
}