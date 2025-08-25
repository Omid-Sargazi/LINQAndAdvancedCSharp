namespace Patterns.StructuralPatterns
{

    public class Adaptee
    {
        public string GetSpecificRequest()
        {
            return "Specific request.";
        }
    }

    public interface ITarget
    {
        string GetRequest();
    }
    public class Adapter : ITarget
    {
        private readonly Adaptee _adaptee;
        public Adapter(Adaptee adaptee)
        {
            _adaptee = adaptee;
        }
        public string GetRequest()
        {
            return $"This is `{_adaptee.GetSpecificRequest()}`";
        }
    }


    public class ClientAdaptee
    {
        public static void Run()
        {
            var adaptee = new Adaptee();
            var adapter = new Adapter(adaptee);

            Console.WriteLine(adapter.GetRequest());
        }
    }
}