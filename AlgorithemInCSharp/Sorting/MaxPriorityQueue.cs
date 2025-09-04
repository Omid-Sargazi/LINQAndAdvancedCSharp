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

        
    }
}