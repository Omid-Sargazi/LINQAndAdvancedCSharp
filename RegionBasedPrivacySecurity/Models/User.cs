namespace RegionBasedPrivacySecurity.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public string Region { get; set; }
        public string passwordHash { get; set; }
    }


    public class Region
    {
        public int RegionId { get; set; }
        public string Name { get; set; }
        public ICollection<User> Users { get; set; }
    }

    public record LoginRequest(string UserName, string Password);
}