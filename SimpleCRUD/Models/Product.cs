using Microsoft.EntityFrameworkCore;

namespace SimpleCRUD.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }

    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions options):base(options){}
        public DbSet<Product> Products { get; set; }
    }
}