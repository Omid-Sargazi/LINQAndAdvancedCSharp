namespace AdventureWorksLINQ.Console.EFCore
{
    public class EFCoreExample
    {
        public static void Run()
        {
            var book1 = new Book { Id = 1, AuthorId = 10, Name = "C#" };
            var book2 = new Book { Id = 2, AuthorId = 11, Name = "C++" };
            var book3 = new Book { Id = 3, AuthorId = 12, Name = "Python" };

            var author10Books = new List<Book> { book1, book2 };
            var author12Books = new List<Book> { book1 };


            var author10 = new Author { Id = 10, Name = "Omid", Books = author10Books };
            var author11 = new Author { Id = 11, Name = "Saeed", Books = null };
            var author12 = new Author { Id = 12, Name = "Vahid", Books = author12Books };
            var authors = new List<Author> { author10, author11, author12 };

            var bookDetailsDto = new List<BookDetailsDto>();


            foreach (var author in authors)
            {
                if (author.Books != null && author.Books.Count > 1)
                {
                    System.Console.WriteLine($"Author {author.Name} has more than 1 book.");
                    foreach (var book in author.Books)
                    {
                        System.Console.WriteLine($"  - {book.Name}");

                        bookDetailsDto.Add(new BookDetailsDto
                        {
                            Name = book.Name,
                            AuthorName = author.Name
                        });
                    }
                }

            }


            foreach (var detail in bookDetailsDto)
            {

            }

            

        }
    }

    public class Author
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Book> Books { get; set; } = new List<Book>();
    }

    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int AuthorId { get; set; }
    }

    public class BookDetailsDto
    {
        public string Name { get; set; }
        public string AuthorName { get; set; }
    }
}