using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace EmailService.Projects
{
    public class EmailConsumer
    {
        public static void Execute()
        {
            Console.WriteLine("📧 Email Service Started...");

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


            var queueName = channel.QueueDeclare().QueueName;

            channel.QueueBind(
            queue: queueName,
            exchange: "news_exchange",
            routingKey: ""
            );


            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                
                // 📌 شبیه‌سازی ارسال ایمیل
                Console.WriteLine($"📧 SENDING EMAIL: {message}");
                Thread.Sleep(1000);
                Console.WriteLine($"✅ Email sent successfully!");
            };

            channel.BasicConsume(
                queue: queueName,
                autoAck: true,
                consumer: consumer
            );

            Console.WriteLine("⏳ Email service waiting for news...");
            Console.ReadLine();

        }
    }
}