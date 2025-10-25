namespace LinqExamples.Problem1
{
    public class UserRequest
    {
        public string Title { get; set; }
        public string Context { get; set; }
    }
    public interface IHandler
    {
        void HandleRequest(UserRequest request);
        IHandler SetNext(IHandler nextHandler);
    }

    public class AdminHandler : IHandler
    {
        private IHandler _handler;

       
        public void HandleRequest(UserRequest request)
        {
            if (request.Title == "Admin")
            {
                Console.WriteLine($"Admin Handled");
                return;
            }
            _handler?.HandleRequest(request);
        }

        public IHandler SetNext(IHandler nextHandler)
        {
            _handler = nextHandler;
            return _handler;
        }
    }

    public class ManagerHandler : IHandler
    {
        private IHandler _handler;
        
        public void HandleRequest(UserRequest request)
        {
            if (request.Title == "Manager")
            {
                Console.WriteLine("Request handled by manager");
                return;
            }
            _handler?.HandleRequest(request);
        }

        public IHandler SetNext(IHandler nextHandler)
        {
            _handler = nextHandler;
            return _handler;
        }
    }

    public class CEOHandler : IHandler
    {
        private IHandler _handler;
       
        public void HandleRequest(UserRequest request)
        {
            if (request.Title == "CEO")
    {
                Console.WriteLine("CEO handled the request.");
                return;
    }

            Console.WriteLine("No handler could process the request.");
        }

        public IHandler SetNext(IHandler nextHandler)
        {
            _handler = nextHandler;
            return _handler;
        }
    }
}

