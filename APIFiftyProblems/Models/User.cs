namespace APIFiftyProblems.Models
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public string Role { get; set; }
        public string Region { get; set; }

    }

    public class Region
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<User> Users { get; set; }
    }

    public record UserLoginDto(string Username, string Password);
}