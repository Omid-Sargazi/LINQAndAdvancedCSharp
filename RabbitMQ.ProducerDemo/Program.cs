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

channel.QueueDeclare(queue: "my-queue",
                     durable: false,
                     exclusive: false,
                     autoDelete: false,
                     arguments: null);

string message = "Hello RabbitMQ!";
var body = Encoding.UTF8.GetBytes(message);


channel.BasicPublish(exchange: "",
                     routingKey: "my-queue",
                     basicProperties: null,
                     body: body);
Console.WriteLine($"[x] Sent: {message}");