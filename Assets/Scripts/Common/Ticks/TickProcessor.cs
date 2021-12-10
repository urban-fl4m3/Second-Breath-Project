using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;

namespace SecondBreath.Common.Ticks
{
    public class TickProcessor : ITickProcessor
    {
        private readonly List<Action> _ticks = new List<Action>();
        private readonly Queue<Action> _ticksToAdd = new Queue<Action>();
        private readonly Queue<Action> _ticksToRemove = new Queue<Action>();

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

        public void AddTick(Action tick)
        {
            _ticksToAdd.Enqueue(tick);
        }

        public void RemoveTick(Action tick)
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
                tick();
            }
        }
    }
}