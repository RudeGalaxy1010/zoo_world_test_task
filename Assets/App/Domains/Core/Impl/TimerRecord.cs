using System;

namespace App.Domains.Core.Impl
{
    public class TimerRecord : IDisposable
    {
        public readonly Action Action;
        public float ExecutionTime;
        public readonly float Period;
        private readonly Action<TimerRecord> _disposeAction;

        public bool IsLooped => Period > 0f;

        public TimerRecord(Action action, float executionTime, Action<TimerRecord> disposeAction)
        {
            Action = action;
            ExecutionTime = executionTime;
            Period = -1f;
            _disposeAction = disposeAction;
        }

        public TimerRecord(Action action, float executionTime, float period, Action<TimerRecord> disposeAction)
        {
            Action = action;
            ExecutionTime = executionTime;
            Period = period;
            _disposeAction = disposeAction;
        }

        public void Dispose() =>
            _disposeAction.Invoke(this);
    }
}
