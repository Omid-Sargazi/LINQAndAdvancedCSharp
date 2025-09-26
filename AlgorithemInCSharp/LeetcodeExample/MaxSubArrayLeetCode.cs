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
                maxCurrent = MaxTwoNum(num, maxCurrent+num);
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
    }
}