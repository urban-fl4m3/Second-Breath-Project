using System;
using SecondBreath.Game.Players;
using Sirenix.Serialization;

namespace SecondBreath.Game.Battle.Abilities.TargetChoosers
{
    [Serializable]
    public class TeamChooserData : ITargetChooserData
    {
        [OdinSerialize] private Side _chosenSide;
        
        public Side ChosenSide => _chosenSide;
        
        public Type LogicInstanceType => typeof(TeamChooser);
    }
}