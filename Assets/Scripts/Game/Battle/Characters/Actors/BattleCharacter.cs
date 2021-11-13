using Common.Actors;
using SecondBreath.Common.Logger;
using SecondBreath.Game.Battle.Characters.Configs;
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
    public class BattleCharacter : Actor
    {
        public IStatDataContainer StatContainer { get; private set; }
        
        private MovementComponent _movementComponent;
        private ActorSearcher _actorSearcher;
        
        public void Init(IPlayer owner, BattleCharacterData data, 
            IStatUpgradeFormula statUpgradeFormula, IDebugLogger logger)
        {
            base.Init(owner, logger);
            
            StatContainer = new StatDataContainer(0, statUpgradeFormula, data.Stats, logger);

            _actorSearcher = _components.Create<ActorSearcher>();
            _movementComponent = _components.Create<MovementComponent>(typeof(ITranslatable));
            
            _actorSearcher.Init(logger, owner.Team, _components);
            _movementComponent.Init(logger, StatContainer, _components, data.Radius);
        }

        public override void Enable()
        {
            base.Enable();
            
            _actorSearcher.Enable();
            _movementComponent.Enable();
        }

        public override void Disable()
        {
            base.Disable();
            
            _movementComponent.Disable();
        }
    }
}