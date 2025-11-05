namespace DIPattern.OOPDesign
{
    public interface IFileSystem:IFileDeletable,IFileOpenable,IFileSavable
    {
        
    }

    public class AudioFile : IFileSystem
    {
        public void Delete()
        {
            throw new NotImplementedException();
        }

        public void Open()
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }
    }

    public class ImageFile : IFileSystem
    {
        public void Delete()
        {
            throw new NotImplementedException();
        }

        public void Open()
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }
    }

    public class ReadOnlyConfigFile : IFileOpenable, IFileDeletable
    {
        public void Delete()
        {
            throw new NotImplementedException();
        }

        public void Open()
        {
            throw new NotImplementedException();
        }
    }

    public interface IFileOpenable
    {
        void Open();
    }

    public interface IFileSavable
    {
        void Save();
    }

    public interface IFileDeletable
    {
        void Delete();
    }


    public interface IPayamnt
    {
        PaymentResult ProcessPayment(decimal amount);
    }

    public class PayPal : IPayamnt
    {
        public PaymentResult ProcessPayment(decimal amount)
        {
            Console.WriteLine($"Processing ${amount} via PayPal...");
            return new PaymentResult
            {
                IsSuccess = true,
                TransactionId="Pay"+Guid.NewGuid
            };
        }
    }

    public class Stripe : IPayamnt
    {
        public PaymentResult ProcessPayment(decimal amount)
        {
            Console.WriteLine($"Processing ${amount} via Stripe...");

            return new PaymentResult
            {
                IsSuccess = false,
ErrorMessage="401",
            };
        }
    }

    public class BankTransfer : IPayamnt
    {
        public PaymentResult ProcessPayment(decimal amount)
        {
            throw new NotImplementedException();
        }
    }

    public class PaymentProcessingSystem
    {
        private readonly IPayamnt _payamnt;
        public PaymentProcessingSystem(IPayamnt payamnt)
        {
            _payamnt = payamnt;
        }

        public void Process(decimal amount)
        {
            _payamnt.ProcessPayment(amount);
        }
    }

    public class PaymentResult
    {
        public bool IsSuccess { get; set; }
        public string TransactionId { get; set; }
        public string ErrorMessage { get; set; }
    }
}