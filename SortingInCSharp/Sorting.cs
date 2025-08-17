namespace SortingInCSharp
{
    public class BubbleSorting
    {
        public static void Run(int[] arr)
        {
            for (int i = arr.Length - 1; i >= 0; i--)
            {
                bool swapped = false;
                for (int j = 0; j <= i-1; j++)
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
}