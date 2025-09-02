namespace AlgorithemInCSharp.Sorting
{
    public class HeapExample
    {
        public static void Run(int[] arr)
        {
            Console.WriteLine($"Before Heapify :{string.Join(",", arr)}");
            MaxHeapify(arr);
            HeapSort(arr);
            Console.WriteLine($"After Heapify :{string.Join(",", arr)}");

        }

        private static void HeapSort(int[] arr)
        {
            int n = arr.Length;
            for (int i = n - 1; i >= 1; i--)
            {
                (arr[0], arr[i]) = (arr[i], arr[0]);
                Heapify(arr, 0, i);
            }
        }

        private static void MaxHeapify(int[] arr)
        {
            int n = arr.Length;
            for (int i = n / 2 - 1; i >= 0; i--)
            {
                Heapify(arr, i, n);
            }
        }


        private static void Heapify(int[] arr, int i, int n)
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
                Heapify(arr, largest, n);
            }
        }
       
    }
}