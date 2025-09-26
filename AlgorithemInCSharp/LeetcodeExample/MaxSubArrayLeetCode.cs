namespace AlgorithemInCSharp.LeetcodeExample
{
    public class LeetCodeProblem
    {
        public static int MaxSubArray(int[] nums)
        {
            if (nums == null || nums.Length == 0) return 0;

            int maxCurrent = nums[0];
            int maxSum = nums[0];

            for (int i = 1; i < nums.Length; i++)
            {
                maxCurrent = MaxTwoNum(nums[i], maxCurrent);
                maxSum = MaxTwoNum(maxCurrent, maxSum);
            }

            return maxSum;
        }

        private static int MaxTwoNum(int a, int b)
        {
            if (a > b)
            {
                return a;
            }
            else
            {
                return b;
            }
        }
        private static int MinTwoNum(int a, int b)
        {
            if (a > b)
            {
                return b;
            }
            else
            {
                return a;
            }
        }


        public static int MaxProSubArray(int[] nums)
        {
            int minSofar = nums[0];
            int maxSofar = nums[0];
            int result = nums[0];



            for (int i = 1; i < nums.Length; i++)
            {
                int num = nums[i];

                if (num < 0)
                {
                    (maxSofar, minSofar) = (minSofar, maxSofar);
                }

                maxSofar = MaxTwoNum(num, maxSofar * num);
                minSofar = MinTwoNum(num, minSofar * num);

                result = MaxTwoNum(maxSofar, result);
            }

            return result;

        }

    }

    public class LeetCodeProblems
    {
        public static int MaxSubArray(int[] nums)
        {
            if (nums == null || nums.Length == 0) return 0;

            int maxSum = nums[0];
            int maxCurrent = nums[0];

            for (int i = 1; i < nums.Length; i++)
            {
                int num = nums[i];
                maxCurrent = MaxTwoNum(num, maxCurrent + num);
                maxSum = MaxTwoNum(maxSum, maxCurrent);
            }

            return maxSum;
        }

        private static int MaxTwoNum(int num1, int num2)
        {
            if (num1 > num2)
                return num1;
            else
                return num2;
        }

        private static int MinTwoNum(int num1, int num2)
        {
            if (num1 > num2)
                return num2;
            else
                return num1;
        }

        public static int MaxProSubArray(int[] nums)
        {
            if (nums == null || nums.Length == 0) return 0;

            int maxSofar = nums[0];
            int minSofar = nums[0];
            int result = nums[0];

            for (int i = 1; i < nums.Length; i++)
            {
                int num = nums[i];

                if (num < 0)
                {
                    (maxSofar, minSofar) = (minSofar, maxSofar);
                }

                maxSofar = MaxTwoNum(num, num * maxSofar);
                minSofar = MinTwoNum(num, minSofar * num);

                result = MaxTwoNum(result, maxSofar);
            }

            return result;
        }


        public static int[] MaxSubArrayWithIndices(int[] nums, out int maxSum)
        {
            if (nums == null || nums.Length == 0)
            {
                maxSum = 0;
                return new int[0];
            }

            int currentSum = nums[0];
            int currentMaxSum = nums[0];
            int start = 0, end = 0, tempStart = 0;

            for (int i = 1; i < nums.Length; i++)
            {
                if (nums[i] > currentSum + nums[i])
                {
                    currentSum = nums[i];
                    tempStart = i;
                }
                else
                {
                    currentSum += nums[i];
                }

                if (currentSum > currentMaxSum)
                {
                    currentMaxSum = currentSum;
                    start = tempStart;
                    end = i;
                }
            }
            maxSum = currentMaxSum;
            int[] subArray = new int[end - start + 1];
            Array.Copy(nums, start, subArray, 0, subArray.Length);
            return subArray;
        }
    }

    public class LeetCodeProblemss
    {
        public static int[] TwoSum(int[] nums, int target)
        {
            Dictionary<int, int> seen = new Dictionary<int, int>();

            for (int i = 0; i < nums.Length; i++)
            {
                int needed = target - nums[i];

                if (seen.ContainsKey(needed))
                {
                    return new int[] { seen[needed], i };
                }
                if (!seen.ContainsKey(needed))
                {
                    seen.Add(nums[i], i);
                }
            }

            return new int[0];
        }

        public static int[] TwoSum2(int[] nums, int target)
        {
            var seen = new Dictionary<int, int>();

            for (int i = 0; i < nums.Length; i++)
            {
                int needed = target - nums[i];

                if (seen.TryGetValue(needed, out int index))
                {
                    return new int[] { index, i };
                }

                seen[nums[i]] = i;
            }

            return new int[0];
        }

        public static int[] TwoSum3(int[] nums, int target)
        {
            var pairs = nums.Select((v, i) => (valv:v, idx:i)).ToArray();

            Console.WriteLine($"{string.Join(",", pairs)}");

            Array.Sort(pairs, (a, b) => a.CompareTo(b));
            Console.WriteLine($"{string.Join(",",pairs)}");

            int l = 0, r = nums.Length-1;

            while (l < r)
            {
                int sum = pairs[l].valv + pairs[r].valv;
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
            return new int[] { 0 };
        }


    }
}