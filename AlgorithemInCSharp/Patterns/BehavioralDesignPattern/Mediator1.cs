using System.Reflection;

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

            foreach(var type in handlerType)
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
    }
}