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

    public class UserQuery : IRequest<User>
    {
        public int Id { get; set; }
    }

    public class UserQueryHandler : IRequestHandler<UserQuery, User>
    {
        public User Handle(UserQuery request)
        {
            return new User { Name = "Saeed", Age = 39, Id = 4 };
        }
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

   

    public class Mediator : IMediatore
    {
        // private Dictionary<Type, object> _handlers = new Dictionary<Type, object>();
        private readonly IServiceProvider _serviceProvider;
        
        // public Mediator(IServiceProvider serviceProvider)
        // {
        //     _serviceProvider = serviceProvider;
        //     _handlers[typeof(UserComamnd)] = new UserCommandHandler();
        //     _handlers[typeof(UserQuery)] = new UserQueryHandler();
        // }
        // public TResult Send<TRequest,TResult>(TRequest request) where TRequest:IRequest<TResult>
        // {
        //     var handler = (IRequestHandler<TRequest, TResult>)_handlers[typeof(TRequest)];
        //     return handler.Handle(request);
        // }

        public TResult Send<TRequest, TResult>(TRequest request) where TRequest : IRequest<TResult>
        {
            var handler = _serviceProvider.GetService<IRequestHandler<TRequest, TResult>>();

            if (handler == null)
            {
                throw new InvalidOperationException($"No Handlerd registered for{typeof(TRequest).Name}");
            }
            return handler.Handle(request);
        }
    }

 

    public class ClientMediator
    {
        public static void Run()
        {
            var mediator = new Mediator();

            var result = mediator.Send<UserComamnd, bool>(new UserComamnd { Id = 1 });

            Console.WriteLine(result + "  User Created;");
            var queryResult = mediator.Send<UserQuery, User>(new UserQuery { Id = 40 });
            Console.WriteLine($"Fetched User:{queryResult.Name},{queryResult.Age},{queryResult.Id}");
        }
    }
    
     public interface IMediatore
    {
        TResult Send<TRequest, TResult>(TRequest request) where TRequest : IRequest<TResult>;
    }
}