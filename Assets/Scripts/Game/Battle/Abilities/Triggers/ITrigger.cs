using System;
using Common.Actors;
using UnityEngine;

namespace SecondBreath.Game.Battle.Abilities.Triggers
{
    public interface ITrigger : IDisposable
    {
        event Action Events;
        void Init(IActor actor);
    }
}