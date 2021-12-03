﻿using Common.Actors;
using SecondBreath.Common.Logger;
using SecondBreath.Game.Battle.Animations;
using SecondBreath.Game.Battle.Attack;
using SecondBreath.Game.Battle.Characters.Configs;
using SecondBreath.Game.Battle.Damage;
using SecondBreath.Game.Battle.Health;
using SecondBreath.Game.Battle.Movement;
using SecondBreath.Game.Battle.Movement.Components;
using SecondBreath.Game.Battle.Searchers;
using SecondBreath.Game.Players;
using SecondBreath.Game.Stats;
using SecondBreath.Game.Stats.Formulas;
using UnityEngine;

namespace SecondBreath.Game.Battle.Characters.Actors
{
    [RequireComponent(typeof(MovementComponent))]
    [RequireComponent(typeof(ActorSearcher))]
    [RequireComponent(typeof(RotationComponent))]
    [RequireComponent(typeof(AttackController))]
    [RequireComponent(typeof(BattleCharacterAnimator))]
    [RequireComponent(typeof(HealthComponent))]
    public class BattleCharacter : Actor, IDamageable, IHealable
    {
        public IReadOnlyHealth Health => _healthComponent;
        public IStatDataContainer StatContainer { get; private set; }

        private MovementComponent _movementComponent;
        private AttackController _attackController;
        private ActorSearcher _actorSearcher;
        private HealthComponent _healthComponent;
        
        public void Init(IPlayer owner, BattleCharacterData data, 
            IStatUpgradeFormula statUpgradeFormula, IDebugLogger logger)
        {
            base.Init(owner, logger);
            
            StatContainer = new StatDataContainer(0, statUpgradeFormula, data.Stats, logger);

            SetComponent();

            _actorSearcher.Init(logger, owner.Team, StatContainer, _components);
            _attackController.Init(logger, StatContainer, _components);
            _movementComponent.Init(logger, StatContainer, _components, data.Radius);
            _healthComponent.Init(logger, StatContainer);
        }

        public override void Enable()
        {
            base.Enable();
            
            _actorSearcher.Enable();
            _attackController.Enable();
            _movementComponent.Enable();
            _healthComponent.Enable();
            
            _healthComponent.HealthRemained += HandleHealthChanged;
        }

        public override void Disable()
        {
            base.Disable();
            
            _movementComponent.Disable();
            _attackController.Disable();
            _actorSearcher.Disable();
            _healthComponent.Disable();
            
            _healthComponent.HealthRemained -= HandleHealthChanged;
        }
        
        public void DealDamage(DamageData damageData)
        {
            _healthComponent.RemoveHealth(damageData.DamageAmount);
        }
        
        public void Heal(HealData healData)
        {
            _healthComponent.AddHealth(healData.HealAmount);
        }

        protected override void Kill()
        {
            base.Kill();

            var animator = _components.Get<ICommonCharacterAnimator>();
            animator.SetDeathTrigger();
        }

        private void SetComponent()
        {
            _actorSearcher = _components.Create<ActorSearcher>();
            _attackController = _components.Create<AttackController>();
            _movementComponent = _components.Create<MovementComponent>(typeof(ITranslatable));
            _healthComponent = _components.Create<HealthComponent>(typeof(IReadOnlyHealth));
            _components.Create<IDamageable>();
            _components.Create<IHealable>();
            
            _components.Create<BattleCharacterAnimator>(typeof(IMovementAnimator), typeof(IAttackAnimator),
                typeof(ICommonCharacterAnimator));
        }

        private void HandleHealthChanged(object sender, float health)
        {
            if (health <= 0)
            {
                Kill();
                Disable();
            }
        }
    }
}