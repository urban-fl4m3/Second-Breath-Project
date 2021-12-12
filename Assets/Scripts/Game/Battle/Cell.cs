using System;
using System.Collections.Generic;
using SecondBreath.Common.Extensions;
using UnityEngine;
using Random = UnityEngine.Random;
using Vector2 = System.Numerics.Vector2;

namespace SecondBreath.Game.Battle
{
    public class Cell : IComparable<Cell>
    {
        public bool IsEmpty = true;
        public float CellDirection = Mathf.Infinity;
        public float CellCost = Mathf.Infinity;
        public int unitCounts = 0;
        
        
        private readonly GameObject _cellVisual;
        private readonly Material _cellMaterial;
        public Vector2Int _indexes;

        public static bool operator < (Cell lhs, Cell rhs)
        {
            return lhs?.CellCost < rhs?.CellCost;
        }
        
        public static bool operator > (Cell lhs, Cell rhs)
        {
            return lhs?.CellCost > rhs?.CellCost;
        }
        
        public static bool operator ==(Cell lhs, Cell rhs)
        {
            return lhs?._indexes == rhs?._indexes;
        }
        
        public static bool operator !=(Cell lhs, Cell rhs)
        {
            
            return lhs?._indexes != rhs?._indexes;
        }

        public Cell(int x, int y, GameObject cellVisual)
        {
            _cellVisual = cellVisual;
            _cellMaterial = _cellVisual.GetComponent<Renderer>().material;

            _indexes = new Vector2Int(x, y);
            cellVisual.name = $"Cell({_indexes.x:00},{_indexes.y:00})";
            SetCellColor(Color.green);
        }

        public void SetCellColor(Color newColor)
        {
            _cellMaterial.color = newColor;
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
            return Vector2Int.Distance(_indexes, cell._indexes) * 20.0f;
        }

        public int CompareTo(Cell other)
        {
            return CellCost.CompareTo(other.CellCost);
        }

        public Vector3 GetCellPosition()
        {
            return _cellVisual.transform.position;
        }
    }
}