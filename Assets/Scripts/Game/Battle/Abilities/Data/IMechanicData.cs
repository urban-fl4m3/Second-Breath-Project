using System;
using SecondBreath.Game.Battle.Abilities.TargetChoosers;

namespace SecondBreath.Game.Battle.Abilities
{
    public interface IMechanicData
    {
        ITargetChooserData[] TargetChoosersData { get; }
        Type LogicInstanceType { get; }
    }
}