using System.ComponentModel;

namespace Patterns.PaymentNotification
{
    public interface INotification
    {
        void SendNotify(string message);
    }

    public class SMS : INotification
    {
        public void SendNotify(string message)
        {
            Console.WriteLine($"Notified to SMS Console");
        }
    }

    public class Email : INotification
    {
        public void SendNotify(string message)
        {
            Console.WriteLine($"Notified to Email {message} Console");
        }
    }

    public class Logging : INotification
    {
        public void SendNotify(string message)
        {
            Console.WriteLine($"Notified to Logging {message} Console");

        }
    }


    public delegate void Notify(string message);
    public class ManageNotification
    {
        public event Notify PaymentCompleted;

        public void Execute(string message)
        {
           
            PaymentCompleted?.Invoke(message);
        }
    }

    public class PaymentService
    {
        private bool _subscribe = false;   

        private readonly List<INotification> _notifications;
        ManageNotification manageNotification = new ManageNotification();
        public PaymentService(params INotification[] notifications)
        {
            _notifications = new List<INotification>(notifications);
        }

        public void AllNotify(string message)
        {
            if (!_subscribe)
            {
                foreach (var notification in _notifications)
                {
                    manageNotification.PaymentCompleted += notification.SendNotify;
                }
                _subscribe = true;
            }


            manageNotification.Execute(message);
        }
        
        public void ClearSubscriptions()
        {
            foreach (var notification in _notifications)
            {
                manageNotification.PaymentCompleted -= notification.SendNotify;
            }
            _subscribe = false;
        }
    }

}