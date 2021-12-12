using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Common.Actors;
using Common.Structures;
using SecondBreath.Common.Extensions;
using SecondBreath.Common.Logger;
using SecondBreath.Game.Battle.Movement;
using SecondBreath.Game.Battle.Registration;
using SecondBreath.Game.Players;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace SecondBreath.Game.Battle
{
    public class BattleField : SerializedMonoBehaviour, IBattleField
    {
        [Inject] private IDebugLogger _debugLogger;
        [Inject] private  ITeamObjectRegisterer<IActor> _actorRegisterer;
        
        private const float FRIENDLY_FIELD_HEIGHT_RATIO = 0.0f;
        private const float ENEMY_FIELD_HEIGHT_RATIO = 0.5f;
        private const float HALF_FIELD = 0.5f;
        
        private Rect _mainRect;
        private Rect _greenRect;
        private Rect _redRect;
        
        private readonly Dictionary<Team, Rect> _registeredRects = new Dictionary<Team, Rect>();

        private Dictionary<Vector2Int, Cell> Cells = new Dictionary<Vector2Int, Cell>();
        [SerializeField] private GameObject _cellVisual;
        [SerializeField] private int _hSize = 1;
        [SerializeField] private int _wSize = 1;
        private Vector2 _cellSize;
        [SerializeField] private bool pathFindingTest;

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


            if (pathFindingTest)
            {
                _cellSize = new Vector2((_mainRect.xMax - _mainRect.xMin) / _wSize,
                    (_mainRect.yMax - _mainRect.yMin) / _hSize);

                for (int i = 0; i < _hSize; i++)
                {
                    for (int j = 0; j < _wSize; j++)
                    {
                        var cellPosition = new Vector3(_cellSize.x * (i + 0.5f) + _mainRect.xMin, 0.01f,
                            _cellSize.y * (j + 0.5f) + _mainRect.yMin);

                        var newObject = Instantiate(_cellVisual, cellPosition, Quaternion.identity);
                        newObject.transform.localScale = new Vector3(_cellSize.x - 0.05f, 0.5f, _cellSize.y - 0.05f);
                        Cells.Add(new Vector2Int(i, j), new Cell(i, j, newObject));
                    }
                }
            }
        }

        private void Update()
        {
            if (!pathFindingTest) return;
            foreach (var cell in Cells)
            {
                cell.Value.SetCellColor(Color.green);
                cell.Value.IsEmpty = true;
                cell.Value.CellDirection = Mathf.Infinity;
                cell.Value.CellCost = Mathf.Infinity;
            }

            var objects = _actorRegisterer.GetRegisteredObjects();
            foreach (var actor in objects)
            {
                var translatable = actor.Components.Get<ITranslatable>();
                var position = translatable.Position.Value;
                var cellsInRadius = GetCellByWorldPosition(position);
                cellsInRadius.SetCellColor(Color.black);
                cellsInRadius.IsEmpty = false;
            }

            if (objects.Count() < 2) return;
            PathFinding(objects.First().Components.Get<ITranslatable>().Position.Value, 
                objects.Last().Components.Get<ITranslatable>().Position.Value);
        }

        private Cell GetCellByWorldPosition(Vector3 point)
        {
            point.y = 0.0f;
            int xPos = Mathf.FloorToInt((point.x - _mainRect.xMin) / _cellSize.x);
            int yPos = Mathf.FloorToInt((point.z - _mainRect.yMin) / _cellSize.y);

            return GetCell(xPos, yPos);
        }

        private List<Cell> GetNearCells(Cell cell)
        {
            List<Cell> ans = new List<Cell>();
            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    if (i == 0 && j == 0) continue;
                    if ((cell._indexes.x + i < _hSize) && (cell._indexes.x + i > -1) && 
                        (cell._indexes.y + j < _wSize) && (cell._indexes.y + j > -1))
                    {
                        var chosenCell = Cells.GetValue(cell._indexes + new Vector2Int(i, j));
                        if (!chosenCell.IsEmpty) continue;
                        ans.Add(chosenCell);
                    }
                }
            }
            return ans;
        }

        private Vector3 PathFinding(Vector3 startPosition, Vector3 finishPosition)
        {
            
            Cell startCell = GetCellByWorldPosition(startPosition);
            Cell finishCell = GetCellByWorldPosition(finishPosition);
            bool startCellIsEmpty = startCell.IsEmpty;
            bool finishCellIsEmpty = finishCell.IsEmpty;
            
            startCell.CellDirection = 0.0f;
            startCell.IsEmpty = true;
            finishCell.IsEmpty = true;

            bool finishFinded = false;

            HashSet<Vector2Int> SelectedCells = new HashSet<Vector2Int>();
            Dictionary<Vector2Int, Cell> previousCells = new Dictionary<Vector2Int, Cell>();
            SelectedCells.Add(startCell._indexes);
            previousCells.Add(startCell._indexes, null);

            Heap<Cell> openList = new Heap<Cell>();

            var currentCell = startCell;
            var goalCell = finishCell;

            while (true)
            {
                var nearCells = GetNearCells(currentCell);

                foreach (var cell in nearCells)
                {
                    if (SelectedCells.Contains(cell._indexes)) continue;
                    SelectedCells.Add(cell._indexes);
                    previousCells.Add(cell._indexes, currentCell);
                    cell.CellDirection = currentCell.CellDirection + cell.GetCellDirection(currentCell);
                    cell.UpdateCellCost(goalCell);

                    openList.Add(cell);
                }

                if (openList.Count == 0) break;
                Cell nextCell = openList.Pop();
                float minimumCost = nextCell.CellCost;

                if (goalCell.CellCost < minimumCost)
                {
                    finishFinded = true;
                    break;
                }
                currentCell = nextCell;   

            }
            startCell.IsEmpty = startCellIsEmpty;
            finishCell.IsEmpty = finishCellIsEmpty;
            
            if (!finishFinded) return startCell.GetCellPosition();
            List<Cell> path = new List<Cell>();
            while (goalCell != null)
            {
                goalCell.SetCellColor(Color.yellow);
                path.Add(goalCell);
                goalCell = previousCells.GetValue(goalCell._indexes);
            }
            
            return path.Last().GetCellPosition();
        }  

        private Cell GetCell(int x, int y)
        {
            return Cells.GetValue(new Vector2Int(x, y));
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