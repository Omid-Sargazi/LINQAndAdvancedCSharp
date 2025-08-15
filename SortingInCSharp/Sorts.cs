namespace SortingInCSharp
{
    public static class Sorts
    {
        public static void Bubble(int[] a)
        {
            for (int end = a.Length - 1; end >= 1; end--)
            {
                bool swapped = false;
                for (int i = 0; i < end; i++)
                {
                    if (a[i] > a[i + 1])
                    {
                        (a[i], a[i + 1]) = (a[i + 1], a[i]);
                        swapped = true;
                    }
                }
                if (!swapped) break;
            }
        }

        public static void Selection(int[] a)
        {
            for (int start = 0; start <= a.Length - 1; start++)
            {
                int minIndex = start;
                for (int j = start + 1; j <= a.Length; j++)
                {
                    if (a[j] < a[minIndex])
                        minIndex = j;
                }

                (a[minIndex], a[start]) = (a[start], a[minIndex]);
            }
        }

        public static void Insertion(int[] a)
        {
            for (int i = 1; i < a.Length; i++)
            {
                int key = a[i];
                int j = i - 1;

                while (j >= 0 && a[j] > key)
                {
                    a[j + 1] = a[j];
                    j--;
                }

                a[j + 1] = key;
            }
        }
    }
}