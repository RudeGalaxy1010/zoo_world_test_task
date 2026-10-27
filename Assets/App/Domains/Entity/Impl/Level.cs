using UnityEngine;

namespace App.Domains.Entity.Impl
{
    public class Level : Entity
    {
        [SerializeField] private Transform _spawnPoint = null!;

        public Transform SpawnPoint => _spawnPoint;
    }
}