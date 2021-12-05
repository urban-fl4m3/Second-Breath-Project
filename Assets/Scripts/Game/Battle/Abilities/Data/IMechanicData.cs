using System;
using SecondBreath.Game.Battle.Abilities.TargetChoosers;
using Sirenix.Serialization;

namespace SecondBreath.Game.Battle.Abilities
{
    public interface IMechanicData
    {
        ITargetChooserData[] TargetChoosersData { get; }
        Type LogicInstanceType { get; }
    }
}