namespace AdventureWorksLINQ.Console.EFCore
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public ICollection<BookCategory> BookCategories { get; set; } = new List<BookCategory>();
    }

    public class Book2
    {
        public int BookId { get; set; }
        public string Name { get; set; }
        public ICollection<BookCategory> BookCategories { get; set; } = new List<BookCategory>();
    }

    public class BookCategory
    {
        public int BookId { get; set; }
        public Book Book { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}