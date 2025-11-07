using System.Reflection;

namespace DesignPattern.Reflections
{
    public class Calculator
    {
        public int Add(int a, int b) => a + b;
        public double Divide(double a, double b) => a / b;
        public string Greet(string name) => $"Hello, {name}!";
        public void PrintMessage(string message) => Console.WriteLine($"Message: {message}");
    }

    public class CalReflection
    {
        public static void Execute()
        {
            var calculator = new Calculator();
            Type calType = calculator.GetType();

            string methodName = "Add";
            MethodInfo method = calType.GetMethod(methodName);

            if (method == null)
            {
                Console.WriteLine($"Method {methodName} not Found.");
                return;
            }

            object[] parameters = new object[] { 2, 3 };

            try
            {
                object result = method.Invoke(calculator, parameters);
                Console.WriteLine($"Result {result}");
            }
            catch (Exception ex) 
            {

                Console.WriteLine($"Error:{ex.Message}");
            }
        }
    }
}