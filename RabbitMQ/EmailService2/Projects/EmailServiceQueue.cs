using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace EmailService2.Projects
{
    public class EmailServiceQueue
    {
        public static void Execute()
        {
            Console.WriteLine("📧 Email Service (Persistent) Started...");

            var factory = new ConnectionFactory() { HostName = "localhost" };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.ExchangeDeclare("news_exchange_persistent", ExchangeType.Fanout, durable: true);



            channel.QueueDeclare(
            queue: "email_queue",  // 📌 نام ثابت (نه تصادفی)
            durable: true,         // 📌 صف پایدار
            exclusive: false,
            autoDelete: false
        );

            channel.QueueBind("email_queue", "news_exchange", "");

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
    {
        var body = ea.Body.ToArray();
        var message = Encoding.UTF8.GetString(body);

        try
        {
            // 📌 شبیه‌سازی ارسال ایمیل (ممکن است fail شود)
            Console.WriteLine($"📧 TRYING TO SEND EMAIL: {message}");

            // شبیه‌سازی خطای تصادفی
            if (new Random().Next(0, 3) == 0)  // 📌 33% احتمال خطا
            {
                throw new Exception("SMTP server unavailable!");
            }

            Thread.Sleep(1000);
            Console.WriteLine($"✅ Email sent successfully!");

            // 📌 تأیید موفقیت‌آمیز
            channel.BasicAck(ea.DeliveryTag, false);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ FAILED: {ex.Message}");

            // 📌 عدم تأیید و بازگشت به صف
            channel.BasicNack(
                deliveryTag: ea.DeliveryTag,
                multiple: false,
                requeue: true  // 📌 پیام برای مصرف مجدد بازمی‌گردد
            );
        }
    };

            channel.BasicConsume(
                queue: "email_queue",
                autoAck: false,  // 📌 تأیید دستی
                consumer: consumer
            );

            Console.WriteLine("⏳ Email service waiting (with persistence)...");
            Console.ReadLine();
        }
    }

}