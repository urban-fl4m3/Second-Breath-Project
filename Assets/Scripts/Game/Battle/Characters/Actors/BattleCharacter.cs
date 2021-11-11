using System.Collections.Generic;
using Common.Actors;
using SecondBreath.Common.Logger;
using SecondBreath.Game.Battle.Characters.Components;
using SecondBreath.Game.Players;
using SecondBreath.Game.Stats;
using UnityEngine;

namespace SecondBreath.Game.Battle.Characters.Actors
{
    [RequireComponent(typeof(MovementComponent))]
    public class BattleCharacter : Actor
    {
        private MovementComponent _movementComponent;
        
        public void Init(IPlayer owner, IReadOnlyDictionary<Stat, StatData> stats, IDebugLogger logger)
        {
            base.Init(owner, logger);

            _movementComponent = _components.Create<MovementComponent>();
            _movementComponent.Init(logger);
        }
    }
}