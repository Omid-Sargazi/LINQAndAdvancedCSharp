namespace AlgorithemInCSharp.Patterns
{
    public interface IImplementation
    {
        string OperationImplementation();
    }

    public class ConcreteImplementationA : IImplementation
    {
        public string OperationImplementation()
        {
            return "ConcreteImplementationA: The result in platform A.\n";
        }
    }

    public class ConcreteImplementationB : IImplementation
    {
        public string OperationImplementation()
        {
            return "ConcreteImplementationB: The result in platform B.\n";
        }
    }

    public abstract class Abstraction
    {
        protected IImplementation _implementation;
        public Abstraction(IImplementation implementation)
        {
            _implementation = implementation;
        }

        public virtual string Operation()
        {
            return "bstract: Base operation with:\n" +
            _implementation.OperationImplementation();
        }
    }

    public class ExtendedAbstraction : Abstraction
    {
        public ExtendedAbstraction(IImplementation implementation) : base(implementation)
        {
        }

        public override string Operation()
        {
            return "ExtendedAbstraction: Extended operation with:\n" +
            base._implementation.OperationImplementation();
        }
    }
}