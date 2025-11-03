namespace StandardProblems.Problems
{
    public class Problem1
    {
        public static void Run()
        {
            var res = SumTwoNums(new int[] { 1, 2, 3, 4, 6, 8, 9, 11 }, 10);
            Console.WriteLine($"Indices: {res[0]}, {res[1]}");

        }


        public static int[] SumTwoNums(int[] nums, int target)
        {
            int n = nums.Length;
            int left = 0;
            int right = n - 1;
            while (left < right)
            {
                if (nums[left] + nums[right] == target)
                {
                    return new int[] { left, right };
                }
                else if (nums[left] + nums[right] < target)
                {
                    left++;
                }
                else
                {
                    right--;
                }
            }
            return new int[] { -1, -1 };
        }

        public static bool SumTwoNumsAnotherWay(int[] nums, int target)
        {
            
        }
    }
}