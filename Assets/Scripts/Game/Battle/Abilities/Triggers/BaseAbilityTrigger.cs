using System;
using Common.Actors;

namespace SecondBreath.Game.Battle.Abilities.Triggers
{
    public abstract class BaseAbilityTrigger : ITrigger
    {
        public event EventHandler<EventArgs> Events;
        
        protected IActor Actor { get; private set; }
        
        public void Init(IActor actor)
        {
            Actor = actor;
            OnInit();
        }

        public virtual void Dispose()
        {
            
        }

        protected virtual void OnInit()
        {
            
        }

        protected void Trigger(EventArgs e)
        {
            Events?.Invoke(this, e);
        }
    }
}