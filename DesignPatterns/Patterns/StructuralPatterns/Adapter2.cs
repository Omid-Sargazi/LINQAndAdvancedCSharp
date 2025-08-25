namespace Patterns.StructuralPatterns
{
    public class Adaptee2
    {
        public string GetSpecificRequest()
        {
            return "Specific Request";
        }
    }


    public interface ITarget2
    {
        public string GetRequest();
    }

    public class Adapter2 : ITarget2
    {
        private readonly Adaptee2 _adaptee2;
        public Adapter2(Adaptee2 adaptee2)
        {
            _adaptee2 = adaptee2;
        }
        public string GetRequest()
        {
            return _adaptee2.GetSpecificRequest();
        }
    }
}