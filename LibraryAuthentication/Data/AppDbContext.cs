using LibraryAuthentication.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryAuthentication.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions options):base(options){}
        public DbSet<Book> Books {get;set;}
        public DbSet<User> Users {get;set;}
        public DbSet<Review> Reviews {get;set;}


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if(!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("Data base=LibrrayDB");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }
    }
}