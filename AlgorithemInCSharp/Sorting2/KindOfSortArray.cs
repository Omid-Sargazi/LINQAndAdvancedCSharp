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

        }

        public static void InsertionSorting(int[] arr)
        {

        }

        public static void MergeSorting(int[] arr)
        {

        }

        public static void QuickSorting(int[] arr)
        {

        }


    }
}