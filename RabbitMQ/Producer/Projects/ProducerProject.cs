using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;

namespace Producer.Projects
{
    public class ProducerProject
    {
        public static void Execute()
        {
            var factory = new ConnectionFactory()
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest"
            };
            using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();

           channel.QueueDeclare(queue: "hello",
                            durable: false,
                            exclusive: false,
                            autoDelete: false,
                            arguments: null);

       string message = "Hello RabbitMQ from C#!";
        var body = Encoding.UTF8.GetBytes(message);

        channel.BasicPublish(exchange: "",
                            routingKey: "hello",
                            basicProperties: null,
                            body: body);
        
        Console.WriteLine($" [x] Sent {message}");
                            
        }
    }
}