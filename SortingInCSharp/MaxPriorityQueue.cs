using System.Security.AccessControl;

namespace SortingInCSharp
{
    public class MaxPriorityQueue
    {
        private List<int> heap = new List<int>();

        public int Count => heap.Count;
        public bool IsEmpty => heap.Count == 0;

        public void Insert(int value)
        {
            heap.Add(value);
            SiftUp(heap.Count - 1);

        }

        private void SiftUp(int index)
        {
            int current = index;
            while (current > 0)
            {
                int parent = (current - 1) / 2;
                if (heap[parent] < heap[current])
                {
                    Swap(parent, current);
                    current = parent;
                }
                else
                {
                    break;
                }
            }
        }

        private int PeekMax()
        {
            if (IsEmpty) throw new InvalidOperationException("PriorityQueue is empty");
            return heap[0];
        }

        private void Swap(int i, int j)
        {
            (heap[i], heap[j]) = (heap[j], heap[i]);
        }

        public override string ToString()
        {
            return string.Join(", ", heap);
        }
    }
}