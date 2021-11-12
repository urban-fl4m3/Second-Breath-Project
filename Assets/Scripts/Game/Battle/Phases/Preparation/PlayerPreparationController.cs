using System;
using SecondBreath.Common.Logger;
using SecondBreath.Game.Battle.Characters;
using SecondBreath.Game.Battle.Ticks;
using SecondBreath.Game.Players;
using SecondBreath.Game.States.Concrete;
using SecondBreath.Game.Ticks;
using UnityEngine;

namespace SecondBreath.Game.Battle.Phases
{
    public class PlayerPreparationController : IBattlePreparationController
    {
        public event EventHandler UnitsPrepared;

        private readonly IPlayer _player;
        private readonly IDebugLogger _debugLogger;
        private readonly BattleCharactersFactory _battleCharactersFactory;
        private readonly IGameTickCollection _gameTickHandler;
        private readonly BattleFieldPointSelection _fieldMouseSelector;

        private int _selectedPoints;

        public PlayerPreparationController(IPlayer player, IBattleField battleField, IGameTickCollection gameTickHandler,
            IDebugLogger debugLogger, BattleCharactersFactory battleCharactersFactory)
        {
            _player = player;
            _debugLogger = debugLogger;
            _battleCharactersFactory = battleCharactersFactory;
            _gameTickHandler = gameTickHandler;
            _fieldMouseSelector = new BattleFieldPointSelection(player, battleField, debugLogger);
        }
        
        public void PrepareUnits()
        {
            _fieldMouseSelector.PositionSelected += HandleSelectedPosition;
            _gameTickHandler.AddTick(_fieldMouseSelector); 
        }
        
        private void HandleSelectedPosition(object sender, Vector2 selectedPosition)
        {
            _selectedPoints++;

            var spawnPosition = new Vector3(selectedPosition.x, 0, selectedPosition.y);
            _battleCharactersFactory.SpawnRandomCharacter(_player, spawnPosition);
            
            if (_selectedPoints >= BattleState.DEBUG_UNITS_COUNT)
            {
                _fieldMouseSelector.PositionSelected -= HandleSelectedPosition;
                UnitsPrepared?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}