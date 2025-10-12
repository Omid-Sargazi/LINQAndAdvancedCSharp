using System.ComponentModel;
using System.Reflection;
using System.Runtime.InteropServices;

namespace AlgorithemInCSharp.Patterns.BehavioralDesignPattern
{
    public class User
    {
        public string Name { get; }
        private IChatRoomMediator _mediator;
        public User(string name, IChatRoomMediator mediator)
        {
            _mediator = mediator;
            Name = name;
            _mediator.Register(this);
        }

        public void SendMessage(string message)
        {
            _mediator.SendMessage(message, this);
        }
        
        public void ReceiveMessage(string message, User sender)
        {
            Console.WriteLine($"Message: {message} receive from {sender.Name}by{Name}");
        }
    }
    public interface IChatRoomMediator
    {
        void Register(User user);
        void SendMessage(string message, User sender);
    }

    public class ChatRoomMediator : IChatRoomMediator
    {
        private List<User> _users = new();
        public void Register(User user)
        {
            if (!_users.Contains(user))
            {
                _users.Add(user);
            }
        }

        public void SendMessage(string message, User sender)
        {
            foreach (var user in _users)
            {
                if (user != sender)
                {
                    user.ReceiveMessage(message, sender);
                }
            }
        }
    }

    public class ClientMediator
    {
        public static void Run()
        {
            IChatRoomMediator chatRoom = new ChatRoomMediator();

            User user1 = new User("Omid", chatRoom);
            User user2 = new User("Saeed", chatRoom);
            User user3 = new User("Vahid", chatRoom);

            user1.SendMessage("Hiiii");
            user2.SendMessage("Hii, I`m Good");
        }
    }

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

            var handlerInsatnce = Activator.CreateInstance(handler);

            var result = handler.GetMethod("Handle").Invoke(handlerInsatnce, new object[] { request });

            return (TResult)result;
        }
    }

    public class TestMakeGeneruc
    {
        public static void MakeGeneric()
        {
            Type listType = typeof(List<>);
            Type stringListType = listType.MakeGenericType(typeof(string));
            Console.WriteLine(stringListType);

            var stringList = Activator.CreateInstance(stringListType);
            Console.WriteLine($"instance created {stringList.GetType()}");
        }

        public static void FindTypesInAssembly()
        {
            var handlerType = Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(t => t.Name.EndsWith("Handle")).ToList();

            foreach (var type in handlerType)
            {
                try
                {
                    Console.WriteLine($"Found handler:{type.Name}");
                }
                catch (System.Exception ex)
                {

                    Console.WriteLine($"{ex.Message}");
                }
            }
        }

        public static void TestSimpleInvoke()
        {
            var calculatorType = typeof(Calculator);
            var calculatorInstance = Activator.CreateInstance(calculatorType);

            var addMethod = calculatorType.GetMethod("Add");
            var result1 = addMethod.Invoke(calculatorInstance, new object[] { 5, 3 });

            var greetingMethod = calculatorType.GetMethod("Greet");
            var result2 = greetingMethod.Invoke(calculatorInstance, new object[] { "Omid" });
        }

        public static void TestAssignableFrom()
        {
            var animalType = typeof(IAnimal);

            var allType = Assembly.GetExecutingAssembly().GetTypes();

            foreach (var type in allType)
            {
                if (animalType.IsAssignableFrom(type) && !type.IsInterface)
                {
                    Console.WriteLine($"{type.Name} is an animal..");

                    var animal = Activator.CreateInstance(type);
                    Console.WriteLine($"Created {animal.GetType().Name}");
                }
            }
        }
        
        public static void TestSimpleMediator()
        {
            var mediator = new SimpleMediator();
            var command = new SayHelloCommand { Name = "omid" };
            mediator.Send(command);
        }
        
        
    }

    public class Calculator
    {
        public int Add(int a, int b) => a + b;
        public string Greet(string name) => $"Hello:{name}";
    }

    public interface IAnimal { }
    public class Dog : IAnimal { }
    public class Cat : IAnimal { }
    public class Car { }

    public interface ICommand { }
    public class SayHelloCommand : ICommand
    {
        public string Name { get; set; }
    }

    public interface ICommandhandler<T> where T : ICommand
    {
        void Handle(T command);
    }

    public class SayHelloHandler : ICommandhandler<SayHelloCommand>
    {
        public void Handle(SayHelloCommand command)
        {
            Console.WriteLine($"Hello{command.Name}");
        }
    }

    public class SimpleMediator
    {
        public void Send<T>(T command)where T: ICommand
        {
            var commandType = command.GetType();
            var handlerType = typeof(ICommandhandler<>).MakeGenericType(commandType);

            var handler = Assembly.GetExecutingAssembly()
            .GetTypes()
            .FirstOrDefault(t => handlerType.IsAssignableFrom(t));

            if(handler!=null)
            {
                var handlerInstance = Activator.CreateInstance(handler);
                var handlerMethod = handlerType.GetMethod("Handle");
                handlerMethod.Invoke(handlerInstance, new object[] { command});
            }
        }
    }
}