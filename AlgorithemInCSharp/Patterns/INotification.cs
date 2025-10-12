using System.Reflection;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace AlgorithemInCSharp.Patterns
{
    public interface INotification
    {
        public string Message { get; set; }
    }

    public interface INotificationSender<T> where T : INotification
    {
        Task<bool> SendAsync(T notification);
    }

    public class EmailNotification : INotification
    {
        public string Message { get; set; }
        public string To { get; set; }
        public string Subject { get; set; }
    }

    public class EmailNotificationSender : INotificationSender<EmailNotification>
    {
        public async Task<bool> SendAsync(EmailNotification notification)
        {
            Console.WriteLine($"sending email to {notification.To} subject:{notification.Subject}");
            await Task.Delay(1000);
            return true;
        }
    }

    public class SMSNotification : INotification
    {
        public string Message { get; set; }
        public string PhoneNumber { get; set; }
    }

    public class SMSNotificationSender : INotificationSender<SMSNotification>
    {
        public Task<bool> SendAsync(SMSNotification notification)
        {
            throw new NotImplementedException();
        }
    }

    public class PushNotification : INotification
    {
        public string Message { get; set; }
        public string DeviceToken { get; set; }
    }

    public class PushNotificationSender : INotificationSender<PushNotification>
    {
        public Task<bool> SendAsync(PushNotification notification)
        {
            throw new NotImplementedException();
        }
    }

    public class NotificatioService
    {
        public async Task<bool> SendNotificationAsync<T>(T notification) where T : INotification
        {
            var notificationType = notification.GetType();
            var notificationSender = typeof(INotificationSender<>).MakeGenericType(notificationType);

            var sender = Assembly.GetExecutingAssembly()
            .GetTypes()
            .FirstOrDefault(t => notificationSender.IsAssignableFrom(t));

            if (sender == null)
            {
                throw new InvalidOperationException($"handler for {notificationType.Name} not found");
            }

            var senderInsatnce = Activator.CreateInstance(sender);

            var result = notificationSender.GetMethod("SendAsync").Invoke(senderInsatnce, new object[] { notification });

            var task = (Task<bool>)result;
            return await task;
        }
    }

    public class NotificationClient
    {
        public static async Task Run()
        {
            var service = new NotificatioService();

            var email = new EmailNotification
            {
                Message = "Hello",
                To = "test@test.com",
                Subject = "Test"
            };
            var res = await service.SendNotificationAsync(email);
           Console.WriteLine($"Available senders: {string.Join(", ", res)}");
        }
    }
}