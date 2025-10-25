namespace LinqExamples.Problem1
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public SubProduct SubProduct { get; set; }

    }

    public class SaleItem
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; } // price per item
    }

    public class Sales
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public ICollection<SaleItem> SaleItems { get; set; }
    }

    public class SubProduct
    {
        public int Id { get; set; }
        public string Title { get; set; }
    }
    public class ProblemsOfLinq
    {
        public static void Execute()
        {


            var categories = new List<SubProduct>
            {
                new SubProduct{Id=100,Title="IT"},
                new SubProduct{Id=101,Title="Electronics"},
                new SubProduct{Id=102,Title="Study"},
                new SubProduct{Id=103,Title="Toys"},
                new SubProduct{Id=104,Title="Clothes"},
            };
            
             var products = new List<Product>
            {
                new Product{Id=1,Name="Laptop",SubProduct=categories.First(c=>c.Title=="IT")},
                new Product{Id=2,Name="TV",SubProduct=categories.First(c=>c.Title=="Electronics")},
                new Product{Id=3,Name="Car",SubProduct= categories.First(c=>c.Title=="Toys") },
                new Product{Id=5,Name="SmartWatch",SubProduct=categories.First(c=>c.Title=="IT")},
                new Product{Id=6,Name="Bus",SubProduct=categories.First(c=>c.Title=="Toys")},
                new Product{Id=7,Name="Pants",SubProduct=categories.First(c=>c.Title=="Clothes")},
            };
        }
    }
}