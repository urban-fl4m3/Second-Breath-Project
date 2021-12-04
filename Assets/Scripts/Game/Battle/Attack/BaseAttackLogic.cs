using System;
using Common.Actors;
using Common.Animations;
using SecondBreath.Common.Logger;
using SecondBreath.Common.Ticks;
using SecondBreath.Game.Battle.Animations;
using SecondBreath.Game.Battle.Characters.Configs;
using SecondBreath.Game.Battle.Damage;
using SecondBreath.Game.Battle.Movement;
using SecondBreath.Game.Battle.Searchers;
using SecondBreath.Game.Stats;
using UnityEngine;

namespace SecondBreath.Game.Battle.Attack
{
    public abstract class BaseAttackLogic : ITickUpdate
    {
        
        protected string _attackEvent;
        protected IDebugLogger _logger;
        protected ActorSearcher _searcher;
        protected BattleCharacterData _data;
        protected ITranslatable _translatable;
        protected IAttackAnimator _attackAnimator;
        protected IStatDataContainer _statDataContainer;
        protected AnimationEventHandler _animationEventHandler;

        protected IDamageable _target;
        protected float _lastAttackTime;
        protected bool _isAttacking;
        
        public BaseAttackLogic()
        {

        }

        public void init(IDebugLogger logger, ITranslatable translatable,  ActorSearcher searcher, 
            IAttackAnimator attackAnimator, BattleCharacterData data, IStatDataContainer statDataContainer, AnimationEventHandler animationEventHandler,
            string attackEvent)
        {
            _data = data;
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

        public void SetTarget(IActor target)
        {
            _target = target.Components.Get<IDamageable>();
        }

        protected abstract void HandleAttackEvent(object sender, EventArgs e);
    }
}