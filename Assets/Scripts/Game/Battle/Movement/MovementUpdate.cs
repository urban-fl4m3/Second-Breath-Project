using System;
using Common.Actors;
using SecondBreath.Common.Ticks;
using SecondBreath.Game.Battle.Animations;
using SecondBreath.Game.Battle.Movement.Components;
using SecondBreath.Game.Battle.Searchers;
using UniRx;
using UnityEngine;

namespace SecondBreath.Game.Battle.Movement
{
    //todo refactor. Subscribe should be in movement component
    public class MovementUpdate : ITickUpdate, IDisposable
    {
        //todo remove into some config
        private const float _movementFixDivider = 0.01f;
        
        private readonly RotationComponent _rotationComponent;
        private readonly ActorSearcher _searcher;
        private readonly Transform _transform;
        private readonly float _movementSpeed;
        private readonly IMovementAnimator _movementAnimator;

        private readonly IDisposable _targetSearchSub;
        private ITranslatable _target;
        
        public MovementUpdate(ActorSearcher searcher, RotationComponent rotationComponent, Transform transform,
            float movementSpeed, IMovementAnimator movementAnimator)
        {
            _searcher = searcher;
            _transform = transform;
            _movementSpeed = movementSpeed;
            _movementAnimator = movementAnimator;
            _rotationComponent = rotationComponent;

            _targetSearchSub = searcher.CurrentTarget.Subscribe(OnTargetFound);
        }
        
        public void Update()
        {
            if (_target != null)
            {
                var canMove = CanMove();
                if (canMove)
                {
                    var direction = _searcher.GetDirectionToCurrentTarget();
                    var distance = Vector3.SqrMagnitude(direction);
                    var position = _transform.position;
                    
                    if (distance > _target.Radius * _target.Radius)
                    {
                        position += direction.normalized * _movementSpeed * Time.deltaTime * _movementFixDivider;
                        _transform.position = position;
                    }
                }

                _rotationComponent.LookAt(_target);

                _movementAnimator.IsRunning = canMove;
                return;
            }

            _movementAnimator.IsRunning = false;
        }

        public void Dispose()
        {
            _targetSearchSub?.Dispose();
        }

        private void OnTargetFound(IActor actor)
        {
            _target = actor?.Components.Get<ITranslatable>();
        }

        private bool CanMove()
        {
            return !_searcher.IsInAttackRange();
        }
    }
}