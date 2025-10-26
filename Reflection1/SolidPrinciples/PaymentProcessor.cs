namespace Reflection1.SolidPrinciples
{
    public class PaymentProcessor
    {
        public void ProcessPayment(string paymentType)
        {
            if (paymentType == "CreditCard")
            {
                Console.WriteLine("Processing credit card payment...");
            }
            else if (paymentType == "Paypal")
            {
                Console.WriteLine("Processing PayPal payment...");
            }
            else if (paymentType == "Cripto")
            {
                Console.WriteLine("Processing Crypto payment...");
            }
        }

        public void GenerateReport()
        {
            Console.WriteLine("Generating Payment Report...");
        }

        public void SaveToDatabase()
        {
            Console.WriteLine("Saving Payment to DB...");
        }
    }

    public class GoodPaymentProcessor
    {
        // private readonly PaymentService _paymentService;
        // private readonly ReportService _reportService;
        // public GoodPaymentProcessor()
        // {
        //     _paymentService = new PaymentService();
        //     _reportService = new ReportService();
        // }

        // public void Process()
        // {
            
        // }


        public void Process(IPaymnet paymnet)
        {
            paymnet.Pay();
        }
    }

    public class PaymentService
    {
        public void Pay(string paymentType)
        {
            if (paymentType == "CreditType")
                Console.WriteLine("Processing credit card payment...");
            else if(paymentType=="PayPal")
            {
                Console.WriteLine("Processing PayPal payment...");
            }
        }
    }

    public class ReportService
    {
        public void Generate() => Console.WriteLine("Generating Payment Report...");
    }

    public class DatabaseService
    {
        public void Save() => Console.WriteLine("Saving Payment to DB...");
    }

    public interface IPaymnet
    {
        void Pay();
    }

    public class CreditCardPayment : IPaymnet
    {
        
             public void Pay() => Console.WriteLine("Processing credit card payment...");
        
    }

    public class PayPalPayment : IPaymnet
    {
        public void Pay() => Console.WriteLine("Processing PayPal payment...");
    }

    public class CryptoPayment : IPaymnet
    {
        public void Pay() => Console.WriteLine("Processing Crypto payment...");
    }

}