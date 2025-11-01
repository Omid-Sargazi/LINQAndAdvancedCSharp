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

    public class LibraryContext : DbContext
    {
        public DbSet<Author> Authors => Set<Author>();
        public DbSet<Book> Books => Set<Book>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=library.db");
        }
    }

    public class TrackingDemo
    {
        public static void Run()
        {
            using var db = new LibraryContext();
            var author = db.Authors.FirstOrDefault();
            if (author == null)
            {
                Console.WriteLine("No author found!");
                return;
            }

            Console.WriteLine($"Before Change:{db.Entry(author).State}");

            author.Name = "New Name";
            Console.WriteLine($"After Change:{db.Entry(author).State}");

            db.SaveChanges();
            Console.WriteLine("✔ Changes saved to database.");
            Console.WriteLine($"After SaveChanges: {db.Entry(author).State}");


            var author2 = new Author { Id = 1, Name = "New Name" };
            using var db2 = new LibraryContext();
            db.Authors.Update(author2);
            db.SaveChanges(); 
        }
    }
}