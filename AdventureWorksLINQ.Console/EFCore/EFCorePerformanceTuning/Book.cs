using System.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace AdventureWorksLINQ.Console.EFCore.EFCorePerformanceTuning
{
    public class Book
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public List<Review> Reviews { get; set; } = new List<Review>();
    }

    public class Review
    {
        public int ReviewId { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
        public int Rating { get; set; } // 1 تا 5
        public string Comment { get; set; }
    }

    public class ContextOfLibrary : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Review> Reviews { get; set; }

        public ContextOfLibrary(DbContextOptions<ContextOfLibrary> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Review>()
                .HasOne(r => r.Book)
                .WithMany(b => b.Reviews)
                .HasForeignKey(r => r.BookId);
        }

    }


    public class RunContextOfLibrary
    {
        public static void Run()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ContextOfLibrary>();
            optionsBuilder.UseInMemoryDatabase("LibraryDB");

            using (var context = new ContextOfLibrary(optionsBuilder.Options))
            {
                // تولید داده‌های تستی
                SeedData(context);

                // اندازه‌گیری زمان کوئری‌ها
                var stopwatch = new Stopwatch();

                stopwatch.Start();
                var booksWithInclude = context.Books
                    .Include(b => b.Reviews)
                        .ToList();
                stopwatch.Stop();
                System.Console.WriteLine($"Query with Include: {stopwatch.ElapsedTicks} ticks");
                PrintBooks(booksWithInclude);


                // کوئری 2: AsSplitQuery
                stopwatch.Restart();
                var booksWithSplitQuery = context.Books
                    .Include(b => b.Reviews)
                    .AsSplitQuery()
                    .ToList();
                stopwatch.Stop();
                System.Console.WriteLine($"Query with AsSplitQuery: {stopwatch.ElapsedTicks} ticks");
                PrintBooks(booksWithSplitQuery);

                // کوئری 3: AsNoTracking
                stopwatch.Restart();
                var booksWithNoTracking = context.Books
                    .Include(b => b.Reviews)
                    .AsNoTracking()
                    .ToList();
                stopwatch.Stop();
                System.Console.WriteLine($"Query with AsNoTracking: {stopwatch.ElapsedTicks} ticks");
                PrintBooks(booksWithNoTracking);
            }
            

        }



        static void SeedData(ContextOfLibrary context)
        {
            var random = new Random();
            var books = new List<Book>();
            var authors = new[] { "Author A", "Author B", "Author C", "Author D", "Author E" };
            var titles = new[] { "Book One", "Book Two", "Book Three", "Book Four", "Book Five",
                             "Book Six", "Book Seven", "Book Eight", "Book Nine", "Book Ten" };

            // تولید 10 کتاب
            for (int i = 0; i < 10; i++)
            {
                books.Add(new Book
                {
                    Title = titles[i],
                    Author = authors[random.Next(authors.Length)]
                });
            }
            context.Books.AddRange(books);
            context.SaveChanges();

            // تولید 100 ریویو
            var comments = new[] { "Great read!", "Very informative.", "Could be better.", "Loved it!", "Not my favorite." };
            var reviews = new List<Review>();
            foreach (var book in books)
            {
                // هر کتاب بین 5 تا 15 ریویو دارد
                int reviewCount = random.Next(5, 16);
                for (int i = 0; i < reviewCount; i++)
                {
                    reviews.Add(new Review
                    {
                        BookId = book.BookId,
                        Rating = random.Next(1, 6),
                        Comment = comments[random.Next(comments.Length)]
                    });
                }
            }
            context.Reviews.AddRange(reviews);
            context.SaveChanges();
        }
    
    
    static void PrintBooks(List<Book> books)
        {
            System.Console.WriteLine("\nBooks and Review Counts:");
            System.Console.WriteLine("-----------------------");
            foreach (var book in books)
            {
                System.Console.WriteLine($"Book: {book.Title}, Author: {book.Author}, Reviews: {book.Reviews.Count}");
            }
            System.Console.WriteLine("-----------------------");
        }
    }


}