using System.Data.Common;

namespace LinqExamples.LiveCoding
{
    public class IsPalindrome
    {
        public static bool Run(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return true;
            }

            int left = 0;
            int right = str.Length - 1;
            while (left < right)
            {
                if (str[left] != str[right])
                {
                    return false;
                }
            }

            return true;
        }

        public static char FirstRepeatingChar(string str)
        {
            var seen = new HashSet<char>();
            foreach (var c in str)
            {
                if (seen.Contains(c))
                    return c;
                seen.Add(c);
            }

            return '\0';

        }

        public static bool AreAnagrams(string s1, string s2)
        {
            if (s1.Length != s2.Length) return false;

            var charCount = new int[256];

            foreach (char c in s1)
            {
                charCount[c]++;
            }


            foreach (char c in s2)
            {
                charCount[c]--;
                if (charCount[c] < 0)
                {
                    return false;
                }
            }

            return true;
        }

        public static int MostFrequent(int[] nums)
        {
            var frequency = new Dictionary<int, int>();
            int maxCount = 0, result = nums[0];

            foreach (var num in nums)
            {
                frequency[num] = frequency.GetValueOrDefault(num, 0) + 1;
                if (frequency[num] > maxCount)
                {
                    maxCount = frequency[num];
                    result = num;
                }
            }

            return result;
        }

        public static int MostFrequent2(int[] nums)
        {
            var frequency = new Dictionary<int, int>();
            int maxCount = 0, result = nums[0];

            foreach (var num in nums)
            {
                frequency[num] = frequency.GetValueOrDefault(num, 0) + 1;
                if (frequency[num] > maxCount)
                {
                    maxCount = frequency[num];
                    return num;
                }
            }
            return result;
        }
    }
}