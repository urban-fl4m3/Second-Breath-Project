using Common.Actors;
using SecondBreath.Common.Logger;
using SecondBreath.Game.Battle.Animations;
using SecondBreath.Game.Battle.Attack;
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
    [RequireComponent(typeof(AttackController))]
    [RequireComponent(typeof(BattleCharacterAnimator))]
    public class BattleCharacter : Actor
    {
        public IStatDataContainer StatContainer { get; private set; }

        private MovementComponent _movementComponent;
        private AttackController _attackController;
        private ActorSearcher _actorSearcher;
        
        public void Init(IPlayer owner, BattleCharacterData data, 
            IStatUpgradeFormula statUpgradeFormula, IDebugLogger logger)
        {
            base.Init(owner, logger);
            
            StatContainer = new StatDataContainer(0, statUpgradeFormula, data.Stats, logger);

            SetComponent();

            _actorSearcher.Init(logger, owner.Team, StatContainer, _components);
            _attackController.Init(logger, StatContainer, _components);
            _movementComponent.Init(logger, StatContainer, _components, data.Radius);
        }

        public override void Enable()
        {
            base.Enable();
            
            _actorSearcher.Enable();
            _attackController.Enable();
            _movementComponent.Enable();
        }

        public override void Disable()
        {
            base.Disable();
            
            _movementComponent.Disable();
        }

        private void SetComponent()
        {
            _actorSearcher = _components.Create<ActorSearcher>();
            _attackController = _components.Create<AttackController>();
            _movementComponent = _components.Create<MovementComponent>(typeof(ITranslatable));
            _components.Create<BattleCharacterAnimator>(typeof(IMovementAnimator), typeof(IAttackAnimator),
                typeof(ICommonCharacterAnimator));
        }
    }
}