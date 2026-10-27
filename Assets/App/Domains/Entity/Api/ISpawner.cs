using System;
using UnityEngine;

namespace App.Domains.Entity.Api
{
    public interface ISpawner<out T> where T : Component
    {
        event Action<T> Spawned;

        void Start(float period);
        void Stop();
    }
}