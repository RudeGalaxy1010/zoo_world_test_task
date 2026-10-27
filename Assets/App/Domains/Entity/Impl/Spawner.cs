using System;
using App.Domains.Core.Api;
using App.Domains.Entity.Api;
using UnityEngine;
using Object = UnityEngine.Object;

namespace App.Domains.Entity.Impl
{
    public class Spawner<T> : ISpawner<T> where T : Component
    {
        private readonly T _prefab;
        private readonly Transform _spawnPoint;
        private readonly ITimerService _timerService;

        private IDisposable? _handle;

        public event Action<T> Spawned = delegate { };

        public Spawner(T prefab, Transform spawnPoint, ITimerService timerService)
        {
            _prefab = prefab;
            _spawnPoint = spawnPoint;
            _timerService = timerService;
        }

        public void Start(float period) =>
            _handle = _timerService.AddLooped(Spawn, period);

        public void Stop() =>
            _handle?.Dispose();

        private void Spawn()
        {
            T obj = Object.Instantiate(_prefab, _spawnPoint);
            Spawned.Invoke(obj);
        }
    }
}