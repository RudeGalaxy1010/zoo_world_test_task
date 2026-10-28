using App.Domains.Entity.Api;
using TMPro;
using UnityEngine;

namespace App.Domains.Entity.Impl
{
    public class Animal : Entity, IMovable
    {
        [SerializeField] private MovementStrategy _movementStrategy = null!;
        [SerializeField] private MeshRenderer _renderer = null!;
        [SerializeField] private TMP_Text _nameLabel = null!;

        public bool DestinationReached => _movementStrategy.DestinationReached;

        private void Update() =>
            _movementStrategy.Tick(Time.deltaTime);

        public void SetColor(Color color) =>
            _renderer.material.color = color;

        public void SetDestination(Vector3 destination) =>
            _movementStrategy.SetDestination(destination);

        public void Stop() =>
            _movementStrategy.Stop();

        public void SetName(string value) =>
            _nameLabel.text = value;
    }
}
