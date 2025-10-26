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
}