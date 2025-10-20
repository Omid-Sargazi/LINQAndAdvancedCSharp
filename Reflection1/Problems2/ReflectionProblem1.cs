using System.Reflection;

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

            if (!type.IsClass)
            {
                Console.WriteLine("Only class types are allowed!");
                return;
            }


            Type[] interfeces = type.GetInterfaces();
            Console.WriteLine($"Interfaces implemented by {type.Name}:");

            foreach (var i in interfeces)
            {
                Console.WriteLine(i);
            }
            if (interfeces.Length == 0)
            {
                return;
            }

            var targetInterface = interfeces.First();
            Console.WriteLine($"\nFinding all classes that implement {targetInterface.Name}...\n");

            var assembly = Assembly.GetExecutingAssembly();

            var implementations = assembly.GetTypes()
            .Where(t => targetInterface.IsAssignableFrom(t) && t.IsClass && !t.IsAbstract).ToList();

            foreach (var impl in implementations)
            {
                Console.WriteLine($"{impl}");
            }
        }
    }

    public class ClientReflection
    {
        public static void Run(object obj)
        {
            Type type = obj.GetType();
            Console.WriteLine($"Is class? {type.IsClass}");

            var interfaces = type.GetInterfaces();
            var assembly = Assembly.GetExecutingAssembly();

            foreach (var i in interfaces)
            {
                Console.WriteLine($"\nInterface: {i.Name}");
                var interfaceType = i;

                var implementations = assembly.GetTypes()
                .Where(t => interfaceType.IsAssignableFrom(t) && t.IsClass && !t.IsAbstract).ToList();

                foreach(var impl in implementations)
                {
                    Console.WriteLine($"  -> {impl.Name}");
                }
            }

        }
    }
}