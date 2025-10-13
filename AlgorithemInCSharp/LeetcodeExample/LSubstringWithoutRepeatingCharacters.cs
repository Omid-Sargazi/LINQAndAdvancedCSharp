using System.ComponentModel.DataAnnotations;

namespace AlgorithemInCSharp.LeetcodeExample
{
    public class LSubstringWithoutRepeatingCharacters
    {
        public static int Run(string s)
        {

            int maxLong = 0;
            int left = 0;
            int right = 0;
            HashSet<char> chars = new();
            for (right = 0; right < s.Length; right++)
            {
                while (chars.Contains(s[right]))
                {
                    chars.Remove(s[left]);
                    left++;
                }
                chars.Add(s[right]);
                maxLong = Math.Max(maxLong, right - left + 1);
            }
            return maxLong;
        }
    }

    public class Node
    {
        public int Value;
        public Node _next;
        public Node(int value)
        {
            Value = value;
            _next = null;
        }
    }

    public class LinkedList
    {
        private Node _head;
        public LinkedList() { }
        public LinkedList(Node node)
        {
            _head = node;
        }

        public void Add(int value)
        {
            if (_head == null)
            {
                _head = new Node(value);
                return;
            }

            var current = _head;
            while (current._next != null)
            {
                current = current._next;
            }

            current._next = new Node(value);
        }

        public void Prinf()
        {
            if (_head == null)
            {
                Console.WriteLine("List is empty");
                return;
            }

            var current = _head;
            while (current != null)
            {
                Console.WriteLine($"{current.Value}");
                current = current._next;
            }
        }
        
        public void Reverse()
        {
            if (_head == null)
            {
                Console.WriteLine("List is Empty");
                return;
            }

            var current = _head;
            Node prev = null;
            Node next = null;
            while (current != null)
            {
                next = current._next;
                current._next = prev;
                prev = current;
                current = next;
            }
            _head = prev;
        }
    }

    public class ClientLits
    {
        public static void Run()
        {
            LinkedList l1 = new LinkedList();
            l1.Add(1);
            l1.Add(10);
            l1.Add(11);
            l1.Add(12);
            l1.Prinf();
            Console.WriteLine("Reverse is:");
            l1.Reverse();
            l1.Prinf();

        }
    }
}