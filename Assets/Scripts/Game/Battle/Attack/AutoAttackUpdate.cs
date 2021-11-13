using Common.Actors;
using SecondBreath.Common.Logger;
using SecondBreath.Common.Ticks;
using SecondBreath.Game.Battle.Movement;
using SecondBreath.Game.Battle.Searchers;

namespace SecondBreath.Game.Battle.Attack
{
    public class AutoAttackUpdate : ITickUpdate
    {
        private readonly IDebugLogger _logger;
        private readonly ActorSearcher _searcher;
        private readonly ITranslatable _translatable;

        private IActor _target;
        
        public AutoAttackUpdate(IDebugLogger logger, ITranslatable translatable,  ActorSearcher searcher)
        {
            _logger = logger;
            _searcher = searcher;
            _translatable = translatable;
        }
        
        public void Update()
        {
            if (_target == null || _translatable == null)
            {
                _logger.LogError("Trying auto attack null object!");
                return;
            }

            if (_searcher.IsInAttackRange())
            {
                
            }         
        }

        public void SetTarget(IActor target)
        {
            _target = target;
        }
    }
}