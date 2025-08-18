namespace LinqExamples.SortingWithCSharp
{
    public class BubbleSorting
    {
        public static void Run(int[] arr)
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
                if (!swapped) break;
            }

            Console.WriteLine(string.Join(",", arr));
        }
    }

    public class SelectionSort
    {
        public static void Run(int[] arr)
        {
            for (int i = 0; i <= arr.Length - 1; i++)
            {
                int minIndex = i;
                for (int j = i; j <= arr.Length - 1; j++)
                {
                    if (arr[j] < arr[minIndex])
                    {
                        minIndex = j;
                    }
                }

                (arr[i], arr[minIndex]) = (arr[minIndex], arr[i]);
            }


            Console.WriteLine(string.Join(",", arr));
        }
    }

    public class InsertionSort
    {
        public static void Run(int[] arr)
        {
            for (int i = 1; i < arr.Length; i++)
            {
                int j = i - 1;
                int temp = arr[i];

                while (j >= 0 && temp > arr[j])
                {
                    arr[j+1] = arr[j];
                    j--;
                }

                arr[j + 1] = temp;


            }

            Console.WriteLine(string.Join(",", arr));
        }
    }
}