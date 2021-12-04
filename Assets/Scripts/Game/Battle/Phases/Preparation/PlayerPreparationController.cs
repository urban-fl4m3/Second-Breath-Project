using System;
using SecondBreath.Common.Logger;
using SecondBreath.Game.Battle.Characters;
using SecondBreath.Game.Battle.Signals;
using SecondBreath.Game.Battle.Ticks;
using SecondBreath.Game.Players;
using SecondBreath.Game.States.Concrete;
using SecondBreath.Game.Ticks;
using SecondBreath.Game.UI;
using UnityEngine;
using Zenject;

namespace SecondBreath.Game.Battle.Phases
{
    public class PlayerPreparationController : IBattlePreparationController
    {
        public event EventHandler UnitsPrepared;

        private readonly IPlayer _player;
        private readonly BattleCharactersFactory _battleCharactersFactory;
        private readonly ViewFactory _viewFactory;
        private readonly ViewProvider _viewProvider;
        private readonly SignalBus _signalBus;
        private readonly IGameTickCollection _gameTickHandler;
        private readonly BattleFieldPointSelection _fieldMouseSelector;

        private int _selectedPoints;
        private int _selectedUnitId;

        private IView _selectionView;
        
        public PlayerPreparationController(IPlayer player, IBattleField battleField, IGameTickCollection gameTickHandler,
            IDebugLogger debugLogger, BattleCharactersFactory battleCharactersFactory, ViewFactory viewFactory,
            ViewProvider viewProvider, SignalBus signalBus)
        {
            _player = player;
            _battleCharactersFactory = battleCharactersFactory;
            _viewFactory = viewFactory;
            _viewProvider = viewProvider;
            _signalBus = signalBus;
            _gameTickHandler = gameTickHandler;
            
            _fieldMouseSelector = new BattleFieldPointSelection(player, battleField, debugLogger);
        }
        
        public void PrepareUnits()
        {
            _fieldMouseSelector.PositionSelected += HandleSelectedPosition;
            _gameTickHandler.AddTick(_fieldMouseSelector);

            var viewData = _viewProvider.CharacterSelectionViewData;
            var model = new ViewModel(viewData.ViewObject, viewData.Layer);
            _selectionView = _viewFactory.Create(model);
            
            _signalBus.Subscribe<PlayerSelectedUnitCard>(OnPlayerCardSelect);
        }

        private void HandleSelectedPosition(object sender, Vector2 selectedPosition)
        {
            _selectedPoints++;

            var spawnPosition = new Vector3(selectedPosition.x, 0, selectedPosition.y);
            _battleCharactersFactory.SpawnCharacter(_selectedUnitId, _player, spawnPosition);
            
            if (_selectedPoints >= BattleState.DEBUG_UNITS_COUNT)
            {
                _fieldMouseSelector.PositionSelected -= HandleSelectedPosition;
                UnitsPrepared?.Invoke(this, EventArgs.Empty);
                
                _selectionView.Hide();
                _selectionView = null;
                
                _signalBus.Unsubscribe<PlayerSelectedUnitCard>(OnPlayerCardSelect);
            }
        }
        
        private void OnPlayerCardSelect(PlayerSelectedUnitCard obj)
        {
            _selectedUnitId = obj.UnitID;
        }
    }
}