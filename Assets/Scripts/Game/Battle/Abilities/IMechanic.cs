using System;
using System.Collections.Generic;
using Common.Actors;
using SecondBreath.Game.Battle.Abilities.Triggers;
using SecondBreath.Game.Battle.Registration;
using Zenject;

namespace SecondBreath.Game.Battle.Abilities
{
    public interface IMechanic : IDisposable
    {
        void Init(IActor caster, IMechanicData data, int level, DiContainer container);

        void Action(Object sender, EventArgs args);

        void Register(List<ITrigger> triggers);
        void UnRegister(List<ITrigger> triggers);
    }
}