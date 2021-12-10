using System;
using SecondBreath.Common.Logger;
using SecondBreath.Game.Battle.Characters;
using SecondBreath.Game.Battle.Signals;
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
        private const int LEFT_MOUSE_BUTTON = 0;
        
        public event EventHandler UnitsPrepared;

        private readonly IPlayer _player;
        private readonly IDebugLogger _logger;
        private readonly IBattleField _battleField;
        private readonly IGameTickWriter _gameTickHandler;
        
        private readonly SignalBus _signalBus;
        private readonly ViewFactory _viewFactory;
        private readonly ViewProvider _viewProvider;
        private readonly BattleCharactersFactory _battleCharactersFactory;

        private int _selectedPoints;
        private int _selectedUnitId;

        private IView _selectionView;
        
        public PlayerPreparationController(
            IPlayer player,
            IBattleField battleField, 
            IGameTickWriter gameTickHandler,
            IDebugLogger debugLogger,
            BattleCharactersFactory battleCharactersFactory,
            ViewFactory viewFactory,
            ViewProvider viewProvider,
            SignalBus signalBus)
        {
            _player = player;
            _logger = debugLogger;
            _signalBus = signalBus;
            _battleField = battleField;
            _viewFactory = viewFactory;
            _viewProvider = viewProvider;
            _gameTickHandler = gameTickHandler;
            _battleCharactersFactory = battleCharactersFactory;
        }
        
        public void PrepareUnits()
        {
            _gameTickHandler.AddTick(HandleMouseClickOnField);

            var viewData = _viewProvider.CharacterSelectionViewData;
            var model = new ViewModel(viewData.ViewObject, viewData.Layer);
            _selectionView = _viewFactory.Create(model);
            
            _signalBus.Subscribe<PlayerSelectedUnitCard>(OnPlayerCardSelect);
        }
        
        private void HandleMouseClickOnField()
        {
            if (Input.GetMouseButtonDown(LEFT_MOUSE_BUTTON))
            {
                var camera = Camera.main;

                if (camera == null)
                {
                    _logger.LogError("Can't point on battle field. Camera main is null!");
                    return;
                }

                var point = GetPointOnPlane(camera);
                var rect = _battleField.GetTeamRect(_player.Team);
                
                if (rect.Contains(point))
                {
                    SelectPoint(point);
                }
            }        
        }

        private Vector2 GetPointOnPlane(Camera camera)
        {
            var plane = _battleField.GetPlane();
            var ray = camera.ScreenPointToRay(Input.mousePosition);

            if (plane.Raycast(ray, out var distance))
            {
                var point = ray.GetPoint(distance);
                return new Vector2(point.x, point.z);
            }
            
            return Vector2.negativeInfinity;
        }

        private void SelectPoint(Vector2 selectedPosition)
        {
            _selectedPoints++;

            var spawnPosition = new Vector3(selectedPosition.x, 0, selectedPosition.y);
            _battleCharactersFactory.SpawnCharacter(_selectedUnitId, _player, spawnPosition);
            
            if (_selectedPoints >= BattleState.DEBUG_UNITS_COUNT)
            {
                _gameTickHandler.RemoveTick(HandleMouseClickOnField);
                
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