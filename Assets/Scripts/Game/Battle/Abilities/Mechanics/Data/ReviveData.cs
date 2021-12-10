using System;
using SecondBreath.Game.Stats;
using Sirenix.Serialization;
using UnityEngine;

namespace SecondBreath.Game.Battle.Abilities.Mechanics
{
    public class ReviveData : BaseMechanicData
    {
        [OdinSerialize] private StatData _hpAmount;
        [OdinSerialize] private float _delay;
        
        public StatData HPAmount => _hpAmount;
        public float Delay => _delay;
        public override Type LogicInstanceType => typeof(Revive);
        
    }
}