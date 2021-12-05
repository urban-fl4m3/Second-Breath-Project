using System;
using Common.Actors;
using SecondBreath.Game.Battle.Registration;
using UnityEngine;
using Object = System.Object;

namespace SecondBreath.Game.Battle.Abilities.Triggers
{
    public interface ITrigger : IDisposable
    {
        event EventHandler<EventArgs> Events;
        void Init(IActor actor);
    }
}