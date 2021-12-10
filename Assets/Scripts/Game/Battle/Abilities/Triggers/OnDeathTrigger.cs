using System;
using Common.Actors;

namespace SecondBreath.Game.Battle.Abilities.Triggers
{
    public class OnDeathTrigger : ITrigger
    {
        public void Dispose()
        {
            
        }

        public event EventHandler<EventArgs> Events;

        public void Action(object sender, EventArgs args)
        {
            Events?.Invoke(this, EventArgs.Empty);
        }
        public void Init(IActor actor)
        {
            actor.Killed += Action;
        }
    }
}