using System.Collections.Generic;
using App.Domains.Entity.Api;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace App.Domains.Entity.Impl
{
    public class MoveService : IMoveService, ITickable
    {
        private const float MoveDiameter = 8f;
        private const int MaxDestinationAttempts = 8;

        private readonly List<IMovable> _movables = new List<IMovable>();

        public void Tick()
        {
            for (int i = 0; i < _movables.Count; i++)
            {
                IMovable movable = _movables[i];

                if (movable.DestinationReached)
                {
                    ChangeDestination(movable);
                }
            }
        }

        public void Register(IMovable movable)
        {
            if (!_movables.Contains(movable))
            {
                _movables.Add(movable);
                ChangeDestination(movable);
            }
        }

        public void Unregister(IMovable movable) =>
            _movables.Remove(movable);

        private void ChangeDestination(IMovable movable)
        {
            for (int i = 0; i < MaxDestinationAttempts; i++)
            {
                float x = (Random.value - 0.5f) * MoveDiameter;
                float z = (Random.value - 0.5f) * MoveDiameter;

                if (NavMesh.SamplePosition(new Vector3(x, 0f, z), out NavMeshHit hit, 1f, NavMesh.AllAreas))
                {
                    movable.SetDestination(hit.position);
                    return;
                }
            }

            movable.Stop();
        }
    }
}
