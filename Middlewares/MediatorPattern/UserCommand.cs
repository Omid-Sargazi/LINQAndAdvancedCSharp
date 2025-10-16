using System.Security.Cryptography.X509Certificates;

namespace Middlewares.MediatorPattern
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
    }

    public interface IRequest<TResult>
    {

    }

    public interface IQuery<TQuery>
    {

    }
    
    public interface IQueryHandler<TResult, TQuery> where TQuery : IQuery<TQuery>
    {
        TResult Handle(TQuery query);
    }

    public class UserQuery
    {

    }

    public class UserQueryHandler
    {
        
    }

    public interface IRequestHandler<TRequest,TResult> where TRequest : IRequest<TResult>
    {
        TResult Handle(TRequest request);
    }

    public class UserComamnd : IRequest<bool>
    {
        public int Id { get; set; }
    }

    public class UserCommandHandler : IRequestHandler<UserComamnd, bool>
    {
        public bool Handle(UserComamnd request)
        {
            var user = new User { Name = "omid", Age = 42, Id = request.Id };
            return true;
        }
    }

    public class Mediator
    {
        private Dictionary<Type, object> _handlers = new Dictionary<Type, object>();
        
        public Mediator()
        {
            _handlers[typeof(UserComamnd)] = new UserCommandHandler();
        }
        public TResult Send<TRequest,TResult>(TRequest request) where TRequest:IRequest<TResult>
        {
            var handler = (IRequestHandler<TRequest, TResult>)_handlers[typeof(TRequest)];
            return handler.Handle(request);
        }
    }

    public class ClientMediator
    {
        public static void Run()
        {
            var mediator = new Mediator();

            var result = mediator.Send<UserComamnd, bool>(new UserComamnd { Id = 1 });
            Console.WriteLine(result+"  User Created;");
        }
    }
}