using System;
using Common.Actors;
using Common.Animations;
using SecondBreath.Common.Logger;
using SecondBreath.Common.Ticks;
using SecondBreath.Game.Battle.Animations;
using SecondBreath.Game.Battle.Damage;
using SecondBreath.Game.Battle.Movement;
using SecondBreath.Game.Battle.Searchers;
using SecondBreath.Game.Stats;
using UnityEngine;

namespace SecondBreath.Game.Battle.Attack
{
    public class AutoAttackUpdate : ITickUpdate
    {
        private readonly string _attackEvent;
        private readonly IDebugLogger _logger;
        private readonly ActorSearcher _searcher;
        private readonly ITranslatable _translatable;
        private readonly IAttackAnimator _attackAnimator;
        private readonly IStatDataContainer _statDataContainer;
        private readonly AnimationEventHandler _animationEventHandler;

        private IDamageable _target;
        private float _lastAttackTime;
        private bool _isAttacking;
        
        public AutoAttackUpdate(IDebugLogger logger, ITranslatable translatable,  ActorSearcher searcher, 
            IAttackAnimator attackAnimator, IStatDataContainer statDataContainer, AnimationEventHandler animationEventHandler,
            string attackEvent)
        {
            _logger = logger;
            _searcher = searcher;
            _attackEvent = attackEvent;
            _translatable = translatable;
            _attackAnimator = attackAnimator;
            _statDataContainer = statDataContainer;
            _animationEventHandler = animationEventHandler;

            _lastAttackTime = Mathf.NegativeInfinity;
        }
        
        public void Update()
        {
            if (_isAttacking)
            {
                return;
            }
            
            if (_target == null || _translatable == null)
            {
                _logger.LogError("Trying auto attack null object!");
                return;
            }

            var attackSpeed = _statDataContainer.GetStatValue(Stat.AttackSpeed);

            if (_lastAttackTime + 1 / attackSpeed > Time.time)
            {
                return;
            }

            if (_searcher.IsInAttackRange())
            {
                _attackAnimator.SetAttackTrigger();
                _isAttacking = true;
                
                _animationEventHandler.Subscribe(_attackEvent, HandleAttackEvent);
            }         
        }

        private void HandleAttackEvent(object sender, EventArgs e)
        {
            _isAttacking = false;
            _animationEventHandler.Unsubscribe(_attackEvent);
            
            _lastAttackTime = Time.time;

            var damageData = new DamageData(_statDataContainer.GetStatValue(Stat.AttackDamage));
            _target.DealDamage(damageData);
        }

        public void SetTarget(IActor target)
        {
            _target = target.Components.Get<IDamageable>();
        }
    }
}