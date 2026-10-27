using System;
using App.Domains.Core.Api;
using App.Domains.Entity.Api;
using UnityEngine;
using Object = UnityEngine.Object;

namespace App.Domains.Entity.Impl
{
    public class Spawner : ISpawner
    {
        private readonly GameObject _prefab;
        private readonly Transform _spawnPoint;
        private readonly ITimerService _timerService;

        private IDisposable? _handle;

        public Spawner(GameObject prefab, Transform spawnPoint, ITimerService timerService)
        {
            _prefab = prefab;
            _spawnPoint = spawnPoint;
            _timerService = timerService;
        }

        public void Start(float period) =>
            _handle = _timerService.AddLooped(() => Object.Instantiate(_prefab, _spawnPoint), period);

        public void Stop() =>
            _handle?.Dispose();
    }
}