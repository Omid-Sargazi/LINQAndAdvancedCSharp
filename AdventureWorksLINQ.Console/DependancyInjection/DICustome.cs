namespace AdventureWorksLINQ.Console.DependancyInjection
{
    public interface ISomeService
    {
        public void SomeService();
    }
    public interface IOrder1
    {
        public void A();
        public void B();
    }

    public class Order1 : IOrder1
    {
        private readonly ISomeService _service;
        public Order1(ISomeService service)
        {
            _service = service;
        }
        public void A()
        {
            System.Console.WriteLine("I'm A class");
        }

        public void B()
        {
            System.Console.WriteLine("I'm B class");

        }
    }

    public interface IOrder2
    {
        public void C();
        public void D();
    }

    public class Order2 : IOrder2
    {
        public void C()
        {
            System.Console.WriteLine("I'm C class");

        }

        public void D()
        {
            System.Console.WriteLine("I'm D class");

        }
    }


    public class Master
    {
        private readonly IOrder1 _order1;
        private readonly IOrder2 _order2;

        public Master(IOrder1 order1, IOrder2 order2)
        {
            _order1 = order1;
            _order2 = order2;
        }

        public void DoWork()
        {
            _order1.A();
            _order1.B();
            _order2.C();
            _order2.D();
        }
    }


    public class DIContainer
    {
        private Dictionary<Type, Type> pairs = new Dictionary<Type, Type>();
        public void Register<TInterface, TImplemantation>()
        {
            Type typeInterface = typeof(TInterface);
            Type typeImplementation = typeof(TImplemantation);
            if (!pairs.ContainsKey(typeInterface))
            {
                pairs[typeInterface] = typeImplementation;
           }
        }

        public T Resolve<T>()
        {
            Type interfaceType = typeof(T);
            if (!pairs.ContainsKey(interfaceType))
            {
                throw new InvalidOperationException($"Type {interfaceType.Name} is not registered.");
            }

            Type implementationType = pairs[interfaceType];
            return (T)Activator.CreateInstance(implementationType);
        }
    }
}

 

