using System.Text;
using RabbitMQ.Client;

namespace NewsPublisher.Projects
{
    public class MainPublisher
    {
        public static void Execute()
        {
            Console.WriteLine("📢 Starting News Publisher...");

            var factory = new ConnectionFactory()
            {
                 HostName = "localhost",
                    UserName = "guest",
                    Password = "guest"
            };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.ExchangeDeclare(
                exchange: "news_exchange",
                type: ExchangeType.Fanout
            );

            Console.WriteLine("Enter news message (or 'exit' to quit):");

            while(true)
            {
                var input = Console.ReadLine();
                if (input?.ToLower() == "exit") break;

                var message = $"📰 NEWS: {input} - {DateTime.Now}";
                var body = Encoding.UTF8.GetBytes(message);


                channel.BasicPublish(
                exchange: "news_exchange",
                routingKey: "",
                basicProperties: null,
                body = body
                );
                Console.WriteLine($"✅ Broadcasted: {message}");
            }
            
        }
    }
}