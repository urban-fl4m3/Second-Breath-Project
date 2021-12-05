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

        private float _radius;
        private float _damage;

        private VfxObject _vfx;

        protected override void OnInit(DamageAuraData data)
        {
            _casterTranslatable = Caster.Components.Get<ITranslatable>();  
        
            _radius  = StatUpgradeFormula.GetValue(Data.Radius, Level);
            _damage = StatUpgradeFormula.GetValue(Data.Damage, Level);

            _vfx = Object.Instantiate(data.VFX, Caster.Components.Get<RotationComponent>().transform).GetComponent<VfxObject>();
            _vfx.UpdateScale(_radius);
        }

        public override void Dispose()
        {
            Object.Destroy(_vfx.gameObject);
        }

        public override void Action(object sender, EventArgs args)
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