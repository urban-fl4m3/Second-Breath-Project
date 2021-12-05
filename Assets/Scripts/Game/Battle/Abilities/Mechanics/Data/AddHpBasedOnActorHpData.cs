using System;
using SecondBreath.Game.Players;
using SecondBreath.Game.Stats;
using Sirenix.Serialization;

namespace SecondBreath.Game.Battle.Abilities.Mechanics
{
    public class AddHpBasedOnActorHpData : BaseMechanicData
    {
        [OdinSerialize] private StatData _hpPercent;
        [OdinSerialize] private StatData _radius;
        [OdinSerialize] private Side _choosenSide;

        public StatData HpPercent => _hpPercent;
        public StatData Radius => _radius;
        public Side ChoosenSide => _choosenSide;

        public override Type LogicInstanceType => typeof(AddHpBasedOnActorHp);
        
    }
}