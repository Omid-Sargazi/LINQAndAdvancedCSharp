using System.Text;

namespace SortingInCSharp
{
    public class CArrayInt
    {
        private readonly int[] _arr;
        private int _count = 0;
        public CArrayInt(int size)
        {
            if (size <= 0) throw new ArgumentOutOfRangeException(nameof(size));
            _arr = new int[size];
        }

        public void Insert(int item)
        {
            if (_count == _arr.Length) throw new InvalidOperationException("Array Full");
            _arr[_count++] = item;
        }

        public void Clear()
        {
            Array.Clear(_arr, 0, _count);
            _count = 0;
        }

        public int[] Data => _arr;
        public int Count => _count;
        public int Upper => _arr.Length - 1;


        public string Display()
        {
            var sb = new StringBuilder();
            for (int i = 0; i <= _arr.Length - 1; i++)
            {
                if (i > 0) sb.Append(' ');
                sb.Append(_arr[i]);
            }

            return sb.ToString();
        }


    }
}