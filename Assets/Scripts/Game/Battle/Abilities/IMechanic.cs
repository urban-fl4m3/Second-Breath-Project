using System;
using Common.Actors;
using Zenject;

namespace SecondBreath.Game.Battle.Abilities
{
    public interface IMechanic : IDisposable
    {
        void Init(IActor caster, IMechanicData data, int level, DiContainer container);
    }
}