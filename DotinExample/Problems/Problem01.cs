namespace DotinExample.Problems
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        public int AccountId { get; set; }
        public decimal Amount { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public DateTime Timestamp { get; set; }
    }

    public class AccountReport
    {
        public int AccountId { get; set; }
        public decimal Balance { get; set; }
        public int SuccessCount { get; set; }
        public int FailedCount { get; set; }
        public decimal MaxTransaction { get; set; }
        public decimal MinTransaction { get; set; }
        public List<Transaction> TodayTransactions { get; set; }
    }

    public class Report
    {
        public List<AccountReport> Accounts { get; set; }
        public DateTime ReportGeneratedAt { get; set; }
    }

    public interface ITransactionService
    {
        List<Transaction> LoadFile(string path);
        Report GenerateReport(List<Transaction> transactions);
    }

    public class TransactionService : ITransactionService
    {
        public Report GenerateReport(List<Transaction> transactions)
        {
            var today = DateTime.Today;

            var accountReport = transactions.GroupBy(t => t.AccountId)
            .Select(g => new AccountReport
            {
                AccountId = g.Key,
                Balance = g.Where(t => t.Status == "Success").Sum(t => t.Amount),
                SuccessCount = g.Count(t => t.Status == "Success"),
                FailedCount = g.Count(t => t.Status == "Failed"),
                MaxTransaction = g.Where(t => t.Status == "Success").Max(t => t.Amount),
                MinTransaction = g.Where(t => t.Status == "Success").Min(t => t.Amount),
                TodayTransactions = g.Where(t => t.Timestamp.Date == today && t.Status == "Success").ToList()
            }).ToList();

            return new Report
            {
                Accounts = accountReport,
                ReportGeneratedAt = DateTime.Now
            };
        }

        public List<Transaction> LoadFile(string path)
        {
            return File.ReadAllLines(path).Skip(1)
            .Select(line =>
            {
                var p = line.Split(',');
                return new Transaction
                {
                    TransactionId = int.Parse(p[0]),
                    AccountId = int.Parse(p[1]),
                    Amount = decimal.Parse(p[2]),
                    Type = p[3],
                    Status = p[4],
                    Timestamp = DateTime.Parse(p[5]),
                };
            }).ToList();
        }
    }

}