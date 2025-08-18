namespace MonolithOrderSystem.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required, MinLength(3)]
        public string UserName { get; set; } = "";

        [Required, EmailAddress]
        public string Email { get; set; } = "";
    }
    

    public record CreateUserRequest(
    [Required, MinLength(3)] string UserName,
    [Required, EmailAddress] string Email
);
}