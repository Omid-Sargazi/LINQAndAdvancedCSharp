namespace LinqExamples.DelegateProblems
{
    public class Problems2
    {
        Func<int, bool> evenn = delegate (int a)
        {
            return a % 2 == 0;
        };
        public void Run(int num)
        {
            Func<int, bool> even = (int a) => { return a % 2 == 0; };
            evenn(2);
            Console.WriteLine(even(num));
            Console.WriteLine(evenn(10));


        }
    }

    public class Problem3
    {
        public delegate int Cal(int a, int b);
        public void Run()
        {
            Cal cal = Add;

            PassDelegate(cal);
        }

        public void PassDelegate(Cal cal)
        {
            var res = cal(3, 5);
            Console.WriteLine(res);
        }

        public int Add(int a, int b)
        {
            return a + b;
        }
    }

    public class Problem4
    {
        public delegate int Cal(int a, int b);

        public void Execute(int a, int b, Func<int, int, int> operation)
        {
            Console.WriteLine(operation(a, b));
        }

        public int Add(int a, int b) => a + b;
    }

    public class Problem5
    {
        public void Execute(int a, int b, Action<int, int> action)
        {
            action(a, b);
        }
    }

    public delegate void TemperatureChangedHandler(int newTemperature);
    public class Thermometer
    {
        public event TemperatureChangedHandler OnTemperatureTooHigh;

        private int _temp;

        public void SetTemp(int temp)
        {
            _temp = temp;

            if(_temp>30)
            {
                OnTemperatureTooHigh?.Invoke(_temp);
            }
        }
    }
}