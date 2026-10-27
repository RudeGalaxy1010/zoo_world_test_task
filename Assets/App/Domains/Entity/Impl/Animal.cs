using UnityEngine;
using UnityEngine.AI;

namespace App.Domains.Entity.Impl
{
    public class Animal : Entity
    {
        [SerializeField] private NavMeshAgent _agent = null!;
        [SerializeField] private MeshRenderer _renderer = null!;

        public NavMeshAgent Agent => _agent;

        public void SetColor(Color color) =>
            _renderer.sharedMaterial.color = color;
    }
}