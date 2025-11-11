namespace LinqExamples.Problems5
{
    public class BigData
    {
        public static void Execute()
        {

        }
    }

   

    public class BankTransaction
{
    public long Id { get; set; }
    public int AccountId { get; set; }
    public decimal Amount { get; set; }
    public DateTime TransactionDate { get; set; }
    public string TransactionType { get; set; } // "Deposit", "Withdrawal", "Transfer"
    public string Status { get; set; } // "Completed", "Pending", "Failed"
    public int BranchId { get; set; }
}

public class Customer
{
    public int AccountId { get; set; }
    public string CustomerName { get; set; }
    public string CustomerType { get; set; } // "Individual", "Business", "VIP"
    public DateTime AccountOpenDate { get; set; }
    public decimal CurrentBalance { get; set; }
}
}