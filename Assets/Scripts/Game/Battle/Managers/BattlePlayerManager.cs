using System;
using SecondBreath.Common.Logger;
using SecondBreath.Game.Battle.Ticks;
using SecondBreath.Game.Players;
using SecondBreath.Game.Ticks;
using UnityEngine;

namespace SecondBreath.Game.Battle.Managers
{
    public class BattlePlayerManager : IBattleManager
    {
        public event EventHandler UnitsPrepared;
        public  IPlayer Player { get; }
        
        private readonly IGameTickCollection _gameTickHandler;
        private readonly IBattleField _battleField;
        private readonly IDebugLogger _debugLogger;

        private BattleFieldPointSelection _fieldMouseSelector;
        
        public BattlePlayerManager(IGameTickCollection gameTickHandler, IBattleField battleField,
            IDebugLogger debugLogger)
        {
            _gameTickHandler = gameTickHandler;
            _battleField = battleField;
            _debugLogger = debugLogger;
            Player = new GamePlayer(Team.Green);
        }
        
        public void PrepareUnits()
        {
            _fieldMouseSelector = new BattleFieldPointSelection(Player, _battleField, _debugLogger);
            _fieldMouseSelector.PositionSelected += HandleSelectedPosition;
            _gameTickHandler.AddTick(_fieldMouseSelector); 
        }

        private int _selectedPoints;

        private void HandleSelectedPosition(object sender, Vector2 selectedPosition)
        {
            _debugLogger.Log($"Selected {selectedPosition}.");
            _selectedPoints++;

            if (_selectedPoints >= 3)
            {
                _fieldMouseSelector.PositionSelected -= HandleSelectedPosition;
                UnitsPrepared?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}