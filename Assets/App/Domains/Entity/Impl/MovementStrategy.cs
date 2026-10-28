using App.Domains.Entity.Api;
using UnityEngine;

namespace App.Domains.Entity.Impl
{
    public abstract class MovementStrategy : MonoBehaviour, IMovable
    {
        public abstract bool DestinationReached { get; }

        public abstract void Tick(float deltaTime);
        public abstract void SetDestination(Vector3 destination);
        public abstract void Stop();
    }
}
