using System;
using SecondBreath.Common.Logger;
using SecondBreath.Common.Ticks;
using SecondBreath.Game.Players;
using UnityEngine;

namespace SecondBreath.Game.Battle.Ticks
{
    public class BattleFieldPointSelection : ITickUpdate
    {
        public event EventHandler<Vector2> PositionSelected;
        
        private const int LEFT_MOUSE_BUTTON = 0;

        private readonly IPlayer _selector;
        private readonly IBattleField _battleField;
        private readonly IDebugLogger _logger;

        public BattleFieldPointSelection(IPlayer selector, IBattleField battleField, IDebugLogger logger)
        {
            _selector = selector;
            _battleField = battleField;
            _logger = logger;
        }
        
        public void Update()
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
                var rect = _battleField.GetTeamRect(_selector.Team);
                
                if (rect.Contains(point))
                {
                    PositionSelected?.Invoke(this, point);
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
    }
}