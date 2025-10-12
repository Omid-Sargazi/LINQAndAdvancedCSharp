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

}