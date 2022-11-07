using UnityEngine;
using UnityEngine.AI;

namespace Core.Views
{
    public class UnitView : MonoBehaviour
    {
        [field: SerializeField] public Animator Animator { get; private set; }
        [field: SerializeField] public AnimatorEventHandler AnimatorEventHandler { get; private set; }
        [field: SerializeField] public NavMeshAgent NavAgent { get; private set; }
    }
}