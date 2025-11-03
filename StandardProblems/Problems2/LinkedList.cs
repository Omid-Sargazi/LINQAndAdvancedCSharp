namespace StandardProblems.Problems2
{
    public class Node
    {
        public Node Next;
        public int Value;
        public Node(int value)
        {
            Next = null;
            Value = value;
        }
    }

    public class LinkedListt
    {
        private Node _head;

        public void Add(int value)
        {
            Node node = new Node(value);

            if (_head == null)
            {
                _head = node;
            }

            else
            {
                Node current = _head;
                while (current.Next != null)
                {
                    current = current.Next;
                }
                current.Next = node;
            }
        }

        public void Reverse()
        {
            if (_head == null) return;

            Node current = _head;
            Node prev = null;
            while (current != null)
            {
                Node next = current.Next;
                current.Next = prev;
                prev = current;
                current = next;
            }
            _head = prev;
        }


        public void PrintList()
        {
            Node current = _head;
            while (current != null)
            {
                Console.Write($"{current.Value}+->");
                current = current.Next;
            }

            Console.Write("Null");
        }
    }

    public class ClientList
    {
        public static void Run()
        {
            LinkedListt listt = new LinkedListt();
            listt.Add(1);
            listt.Add(10);
            listt.Add(100);
            listt.Add(12);
            listt.Add(13);
            listt.Add(17);
            listt.PrintList();
            listt.Reverse();
            listt.PrintList();
        }

        public static void MergeTwoLinkedList(LinkedListt list1, LinkedListt list2)
        {
            LinkedListt result = new LinkedListt();
            result.Add
        }
    }
}