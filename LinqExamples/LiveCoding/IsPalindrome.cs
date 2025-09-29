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
    }
}