namespace DIPattern.DIContainer
{
    public interface IPayment
    {
        void Pay(decimal amount);
    }

    public class PayPalPayment : IPayment
    {
        public void Pay(decimal amount)
        {
            Console.WriteLine($"Paymented by Paypal {amount}");
        }
    }

    public class CardPayment : IPayment
    {
        public void Pay(decimal amount)
        {
            Console.WriteLine($"Paymented by Card");

        }
    }

    public class CryptoPayment : IPayment
    {
        public void Pay(decimal amount)
        {
            Console.WriteLine($"💰 Paid {amount} by Bitcoin");
        }
    }

    public class Client
    {
        private readonly IPayment _payment;
        private decimal _amount;
        public Client(IPayment payment, decimal amount)
        {
            _payment = payment;
            _amount = amount;
        }
        public void Execute()
        {
            _payment.Pay(_amount);
        }
    }
}