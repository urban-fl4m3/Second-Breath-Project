﻿using System.Collections.Generic;
using Common.Actors;
using Common.VFX;
using SecondBreath.Common.Ticks;
using SecondBreath.Game.Battle.Abilities.Triggers;
using SecondBreath.Game.Battle.Damage;
using SecondBreath.Game.Battle.Movement;
using SecondBreath.Game.Battle.Movement.Components;
using SecondBreath.Game.Battle.Registration;
using SecondBreath.Game.Stats.Formulas;
using SecondBreath.Game.Ticks;
using UnityEngine;

namespace SecondBreath.Game.Battle.Abilities.Mechanics
{
    public class DamageAura : BaseMechanic<DamageAuraData>
    {
        private IStatUpgradeFormula _statUpgradeFormula;

        private ITranslatable _casterTranslatable;

        private float _radius;
        private float _damage;

        private VfxObject _vfx;

        protected override void OnInit(IActor owner, DamageAuraData data)
        {
            _statUpgradeFormula = Container.Resolve<IStatUpgradeFormula>();
            _casterTranslatable = Caster.Components.Get<ITranslatable>();  
        
            _radius  = _statUpgradeFormula.GetValue(Data.Radius, Level);
            _damage = _statUpgradeFormula.GetValue(Data.Damage, Level);

            _vfx = Object.Instantiate(data.VFX, owner.Components.Get<RotationComponent>().transform).GetComponent<VfxObject>();
            _vfx.UpdateScale(_radius);
        }

        public override void Dispose()
        {
            Object.Destroy(_vfx.gameObject);
        }

        public override void Action()
        {
            List<IActor> targets = new List<IActor>();
            foreach (var chooser in _choosers)
            {
                targets.AddRange(chooser.ChooseTarget());
            }
            
            foreach (var enemyActor in targets)
            {
                var enemyTranslatable = enemyActor.Components.Get<ITranslatable>();

                var sqrMagnitude =
                    Vector3.SqrMagnitude(enemyTranslatable.Position.Value - _casterTranslatable.Position.Value);

                    
                var radiusDiff = _radius * _radius + 2 * _radius * enemyTranslatable.Radius +
                               enemyTranslatable.Radius * enemyTranslatable.Radius;
                
                if (sqrMagnitude <= radiusDiff)
                {
                    var damageable = enemyActor.Components.Get<IDamageable>();
                    damageable.DealDamage(new DamageData(_damage * Time.deltaTime));
                }
            }
        }
    }
}