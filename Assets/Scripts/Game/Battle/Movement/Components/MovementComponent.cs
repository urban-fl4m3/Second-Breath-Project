using Common.Actors;
using SecondBreath.Common.Logger;
using SecondBreath.Game.Battle.Animations;
using SecondBreath.Game.Battle.Searchers;
using SecondBreath.Game.Stats;
using SecondBreath.Game.Ticks;
using UniRx;
using UnityEngine;
using Zenject;

namespace SecondBreath.Game.Battle.Movement.Components
{
    public class MovementComponent : ActorComponent, ITranslatable
    {
        [Inject] private IGameTickCollection _tickHandler;
    
        public IReadOnlyReactiveProperty<Vector3> Position => _position;
        public float Radius { get; private set; }

        private readonly ReactiveProperty<Vector3> _position = new ReactiveProperty<Vector3>()
            ;
        private RotationComponent _rotationComponent;
        private IMovementAnimator _movementAnimator;
        private IStatDataContainer _statContainer;
        private ActorSearcher _actorSearcher;
        private MovementUpdate _movement;
        private Transform _transform;

        public void Init(IDebugLogger logger, IStatDataContainer statContainer, IReadOnlyComponentContainer components,
            Vector3 initialPosition, float radius)
        {
            base.Init(logger);

            Radius = radius;
            
            _transform = transform;
            _statContainer = statContainer;
            
            _rotationComponent = components.Get<RotationComponent>();
            _movementAnimator = components.Get<IMovementAnimator>();
            _actorSearcher = components.Get<ActorSearcher>();
            
            ChangePosition(initialPosition);
        }

        public override void Enable()
        {
            base.Enable();

            var movementSpeed = _statContainer.GetStatValue(Stat.MovementSpeed);
            _movement = new MovementUpdate(_actorSearcher, _rotationComponent, _transform, movementSpeed, _movementAnimator);
            _movement.PositionChanged += HandlePositionChanged; 
            
            _tickHandler.AddTick(_movement);
        }

        public override void Disable()
        {
            base.Disable();

            if (_movement != null)
            {
                _movement.Dispose();
                _movement.PositionChanged -= HandlePositionChanged;
            }

            _tickHandler.RemoveTick(_movement);
        }

        private void HandlePositionChanged(object sender, Vector3 e)
        {
            ChangePosition(e);
        }

        private void ChangePosition(Vector3 newPosition)
        {
            _position.Value = newPosition;
            transform.position = newPosition;
        }
    }
}