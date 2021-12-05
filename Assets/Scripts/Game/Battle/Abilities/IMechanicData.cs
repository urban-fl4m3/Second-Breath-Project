using System;
using SecondBreath.Game.Battle.Abilities.TargetChoosers;
using Sirenix.Serialization;

namespace SecondBreath.Game.Battle.Abilities
{
    public interface IMechanicData
    {
        ITargetChooser[] TargetChoosers { get; }
        Type LogicInstanceType { get; }
    }
}