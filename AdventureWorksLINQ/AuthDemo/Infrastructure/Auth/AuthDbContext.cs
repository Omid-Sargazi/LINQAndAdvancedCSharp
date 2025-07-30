using AdventureWorksLINQ.AuthDemo.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AdventureWorksLINQ.AuthDemo.Infrastructure.Auth
{
    public class AuthDbContext : DbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options) { }
        public DbSet<User> Users => Set<User>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();
        }
    }
}