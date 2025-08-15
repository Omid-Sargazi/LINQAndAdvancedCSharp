using Microsoft.EntityFrameworkCore;

namespace SecureEfCoreDemo.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public byte[] PasswordHash { get; set; } = null!;
        public byte[] PasswordSalt { get; set; } = null!;
        public string NationalId { get; set; } = null!;
    }

    public class Record
    {
        public int Id { get; set; }
        public string Data { get; set; } = null!;
        public int UserId { get; set; }
        public User User { get; set; } = null!;
    }

    public class AppDbContext : DbContext
    {
        private readonly int _currentUserId;
        public AppDbContext(DbContextOptions<AppDbContext> options, IHttpContextAccessor accessor)
       : base(options)
        {
            // فرض: کاربر لاگین شده، UserId توی Claims ذخیره شده
            _currentUserId = int.TryParse(accessor.HttpContext?.User?.FindFirst("UserId")?.Value, out var id) ? id : 0;
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Record> Records { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Record>().HasQueryFilter(r => r.UserId == _currentUserId);
        }
    }

    public class PasswordHasher
    {
        public static void CreatePasswordHash(string password, out byte[] hash, out byte[] salt)
    {
        using var hmac = new System.Security.Cryptography.HMACSHA512();
        salt = hmac.Key;
        hash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
    }

    public static bool VerifyPasswordHash(string password, byte[] hash, byte[] salt)
    {
        using var hmac = new System.Security.Cryptography.HMACSHA512(salt);
        var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        return computedHash.SequenceEqual(hash);
    }
    }
}