namespace Reflection1.Problems2
{
    public interface A
    {
        void AMethod();
    }
    public interface B
    {
        void BMethod();
    }
    public interface C { }
    public class AA : A
    {
        public void AMethod()
        {
            Console.WriteLine($"here is A class...");
        }
    }

    public class BB : A, B
    {
        public void AMethod()
        {
            Console.WriteLine($"here is B class... and AMethod");

        }

        public void BMethod()
        {
            Console.WriteLine($"here is B class... and BMethod");

        }
    }

    public class ReflectionProblem1
    {
        public static void Run(object obj)
        {
            var type = obj.GetType();
            Type[] interfeces = type.GetInterfaces();
            foreach(var i in interfeces)
            {
                Console.WriteLine(i);
            }
        }
    }
}