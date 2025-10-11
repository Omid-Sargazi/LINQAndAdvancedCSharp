using System.Reflection;
using System.Threading.Tasks.Dataflow;

namespace Patterns.Mediator
{
    public interface IRequest<TResult>
    {

    }

    public interface IRequestHandler<TRequest, TResult>
    {
        TResult Handle(TRequest request);
    }

    public class Mediator
    {
        public TResult Send<TResult>(IRequest<TResult> request)
        {
            var requestType = request.GetType();
            var handlerType = typeof(IRequestHandler<,>).MakeGenericType(requestType, typeof(TResult));

            var handler = Assembly.GetExecutingAssembly()
            .GetTypes()
            .FirstOrDefault(t => handlerType.IsAssignableFrom(t));

            var handlerInstance = Activator.CreateInstance(handler);

            var result = handlerType.GetMethod("Handle").Invoke(handlerInstance, new object[] { request });
            return (TResult)result;
        }
    }

    public class CreateUserCommand : IRequest<bool>
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }

    public class CreateUserHandler : IRequestHandler<CreateUserCommand, bool>
    {
        public bool Handle(CreateUserCommand request)
        {
            Console.WriteLine($"User{request.Name}{request.Age} created Successfully.");
            return true;
        }
    }


    public class ClientMediator
    {
        public static void Run()
        {
            var mediator = new Mediator();
            var command = new CreateUserCommand { Name = "omid", Age = 42 };

            var result = mediator.Send(command);

            Console.WriteLine($"Operation result:{result}");
        }
    }


    public class TypeProblem
    {
        public static void TestMakeGeneric()
        {
            Type listType = typeof(List<>);
            Type strinListType = listType.MakeGenericType(typeof(string));

            Console.WriteLine($"List type:{listType}");
            Console.WriteLine($"List<string> type:{strinListType}");

            var strinList = Activator.CreateInstance(strinListType);
            Console.WriteLine($"Instance created: {strinList.GetType()}");


            Type dicType = typeof(Dictionary<,>);

            Type specificDict = dicType.MakeGenericType(typeof(string), typeof(int));

            var myDic = Activator.CreateInstance(specificDict);
            var addMethod = specificDict.GetMethod("Add");
            addMethod.Invoke(myDic, new object[] { "Omid", 1 });

            Type nullableType = typeof(Nullable<>);
            Type specificNullable = nullableType.MakeGenericType(typeof(int));
            var myNullable = Activator.CreateInstance(specificNullable);

        }
    }

    public class Calculator
    {
        public int Add(int a, int b) => a + b;
        public string Great(string name) => $"Hello {name}";
    }

    public class ClientCalculator
    {
        public static void TestSimpleInvoke()
        {
            var calType = typeof(Calculator);
            var calculatorInstance = Activator.CreateInstance(calType);

            var addmethod = calType.GetMethod("Add");
            var res1 = addmethod.Invoke(calculatorInstance, new object[] { 5, 3 });
            Console.WriteLine($"5+3= {res1}");

            var greetMethod = calType.GetMethod("Great");
            var res2 = greetMethod.Invoke(calculatorInstance, new object[] { "Omid" });
            Console.WriteLine($"{res2}");
        }
    }


}