using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;

namespace SecondBreath.Common.Ticks
{
    public class TickProcessor : ITickProcessor
    {
        private readonly List<ITickUpdate> _ticks = new List<ITickUpdate>();
        private readonly Queue<ITickUpdate> _ticksToAdd = new Queue<ITickUpdate>();
        private readonly Queue<ITickUpdate> _ticksToRemove = new Queue<ITickUpdate>();

        private IDisposable _tickSub;
        
        public void Start()
        {
            if (_tickSub != null)
            {
                return;
            }

            _tickSub = Observable
                .EveryUpdate()
                .Subscribe(_ => OnTick());
        }

        public void Stop()
        {
            _tickSub?.Dispose();
            _tickSub = null;
        }

        public void AddTick(ITickUpdate tick)
        {
            _ticksToAdd.Enqueue(tick);
        }

        public void RemoveTick(ITickUpdate tick)
        {
            _ticksToRemove.Enqueue(tick);
        }

        private void OnTick()
        {
            while (_ticksToRemove.Any())
            {
                _ticks.Remove(_ticksToRemove.Dequeue());
            }

            while (_ticksToAdd.Any())
            {
                _ticks.Add(_ticksToAdd.Dequeue());
            }

            foreach (var tick in _ticks)
            {
                tick.Update();
            }
        }
    }
}