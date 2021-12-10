using System;
using System.Collections.Generic;
using SecondBreath.Common.Extensions;
using UnityEngine;
using Random = UnityEngine.Random;
using Vector2 = System.Numerics.Vector2;

namespace SecondBreath.Game.Battle
{
    public class Cell
    {
        public static Dictionary<Tuple<int, int>, Cell> Cells = new Dictionary<Tuple<int, int>, Cell>();
        public bool IsEmpty = true;
        public bool IsSelected;
        public Cell PreviousCell;
        public float CellDirection = Mathf.Infinity;
        public float CellCost = Mathf.Infinity;
        
        
        private GameObject _cellVisual;
        private Material _cellMaterial;
        private Vector2Int _indexes;
        private Vector2Int _fieldSize;

        public static bool operator ==(Cell lhs, Cell rhs)
        {
            
            return lhs?._indexes == rhs?._indexes;
        }
        
        public static bool operator !=(Cell lhs, Cell rhs)
        {
            
            return lhs?._indexes != rhs?._indexes;
        }

        public Cell(int x, int y, GameObject cellVisual, Vector2Int fieldSize)
        {
            Cells.Add(new Tuple<int, int>(x, y), this);
            _cellVisual = cellVisual;
            _cellMaterial = _cellVisual.GetComponent<Renderer>().material;

            _indexes = new Vector2Int(x, y);
            _fieldSize = fieldSize;

            if (Random.Range(0.0f, 1.0f) < 0.25f)
            {
                IsEmpty = false;
                SetCellColor(Color.black);
            }
            else
            {
                SetCellColor(Color.green);
            }
        }

        public void SetCellColor(Color newColor)
        {
            _cellMaterial.color = newColor;
        }

        public List<Cell> GetNearCells()
        {
            List<Cell> ans = new List<Cell>();
            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    if (i == 0 && j == 0) continue;
                    if (i != 0 && j != 0) continue;
                    if ((_indexes.x + i < _fieldSize.x) && (_indexes.x + i > -1) && 
                        (_indexes.y + j < _fieldSize.y) && (_indexes.y + j > -1))
                    {
                        var chosenCell = Cells.GetValue(new Tuple<int, int>(_indexes.x + i, _indexes.y + j));
                        if (!chosenCell.IsEmpty) continue;
                        ans.Add(chosenCell);
                    }
                }
            }
            return ans;
        }

        public float GetCellDirection(Cell cell)
        {
            int dx = _indexes.x - cell._indexes.x;
            int dy = _indexes.y - cell._indexes.y;
            if (dx != 0 && dy != 0) return 14.0f;
            return 10.0f;
        }

        public void UpdateCellCost(Cell finishCell)
        {
            CellCost = CellDirection + DistanceToCell(finishCell);
        }

        public float DistanceToCell(Cell cell)
        {
            return Vector2Int.Distance(cell._indexes, _indexes) * 10.0f;
        }
        
    }
}