using System;
using SecondBreath.Game.Players;
using Sirenix.Serialization;

namespace SecondBreath.Game.Battle.Abilities.TargetChoosers
{
    public class TeamChooserData : ITargetChooserData
    {
        [OdinSerialize] private Side _choosenSide;
        
        public Side ChoosenSide => _choosenSide;
        public virtual Type LogicInstanceType => typeof(TeamChooser);
    }
}