using System.Numerics;
using System.Reflection.Metadata;

namespace AlgorithemInCSharp.PatternsInCSharp
{
    public class User
    {
        public string Name { get; set; }
        private IChatMediator _mediator;

        public User(string name, IChatMediator mediator)
        {
            Name = name;
            _mediator = mediator;
        }

        public void Send(string message)
        {
            _mediator.SendMessage(message, this);
        }

        public void ReceiveMessage(string from, string message)
        {
            Console.WriteLine($"{Name} received '{message}' from {from}");
        }
    }
    public interface IChatMediator
    {
        void SendMessage(string message, User user);
        void RegisterUser(User user);
    }

    public class ChatRoom : IChatMediator
    {
        private List<User> _users = new();
        public void RegisterUser(User user)
        {
            _users.Add(user);
        }

        public void SendMessage(string message, User sender)
        {
            foreach (var user in _users)
            {
                if (user != sender)
                {
                    user.ReceiveMessage(sender.Name, message);
                }
            }
        }
    }
}