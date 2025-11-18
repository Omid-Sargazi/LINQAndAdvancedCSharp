namespace BimeDotCom.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Role { get; set; }
        public string Region { get; set; }

    }

    public class ClaimRequest
    {
        public int Id { get; set; }
        public string PolicyNumber { get; set; }
        public decimal Amount { get; set; }
        public string Region { get; set; }
        public string Status { get; set; } = "Pending";
        public int UserId { get; set; }
    }

    public class LoginDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }


    public static class FakeDb
    {
          public static List<User> Users = new List<User>
        {
            new User {
                Id = 1,
                Username = "omid",
                PasswordHash = "1234",
                Role = "admin",
                Region = "north"
            },
            new User {
                Id = 2,
                Username = "reza",
                PasswordHash = "1234",
                Role = "agent",
                Region = "south"
            },
            new User {
                Id = 3,
                Username = "fatemeh",
                PasswordHash = "1234",
                Role = "viewer",
                Region = "north"
            }
        };

        public static List<ClaimRequest> Claims = new List<ClaimRequest>
        {
            new ClaimRequest
            {
                Id = 1,
                PolicyNumber = "PN-1001",
                Amount = 5000,
                Region = "north",
                Status = "Pending",
                UserId = 1
            },
            new ClaimRequest
            {
                Id = 2,
                PolicyNumber = "PN-2002",
                Amount = 15000,
                Region = "south",
                Status = "Approved",
                UserId = 2
            }
        };
    }
}