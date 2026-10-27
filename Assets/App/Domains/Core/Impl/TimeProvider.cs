using App.Domains.Core.Api;
using UnityEngine;

namespace App.Domains.Core.Impl
{
    public class TimeProvider : ITimeProvider
    {
        public float DeltaTime => Time.deltaTime;
        public float Now => Time.time;
    }
}