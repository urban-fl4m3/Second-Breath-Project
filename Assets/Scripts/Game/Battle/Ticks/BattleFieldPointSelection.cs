using SecondBreath.Common.Logger;
using SecondBreath.Common.Ticks;
using UnityEngine;

namespace SecondBreath.Game.Battle.Ticks
{
    public class BattleFieldPointSelection : ITickUpdate
    {
        private const int LEFT_MOUSE_BUTTON = 0;
        
        private readonly IBattleField _battleField;
        private readonly IDebugLogger _logger;

        public BattleFieldPointSelection(IBattleField battleField, IDebugLogger logger)
        {
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

                if (_battleField.InFriendlyField(point))
                {
                    Debug.Log("CLICKED FRIENDLY RECT");
                }

                if (_battleField.InEnemyField(point))
                {
                    Debug.Log("CLICKED ENEMY RECT");
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