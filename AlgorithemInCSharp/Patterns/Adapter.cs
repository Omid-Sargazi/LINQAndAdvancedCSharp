namespace AlgorithemInCSharp.Patterns
{
    public interface ITarget
    {
        public string GetRequest();
    }
    public class Adaptee
    {
        public string getOldRequest()
        {
            return $"This is a old method.";
        }
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
            return $"Adapter and {_adaptee.getOldRequest()}";
        }
    }
}