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

            channel.QueueDeclare(queue: "task_queue",
                             durable: true,
                             exclusive: false,
                             autoDelete: false,
                             arguments: null);

            var tasks = new[]
            {
            "Send email",
            "Process image",
            "Generate report",
            "Backup database",
            "Clean cache"
        };
            foreach (var task in tasks)
            {
                var message = $"{task} - {DateTime.Now}";
                var body = Encoding.UTF8.GetBytes(message);
                var properties = channel.CreateBasicProperties();
                properties.Persistent = true;


                channel.BasicPublish(exchange: "",
                routingKey: "task_queue",
                basicProperties: properties,
                body: body);
                Console.WriteLine($"✅ Sent task: {task}");
            }
            
            Console.WriteLine("🎯 All tasks dispatched!");
                        
        }
    }
}