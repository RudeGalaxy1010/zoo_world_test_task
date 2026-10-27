using System;

namespace App.Domains.Core.Api
{
    public interface ITimerService
    {
        IDisposable Add(Action action, float delay);
        IDisposable AddLooped(Action action, float period);
    }
}
