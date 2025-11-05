namespace StandardProblems.Problems4
{
    public class LeetCodeExamples
    {
        public static void MaximumSubarray(int[] arr)
        {
            int current = arr[0];
            int best = arr[0];

            for (int i = 1; i < arr.Length; i++)
            {
                current = Math.Max(arr[i], current + arr[i]);
                best = Math.Max(best, current);
            }

            Console.WriteLine($"MaxSubArray:{best}");
        }
    }
}