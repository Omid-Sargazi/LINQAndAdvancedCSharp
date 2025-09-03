namespace AlgorithemInCSharp.Sorting2
{
    public class HeapArraySorting
    {
        public static void Run(int[] arr)
        {
            Console.WriteLine($"Before Sorting: {string.Join(", ", arr)}");
            MaxHeapify(arr);
            SortHeapify(arr);
            Console.WriteLine($"After Sorting: {string.Join(", ", arr)}");
        }

        private static void MaxHeapify(int[] arr)
        {
            int n = arr.Length;
            for (int i = n / 2 - 1; i >= 0; i--)
            {
                Heapifying(arr, i, n);
            }
        }

        private static void SortHeapify(int[] arr)
        {
            int n = arr.Length;
            for (int i = n - 1; i >= 1; i--)
            {
                (arr[0], arr[i]) = (arr[i], arr[0]);
                Heapifying(arr, 0, i);
            }
        }


        private static void Heapifying(int[] arr, int i, int n)
        {

            int largest = i;
            int left = 2 * i + 1;
            int right = 2 * i + 2;

            if (left < n && arr[left] > arr[largest])
                largest = left;

            if (right < n && arr[right] > arr[largest])
                largest = right;

            if (largest != i)
            {
                (arr[i], arr[largest]) = (arr[largest], arr[i]);
                Heapifying(arr, largest, n);
            }
        }
    }
}