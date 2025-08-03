namespace AdventureWorksLINQ.Console.EFCore
{
    public class Tag
    {
        public int TagId { get; set; }
        public string Name { get; set; }
        public ICollection<TagProduct> TagProducts { get; set; } = new List<TagProduct>();
    }

    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public ICollection<TagProduct> TagProducts { get; set; } = new List<TagProduct>();
    }

    public class TagProduct
    {
        public int TagId { get; set; }
        public Tag Tag { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public DateTime AddedDate { get; set; }
    }
}