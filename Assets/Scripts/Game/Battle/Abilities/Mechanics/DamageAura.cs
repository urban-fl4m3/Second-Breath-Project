using System.Collections.Generic;
using Common.Actors;
using SecondBreath.Game.Battle.Damage;
using SecondBreath.Game.Battle.Movement;
using SecondBreath.Game.Battle.Registration;
using SecondBreath.Game.Stats.Formulas;
using SecondBreath.Game.Ticks;
using UnityEngine;

namespace SecondBreath.Game.Battle.Abilities.Mechanics
{
    public class DamageAura : BaseMechanic<DamageAuraData>
    {
        private ITeamObjectRegisterer<IActor> _actorRegisterer;
        private IStatUpgradeFormula _statUpgradeFormula;
        private ITranslatable _casterTranslatable;
        private IGameTickWriter _gameTickWriter;

        private float _radius;
        private float _damage;

        protected override void OnInit(IActor owner, DamageAuraData data)
        {
            _actorRegisterer = Container.Resolve<ITeamObjectRegisterer<IActor>>();
            _gameTickWriter = Container.Resolve<IGameTickWriter>();
            _statUpgradeFormula = Container.Resolve<IStatUpgradeFormula>();
            _casterTranslatable = Caster.Components.Get<ITranslatable>();  
        
            _radius  = _statUpgradeFormula.GetValue(Data.Radius, Level);
            _damage = _statUpgradeFormula.GetValue(Data.Damage, Level);

            _gameTickWriter.AddTick(ApplyMechanic);
        }

        public override void Dispose()
        {
            _gameTickWriter.RemoveTick(ApplyMechanic);
        }

        private void ApplyMechanic()
        {
            var enemies = _actorRegisterer.GetOppositeTeamObjects(Caster.Owner.Team);
            var localEnemies = new List<IActor>(enemies);
            
            foreach (var enemyActor in localEnemies)
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