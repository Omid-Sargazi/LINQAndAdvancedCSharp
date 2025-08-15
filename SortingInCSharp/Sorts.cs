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
    }
}