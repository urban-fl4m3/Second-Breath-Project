using System;

namespace SecondBreath.Game.Battle.Abilities.Triggers
{
    public interface ITrigger : IDisposable
    {
        event EventHandler<EventArgs> Events;
    }
}