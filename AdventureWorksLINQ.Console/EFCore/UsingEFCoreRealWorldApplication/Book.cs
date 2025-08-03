namespace AdventureWorksLINQ.Console.EFCore.UsingEFCoreRealWorldApplication
{
    public class Book
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public decimal Price { get; set; }
    }

    public class BookDto
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public decimal Price { get; set; }
    }

    public class BookService
    {
        private readonly LibraryContext _context;
        public BookService(LibraryContext context)
        {
            _context = context;
        }

        public void AddBook(BookDto bookDto)
        {
            var book = new Book
            {
                Title = bookDto.Title,
                Author = bookDto.Author,
                Price = bookDto.Price
            };
            _context.Books.Add(book);
            _context.SaveChanges();
        }

        public List<BookDto> GetAllBooks()
        {
            return _context.Books
            .Select(b => new BookDto
            {
                BookId = b.BookId,
                Title = b.Title,
                Price = b.Price,
                Author = b.Author
            }).ToList();
        }
    }


   
}