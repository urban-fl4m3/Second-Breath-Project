using System;
using System.Collections.Generic;
using Common.Actors;
using SecondBreath.Common.Logger;
using SecondBreath.Common.States;
using SecondBreath.Game.Battle.Managers;
using SecondBreath.Game.Battle.Phases;
using SecondBreath.Game.Battle.Registration;
using SecondBreath.Game.Players;
using SecondBreath.Game.Ticks;
using Zenject;

namespace SecondBreath.Game.States.Concrete
{
    public class BattleState : BaseState
    {
        public const int DEBUG_UNITS_COUNT = 10;

        private readonly IDebugLogger _logger;
        private readonly DiContainer _container;
        private readonly IGameTickHandler _tickHandler;
        private readonly ITeamObjectRegisterer<IActor> _actorRegisterer;

        private Dictionary<IPlayer, IBattleManager> _battleManagers;
        private Queue<IBattlePhase> _battlePhases;

        private IBattlePhase _currentPhase;
        
        public BattleState(DiContainer container, IDebugLogger logger, IGameTickHandler tickHandler,
            ITeamObjectRegisterer<IActor> actorRegisterer)
        {
            _logger = logger;
            _container = container;
            _tickHandler = tickHandler;
            _actorRegisterer = actorRegisterer;
        }

        protected override void OnEnter()
        {
            CreateBattleManagers();
            
            _tickHandler.StartTicking();
            
            _battlePhases = new Queue<IBattlePhase>();
            _battlePhases.Enqueue(new PreparePhase(_battleManagers.Values, _logger, _actorRegisterer));
            
            NextPhase();
        }

        protected override void OnExit()
        {
            _battleManagers.Clear();
            _tickHandler.StopTicking();
        }

        private void CreateBattleManagers()
        {
            var playerManager = _container.Instantiate<BattlePlayerManager>();
            var enemyManager = _container.Instantiate<BattleEnemyManager>();

            _battleManagers = new Dictionary<IPlayer, IBattleManager>
            {
                { playerManager.Player, playerManager },
                { enemyManager.Player, enemyManager }
            };
        }

        private void NextPhase()
        {
            if (_battlePhases.Count > 0)
            {
                var nextPhase = _battlePhases.Dequeue();
                _currentPhase = nextPhase;

                nextPhase.PhaseCompleted += HandlePhaseCompleted;
                nextPhase.Run();
            }
        }

        private void HandlePhaseCompleted(object sender, EventArgs e)
        {
            if (_currentPhase != null)
            {
                _currentPhase.End();
                _currentPhase.PhaseCompleted -= HandlePhaseCompleted;
            }
            
            NextPhase();
        }
    }
}