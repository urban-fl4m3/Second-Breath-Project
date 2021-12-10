using System;
using System.Collections.Generic;
using Common.Actors;
using Common.VFX;
using SecondBreath.Game.Battle.Damage;
using SecondBreath.Game.Battle.Movement;
using SecondBreath.Game.Battle.Movement.Components;
using UnityEngine;
using Object = UnityEngine.Object;

namespace SecondBreath.Game.Battle.Abilities.Mechanics
{
    public class DamageAura : BaseMechanic<DamageAuraData>
    {
        private ITranslatable _casterTranslatable;
        private VfxObject _vfx;

        private float _radius;
        private float _damage;

        protected override void OnInit()
        {
            _casterTranslatable = Caster.Components.Get<ITranslatable>();  
        
            _radius  = StatUpgradeFormula.GetValue(Data.Radius, Level);
            _damage = StatUpgradeFormula.GetValue(Data.Damage, Level);

            _vfx = Object.Instantiate(Data.VFX, Caster.Components.Get<RotationComponent>().transform).GetComponent<VfxObject>();
            _vfx.UpdateScale(_radius);
        }

        public override void Dispose()
        {
            Object.Destroy(_vfx.gameObject);
        }

        protected override void ApplyMechanic(object sender, EventArgs args)
        {
            var targets = new List<IActor>();
            
            foreach (var chooser in _choosers)
            {
                targets.AddRange(chooser.ChooseTarget());
            }
            
            foreach (var enemyActor in targets)
            {
                var enemyTranslatable = enemyActor.Components.Get<ITranslatable>();
                var diff = enemyTranslatable.Position.Value - _casterTranslatable.Position.Value;
                var sqrMagnitude = Vector3.SqrMagnitude(diff);
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