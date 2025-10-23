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
}