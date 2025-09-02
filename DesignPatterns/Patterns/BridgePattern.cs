namespace Patterns
{
    public interface IPaymentProcessor
    {
        void ProcessPayment(decimal amount);
    }

    public class BankAPIPaymentProcessor : IPaymentProcessor
    {
        public void ProcessPayment(decimal amount)
        {
            Console.WriteLine($"paied {amount} by bank is successfull");
        }
    }

    public class ThirdPartyPaymentProcessor : IPaymentProcessor
    {
        public void ProcessPayment(decimal amount)
        {
            Console.WriteLine($"paied {amount} by theird person is successfull");

        }
    }

    public class TestPaymentProcessor : IPaymentProcessor
    {
        public void ProcessPayment(decimal amount)
        {
            Console.WriteLine($"paied {amount} by test simulation is successfull");

        }
    }

    public abstract class Payment
    {
        protected readonly IPaymentProcessor _paymentProcessor;
        public Payment(IPaymentProcessor paymentProcessor)
        {
            _paymentProcessor = paymentProcessor;
        }

        public abstract void Pay(decimal amount);
    }

    public class CreditCardPayment : Payment
    {
        public CreditCardPayment(IPaymentProcessor paymentProcessor) : base(paymentProcessor)
        {
        }

        public override void Pay(decimal amount)
        {
            Console.WriteLine("Processing payment by credit card...");
            _paymentProcessor.ProcessPayment(amount);
        }
    }

    public class PayPalPayment : Payment
    {
        public PayPalPayment(IPaymentProcessor paymentProcessor) : base(paymentProcessor)
        {
        }

        public override void Pay(decimal amount)
        {
            Console.WriteLine("process by paypal...");
            _paymentProcessor.ProcessPayment(amount);
        }
    }

    public class CryptoPayment : Payment
    {
        public CryptoPayment(IPaymentProcessor paymentProcessor) : base(paymentProcessor)
        {
        }

        public override void Pay(decimal amount)
        {
            Console.WriteLine("Process payment by crypto...");
            _paymentProcessor.ProcessPayment(amount);
        }
    }
}