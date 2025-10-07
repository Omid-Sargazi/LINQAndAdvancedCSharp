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
        // private readonly ISomeService _service;
        // public Order1(ISomeService service)
        // {
        //     _service = service;
        // }
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

        private object CreateInstance(Type implementationType)
        {
            var constructors = implementationType.GetConstructors();
            var constructor = constructors.OrderByDescending(c => c.GetParameters().Length).First();
            var parameters = constructor.GetParameters();
            if (parameters.Length == 0)
            {
                return Activator.CreateInstance(implementationType);
            }

            var parameterInstances = new List<Object>();

            foreach (var parameter in parameters)
            {
                var parameterType = parameter.ParameterType;
                var parameterInstance = GetType().GetMethod("Resolve")
                .MakeGenericMethod(parameterType).Invoke(this, null);
                parameterInstances.Add(parameterInstance);
            }

            return constructor.Invoke(parameterInstances.ToArray());
        }
    }

    public class DIClient
    {
        public void Run()
        {
            DIContainer di = new DIContainer();
            di.Register<IOrder1, Order1>();
            di.Register<IOrder2, Order2>();

            IOrder1 order1 = di.Resolve<IOrder1>();
            IOrder2 order2 = di.Resolve<IOrder2>();

            Master master = new(order1, order2);
            master.DoWork();


            var method = typeof(Example).GetMethod("TestMethod");
            var parameters = method.GetParameters();

            foreach (var param in parameters)
            {
                System.Console.WriteLine($"Parameter Name:{param.Name}");
                System.Console.WriteLine($"Parameter Type:{param.ParameterType}");
                System.Console.WriteLine($"Is interface:{param.ParameterType.IsInterface}");
                System.Console.WriteLine($"*******************");

            }

            Type serviceBType = typeof(ServiceB);
            var constructor = serviceBType.GetConstructors()[0];
            var parameters2 = constructor.GetParameters();

            Type firstParameters = parameters2[0].ParameterType;

            object serviceAInstance = GetType().GetMethod("Resolve").MakeGenericMethod(firstParameters)
            .Invoke(this, null);

            var cal = new Calculator();
            cal.Add(3, 5);

            var calMethod = typeof(Calculator).GetMethod("Add");
            object[] calParameter = new object[] { 5, 3 };
            int res = (int)calMethod.Invoke(cal, calParameter); 
        }


    }

    public interface IServiceA { }
    public class ServiceA : IServiceA { }

    public interface IServiceB { }
    public class ServiceB : IServiceB
    {
        private readonly IServiceA _serviceA;
        public ServiceB(IServiceA serviceA)
        {
            _serviceA = serviceA;
        }
    }


    public class Example
    {
        public void TestMethod(IServiceA serviceA, IServiceB serviceB, string name, int age)
        {
        }

    }

    public class Calculator
    {
        public int Add(int a, int b) => a + b;
    }


}

 

