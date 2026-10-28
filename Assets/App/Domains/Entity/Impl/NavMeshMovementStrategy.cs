using UnityEngine;
using UnityEngine.AI;

namespace App.Domains.Entity.Impl
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class NavMeshMovementStrategy : MovementStrategy
    {
        private const float DestinationAccuracy = 0.5f;

        [SerializeField] private NavMeshAgent _agent = null!;

        protected NavMeshAgent Agent => _agent;
        protected bool IsAgentReady => Agent != null && Agent.isActiveAndEnabled && Agent.isOnNavMesh;

        public override bool DestinationReached => !Agent.pathPending && Agent.remainingDistance <= Mathf.Max(
            Agent.stoppingDistance,
            DestinationAccuracy);

        private void OnDisable() =>
            Stop();

        public override void Tick(float deltaTime)
        {
            if (DestinationReached)
            {
                Stop();
            }
        }

        public override void SetDestination(Vector3 destination)
        {
            Stop();

            if (!isActiveAndEnabled || !IsAgentReady)
            {
                return;
            }

            Agent.isStopped = !Agent.SetDestination(destination);
        }

        public override void Stop()
        {
            if (IsAgentReady)
            {
                Agent.ResetPath();
                Agent.isStopped = true;
            }
        }
    }
}
