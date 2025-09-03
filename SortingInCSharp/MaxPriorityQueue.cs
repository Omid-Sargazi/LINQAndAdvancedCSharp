namespace SortingInCSharp
{
    public class MaxPriorityQueue
    {
        private List<int> heap = new List<int>();

        public int Count => heap.Count();
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
    }
}