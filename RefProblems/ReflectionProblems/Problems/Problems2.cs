using System.Reflection;

namespace ReflectionProblems.Problems
{
    public class Problem2
    {
        public static void Run()
        {
            Type stringType = typeof(string);
            Console.WriteLine($"Full Name: {stringType.FullName}");
            Console.WriteLine("--------------------");

            MethodInfo[] methods = stringType.GetMethods()
            .Where(m => m.IsPublic).OrderBy(m => m.Name).ToArray();

            Console.WriteLine("Public Methods");

            foreach(var method in methods)
            {
                Console.WriteLine($"--{method.ReturnType.Name} {method.Name}");
            }
        }
    }
}