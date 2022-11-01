using UnityEngine;

namespace Shooter.Core
{
    public interface IMove
    {
        void Move(Vector3 point);
        void Stop();
    }
}