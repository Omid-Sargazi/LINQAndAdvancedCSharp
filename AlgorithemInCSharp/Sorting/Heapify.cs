namespace AlgorithemInCSharp.Sorting
{
    public class HeapifySorting
    {
        public static void Run(int[] arr)
        {
            Console.WriteLine($"Before Heapify:{string.Join(",", arr)}");
            Heapify(arr, 0);
            Console.WriteLine($"After Heapify:{string.Join(",", arr)}"); 
        }


        private static void Heapify(int[] arr, int i)
        {
            int largest = i;
            int left = 2 * i + 1;
            int right = 2 * i + 2;

            if (left < arr.Length && arr[left] > arr[largest])
                largest = left;

            if (right < arr.Length && arr[right] > arr[largest])
                largest = right;

            if (largest != i)
            {
                (arr[i], arr[largest]) = (arr[largest], arr[i]);

                Heapify(arr, largest);
            }
        }
    }
}