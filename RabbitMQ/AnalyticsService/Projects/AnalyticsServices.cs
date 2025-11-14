using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace AnalyticsService.Projects
{
    public class AnalyticsServices
    {
        public static void Execute()
        {
            Console.WriteLine("📈 Analytics Service Started...");

            var factory = new ConnectionFactory() { HostName = "localhost" };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.ExchangeDeclare("news_exchange", ExchangeType.Fanout);

            var queueName = channel.QueueDeclare().QueueName;
            channel.QueueBind(queueName, "news_exchange", "");

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                
                // 📌 شبیه‌سازی آنالیز داده
                Console.WriteLine($"📈 ANALYTICS: Processing news data...");
                Thread.Sleep(500);
                Console.WriteLine($"📈 News analyzed: {message.Split(':')[1]}");
            };

            channel.BasicConsume(queueName, autoAck: true, consumer: consumer);

            Console.WriteLine("⏳ Analytics service waiting for news...");
            Console.ReadLine();
        }
    }
}