using System.Reflection;
using AlgorithemInCSharp.Patterns.BehavioralDesignPattern;

namespace AlgorithemInCSharp.Patterns
{
    public interface ICommand
    { }

    public interface ICommandHandler<T> where T : ICommand
    {
        object Handle(T command);
    }

    public class CreateUserCommand : ICommand
    {
        public string UserName { get; set; }
        public string Email { get; set; }
    }

    public class CreateUserCommandHandler : ICommandHandler<CreateUserCommand>
    {
        public object Handle(CreateUserCommand command)
        {
            Console.WriteLine($"Creating user: {command.UserName}, Email: {command.Email}");
            return $"User{command.UserName} creeated successfully.";
        }
    }


    public class ProcessOrderCommand : ICommand
    {
        public int OrderId { get; set; }
        public decimal Amount { get; set; }
    }

    public class ProcessOrderCommandHandler : ICommandHandler<ProcessOrderCommand>
    {
        public object Handle(ProcessOrderCommand command)
        {
            Console.WriteLine($"Processing order: {command.OrderId}, Amount: {command.Amount}");
            return $"order{command.OrderId} processed";
        }
    }

    public class SendEmailCommand : ICommand
    {
        public string To { get; set; }
        public string Subject { get; set; }
    }

    public class SendEmailCommandHandler : ICommandHandler<SendEmailCommand>
    {
        public object Handle(SendEmailCommand command)
        {
            Console.WriteLine($"Sending email to: {command.To}, Subject: {command.Subject}");
              return $"Email sent to {command.To}";
        }
    }

    public class CommandProcessor
    {
        public object Process<T>(T command) where T : ICommand
        {
            var commandType = command.GetType();

            var handleType = typeof(ICommandHandler<>).MakeGenericType(commandType);

            var handler = Assembly.GetExecutingAssembly()
            .GetTypes()
            .FirstOrDefault(t => handleType.IsAssignableFrom(t));
            if(handler==null)
            {
                throw new InvalidOperationException($"Handler for {commandType.Name} not found");
            }

            var handlerInstance = Activator.CreateInstance(handler);

            var result = handleType.GetMethod("Handle").Invoke(handlerInstance, new object[] { command });
            return result;
        }
    }


    public class CommandClient
    {
        public static void Run()
        {
            ICommand user1 = new CreateUserCommand { Email = "o@o.com", UserName = "omid" };
            ICommand process = new ProcessOrderCommand { Amount = 12m, OrderId = 1 };
            ICommand sendEmail = new SendEmailCommand { Subject = "Hiring", To = "S@s.com" };

            var proces = new CommandProcessor();
           try
           {
             var res = proces.Process(user1);
            Console.WriteLine(res);
           }
           catch (Exception ex)
           {

                throw new Exception($"{ex.Message}");
           }
        }
    }
}