using System;
using System.Collections.Generic;
using SecondBreath.Game.Battle;

namespace Common.Structures
{
    public class Heap<T> where T : IComparable<T>
    {
        private List<T> _heap = new List<T>();

        public int Count
        {
            get
            {
                return _heap.Count;
            }
        }

        public void Add(T newItem)
        {
            _heap.Add(newItem);
            UpdateUp(_heap.Count - 1);
        }

        public T Pop()
        {
            T ans = _heap[0];
            (_heap[0], _heap[_heap.Count - 1]) = (_heap[_heap.Count - 1], _heap[0]);
            _heap.RemoveAt(_heap.Count - 1);
            if (_heap.Count != 0) UpdateDown(0);
            return ans;
        }

        private void UpdateUp(int itemIndex)
        {
            if (itemIndex == 0) return;
            if (_heap[itemIndex].CompareTo(_heap[(itemIndex - 1) / 2]) < 0)
            {
                (_heap[itemIndex], _heap[(itemIndex - 1) / 2]) = (_heap[(itemIndex - 1) / 2], _heap[itemIndex]);
                UpdateUp((itemIndex - 1) / 2);
            }
        }

        private void UpdateDown(int itemIndex)
        {
            T currElem = _heap[itemIndex];
            T childElem;
            int nextIndex;
            if (itemIndex * 2 + 1 >= _heap.Count) return;
            if (itemIndex * 2 + 2 >= _heap.Count)
            {
                childElem = _heap[itemIndex * 2 + 1];
                nextIndex = itemIndex * 2 + 1;
                if (childElem.CompareTo(currElem) < 0)
                {
                    (_heap[itemIndex], _heap[nextIndex]) = (_heap[nextIndex], _heap[itemIndex]);
                    UpdateDown(nextIndex);
                }

                return;
            }
            
            if (_heap[itemIndex * 2 + 1].CompareTo(_heap[itemIndex * 2 + 2]) < 0)
            {
                nextIndex = itemIndex * 2 + 1;
                childElem = _heap[itemIndex * 2 + 1];
            }
            else
            {
                nextIndex = itemIndex * 2 + 2;
                childElem = _heap[itemIndex * 2 + 2];
            }

            if (childElem.CompareTo(currElem) < 0)
            {
                (_heap[itemIndex], _heap[nextIndex]) = (_heap[nextIndex], _heap[itemIndex]);
                UpdateDown(nextIndex);
            }
        }
    }
}