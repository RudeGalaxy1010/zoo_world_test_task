using System;
using App.Domains.Core.Api;
using App.Domains.Entity.Api;
using UnityEngine;

namespace App.Domains.Entity.Impl
{
    public class Spawner<T> : ISpawner<T> where T : Component
    {
        private readonly Func<T> _create;
        private readonly ITimerService _timerService;

        private IDisposable? _handle;

        public event Action<T> Spawned = delegate { };

        public Spawner(Func<T> create, ITimerService timerService)
        {
            _create = create;
            _timerService = timerService;
        }

        public void Start(float period) =>
            _handle = _timerService.AddLooped(Spawn, period);

        public void Stop() =>
            _handle?.Dispose();

        private void Spawn()
        {
            T obj = _create();
            Spawned.Invoke(obj);
        }
    }
}
