// See https://aka.ms/new-console-template for more information
using System.Diagnostics.Metrics;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

Console.WriteLine("Hello, World!");


var factory = new ConnectionFactory()
{
    HostName = "localhost",
    UserName = "admin",
    Password = "admin",
};

using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

string mainQueue = "main-queue";
string dlqQueue = "main-queue-dlq";
string dlqExchange = "dlx";

channel.QueueDeclare(queue: dlqQueue,
    durable: false,
    exclusive: false,
    autoDelete: false,
    arguments: null
);

channel.ExchangeDeclare(exchange: dlqExchange, type: ExchangeType.Direct);
channel.QueueDeclare(queue: dlqQueue, durable: false, exclusive: false, autoDelete: false, arguments: null);
channel.QueueBind(queue: dlqQueue, exchange: dlqExchange, routingKey: dlqQueue);

var mainQueueArgs = new Dictionary<string, object>
{
    {"x-dead-letter-exchange",dlqExchange},
    {"x-dead-letter-routing-key",dlqQueue}
};

try { channel.QueueDelete(mainQueue); } catch { }



channel.QueueDeclare(queue: mainQueue,
    durable: false,
    exclusive: false,
    autoDelete: false,
    arguments: mainQueueArgs
);

Console.WriteLine(" [*] Waiting for messages...");



var consumer = new EventingBasicConsumer(channel);

consumer.Received += (model, ea) =>
{
    var body = ea.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);
    Console.WriteLine(" [x] Received: " + message);

    try
    {
        if (message.Contains("error"))
            throw new Exception("Simulated error");

        Console.WriteLine(" ✅ Processed OK");

        channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
    }
    catch (Exception ex)
    {

        Console.WriteLine(" ❌ Error: " + ex.Message);
        channel.BasicNack(deliveryTag: ea.DeliveryTag, multiple: false, requeue: false);
    }

};
    channel.BasicConsume(queue: mainQueue, autoAck: false, consumer: consumer);
    Console.ReadLine();