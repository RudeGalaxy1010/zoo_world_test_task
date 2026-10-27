using UnityEngine;

namespace App.Domains.Entity.Api
{
    public interface IMovable
    {
        bool DestinationReached { get; }

        void SetDestination(Vector3 destination);
        void Stop();
    }
}