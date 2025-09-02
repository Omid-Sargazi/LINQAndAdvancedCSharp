namespace Patterns
{
    public interface ITarget
    {
        string GetRequest();
    }

    public class Adaptee
    {
        public string GetOldRequest()
        {
            return $"This is commnad from old version";
        }
    }
    public class Adapter : ITarget
    {
        protected readonly Adaptee _adaptee;
        public Adapter(Adaptee adaptee)
        {
            _adaptee = adaptee;
        } 
        public string GetRequest()
        {
            return _adaptee.GetOldRequest() + "add from adapter";
        }
    }
}