namespace LinqExamples.DelegateProblems
{
    public delegate int Cal(int a, int b);
    public class DelegateProblem1
    {
        public void Execute(int a, int b)
        {
            Cal cal = Add;
            Console.WriteLine(cal(a, b));
            Cal cal1 = Mul;
            Console.WriteLine(cal1(a, b));

        }

        public int Add(int a, int b) => a + b;
        public int Mul(int a, int b) => a * b;
    }


    public class ClientDelegate1
    {
        public static void Run()
        {
            DelegateProblem1 delegateProblem1 = new DelegateProblem1();
            delegateProblem1.Execute(3, 5);
        }
    }



    public class FuncDelegate
    {
        public int Execute(int a, int b, Func<int, int, int> operations)
        {
            return operations(a, b);
        }
    }

    public class FuncClient
    {
        public static void Execute()
        {
            FuncDelegate funcDelegate = new FuncDelegate();
            var res = funcDelegate.Execute(3, 4, (x, y) => x + y);
            Console.WriteLine($"Func Result: {res}");

            res = funcDelegate.Execute(4, 5, (x, y) => x * y);
            Console.WriteLine($"Func Result: {res}");

        }
    }

    public class ActionDelegate
    {
        public void Execute(int a, int b, Action<int, int> action)
        {
            action(a, b);
        }
    }

    public class ActionClient
    {
        public static void Run(int a, int b)
        {
            ActionDelegate actionDelegate = new ActionDelegate();
            actionDelegate.Execute(3, 4, (x, y) => Console.WriteLine($"{x}"));
        }
    }

    public delegate void TempEvent(int temp);
    public class TemAlert
    {
        private int _temp;
        public event TempEvent _handler;

        public void Execute(int temp)
        {
            _temp = temp;
            if (_temp > 35)
            {
                _handler?.Invoke(temp);
            }

            else
            {
                Console.WriteLine("Temp is less than 35");
            }
        }
    }

    public class ClientTemp
    {
        public  void Run(int temp)
        {
            TemAlert temAlert = new TemAlert();
            temAlert._handler += TempConsole;
            temAlert._handler += TemScreen;
            temAlert.Execute(temp);
            temAlert.Execute(temp);

        }

        public void TempConsole(int temp) => Console.WriteLine($"Temp showed in console: {temp}");
        public void TemScreen(int temp) => Console.WriteLine($"Temp showed in Screen: {temp}");
    }
}