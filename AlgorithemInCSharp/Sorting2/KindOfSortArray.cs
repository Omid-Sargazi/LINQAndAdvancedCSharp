using System.Linq.Expressions;
using System.Runtime.ExceptionServices;

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

            Console.WriteLine($"Bubble sorting: {string.Join(",", arr)}");
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

            Console.WriteLine($"Selection sorting: {string.Join(",", arr)}");
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
            Console.WriteLine($"Insertion sorting: {string.Join(",", arr)}");
        }

        public static int[] MergeSorting(int[] arr)
        {
            if (arr.Length <= 1) return arr;
            int n = arr.Length;
            int mid = n / 2;

            int[] left = new int[mid];
            int[] right = new int[n - mid];

            Array.Copy(arr, 0, left, 0, mid);
            Array.Copy(arr, mid, right, 0, right.Length);
            // Console.WriteLine($"Insertion sorting: {string.Join(",", left)}");
            // Console.WriteLine($"Insertion sorting: {string.Join(",", right)}");


            MergeSorting(left);
            MergeSorting(right);
            var result = Merge(arr, left, right);

            return result;
        }

        private static int[] Merge(int[] result, int[] left, int[] right)
        {
            int p1 = 0;
            int p2 = 0;
            int p3 = 0;

            int n1 = left.Length;
            int n2 = right.Length;

            while (p1 < n1 && p2 < n2)
            {
                if (left[p1] < right[p2])
                {
                    result[p3] = left[p1];
                    p1++;
                }
                else
                {
                    result[p3] = right[p2];
                    p2++;
                }

                p3++;
            }

            while (p1 < n1)
            {
                result[p3] = left[p1];
                p1++;
                p3++;
            }

            while (p2 < n2)
            {
                result[p3] = right[p2];
                p3++;
                p2++;
            }

            // Console.WriteLine($"Merge sort an arry:{string.Join(",", result)}"); ;
            return result;

        }

        public static void QuickSorting(int[] arr, int lo, int hi)
        {
            if (lo < hi)
            {
                var pi = Partition(arr, lo, hi);
                QuickSorting(arr, lo, pi - 1);
                QuickSorting(arr, pi + 1, hi);
            }
            if (lo == 0 && hi == arr.Length - 1)
            {
                Console.WriteLine($"Quick sorting : {string.Join(", ",arr)}");
            }
        }

        private static int Partition(int[] arr, int lo, int hi)
        {
            int pivot = arr[hi];
            int i = lo - 1;
            for (int j = lo; j < hi; j++)
            {
                if (arr[j] < pivot)
                {
                    i++;
                    (arr[j], arr[i]) = (arr[i], arr[j]);
                }
            }

            (arr[hi], arr[i + 1]) = (arr[i + 1], arr[hi]);
            return i + 1;
        }


    }
}