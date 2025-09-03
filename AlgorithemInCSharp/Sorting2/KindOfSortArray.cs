namespace AlgorithemInCSharp.Sorting2
{
    public class KindOfSortArray
    {
        public static void BubleSorting(int[] arr)
        {
            for (int start = arr.Length - 1; start >= 0; start--)
            {
                bool swapped = false;
                for (int j = 0; j < start; j++)
                {
                    if (arr[j] > arr[j + 1])
                    {
                        (arr[j], arr[j + 1]) = (arr[j + 1], arr[j]);
                        swapped = true;
                    }
                }
                if (!swapped)
                {
                    break;
                }
            }

            Console.WriteLine($"Bubble sorting: {string.Join(",",arr)}");
        }

        public static void SelectionSorting(int[] arr)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                int minIndex = i;
                for (int j = i + 1; j < arr.Length; j++)
                {
                    if (arr[j] < arr[minIndex])
                    {
                        minIndex = j;
                    }
                }

                (arr[i], arr[minIndex]) = (arr[minIndex], arr[i]);
            }

            Console.WriteLine($"Selection sorting: {string.Join(",",arr)}");
        }

        public static void InsertionSorting(int[] arr)
        {
            int n = arr.Length;
            for (int i = 1; i < n; i++)
            {
                int j = i - 1;
                int current = arr[i];
                while (j >= 0 && arr[j] > current)
                {
                    arr[j + 1] = arr[j];
                    j--;
                }

                arr[j + 1] = current;
            }
            Console.WriteLine($"Insertion sorting: {string.Join(",",arr)}");
        }

        public static void MergeSorting(int[] arr)
        {

        }

        public static void QuickSorting(int[] arr)
        {

        }


    }
}