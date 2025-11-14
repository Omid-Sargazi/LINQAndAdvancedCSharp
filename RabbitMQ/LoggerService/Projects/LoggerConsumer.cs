using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace LoggerService.Projects
{
    public class LoggerConsumer
    {
        public static void Execute()
        {
            Console.WriteLine("📝 Logger Service Started...");

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
                
                // 📌 شبیه‌سازی ثبت در فایل لاگ
                var logEntry = $"[LOG] {DateTime.Now:HH:mm:ss} - {message}";
                Console.WriteLine(logEntry);
                
                // 📌 ذخیره در فایل (اختیاری)
                File.AppendAllText("news.log", logEntry + Environment.NewLine);
            };

            channel.BasicConsume(queueName, autoAck: true, consumer: consumer);

            Console.WriteLine("⏳ Logger service waiting for news...");
            Console.ReadLine();
        }
    }
}