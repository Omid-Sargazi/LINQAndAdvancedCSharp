namespace AlgorithemInCSharp.Sorting2
{
    public class SortingHeapify
    {
        public static void Run(int[] arr)
        {
            Console.WriteLine($"Before Heapify: {string.Join(",", arr)}");
            MaxHeapify(arr);
            Console.WriteLine($"After Heapify: {string.Join(",", arr)}");
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

    public class SortArrays
    {
        public static void Bubble(int[] arr)
        {
            for (int i = arr.Length - 1; i >= 0; i--)
            {
                bool swapped = false;
                for (int j = 0; j < i; j++)
                {
                    if (arr[j] > arr[j + 1])
                    {
                        (arr[j], arr[j + 1]) = (arr[j + 1], arr[j]);
                        swapped = true;
                    }
                }

                if (!swapped) break;
            }

            Console.WriteLine($"Bubble Sort:{string.Join(",",arr)}");
        }
    }
}