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

    public class Client
    {
        public static void Execute()
        {
            IPayment payment = new PayPalPayment();
            payment.Pay(32);
            payment = new CardPayment();
            payment.Pay(40);
        }
    }
}