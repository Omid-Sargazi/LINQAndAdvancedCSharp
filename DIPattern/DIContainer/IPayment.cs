using Microsoft.AspNetCore.DataProtection;

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
            Console.WriteLine($"Paymented by Card{amount}");

        }
    }

    public class CryptoPayment : IPayment
    {
        public void Pay(decimal amount)
        {
            Console.WriteLine($"💰 Paid {amount} by Bitcoin {amount}");
        }
    }

    public class Client
    {
        private readonly IPayment _payment;
        private decimal _amount;
        public Client(IPayment payment)
        {
            _payment = payment;
            _amount = 50;
        }
        public void Execute()
        {
            _payment.Pay(_amount);
        }
    }

    // public static class ServiceCollectionExtensions
    // {
    //     public static IServiceCollection AddPaymentServices(this IServiceCollection services)
    //     {
    //         return services.AddTransient<IPayment, CryptoPayment>();

    //     }
    // }

    public class RunClient
    {
        public static void Run(decimal amount=100)
        {
            var services = new ServiceCollection();

            services.AddTransient<IPayment, PayPalPayment>();
            // services.AddTransient<IPayment, CardPayment>();
            services.AddTransient<Client>();
            // services.AddPaymentServices();

            var provider = services.BuildServiceProvider();
            var client = provider.GetRequiredService<Client>();
            client.Execute();

            // var client = ActivatorUtilities.CreateInstance<Client>(provider, 100m);


        }
    }
}