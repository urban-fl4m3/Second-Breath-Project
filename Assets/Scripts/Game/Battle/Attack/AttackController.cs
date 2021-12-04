using System;
using Common.Actors;
using Common.Animations;
using SecondBreath.Common.Logger;
using SecondBreath.Game.Battle.Animations;
using SecondBreath.Game.Battle.Characters.Configs;
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
        [Inject] private DiContainer _diContainer;

        [SerializeField] private string _attackEvent;
        
        private IStatDataContainer _statContainer;

        private ActorSearcher _searcher;
        private ITranslatable _translatable;
        private IAttackAnimator _attackAnimator;
        private IDisposable _targetSearchingSub;
        private BaseAttackLogic _attackLogic;
        private AnimationEventHandler _animationEventHandler;
        private BattleCharacterData _data;
        
        public void Init(IDebugLogger logger, BattleCharacterData data, IStatDataContainer statContainer, IReadOnlyComponentContainer components)
        {
            base.Init(logger);

            _data = data;
            _statContainer = statContainer;

            _searcher = components.Get<ActorSearcher>();
            _translatable = components.Get<ITranslatable>();
            _attackAnimator = components.Get<IAttackAnimator>();
            _animationEventHandler = components.Get<AnimationEventHandler>();
        }

        public override void Enable()
        {
            base.Enable();

            _attackLogic = _diContainer.Instantiate(_data.attackLogic.GetType()) as BaseAttackLogic;
            _attackLogic?.init(_logger, _translatable, _searcher, _attackAnimator, _data, _statContainer,
                _animationEventHandler, _attackEvent);
            
            _targetSearchingSub = _searcher.CurrentTarget.Subscribe(OnTargetFound);
        }

        public override void Disable()
        {
            base.Disable();
            
            _targetSearchingSub?.Dispose();
            _tickHandler.RemoveTick(_attackLogic);
        }

        private void OnTargetFound(IActor target)
        {
            if (target != null)
            {
                _attackLogic.SetTarget(target);
                _tickHandler.AddTick(_attackLogic);
            }
            else
            {
                _tickHandler.RemoveTick(_attackLogic);
            }
        }
    }
}