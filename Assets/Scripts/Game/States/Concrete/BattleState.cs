using System;
using System.Collections.Generic;
using SecondBreath.Common.Logger;
using SecondBreath.Common.States;
using SecondBreath.Game.Battle;
using SecondBreath.Game.Battle.Characters;
using SecondBreath.Game.Battle.Managers;
using SecondBreath.Game.Battle.Phases;
using SecondBreath.Game.Players;
using SecondBreath.Game.Ticks;

namespace SecondBreath.Game.States.Concrete
{
    public class BattleState : BaseState
    {
        public const int DEBUG_UNITS_COUNT = 3;
        
        private readonly IGameTickHandler _tickHandler;
        private readonly IBattleScene _battleScene;
        private readonly IDebugLogger _logger;
        private readonly BattleCharactersFactory _battleCharactersFactory;

        private Dictionary<IPlayer, IBattleManager> _battleManagers;
        private Queue<IBattlePhase> _battlePhases;

        private IBattlePhase _currentPhase;
        
        public BattleState(IGameTickHandler tickHandler, IBattleScene battleScene, IDebugLogger logger,
            BattleCharactersFactory battleCharactersFactory)
        {
            _tickHandler = tickHandler;
            _battleScene = battleScene;
            _logger = logger;
            _battleCharactersFactory = battleCharactersFactory;
        }

        protected override void OnEnter()
        {
            CreateBattleManagers();
            
            _tickHandler.StartTicking();
            
            _battlePhases = new Queue<IBattlePhase>();
            _battlePhases.Enqueue(new PreparePhase(_battleManagers.Values, _logger));
            
            NextPhase();
        }

        protected override void OnExit()
        {
            _battleManagers.Clear();
            _tickHandler.StopTicking();
        }

        private void CreateBattleManagers()
        {
            var playerManager = new BattlePlayerManager(_tickHandler, _battleScene.Field, _logger, _battleCharactersFactory);
            var enemyManager = new BattleEnemyManager(_battleScene.Field, _battleCharactersFactory);

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