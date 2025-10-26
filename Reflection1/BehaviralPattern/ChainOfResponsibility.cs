namespace Reflection1.BehaviralPattern
{
    public class UserRequest
    {
        public int Leave {get;set;}
    }
    public interface IHandler
    {
        void HandleRequets(UserRequest request);
        IHandler SetNext(IHandler handler);
    }

    public class AdminHanler : IHandler
    {
        private IHandler _handler;
        public void HandleRequets(UserRequest request)
        {
            if (request.Leave < 3)
            {
                Console.WriteLine("request is done");
            }
            _handler.HandleRequets(request);

        }

        public IHandler SetNext(IHandler handler)
        {
            _handler = handler;
            return handler;
        }
    }

    public class CEOHandler : IHandler
    {
        private IHandler _handler;
        public void HandleRequets(UserRequest request)
        {
            if (request.Leave > 3 && request.Leave < 6)
            {
                Console.WriteLine($"Leave is done");
            }

            _handler.HandleRequets(request);
        }

        public IHandler SetNext(IHandler handler)
        {
            _handler = handler;
            return handler;
        }
    }

    public class ManagerHandler : IHandler
    {
        private IHandler _handler;
        public void HandleRequets(UserRequest request)
        {
            if (request.Leave > 7)
            {
                Console.WriteLine("leave is done.");
            }
            _handler.HandleRequets(request);
        }

        public IHandler SetNext(IHandler handler)
        {
            _handler = handler;
            return handler;
        }
    }
}