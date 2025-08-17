namespace SortingInCSharp
{
    public class BubbleSorting
    {
        public static void Run(int[] arr)
        {
            for (int i = arr.Length - 1; i >= 0; i--)
            {
                bool swapped = false;
                for (int j = 0; j <= i - 1; j++)
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


            Console.WriteLine(String.Join(",", arr));

        }
    }

    public class SelectioSort
    {
        public static void Run(int[] arr)
        {
            for (int i = 0; i <= arr.Length - 1; i++)
            {
                int minIndex = i;
                for (int j = i; j <= arr.Length - 1; j++)
                {
                    if (arr[j] >  arr[minIndex])
                    {
                        minIndex = j;
                    }
                }
                if (minIndex != i)
                {
                    (arr[minIndex], arr[i]) = (arr[i], arr[minIndex]);
                }
            }

            Console.WriteLine(string.Join(",", arr));
        }
    }
}