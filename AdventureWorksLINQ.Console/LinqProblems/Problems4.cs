using AdventureWorksLINQ.Console.Models;
using Microsoft.EntityFrameworkCore;

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

            IQueryable<AdventureWorksLINQ.Console.Models.Product> q = db.Products.AsQueryable();

            if (minPrice.HasValue)
            {
                q = q.Where(p => p.ListPrice >= minPrice.Value);
            }

            if (ProductCategoryId.HasValue)
            {
                q = q.Where(p => p.ProductSubcategory != null && p.ProductSubcategory.ProductCategoryId == ProductCategoryId.Value);
            }


            if (!string.IsNullOrWhiteSpace(search))
            {
                var s = search.Trim();
                q = q.Where(p => EF.Functions.Like(p.Name, $"%{s}%") || EF.Functions.Like(p.ProductNumber, $"%{s}%"));
            }

            var totalCount = await q.CountAsync().ConfigureAwait(false);

            q = (sortBy.ToLower(), asc) switch
            {
                ("price", true) => q.OrderBy(p => p.ListPrice).ThenBy(p => p.ProductId),
                ("price", false) => q.OrderByDescending(p => p.ListPrice).ThenBy(p => p.ProductId),
                _ => (asc ? q.OrderBy(p => p.Name).ThenBy(p => p.ProductId) : q.OrderByDescending(p => p.Name).ThenBy(p => p.ProductId))
            };

            var pageQuery = q
            .Select(p => new ProductDto
            {
                ProductId = p.ProductId,
                Name = p.Name,
                ListPrice = p.ListPrice,
                SubcategoryName = p.ProductSubcategory != null ? p.ProductSubcategory.Name : null,
                CategoryName = p.ProductSubcategory != null && p.ProductSubcategory.ProductCategory != null ? p.ProductSubcategory.ProductCategory.Name : null
            }).AsNoTracking().Skip((page - 1) * pageSize).Take(pageSize);

            var items = await pageQuery.ToListAsync().ConfigureAwait(false);

            return new PagedResult<ProductDto>
            {
                TotalCount = totalCount,
                Items = items
            };


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