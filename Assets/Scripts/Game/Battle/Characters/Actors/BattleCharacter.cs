using System.Collections.Generic;
using Common.Actors;
using SecondBreath.Common.Logger;
using SecondBreath.Game.Battle.Movement.Components;
using SecondBreath.Game.Players;
using SecondBreath.Game.Stats;
using SecondBreath.Game.Stats.Formulas;
using UnityEngine;

namespace SecondBreath.Game.Battle.Characters.Actors
{
    [RequireComponent(typeof(MovementComponent))]
    public class BattleCharacter : Actor
    {
        public IStatDataContainer StatContainer { get; private set; }
        
        private MovementComponent _movementComponent;
        
        public void Init(IPlayer owner, IReadOnlyDictionary<Stat, StatData> stats, 
            IStatUpgradeFormula statUpgradeFormula, IDebugLogger logger)
        {
            base.Init(owner, logger);
            
            StatContainer = new StatDataContainer(0, statUpgradeFormula, stats, logger);
            _movementComponent = _components.Create<MovementComponent>();
            _movementComponent.Init(logger, StatContainer, _components);
        }

        public override void Enable()
        {
            base.Enable();
            
            _movementComponent.Enable();
        }

        public override void Disable()
        {
            base.Disable();
            
            _movementComponent.Disable();
        }
    }
}