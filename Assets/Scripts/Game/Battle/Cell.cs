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
        public bool IsSelected;
        public Cell PreviousCell;
        public float CellDirection = Mathf.Infinity;
        public float CellCost = Mathf.Infinity;
        
        
        private GameObject _cellVisual;
        private Material _cellMaterial;
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

            if (Random.Range(0.0f, 1.0f) < 0.35f)
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
            return (Mathf.Abs(_indexes.x - cell._indexes.x) + Mathf.Abs(_indexes.y - cell._indexes.y)) * 10.0f;
        }

        public int CompareTo(Cell other)
        {
            return CellCost.CompareTo(other.CellCost);
        }
    }
}