namespace AdventureWorksLINQ.Console.Reflection
{
    public interface ISomeTask
    {
        public void Some();
    }

    public class SOmeTask : ISomeTask
    {
        public void Some()
        {
            System.Console.WriteLine("Class Some");

        }
    }

    public interface AInterface
    {
        public void AMethod();
    }

    public interface BInterface
    {
        public void BMethod();
    }

    public class AClass : AInterface
    {
        private readonly ISomeTask _someTask;
        public AClass() { }
        public AClass(ISomeTask someTask)
        {
            _someTask = someTask;
        }
        public void AMethod()
        {
            System.Console.WriteLine("Class A");
        }
    }

    public class BClass : BInterface
    {
        public void BMethod()
        {
            System.Console.WriteLine("Class B");
        }
    }

    public class Master2
    {
        private readonly AInterface _aInterface;
        private readonly BInterface _bInterface;

        public Master2(AInterface aInterface, BInterface bInterface)
        {
            _aInterface = aInterface;
            _bInterface = bInterface;
        }

        public void DoTask()
        {
            _aInterface.AMethod();
            _bInterface.BMethod();
        }
    }

    public class DIContainer2
    {
        private Dictionary<Type, Type> pairs = new();
        public void Register<TInterface, TImplementation>()
        {
            Type interfaceType = typeof(TInterface);
            Type implementationType = typeof(TImplementation);
            if (!pairs.ContainsKey(interfaceType))
            {
                pairs[interfaceType] = implementationType;
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
            return (T)CreateInstance(implementationType);
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
                var parameterInstance = GetType().GetMethod("Resolve").
                MakeGenericMethod(parameterType).Invoke(this, null);

                parameterInstances.Add(parameterInstance);
            }
            return constructor.Invoke(parameterInstances.ToArray());
        }
    }

    public class DIClient2
    {
        public static void Run()
        {
            DIContainer2 di = new DIContainer2();
            di.Register<AInterface, AClass>();
            di.Register<BInterface, BClass>();
            di.Register<ISomeTask, SOmeTask>()
;
            var d1 = di.Resolve<AInterface>();
            var d2 = di.Resolve<BInterface>();
            // d1.AMethod();
            // d2.BMethod();

            Master2 master = new Master2(d1, d2);
            master.DoTask();
            //=======================================
            var method = typeof(Example2).GetMethod("TestMethod");
            var parametes = method.GetParameters();

            foreach (var param in parametes)
            {
                System.Console.WriteLine($"{param.ParameterType.Name}");
                System.Console.WriteLine($"{param.ParameterType.IsInterface}");
                System.Console.WriteLine($"{param.Name}");
            }
            //=======================================

        }
    }

    public interface IServiceA
    {

    }

    public interface IServiceB { }

    public class ServiceA : IServiceA
    {

    }

    public class ServiceB : IServiceB
    {

        public ServiceB(IServiceA serviceA)
        {

        }
    }


    public class Example2
    {
        public void TestMethod(IServiceA serviceA, IServiceB serviceB, string name, int age)
        {

        }
    }
    

}