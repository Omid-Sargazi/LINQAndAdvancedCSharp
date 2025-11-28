using Microsoft.AspNetCore.Identity;

namespace CompleteAuth.Models
{
    public class ApplicationUser:IdentityUser
    {
        public string FullName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string ProfilePicture { get; set; }
        public string Country { get; set; }
        public string Region { get; set; } 
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}