using System;
using SecondBreath.Game.Stats;
using UnityEngine;

namespace SecondBreath.Game.Battle.Abilities.Mechanics
{
    [Serializable]
    public class DamageAuraData : IMechanicData
    {
        [SerializeField] private StatData _damage;
        [SerializeField] private StatData _radius;

        public StatData Damage => _damage;
        public StatData Radius => _radius;
        
        public Type LogicInstanceType => typeof(DamageAura);
    }
}