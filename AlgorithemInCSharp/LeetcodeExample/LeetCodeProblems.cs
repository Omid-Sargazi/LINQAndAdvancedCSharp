namespace AlgorithemInCSharp.LeetcodeExample
{
    public class SolvingLeetCode
    {
        public static int[] TwoSum1(int[] nums, int target)
        {
            var seen = new Dictionary<int, int>();
            for (int i = 0; i < nums.Length; i++)
            {
                int needed = target - nums[i];
                if (seen.TryGetValue(needed, out int value))
                {
                    return new int[] { i, value };
                }

                seen[nums[i]] = i;
            }

            return new int[] { 0 };
        }

        public static int[] TwoSum(int[] nums, int target)
        {
            var pairs = nums.Select((v, i) => (val: v, idx: i)).ToArray();

            Array.Sort(pairs, (a, b) => a.val.CompareTo(b.val));

            int l = 0, r = nums.Length - 1;
            while (l < r)
            {
                int sum = pairs[l].val + pairs[r].val;
                if (sum == target)
                {
                    return new int[] { pairs[l].idx, pairs[r].idx };
                }
                else if (sum < target)
                {
                    l++;
                }
                else
                {
                    r--;
                }
            }

            return new int[0];
        }


        public static int MaximumSubarray(int[] nums)
        {
            int maxCurrent = nums[0];
            int maxSum = nums[0];

            for (int i = 1; i < nums.Length; i++)
            {
                maxCurrent = MaxTwoNum(maxCurrent, nums[i]+maxCurrent);
                maxSum = MaxTwoNum(maxSum, maxCurrent);
            }

            return maxSum;
        }

        private static int MaxTwoNum(int a, int b)
        {
            if (a < b)
            {
                return b;
            }
            return a;
        }
        private static int MinTwoNum(int a, int b)
        {
            if (a > b)
            {
                return b;
            }
            return a;
        }
    }
}