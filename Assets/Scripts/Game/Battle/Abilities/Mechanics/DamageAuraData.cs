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
        [SerializeField] private GameObject _vfx;

        public StatData Damage => _damage;
        public StatData Radius => _radius;

        public GameObject VFX => _vfx;
        
        public Type LogicInstanceType => typeof(DamageAura);
    }
}