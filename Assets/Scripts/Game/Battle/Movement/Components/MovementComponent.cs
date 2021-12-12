using System;
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
        //todo remove into some config
        private const float _movementFixDivider = 0.01f;
        
        [Inject] private IGameTickWriter _tickHandler;
        
        [Inject] private IBattleScene _battleScene;
    
        public IReadOnlyReactiveProperty<Vector3> Position => _position;
        public float Radius { get; private set; }
        public float Height { get; private set; }

        private readonly ReactiveProperty<Vector3> _position = new ReactiveProperty<Vector3>();
        
        private RotationComponent _rotationComponent;
        private IMovementAnimator _movementAnimator;
        private IStatDataContainer _statContainer;
        private ActorSearcher _actorSearcher;
        private Transform _transform;
        
        private ITranslatable _target;
        private IDisposable _targetSearchSub;

        private float _movementSpeed;
        
        public void Init(
            IDebugLogger logger, 
            IStatDataContainer statContainer,
            IReadOnlyComponentContainer components,
            Vector3 initialPosition, 
            float radius,
            float height)
        {
            base.Init(logger);

            Radius = radius;
            Height = height;
            
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

            _movementSpeed = _statContainer.GetStatValue(Stat.MovementSpeed);
            _targetSearchSub = _actorSearcher.CurrentTarget.Subscribe(OnTargetFound);
            _tickHandler.AddTick(DoStep);
        }

        public override void Disable()
        {
            base.Disable();

            _targetSearchSub?.Dispose();
            _tickHandler.RemoveTick(DoStep);
        }

        private void DoStep()
        {
            if (_target != null)
            {
                var canMove = CanMove();
                if (canMove)
                {
                    var direction = _actorSearcher.GetDirectionToCurrentTarget();
                    
                    var distance = Vector3.SqrMagnitude(direction);
                    var position = _transform.position;
                    
                    
                    if (distance > _target.Radius * _target.Radius)
                    {
                        var nextPos = _battleScene.Field.PathFinding(this, _target);
                        nextPos.y = 0.0f;
                        _rotationComponent.LookAt(nextPos);
                        direction = nextPos - new Vector3(position.x, 0.0f, position.z);
                        position += direction.normalized * _movementSpeed * Time.deltaTime * _movementFixDivider;
                        ChangePosition(position);
                    }
                }

                _movementAnimator.IsRunning = canMove;
                return;
            }

            _movementAnimator.IsRunning = false;
        }
        
        private void OnTargetFound(IActor actor)
        {
            _target = actor?.Components.Get<ITranslatable>();
        }

        private bool CanMove()
        {
            return !_actorSearcher.IsInAttackRange();
        }

        private void ChangePosition(Vector3 newPosition)
        {
            _position.Value = newPosition;
            transform.position = newPosition;
        }
    }
}