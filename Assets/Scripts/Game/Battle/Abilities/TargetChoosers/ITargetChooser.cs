using System.Collections.Generic;
using Common.Actors;
using Zenject;

namespace SecondBreath.Game.Battle.Abilities.TargetChoosers
{
    public interface ITargetChooser
    {
        List<IActor> ChooseTarget();
        
        void Init(IActor actor);
    }
}