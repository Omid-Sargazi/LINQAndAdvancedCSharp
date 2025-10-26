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
                Console.WriteLine("request is done by Admin");
                return;
            }
            _handler?.HandleRequets(request);

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
                Console.WriteLine($"Leave is done By CEQ");
                return;
            }

            _handler?.HandleRequets(request);
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
                Console.WriteLine("leave is done By Manager");
                return;
            }
            _handler?.HandleRequets(request);
        }

        public IHandler SetNext(IHandler handler)
        {
            _handler = handler;
            return handler;
        }
    }

    public class HandleLeave
    {
        public static void Run()
        {
            AdminHanler admin = new AdminHanler();
            ManagerHandler manager = new ManagerHandler();
            CEOHandler cEOHandler = new CEOHandler();

            UserRequest request = new UserRequest { Leave = 2 };
            UserRequest request1 = new UserRequest { Leave = 8 };
            UserRequest request2 = new UserRequest { Leave = 7 };
            UserRequest request3 = new UserRequest { Leave = 4 };

            admin.SetNext(manager).SetNext(cEOHandler);

            admin.HandleRequets(request);
            admin.HandleRequets(request1);
            admin.HandleRequets(request2);
            admin.HandleRequets(request3);
            admin.HandleRequets(request);
        }
    }
}