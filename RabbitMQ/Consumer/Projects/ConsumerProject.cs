using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Consumer.Projects
{
    public class ConsumerProject
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

            channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

        var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);


                Console.WriteLine($"🔨 Processing: {message}");

                Thread.Sleep(2000);

                Console.WriteLine($"✅ Completed: {message}");

                channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
            };
        


        channel.BasicConsume(queue: "task_queue",
                           autoAck: false,
                           consumer: consumer);

        Console.WriteLine("⏳ Worker ready! Press [enter] to exit.");
        Console.ReadLine();
        }
    }
}