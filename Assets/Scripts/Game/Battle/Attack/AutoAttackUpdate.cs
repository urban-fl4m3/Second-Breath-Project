using Common.Actors;
using SecondBreath.Common.Logger;
using SecondBreath.Common.Ticks;
using SecondBreath.Game.Battle.Animations;
using SecondBreath.Game.Battle.Movement;
using SecondBreath.Game.Battle.Searchers;
using SecondBreath.Game.Stats;
using UnityEngine;

namespace SecondBreath.Game.Battle.Attack
{
    public class AutoAttackUpdate : ITickUpdate
    {
        private readonly IDebugLogger _logger;
        private readonly ActorSearcher _searcher;
        private readonly ITranslatable _translatable;
        private readonly IAttackAnimator _attackAnimator;
        private readonly IStatDataContainer _statDataContainer;

        private IActor _target;
        private float _lastAttackTime;
        
        public AutoAttackUpdate(IDebugLogger logger, ITranslatable translatable,  ActorSearcher searcher, 
            IAttackAnimator attackAnimator, IStatDataContainer statDataContainer)
        {
            _logger = logger;
            _searcher = searcher;
            _translatable = translatable;
            _attackAnimator = attackAnimator;
            _statDataContainer = statDataContainer;
        }
        
        public void Update()
        {
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
                _lastAttackTime = Time.time;
            }         
        }

        public void SetTarget(IActor target)
        {
            _target = target;
        }
    }
}