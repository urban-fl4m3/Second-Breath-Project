using System;

namespace SecondBreath.Game.Battle.Abilities.TargetChoosers
{
    public interface ITargetChooserData
    {
        Type LogicInstanceType { get; }
    }
}