using System;
using System.Collections.Generic;
using System.Linq;
using SecondBreath.Common.Logger;
using SecondBreath.Common.States;
using SecondBreath.Game.Battle;
using SecondBreath.Game.Battle.Managers;
using SecondBreath.Game.Battle.Phases;
using SecondBreath.Game.Players;
using SecondBreath.Game.Ticks;

namespace SecondBreath.Game.States.Concrete
{
    public class BattleState : BaseState
    {
        private readonly IGameTickHandler _tickHandler;
        private readonly IBattleScene _battleScene;
        private readonly IDebugLogger _logger;

        private Dictionary<IPlayer, IBattleManager> _battleManagers;
        private Queue<IBattlePhase> _battlePhases;

        private IBattlePhase _currentPhase;
        
        public BattleState(IGameTickHandler tickHandler, IBattleScene battleScene, IDebugLogger logger)
        {
            _tickHandler = tickHandler;
            _battleScene = battleScene;
            _logger = logger;
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
            var playerManager = new BattlePlayerManager(_tickHandler, _battleScene.Field, _logger);
            var enemyManager = new BattleEnemyManager();

            _battleManagers = new Dictionary<IPlayer, IBattleManager>
            {
                { playerManager.Player, playerManager },
                { enemyManager.Player, enemyManager }
            };
        }

        private void NextPhase()
        {
            if (_battlePhases.Any())
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