using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using SecondBreath.Common.Extensions;
using SecondBreath.Common.Logger;
using SecondBreath.Game.Players;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace SecondBreath.Game.Battle
{
    public class BattleField : SerializedMonoBehaviour, IBattleField
    {
        [Inject] private IDebugLogger _debugLogger;
        
        private const float FRIENDLY_FIELD_HEIGHT_RATIO = 0.0f;
        private const float ENEMY_FIELD_HEIGHT_RATIO = 0.5f;
        private const float HALF_FIELD = 0.5f;
        
        private Rect _mainRect;
        private Rect _greenRect;
        private Rect _redRect;
        
        private readonly Dictionary<Team, Rect> _registeredRects = new Dictionary<Team, Rect>();

        [SerializeField] private GameObject _cellVisual;
        [SerializeField] private int _hSize = 1;
        [SerializeField] private int _wSize = 1;
        private Vector2 _cellSize;

        public Plane GetPlane()
        {
            return new Plane(transform.up, 0);
        }

        public Rect GetTeamRect(Team team)
        {
            if (!_registeredRects.ContainsKey(team))
            {
                _debugLogger.LogError($"Field doesn't have rect for team - {team}");
                return new Rect();
            }
            
            return _registeredRects[team];
        }

        private void Awake()
        {
            UpdateRects();

            _registeredRects.Add(Team.Green, _greenRect);
            _registeredRects.Add(Team.Red, _redRect);

            _cellSize = new Vector2((_mainRect.xMax - _mainRect.xMin) / _wSize, (_mainRect.yMax - _mainRect.yMin) / _hSize);

            for (int i = 0; i < _hSize; i++)
            {
                for (int j = 0; j < _wSize; j++)
                {
                    var cellPosition = new Vector3(_cellSize.x * (i + 0.5f) + _mainRect.xMin, 0.01f,
                        _cellSize.y * (j + 0.5f) + _mainRect.yMin);
                    
                    var newObject = Instantiate(_cellVisual, cellPosition, Quaternion.identity);
                    newObject.transform.localScale = new Vector3(_cellSize.x - 0.05f, 0.5f, _cellSize.y - 0.05f);
                    new Cell(i, j, newObject, new Vector2Int(_hSize, _wSize));
                }
            }

            StartCoroutine(PathFinding());
        }

        private IEnumerator PathFinding()
        {
            Cell startCell = GetCell(9, 0);
            startCell.CellDirection = 0.0f;
            Cell finishCell = GetCell(9, 19);
            startCell.SetCellColor(Color.blue);
            finishCell.SetCellColor(Color.red);
            HashSet<Cell> openList = new HashSet<Cell>();

            while (true)
            {
                startCell.IsSelected = true;
                var nearCells = startCell.GetNearCells();

                foreach (var cell in nearCells)
                {
                    if (cell.IsSelected) continue;
                    if (cell.CellDirection > startCell.CellDirection + cell.GetCellDirection(startCell))
                    {
                        cell.PreviousCell = startCell;
                        cell.CellDirection = startCell.CellDirection + cell.GetCellDirection(startCell);
                        cell.UpdateCellCost(finishCell);
                    }

                    openList.Add(cell);
                }

                if (openList.Count == 0) break;
                float minimumCost = Mathf.Infinity;
                Cell nextCell = startCell;

                foreach (var cell in openList)
                {
                    if (cell.CellCost < minimumCost)
                    {
                        minimumCost = cell.CellCost;
                        nextCell = cell;
                    }
                }

                if (finishCell.CellCost < minimumCost) break;
                openList.Remove(nextCell);
                startCell = nextCell;   

            }

            float coef = 0.0f;
            while (finishCell != null)
            {
                yield return new WaitForSeconds(0.25f);
                finishCell.SetCellColor(Color.yellow);
                finishCell = finishCell.PreviousCell;
            }
            
            yield break;
        }  

        private Cell GetCell(int x, int y)
        {
            return Cell.Cells.GetValue(new Tuple<int, int>(x, y));
        }

        private void UpdateRects()
        {
            var t = transform;
            var position = t.position;
            var size = t.localScale;
            
            _mainRect = new Rect(position.x - size.x * HALF_FIELD, position.z - size.z * HALF_FIELD, size.x, size.z);

            _greenRect = GetPartRect(_mainRect, FRIENDLY_FIELD_HEIGHT_RATIO);
            _redRect = GetPartRect(_mainRect, ENEMY_FIELD_HEIGHT_RATIO);
        }

        private static Rect GetPartRect(Rect mainRect, float ratio)
        {
            var positionX = mainRect.position.x;
            var positionY = mainRect.position.y + ratio * mainRect.height;
            var sizeX = mainRect.width;
            var sizeY = mainRect.height * HALF_FIELD;

            return new Rect(positionX, positionY, sizeX, sizeY);
        }
        
        #region Editor

        private void OnDrawGizmos()
        {
            UpdateRects();
            
            var oldMatrix = Gizmos.matrix;
         
            DrawRect(_greenRect, new Color(0, 1, 0, 0.2f));
            DrawRect(_redRect, new Color(1, 0, 0, 0.2f));

            Gizmos.matrix = oldMatrix;
        }

        private void DrawRect(Rect rect, Color color)
        {
            var pos = new Vector3(rect.center.x, transform.position.y, rect.center.y);
            var matrixSize = new Vector3(rect.width, 0.01f, rect.height);
            Gizmos.matrix = Matrix4x4.TRS(pos, Quaternion.identity, matrixSize);
            Gizmos.color = color;
            
            Gizmos.DrawCube(Vector3.zero, Vector3.one);
        }
        
        #endregion
    }
}