using System.Runtime.Intrinsics.Arm;
using AlgorithemInCSharp.Sorting;

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

            Console.WriteLine($"Bubble Sort:{string.Join(",", arr)}");
        }

        public static void Selection(int[] arr)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                int minIndex = i;
                for (int j = i + 1; j < arr.Length; j++)
                {
                    if (arr[j] < arr[minIndex])
                        minIndex = j;
                }

                if (minIndex != i)
                {
                    (arr[i], arr[minIndex]) = (arr[minIndex], arr[i]);
                }
            }

            Console.WriteLine($"Selection: {string.Join(",", arr)}");
        }

        public static void Insertion(int[] arr)
        {
            for (int i = 1; i < arr.Length; i++)
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

            Console.WriteLine($"Insertion Sort: {string.Join(",", arr)}");
        }

        public static void MergeSort(int[] arr)
        {
            int n = arr.Length;
            if (arr.Length <= 1) return;
            int mid = n / 2;
            int[] lef = new int[mid];
            int[] right = new int[n - mid];

            Array.Copy(arr, 0, lef, 0, mid);
            Array.Copy(arr, mid, right, 0, right.Length);

            Console.WriteLine($"Left: {string.Join(",", lef)}");
            Console.WriteLine($"right: {string.Join(",", right)}");

            MergeSort(lef);
            MergeSort(right);
            Merge(arr, lef, right);
        }

        private static void Merge(int[] result, int[] left, int[] righ)
        {
            int p1 = 0;
            int p2 = 0;
            int p3 = 0;

            int n1 = left.Length;
            int n2 = righ.Length;

            while (p1 < n1 && p2 < n2)
            {
                if (left[p1] < righ[p2])
                {
                    result[p3] = left[p1];
                    p1++;
                }
                else
                {
                    result[p3] = righ[p2];
                    p2++;
                }

                p3++;
            }

            while (p1 < n1)
            {
                result[p3] = left[p1];
                p3++;
                p1++;
            }

            while (p2 < n2)
            {
                result[p3] = righ[p2];
                p2++;
                p3++;
            }


            Console.WriteLine($"Merge Sort: {string.Join(",", result)}");
        }


        public static void QickSorting(int[] arr, int lo, int hi)
        {

            if (lo < hi)
            {
                int pi = Partition(arr, lo, hi);
                QickSorting(arr, lo, pi - 1);
                QickSorting(arr, pi + 1, hi);
            }

            Console.WriteLine($"Quick Sort: {string.Join(",",arr)}");
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

            (arr[i + 1], arr[hi]) = (arr[hi], arr[i + 1]);

            return i + 1;
        }
    }
}