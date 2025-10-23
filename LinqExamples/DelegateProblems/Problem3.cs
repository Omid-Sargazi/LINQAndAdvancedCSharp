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
}