using Microsoft.EntityFrameworkCore;

namespace EfCoreBasics.Models
{
    public class Author
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<Book> Books { get; set; } = new();
    }

    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public int AuthorId { get; set; }
        public Author Author { get; set; } = null!;
    }

    public class LibraryContext: DbContext
    {
        public DbSet<Author> Authors=>Set<Author>();
        public DbSet<Book> Books => Set<Book>();
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=library.db");
        }
    }
}