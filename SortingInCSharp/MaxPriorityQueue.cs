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

        public int ExtractMax()
        {
            if (IsEmpty) throw new InvalidOperationException("PriorityQueue is empty");
            int max = heap[0];
            int lastIndex = heap.Count - 1;

            heap[0] = heap[lastIndex];
            heap.RemoveAt(lastIndex);
            if (!IsEmpty)
            {
                SiftDown(0);
            }

            return max;
        }

        private void SiftDown(int index)
        {
            int current = index;
            int n = heap.Count;

            while (true)
            {
                int left = 2 * current + 1;
                int right = 2 * current + 2;
                int largest = current;

                if (left < n && heap[left] > heap[current])
                    largest = left;

                if (right < n && heap[right] > heap[largest])
                    largest = right;

                if (largest == right)
                    break;

                Swap(current, largest);
                current = largest;
            }
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