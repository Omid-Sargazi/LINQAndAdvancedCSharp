using System.Text;
using RabbitMQ.Client;
// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

var factory = new ConnectionFactory()
{
    HostName = "localhost",
    UserName = "admin",
    Password = "admin"
};

using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

var mainQueueArgs = new Dictionary<string, object>
{
    {"x-dead-letter-exchange", "dlx"},
    {"x-dead-letter-routing-key", "main-queue-dlq"}
};

channel.QueueDeclare(queue: "main-queue",
                     durable: false,
                     exclusive: false,
                     autoDelete: false,
                     arguments: mainQueueArgs);

// string message = "Hello RabbitMQ!";
string message = "This message contains error";

var body = Encoding.UTF8.GetBytes(message);


channel.BasicPublish(exchange: "",
                     routingKey: "main-queue",
                     basicProperties: null,
                     body: body);
Console.WriteLine($"[x] Sent: {message}");