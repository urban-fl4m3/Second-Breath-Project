using System.Collections.Generic;
using Common.Actors;

namespace SecondBreath.Game.Battle.Abilities.TargetChoosers
{
    public interface ITargetChooser
    {
        IEnumerable<IActor> ChooseTarget();
    }
}