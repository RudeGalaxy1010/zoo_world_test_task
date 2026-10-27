using System;
using System.Collections.Generic;
using App.Domains.Core.Api;
using Zenject;

namespace App.Domains.Core.Impl
{
    public class TimerService : ITimerService, ITickable
    {
        private readonly ITimeProvider _timeProvider;
        private readonly List<TimerRecord> _records = new List<TimerRecord>(16);

        public TimerService(ITimeProvider timeProvider) =>
            _timeProvider = timeProvider;

        public void Tick()
        {
            float now = _timeProvider.Now;

            for (int i = _records.Count - 1; i >= 0; i--)
            {
                TimerRecord record = _records[i];

                if (record.ExecutionTime > now)
                {
                    continue;
                }

                if (record.IsLooped)
                {
                    record.ExecutionTime = now + record.Period;
                }
                else
                {
                    _records.RemoveAt(i);
                }

                record.Action.Invoke();
            }
        }

        public IDisposable Add(Action action, float delay)
        {
            TimerRecord record = new TimerRecord(action, _timeProvider.Now + delay, Remove);
            _records.Add(record);
            return record;
        }

        public IDisposable AddLooped(Action action, float period)
        {
            TimerRecord record = new TimerRecord(action, _timeProvider.Now + period, period, Remove);
            _records.Add(record);
            return record;
        }

        private void Remove(TimerRecord record)
        {
            for (int i = 0; i < _records.Count; i++)
            {
                if (ReferenceEquals(_records[i], record))
                {
                    _records.RemoveAt(i);
                    return;
                }
            }
        }
    }
}