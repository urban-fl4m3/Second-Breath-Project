using System;
using Common.Actors;
using SecondBreath.Common.Logger;
using SecondBreath.Game.Battle.Characters.Configs;
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
        [Inject] private IGameTickWriter _tickHandler;
        [Inject] private DiContainer _diContainer;

        [SerializeField] private string _attackEvent;
        [SerializeField] private Transform projectileSpawner;
        
        private IStatDataContainer _statContainer;

        private IReadOnlyComponentContainer _components;
        private IDisposable _targetSearchingSub;
        private BaseAttackLogic _attackLogic;
        private BattleCharacterData _data;
        private ActorSearcher _searcher;
        
        public void Init(
            IDebugLogger logger, 
            BattleCharacterData data,
            IStatDataContainer statContainer,
            IReadOnlyComponentContainer components)
        {
            base.Init(logger);

            _data = data;
            _components = components;
            _statContainer = statContainer;
            
            _searcher = components.Get<ActorSearcher>();
        }

        public override void Enable()
        {
            base.Enable();

            _attackLogic = _diContainer.Instantiate(_data.AttackLogic.GetType()) as BaseAttackLogic;
            _attackLogic?.Init(_logger, _components, _data, _statContainer, _attackEvent, projectileSpawner);
            
            _targetSearchingSub = _searcher.CurrentTarget.Subscribe(OnTargetFound);
        }

        public override void Disable()
        {
            base.Disable();
            
            _targetSearchingSub?.Dispose();
            _tickHandler.RemoveTick(_attackLogic.TryAttack);
        }

        private void OnTargetFound(IActor target)
        {
            if (target != null)
            {
                _attackLogic.SetTarget(target);
                _tickHandler.AddTick(_attackLogic.TryAttack);
            }
            else
            {
                _tickHandler.RemoveTick(_attackLogic.TryAttack);
            }
        }
    }
}