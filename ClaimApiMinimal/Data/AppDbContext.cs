using ClaimApiMinimal.Models;
using Microsoft.EntityFrameworkCore;

namespace ClaimApiMinimal.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions options):base(options){}

        public DbSet<Product> Products {get;set;}
    }
}