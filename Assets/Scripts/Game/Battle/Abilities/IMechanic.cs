using System;
using System.Collections.Generic;
using SecondBreath.Game.Battle.Abilities.Triggers;

namespace SecondBreath.Game.Battle.Abilities
{
    public interface IMechanic : IDisposable
    {
        void Register(IEnumerable<ITrigger> triggers);
        void UnRegister(IEnumerable<ITrigger> triggers);
    }
}