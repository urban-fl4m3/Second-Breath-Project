using System;
using Common.Actors;
using SecondBreath.Common.Ticks;
using SecondBreath.Game.Ticks;

namespace SecondBreath.Game.Battle.Abilities.Triggers
{
    public class UpdateTrigger : ITrigger, ITickUpdate
    {
        private readonly IGameTickCollection _tickCollection;

        public UpdateTrigger(IGameTickCollection tickCollection)
        {
            _tickCollection = tickCollection;
        }

        public event Action Events;

        public void Init(IActor actor)
        {
            _tickCollection.AddTick(this);
        }

        public void Update()
        {
            Events?.Invoke();    
        }

        public void Dispose()
        {
            _tickCollection.RemoveTick(this);
        }
    }
}