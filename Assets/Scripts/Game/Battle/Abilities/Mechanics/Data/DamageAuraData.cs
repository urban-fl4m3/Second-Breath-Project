using System;
using SecondBreath.Game.Battle.Abilities.TargetChoosers;
using SecondBreath.Game.Stats;
using UnityEngine;

namespace SecondBreath.Game.Battle.Abilities.Mechanics
{
    [Serializable]
    public class DamageAuraData : BaseMechanicData
    {
        [SerializeField] private StatData _damage;
        [SerializeField] private StatData _radius;
        [SerializeField] private GameObject _vfx;

        public StatData Damage => _damage;
        public StatData Radius => _radius;

        public GameObject VFX => _vfx;
        public override Type LogicInstanceType => typeof(DamageAura);
    }
}