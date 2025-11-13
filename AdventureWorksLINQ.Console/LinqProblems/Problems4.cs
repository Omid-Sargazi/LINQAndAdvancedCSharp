using AdventureWorksLINQ.Console.Models;

namespace AdventureWorksLINQ.Console.LinqProblems
{
    public class Problems4
    {
        private static AdventureWorks2019Context db = new AdventureWorks2019Context();

        public static void Execute()
        {
            int page = 1;
            var pageSize = 20;
            var minPrice = (decimal?)100;
            var maxPrice = (decimal?)1000;
            int? categoryId = null;
            string search = "Bike";

            var paged = GetProductsAsync(minPrice, maxPrice, categoryId, search, page, pageSize, "price", true).GetAwaiter().GetResult();
            System.Console.WriteLine($"Total items matching filters: {paged.TotalCount}");
        }

        private static async Task<PagedResult<ProductDto>> GetProductsAsync(
            decimal? minPrice,
            decimal? maxPrice,
            int? ProductCategoryId,
            string? search,
            int page,
            int pageSize,
            string sortBy = "name",
            bool asc=true
        )
        {
             if (page <= 0) page = 1;
            if (pageSize <= 0) pageSize = 20;

            
        }
    }


    public class ProductDto
    {
        public int ProductId { get; set; }
        public string Name { get; set; } = "";
        public decimal? ListPrice { get; set; }
        public string? SubcategoryName { get; set; }
        public string? CategoryName { get; set; }
    }
    
    public class PagedResult<T>
    {
        public int TotalCount { get; set; }
         public List<T> Items { get; set; } = new();
    }
}