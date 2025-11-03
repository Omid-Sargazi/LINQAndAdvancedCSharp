namespace StandardProblems.Problems
{
    public class Problem1
    {
        public static void Run()
        {
            // var res = SumTwoNumsAnotherWay(new int[] { 1, 2, 3, 4, 6, 8, 9, 11 }, 10);
            // Console.WriteLine($"Indices: {res[0]}, {res[1]}");
            // var s = "[]{}()()";
            // var res = IsValidParentheses(s);
            // Console.WriteLine(res);

            int[] arr1 = new int[] { 1, 2, 3, 4 };
            int[] arr2 = new int[] { 10, 20, 30, 40 };

            Console.WriteLine($"{string.Join(",",MergeTwoSortedArray(arr1, arr2))}");


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

        public static int[] SumTwoNumsAnotherWay(int[] nums, int target)
        {
            var n = nums.Length;
            var seen = new Dictionary<int, int>();

            for (int i = 0; i < n; i++)
            {
                int completed = target - nums[i];
                if (seen.ContainsKey(completed))
                {
                    return new int[] { seen[completed], i };
                }
                seen[nums[i]] = i;
            }

            return new int[] { -1, -1 };
        }

        public static bool IsValidParentheses(string s)
        {
            Stack<char> chars = new Stack<char>();
            foreach (char c in s)
            {
                if (c == '{' || c == '[' || c == '(')
                {
                    chars.Push(c);
                }

                else if (c == ')' || c == ']' || c == '}')
                {
                    if (chars.Count == 0) return false; // پشته خالی است، پس نامعتبر
                    char top = chars.Pop();
                    if ((c == ')' && top != '(') ||
                        (c == ']' && top != '[') ||
                        (c == '}' && top != '{'))
                    {
                        return false; // پرانتزها مطابقت ندارند
                    }
                }

            }
            return chars.Count == 0;
        }
        
        public static int[] MergeTwoSortedArray(int[] arr1,int[] arr2)
        {
           
            
            int n1 = arr1.Length;
            int n2 = arr2.Length;

            int p1 = 0;
            int p2 = 0;
            int p3 = 0;

            int n3 = n1 + n2;
            int[] result = new int[n1+n2];

            while (p1 < n1 && p2 < n2)
            {
                if (arr1[p1] <= arr2[p2])
                {
                    result[p3] = arr1[p1];
                    p1++;
                }
                else
                {
                    result[p3] = arr2[p2];
                    p2++;
                }

                p3++;
            }

            while (p1 < n1)
            {
                result[p3] = arr1[p1];
                p3++;
                p1++;
            }

            while (p2 < n2)
            {
                result[p3] = arr2[p2];
                p3++;
                p2++;
            }

            return result;
        }
    }
}