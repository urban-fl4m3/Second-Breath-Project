using System;
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
        [Inject] private IGameTickCollection _tickHandler;

        private ActorSearcherUpdate _actorSearcherUpdate;
        private IStatDataContainer _statContainer;
        private ITranslatable _translatable;
        private ITranslatable _targetTranslatable;
        private IDisposable _targetSearchSub;

        public void Init(IDebugLogger logger, Team ownerTeam, IStatDataContainer statContainer, IReadOnlyComponentContainer container)
        {
            base.Init(logger, ownerTeam);

            _statContainer = statContainer;
            _translatable = container.Get<ITranslatable>();
        }

        public override void Enable()
        {
            base.Enable();
            
            _actorSearcherUpdate = new ActorSearcherUpdate(_actorsRegisterer, OwnerTeam, Target, _translatable);
            
            _targetSearchSub = CurrentTarget.Subscribe(OnTargetChanged);
        }

        public override void Disable()
        {
            base.Disable();
            
            _actorSearcherUpdate?.Dispose();
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
                _logger.LogError($"Current target is null for {gameObject.name}!");
                return Vector3.zero;
            }
            
            var position = _translatable.Position;
            var direction = _targetTranslatable.Position - position;

            return direction;
        }
        
        private void OnTargetChanged(IActor target)
        {
            if (target == null)
            {
                _tickHandler.AddTick(_actorSearcherUpdate);
            }
            else
            {
                _targetTranslatable = target.Components.Get<ITranslatable>();
                _tickHandler.RemoveTick(_actorSearcherUpdate);

                target.Killed += HandleTargetKilled;
            }

            void HandleTargetKilled(object sender, EventArgs e)
            {
                target.Killed -= HandleTargetKilled;
                Target.Value = null;
            }
        }
    }
}