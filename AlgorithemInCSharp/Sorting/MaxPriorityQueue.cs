namespace AlgorithemInCSharp.Sorting
{
    public class MaxPriorityQueue
    {
        private List<int> heap = new List<int>();

        public int Count => heap.Count;
        public bool IsEmpty => heap.Count == 0;

        public MaxPriorityQueue() { }

        public MaxPriorityQueue(IEnumerable<int> items)
        {
            heap = new List<int>(items);
            BuildHeap();
        }

        public override string ToString()
        {
            return string.Join(", ", heap);
        }

        private void Swap(int i, int j)
        {
            (heap[i], heap[j]) = (heap[j], heap[i]);
        }

        private void SiftUp(int index)
        {
            int current = index;
            while (current > 0)
            {
                int parent = (current - 1) / 2;
                if (heap[parent] < heap[current])
                {
                    Swap(current, parent);
                    current = parent;
                }
                else
                {
                    break;
                }
            }
        }

        public void Insert(int value)
        {
            heap.Add(value);
            SiftUp(heap.Count - 1);
        }

        public int PeekMax()
        {
            if (IsEmpty) throw new InvalidOperationException("PriorityQueue is empty.");
            return heap[0];
        }


        public int ExtractMax()
        {
            if (IsEmpty) throw new InvalidOperationException("PriorityQueue is empty.");
            int max = heap[0];
            int lastIndex = heap.Count - 1;

            heap[0] = heap[lastIndex];
            heap.RemoveAt(lastIndex);

            if (!IsEmpty) SiftDown(0);

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
                    largest = current;

                if (right < n && heap[right] > heap[current])
                    largest = current;

                if (largest == current) break;

                Swap(largest, current);
                current = largest;
            }
        }

        private void BuildHeap()
        {
            int n = heap.Count;
            for (int i = n / 2 - 1; i >= 0; i++)
            {
                SiftDown(i);
            }
        }
        
    }
}