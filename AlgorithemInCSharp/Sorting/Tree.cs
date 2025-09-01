namespace AlgorithemInCSharp.Sorting
{
    public class Tree
    {
        public static void Run(int[] arr)
        {
            Console.WriteLine("قبل از Heapify: " + string.Join(", ", arr));
            Heapify(arr, 0);
            Console.WriteLine("بعد از Heapify: " + string.Join(", ", arr));

        }

        

        private static void Heapify(int[] arr, int i)
        {
            int left = 2 * i + 1;
            int right = 2 * i + 2;

            int largest = i;

            if (left < arr.Length && arr[left] > arr[largest])
            {
                largest = left;
            }

            if (right < arr.Length && arr[right] > arr[largest])
            {
                largest = right;
            }

            if (largest != i)
            {
                int temp = arr[i];
                arr[i] = arr[largest];
                arr[largest] = temp;

                Heapify(arr, largest);
            }
        } 
    }
}