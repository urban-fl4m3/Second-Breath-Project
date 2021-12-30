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
                    Cells.Add(new Vector2Int(i, j), new Cell(i, j, newObject, pathFindingTest));
                }
            }
        }

        private void ClearBF()
        {
            foreach (var cell in Cells)
            {
                cell.Value.SetCellColor(Color.green);
                cell.Value.IsEmpty = true;
                cell.Value.CellDirection = Mathf.Infinity;
                cell.Value.CellCost = Mathf.Infinity;
                cell.Value.unitCounts = 0;
            }
        }

        private void UpdateActorsOnBF()
        {
            var objects = _actorRegisterer.GetRegisteredObjects();
            foreach (var actor in objects)
            {
                var translatable = actor.Components.Get<ITranslatable>();
                var position = translatable.Position.Value;
                var cellsInRadius = GetCellByWorldPosition(position, translatable.Radius);
                foreach (var cell in cellsInRadius)
                {
                    cell.SetCellColor(Color.black);
                    cell.IsEmpty = false;
                    cell.unitCounts++;
                }
            }
        }

        private void Update()
        {
            ClearBF();
            UpdateActorsOnBF();
        }
        
        private Cell GetCellByWorldPosition(Vector3 point)
        {
            point.y = 0.0f;
            int xPos = Mathf.FloorToInt((point.x - _mainRect.xMin) / _cellSize.x);
            int yPos = Mathf.FloorToInt((point.z - _mainRect.yMin) / _cellSize.y);
            return GetCell(xPos, yPos);
        }

        private List<Cell> GetCellByWorldPosition(Vector3 point, float radius)
        {
            point.y = 0.0f;
            int xPos = Mathf.FloorToInt((point.x - _mainRect.xMin) / _cellSize.x);
            int yPos = Mathf.FloorToInt((point.z - _mainRect.yMin) / _cellSize.y);
            
            HashSet<Tuple<int, int>> flaged = new HashSet<Tuple<int, int>>();
            List<Cell> activeCells = new List<Cell>();
            
            activeCells.Add(GetCell(xPos, yPos));
            flaged.Add(new Tuple<int, int>(xPos, yPos));
            int listIndex = 0;
            while (listIndex < activeCells.Count)
            {
                var cellIndexes = activeCells[listIndex]._indexes;
                for (int i = -1; i < 2; i++)
                {
                    for (int j = -1; j < 2; j++)
                    {
                        if (cellIndexes.x + i < _hSize && cellIndexes.x + i > -1 &&
                            cellIndexes.y + j < _wSize && cellIndexes.y + j > -1 &&
                            !flaged.Contains(new Tuple<int, int>(cellIndexes.x + i, cellIndexes.y + j)))
                        {
                            var cell = GetCell(cellIndexes.x + i, cellIndexes.y + j);
                            var cellPosition = cell.GetCellPosition();
                            cellPosition.y = 0.0f;
                            if (Vector3.Distance(point, cellPosition) <= radius)
                            {
                                flaged.Add(new Tuple<int, int>(cellIndexes.x + i, cellIndexes.y + j));
                                activeCells.Add(cell);
                            }
                        }
                    }
                }
                
                listIndex++;
            }
            
            return activeCells;
        }
        private List<Cell> GetNearCells(Cell cell)
        {
            Cell chosenCell;
            Vector2Int centerIndex;
            List<Cell> ans = new List<Cell>();
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (i == 0 && j == 0) continue;
                    if ((cell._indexes.x + i < _hSize) && (cell._indexes.x + i > -1) && 
                        (cell._indexes.y + j < _wSize) && (cell._indexes.y + j > -1))
                    {
                        centerIndex = cell._indexes + new Vector2Int(i, j);
                        chosenCell = Cells.GetValue(centerIndex);
                        ans.Add(chosenCell);
                    }
                }
            }
            
            
            return ans;
        }

        public Vector3 PathFinding(ITranslatable startPosition, ITranslatable finishPosition)
        {
            List<Cell> startCells = GetCellByWorldPosition(startPosition.Position.Value, startPosition.Radius);
            foreach (var cell in startCells)
            {
                cell.unitCounts--;
            }

            List<int> counts = new List<int>();
            List<Cell> finishCells = GetCellByWorldPosition(finishPosition.Position.Value, finishPosition.Radius);
            foreach (var cell in finishCells)
            {
                counts.Add(cell.unitCounts);
                cell.unitCounts = 0;
            }
            Cell finishCell = GetCellByWorldPosition(finishPosition.Position.Value);
            Cell startCell = GetCellByWorldPosition(startPosition.Position.Value);
            
            finishCell.CellCost = Mathf.Infinity;
            
            startCell.CellDirection = 0.0f;

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

            for (int i = 0; i < finishCells.Count; i++)
            {
                finishCells[i].unitCounts = counts[i];
            }
            
            foreach (var cell in startCells)
            {
                cell.unitCounts++;
            }
            
            if (!finishFinded || goalCell.CellCost > 1000) return startCell.GetCellPosition();
            List<Cell> path = new List<Cell>();
            while (goalCell != null)
            {
                goalCell.SetCellColor(Color.yellow);
                path.Add(goalCell);
                goalCell = previousCells.GetValue(goalCell._indexes);
            }
            
            var nextPos = path[path.Count - 2].GetCellPosition();
            nextPos.y = 0.0f;
            return nextPos;
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