using System;
using Common.Actors;
using Common.Animations;
using SecondBreath.Common.Logger;
using SecondBreath.Game.Battle.Animations;
using SecondBreath.Game.Battle.Movement;
using SecondBreath.Game.Battle.Searchers;
using SecondBreath.Game.Stats;
using SecondBreath.Game.Ticks;
using UniRx;
using UnityEngine;
using Zenject;

namespace SecondBreath.Game.Battle.Attack
{
    public class AttackController : ActorComponent
    {
        [Inject] private IGameTickCollection _tickHandler;

        [SerializeField] private string _attackEvent;
        
        private IStatDataContainer _statContainer;

        private ActorSearcher _searcher;
        private ITranslatable _translatable;
        private IAttackAnimator _attackAnimator;
        private IDisposable _targetSearchingSub;
        private AutoAttackUpdate _autoAttackUpdate;
        private AnimationEventHandler _animationEventHandler;
        
        public void Init(IDebugLogger logger, IStatDataContainer statContainer, IReadOnlyComponentContainer components)
        {
            base.Init(logger);

            _statContainer = statContainer;

            _searcher = components.Get<ActorSearcher>();
            _translatable = components.Get<ITranslatable>();
            _attackAnimator = components.Get<IAttackAnimator>();
            _animationEventHandler = components.Get<AnimationEventHandler>();
        }

        public override void Enable()
        {
            base.Enable();

            _autoAttackUpdate = new AutoAttackUpdate(_logger, _translatable, _searcher, _attackAnimator, _statContainer,
                _animationEventHandler, _attackEvent);
            
            _targetSearchingSub = _searcher.CurrentTarget.Subscribe(OnTargetFound);
        }

        public override void Disable()
        {
            base.Disable();
            
            _targetSearchingSub?.Dispose();
            _tickHandler.RemoveTick(_autoAttackUpdate);
        }

        private void OnTargetFound(IActor target)
        {
            if (target != null)
            {
                _autoAttackUpdate.SetTarget(target);
                _tickHandler.AddTick(_autoAttackUpdate);
            }
            else
            {
                _tickHandler.RemoveTick(_autoAttackUpdate);
            }
        }
    }
}