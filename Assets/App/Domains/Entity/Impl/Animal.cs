using App.Domains.Entity.Api;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

namespace App.Domains.Entity.Impl
{
    public class Animal : Entity, IMovable
    {
        private const float DestinationAccuracy = 0.5f;

        [SerializeField] private NavMeshAgent _agent = null!;
        [SerializeField] private MeshRenderer _renderer = null!;
        [SerializeField] private TMP_Text _nameLabel = null!;

        private bool _destinationReached = true;
        private bool _hasDestination;
        private bool _destinationReached1;

        public bool DestinationReached => _destinationReached;

        private void Update()
        {
            if (_destinationReached || !_hasDestination || _agent.pathPending)
            {
                return;
            }

            if (!_agent.hasPath || _agent.pathStatus != NavMeshPathStatus.PathComplete)
            {
                _hasDestination = false;
                _destinationReached = true;
                return;
            }

            float stoppingDistance = Mathf.Max(_agent.stoppingDistance, DestinationAccuracy);
            if (_agent.remainingDistance <= stoppingDistance)
            {
                _destinationReached = true;
            }
        }

        public void SetColor(Color color) =>
            _renderer.material.color = color;

        public void SetDestination(Vector3 destination)
        {
            if (!_agent.isOnNavMesh)
            {
                return;
            }

            _agent.isStopped = false;
            _hasDestination = _agent.SetDestination(destination);
            _destinationReached = !_hasDestination;
        }

        public void Stop() =>
            StopAgent();

        private void StopAgent()
        {
            _hasDestination = false;
            _destinationReached = true;
            _agent.isStopped = true;
            _agent.ResetPath();
        }

        public void SetName(string value) =>
            _nameLabel.text = value;
    }
}