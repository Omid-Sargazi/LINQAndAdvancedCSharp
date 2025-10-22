using System.Security.Cryptography.X509Certificates;

namespace LinqExamples.DelegateProblems
{
    public class Problem1
    {
        public Func<int, int, int> Calculator;
        public  int  Run(int a, int b,ref int val)
        {
            Calculator += Add;
            val = Calculator(a, b);
            Calculator += Mul;
            return Calculator(a, b);
        }

        public int Add(int a, int b) => a + b;
        public int Mul(int a, int b) => a * b;
    }
}