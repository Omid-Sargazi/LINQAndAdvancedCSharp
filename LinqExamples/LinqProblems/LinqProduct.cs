namespace LinqExamples.LinqProblems
{
    public class LinqProduct
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }


        public static void Execute()
        {
            var products = new List<LinqProduct>
            
                {
                new LinqProduct { Id = 1, Name = "Laptop", Price = 1500, Category = "Electronics" },
                new LinqProduct { Id = 2, Name = "Mouse", Price = 50, Category = "Electronics" },
                new LinqProduct { Id = 3, Name = "Desk", Price = 800, Category = "Furniture" },
                new LinqProduct { Id = 4, Name = "Monitor", Price = 1200, Category = "Electronics" },
                new LinqProduct { Id = 5, Name = "Chair", Price = 300, Category = "Furniture" }
                };

                var expensiveProducts = products
                .Where(p=>p.Price>1000)
                .OrderByDescending(p=>p).Select(p=>new{p.Name,p.Category});



                var products2 = new List<LinqProduct>
                {
                 new LinqProduct { Id = 1, Name = "Gaming Laptop", Price = 2000, Category = "Electronics" },
                new LinqProduct { Id = 2, Name = "Wireless Mouse", Price = 50, Category = "Electronics" },
                new LinqProduct { Id = 3, Name = "C# Programming", Price = 60, Category = "Books" },
                new LinqProduct { Id = 4, Name = "Smartphone", Price = 1200, Category = "Electronics" },
                new LinqProduct { Id = 5, Name = "4K Monitor", Price = 800, Category = "Electronics" },
                new LinqProduct { Id = 6, Name = "Science Fiction Novel", Price = 25, Category = "Books" },
                new LinqProduct { Id = 7, Name = "Noise Cancelling Headphones", Price = 300, Category = "Electronics" },
                new LinqProduct { Id = 8, Name = "Math Textbook", Price = 95, Category = "Books" },
                new LinqProduct { Id = 9, Name = "Office Chair", Price = 250, Category = "Furniture" },
                new LinqProduct { Id = 10, Name = "Desk", Price = 400, Category = "Furniture" }
                };

                var categoryStats = products2.GroupBy(p=>p.Category)
                .Select(g=> new{Ave = g.Average(g=>g.Price),Num=g.Count()}).OrderByDescending(p=>p.Ave);
        }
    }
}