using System;
using System.Collections.Generic;
using Common.Actors;
using SecondBreath.Common.Logger;
using SecondBreath.Game.Battle.Movement;
using SecondBreath.Game.Battle.Registration;
using SecondBreath.Game.Players;
using SecondBreath.Game.Stats;
using SecondBreath.Game.Ticks;
using Zenject;
using UniRx;
using UnityEngine;

namespace SecondBreath.Game.Battle.Searchers
{
    public class ActorSearcher : BaseTargetSearcher<IActor>
    {
        [Inject] private ITeamObjectRegisterer<IActor> _actorsRegisterer;
        [Inject] private IGameTickWriter _tickHandler;
        
        private IStatDataContainer _statContainer;
        private ITranslatable _translatable;
        private ITranslatable _targetTranslatable;
        private IDisposable _targetSearchSub;

        private HashSet<IActor> _enemies;
        
        public void Init(IDebugLogger logger, Team ownerTeam, IStatDataContainer statContainer, IReadOnlyComponentContainer container)
        {
            base.Init(logger, ownerTeam);
            
            _enemies = new HashSet<IActor>(_actorsRegisterer.GetOppositeTeamObjects(ownerTeam));

            _actorsRegisterer.ObjectRegistered += HandleActorRegistered;
            _actorsRegisterer.ObjectUnregistered += HandleActorUnregistered;
            
            _statContainer = statContainer;
            _translatable = container.Get<ITranslatable>();
        }

        public override void Enable()
        {
            base.Enable();
            
            _targetSearchSub = CurrentTarget.Subscribe(OnTargetChanged);
        }

        public override void Disable()
        {
            base.Disable();
            
            _actorsRegisterer.ObjectRegistered -= HandleActorRegistered;
            _actorsRegisterer.ObjectUnregistered -= HandleActorUnregistered;
            
            _targetSearchSub?.Dispose();
        }

        public bool IsInAttackRange()
        {
            if (CurrentTarget.Value == null)
            {
                return false;
            }
            
            var distance = GetDistanceToCurrentTarget();
            var attackRange = _statContainer.GetStatValue(Stat.AttackRange);
            var attackRadius = attackRange * attackRange + _targetTranslatable.Radius * _targetTranslatable.Radius;

            return attackRadius >= distance;
        }

        public float GetDistanceToCurrentTarget()
        {
            var direction = GetDirectionToCurrentTarget();
            return Vector3.SqrMagnitude(direction);
        }
        
        public Vector3 GetDirectionToCurrentTarget()
        {
            if (CurrentTarget.Value == null)
            {
                //    _logger.LogError($"Current target is null for {gameObject.name}!");
                return Vector3.zero;
            }
            
            var position = _translatable.Position;
            var direction = _targetTranslatable.Position.Value - position.Value;

            return direction;
        }

        private void UpdateTarget()
        {
            Target.Value ??= GetNearestEnemyActor();
        }
        
        private void OnTargetChanged(IActor target)
        {
            if (target == null)
            {
                _tickHandler.AddTick(UpdateTarget);
            }
            else
            {
                _targetTranslatable = target.Components.Get<ITranslatable>();
                _tickHandler.RemoveTick(UpdateTarget);

                target.Killed += HandleTargetKilled;
            }

            void HandleTargetKilled(object sender, EventArgs e)
            {
                target.Killed -= HandleTargetKilled;
                Target.Value = null;
            }
        }
        
        private IActor GetNearestEnemyActor()
        {
            IActor nearestActor = null;
            var lowestDistance = float.MaxValue;

            foreach (var enemy in _enemies)
            {
                var enemyTranslatable = enemy.Components.Get<ITranslatable>();
                var diff = enemyTranslatable.Position.Value - _translatable.Position.Value;
                var dist = Vector3.SqrMagnitude(diff);
                
                if (dist < lowestDistance)
                {
                    lowestDistance = dist;
                    nearestActor = enemy;
                }
            }

            return nearestActor;
        }
        
        private void HandleActorRegistered(object sender, RegistrationTeamObjectArgs e)
        {
            if (e.Team != OwnerTeam)
            {
                _enemies.Add(e.Obj);
            }
        }

        private void HandleActorUnregistered(object sender, RegistrationTeamObjectArgs e)
        {
            if (e.Team != OwnerTeam)
            {
                _enemies.Remove(e.Obj);
            }
        }
    }
}