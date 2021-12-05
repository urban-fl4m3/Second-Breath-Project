using System;
using SecondBreath.Game.Battle.Abilities.TargetChoosers;
using Sirenix.Serialization;

namespace SecondBreath.Game.Battle.Abilities
{
    public class BaseMechanicData : IMechanicData
    {
        [OdinSerialize] public ITargetChooser[] TargetChoosers { get; private set;}
        public virtual Type LogicInstanceType => typeof(BaseMechanicData);
    }
}