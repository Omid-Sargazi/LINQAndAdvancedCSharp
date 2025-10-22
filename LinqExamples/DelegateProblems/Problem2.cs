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
}