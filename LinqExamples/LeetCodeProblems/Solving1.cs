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

        public static int[] TwoSum2(int[] arr, int target)
        {
            var pairs = arr.Select((a, b) => (val: a, idx: b)).ToArray();
            Array.Sort(pairs, (a, b) => a.CompareTo(b));

            int left = 0;
            int right = arr.Length - 1;

            while (left < right)
            {
                int sum = arr[left] + arr[right];
                if (sum == target)
                {
                    return new int[] { left, right };
                }
                else if (sum < target)
                {
                    left++;
                }
                else
                {
                    right--;
                }
            }

            return new int[] { 0 };
        }
    }
}