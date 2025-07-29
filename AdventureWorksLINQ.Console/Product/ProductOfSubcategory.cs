using AdventureWorksLINQ.Models;
using Microsoft.EntityFrameworkCore;

namespace AdventureWorksLINQ.Console.Product
{
    public class ProductOfSubcategory
    {
        private static AdventureWorks2019Context _context = new AdventureWorks2019Context();

        public static void Run()
        {
            int subcategoryId = 1;
            var products = _context.Products
            .Where(p => p.ProductSubcategoryId == subcategoryId)
            .Include(p => p.ProductSubcategory)
            .ThenInclude(sc => sc.ProductCategory)
            .Select(p => new
            {
                p.Name,
                p.ProductNumber,
                Subcategory = p.ProductSubcategory!.Name,
                Category = p.ProductSubcategory.ProductCategory!.Name
            }).OrderBy(p => p.Name)
            .ToList();

            foreach (var product in products)
            {
                System.Console.WriteLine($"Name: {product.Name}, Number: {product.ProductNumber}, Subcategory: {product.Subcategory}, Category: {product.Category}");
            }
        }
    }
}