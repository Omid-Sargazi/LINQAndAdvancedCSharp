namespace LinqExamples.LeetCodeProblems
{
    public class TwoSumProblem
    {
        public static int[] TwoSumRun(int[] arr, int target)
        {
            //[2,7,11,12],target=9;
            Dictionary<int, int> seen = [];

            for (int i = 0; i < arr.Length; i++)
            {
                int completed = target - arr[i];

                if (seen.TryGetValue(completed, out int value))
                {
                    return new int[] { value, i };
                }
                seen[arr[i]] = i;
            }

            return new int[] { 0 };
        }
    }
}