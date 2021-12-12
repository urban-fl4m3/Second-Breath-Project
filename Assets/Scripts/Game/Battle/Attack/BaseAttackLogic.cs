using System;
using Common.Actors;
using Common.Animations;
using SecondBreath.Common.Logger;
using SecondBreath.Game.Battle.Animations;
using SecondBreath.Game.Battle.Characters.Configs;
using SecondBreath.Game.Battle.Movement;
using SecondBreath.Game.Battle.Movement.Components;
using SecondBreath.Game.Battle.Searchers;
using SecondBreath.Game.Stats;
using UnityEngine;

namespace SecondBreath.Game.Battle.Attack
{
    public abstract class BaseAttackLogic 
    {
        protected IActor _target;
        protected BattleCharacterData _data;
        protected Transform _projectileSpawner;
        protected IStatDataContainer _statDataContainer;
        protected AnimationEventHandler _animationEventHandler;
        
        protected float _lastAttackTime;
        protected string _attackEvent;
        protected bool _isAttacking;

        private IDebugLogger _logger;
        private ActorSearcher _searcher;
        private ITranslatable _translatable;
        private IAttackAnimator _attackAnimator;
        private RotationComponent _rotation;
        
        public void Init(
            IDebugLogger logger,
            IReadOnlyComponentContainer components,
            BattleCharacterData data,
            IStatDataContainer statDataContainer, 
            string attackEvent,
            Transform projectileSpawner)
        {
            _data = data;
            _logger = logger;
            _attackEvent = attackEvent;
            _statDataContainer = statDataContainer;
            _projectileSpawner = projectileSpawner;


            _searcher = components.Get<ActorSearcher>();
            _translatable = components.Get<ITranslatable>();
            _attackAnimator = components.Get<IAttackAnimator>();
            _animationEventHandler = components.Get<AnimationEventHandler>();
            _rotation = components.Get<RotationComponent>();
            
            _lastAttackTime = Mathf.NegativeInfinity;
        }
        
        public void TryAttack()
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
                _rotation.LookAt(_target.Components.Get<ITranslatable>());
                _attackAnimator.SetAttackTrigger();
                _isAttacking = true;
                
                _animationEventHandler.Subscribe(_attackEvent, HandleAttackEvent);
            }         
        }

        public void SetTarget(IActor target)
        {
            _target = target;
        }

        protected abstract void HandleAttackEvent(object sender, EventArgs e);
    }
}