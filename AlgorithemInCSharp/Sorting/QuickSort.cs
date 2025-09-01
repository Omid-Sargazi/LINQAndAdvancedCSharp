namespace AlgorithemInCSharp.Sorting
{
    public class QuickSort
    {
        public static void Run(int[] arr, int lo, int hi)
        {

            if (lo < hi)
            {
                int pi = Partition(arr, lo, hi);
                Run(arr, lo, pi - 1);
                Run(arr, pi + 1, hi);
           }


            Console.WriteLine($"QuickSort:{string.Join(",",arr)}");
        }

        private static int Partition(int[] arr, int lo, int hi)
        {
            int pivot = arr[hi];
            int i = lo - 1;
            for (int j = lo; j < hi; j++)
            {
                if (arr[j] <= pivot)
                {
                    i++;
                    (arr[j], arr[i]) = (arr[i], arr[j]);
                }
            }

            (arr[i + 1], arr[hi]) = (arr[hi], arr[i + 1]);
            return i + 1;
        }
    }
}