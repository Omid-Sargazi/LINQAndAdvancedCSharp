namespace LinqExamples.Problems4
{
    public class LinqOfExamples
    {
        public static void Execute()
        {
            var products = new List<Product>
            {
                new Product { Id = 1, Name = "لپ‌تاپ", Category = "الکترونیکی", Price = 15000000, StockQuantity = 10, MinimumStockLevel = 5 },
                new Product { Id = 2, Name = "ماوس", Category = "الکترونیکی", Price = 500000, StockQuantity = 50, MinimumStockLevel = 20 },
                new Product { Id = 3, Name = "کیبورد", Category = "الکترونیکی", Price = 800000, StockQuantity = 30, MinimumStockLevel = 15 },
                new Product { Id = 4, Name = "مانیتور", Category = "الکترونیکی", Price = 8000000, StockQuantity = 8, MinimumStockLevel = 10 },
                new Product { Id = 5, Name = "هندزفری", Category = "الکترونیکی", Price = 1200000, StockQuantity = 25, MinimumStockLevel = 10 },
                new Product { Id = 6, Name = "کابل HDMI", Category = "الکترونیکی", Price = 300000, StockQuantity = 100, MinimumStockLevel = 50 }
            };

            var sales = new List<Sale>
            {
                new Sale { Id = 1, ProductId = 1, SaleDate = DateTime.Now.AddDays(-10), Quantity = 2, TotalAmount = 30000000 },
                new Sale { Id = 2, ProductId = 2, SaleDate = DateTime.Now.AddDays(-8), Quantity = 5, TotalAmount = 2500000 },
                new Sale { Id = 3, ProductId = 3, SaleDate = DateTime.Now.AddDays(-5), Quantity = 3, TotalAmount = 2400000 },
                new Sale { Id = 4, ProductId = 1, SaleDate = DateTime.Now.AddDays(-3), Quantity = 1, TotalAmount = 15000000 },
                new Sale { Id = 5, ProductId = 5, SaleDate = DateTime.Now.AddDays(-1), Quantity = 4, TotalAmount = 4800000 },
                new Sale { Id = 6, ProductId = 2, SaleDate = DateTime.Now.AddDays(-1), Quantity = 8, TotalAmount = 4000000 }
            };


            var StockLessThanMinimumStock = products.Where(p => p.StockQuantity < p.MinimumStockLevel)
            .Select(p => new { Name = p.Name, StockQty = p.StockQuantity, MinQty = p.MinimumStockLevel, Shortage = p.MinimumStockLevel - p.StockQuantity });

            var highSellingProducts = sales.GroupBy(s => s.ProductId).Where(g => g.Sum(s => s.Quantity) > 5).Select(p => p.Key);
            var lowStockProducts = products.Where(p => p.StockQuantity < 15).Select(p => p.Name);

            var needReorder = products.Where(p => p.StockQuantity < p.MinimumStockLevel).Select(p => new
            {
                p.Name,
                p.StockQuantity,
                p.MinimumStockLevel,
                Shortage = p.MinimumStockLevel - p.StockQuantity
            });

            



        }

    }

    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public int MinimumStockLevel { get; set; }
    }

    public class Sale
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public DateTime SaleDate { get; set; }
        public int Quantity { get; set; }
        public decimal TotalAmount { get; set; }
    }

}