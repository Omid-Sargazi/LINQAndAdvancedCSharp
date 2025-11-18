namespace BimeDotCom.Models
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public string Role { get; set; }
        public string Region { get; set; }

    }

    public class ClimRequest
    {
        public int Id { get; set; }
        public string PolicyNumber { get; set; } 
        public decimal Amount { get; set; }
        public string Region { get; set; }
        public string Status { get; set; } = "Pending";
         public int UserId { get; set; }
    }
}