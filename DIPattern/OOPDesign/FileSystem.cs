namespace DIPattern.OOPDesign
{
    public interface IFileSystem : IFileDeletable, IFileOpenable, IFileSavable
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
                TransactionId = "Pay" + Guid.NewGuid
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
                ErrorMessage = "401",
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
    //////////////////////////////////////////////////////
    /// 
    /// 


    public interface IReportingSystem
    {
        void MakeReport(string context);
    }
    public class PdfReport : IReportingSystem
    {
        public void MakeReport(string context)
        {
            Console.WriteLine("PDF Report is Created");
        }
    }

    public class ExeclReport : IReportingSystem
    {
        public void MakeReport(string context)
        {
            Console.WriteLine("Excel Report is Created");
        }
    }
    public class HTMLReport : IReportingSystem
    {
        public void MakeReport(string context)
        {
            Console.WriteLine("HTML Report is Created");
        }
    }





    public abstract class ReportingSystem
    {
        protected IReportingSystem _reportingSystem;
        public ReportingSystem(IReportingSystem reportingSystem)
        {
            _reportingSystem = reportingSystem;
        }

        public abstract void Send(string context);
    }
    public class FaxSendReport : ReportingSystem
    {
        public FaxSendReport(IReportingSystem reportingSystem) : base(reportingSystem)
        {
        }

        public override void Send(string context)
        {
            Console.Write("Sending via Fax - ");
            _reportingSystem.MakeReport(context);

        }
    }

    public class EmailSendReport : ReportingSystem
    {
        public EmailSendReport(IReportingSystem reportingSystem) : base(reportingSystem)
        {
        }

        public override void Send(string context)
        {
            Console.Write("Sending via Email - ");
            _reportingSystem.MakeReport(context);
        }
    }

    public class PrinterSendReport : ReportingSystem
    {
        public PrinterSendReport(IReportingSystem reportingSystem) : base(reportingSystem)
        {
        }

        public override void Send(string context)
        {
            throw new NotImplementedException();
        }
    }


    public class ClientBridgePattern
    {
        public static void Execute()
        {
            IReportingSystem makeReport = new HTMLReport();
            makeReport.MakeReport("A report about last task");

            EmailSendReport emailSend = new EmailSendReport(makeReport);
            emailSend.Send("Omid");
        }
    }



    public class FileDestination : ILogDestination
    {
        public LogLevel MinimumLevel { get; }

        public FileDestination(LogLevel minLevel = LogLevel.Info)
        {
            MinimumLevel = minLevel;
        }

        public void Write(LogEntry logEntry)
        {
            System.Console.WriteLine($"[File]{logEntry.Timestamp:o}[{logEntry.Level}]{logEntry.Message}");
        }
    }

    public class DatabaseDestination : ILogDestination
    {
        public LogLevel MinimumLevel { get; }

        public DatabaseDestination(LogLevel logLevel = LogLevel.Error)
        {
            MinimumLevel = logLevel;
        }
        public void Write(LogEntry logEntry)
        {
            System.Console.WriteLine($"[File]{logEntry.Timestamp:o}[{logEntry.Level}]{logEntry.Message}");

        }
    }

    public class ConsoleDestination : ILogDestination
    {
        public LogLevel MinimumLevel { get; }

        public ConsoleDestination(LogLevel logLevel = LogLevel.Wrning)
        {
            MinimumLevel = logLevel;
        }

        public void Write(LogEntry logEntry)
        {
            System.Console.WriteLine($"[File]{logEntry.Timestamp:o}[{logEntry.Level}]{logEntry.Message}");

        }
    }

    public enum LogLevel

    {
        Info = 0,
        Wrning = 1,
        Error = 2,
    };

    public class LogEntry
    {
        public LogLevel Level { get; set; }
        public string Message { get; set; }
        public DateTime Timestamp { get; set; }

        public LogEntry(LogLevel level, string message)
        {
            Level = level;
            Message = message;
            Timestamp = DateTime.UtcNow;
        }
    }

    public interface ILogDestination
    {
        LogLevel MinimumLevel { get; }
        void Write(LogEntry logEntry);
    }

    public class LoggingSystem
    {
        private readonly List<ILogDestination> _destinations = new List<ILogDestination>();

        private readonly object _lock = new object();

        public void AddDistination(ILogDestination logDestination)
        {
            if (logDestination == null) throw new ArgumentNullException(nameof(logDestination));
            lock (_lock)

                _destinations.Add(logDestination);
        }

        public void RemoveDestination(ILogDestination logDestination)
        {
            if (logDestination == null) return;
        lock (_lock)
        {
            _destinations.Remove(logDestination);
        }
        }
    }


}