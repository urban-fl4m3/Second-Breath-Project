using System;
using System.Collections.Generic;
using SecondBreath.Common.Logger;
using SecondBreath.Game.Battle.Managers;

namespace SecondBreath.Game.Battle.Phases
{
    public class PreparePhase : IBattlePhase
    {
        public event EventHandler PhaseCompleted;
        
        private readonly IEnumerable<IBattleManager> _battleManagers;
        private readonly IDebugLogger _debugLogger;

        private int _managersCount;
        
        public PreparePhase(IReadOnlyCollection<IBattleManager> battleManagers, IDebugLogger debugLogger)
        {
            _battleManagers = battleManagers;
            _debugLogger = debugLogger;
            _managersCount = battleManagers.Count;
        }
        
        public void Run()
        {
            foreach (var battleManager in _battleManagers)
            {
                RunManagerPreparation(battleManager);
            }
        }

        public void End()
        {
            _debugLogger.Log("Prepare phase ends.");
        }

        private void RunManagerPreparation(IBattleManager manager)
        {
            var preparationController = manager.GetPreparationController();
            
            preparationController.UnitsPrepared += HandlePlayerPrepared;
            preparationController.PrepareUnits();
            
            void HandlePlayerPrepared(object sender, EventArgs e)
            {
                _managersCount--;
                preparationController.UnitsPrepared -= HandlePlayerPrepared;

                if (_managersCount <= 0)
                {
                    PhaseCompleted?.Invoke(this, EventArgs.Empty);
                }
            }
        }
    }
}