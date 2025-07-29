using System;
using System.Data.Common;
using AdventureWorksLINQ.Models;

namespace AdventureWorksLINQ.Console.Product
{
    public class ActiveProducts
    {
        private static AdventureWorks2019Context db = new AdventureWorks2019Context();
        public static void Run()
        {
            var activeProducts = db.Products
            .Where(p => p.SellEndDate == null)
            .Select(p => new
            {
                p.Name,
                p.ProductNumber
            }).Take(10)
            .ToList();

           foreach (var product in activeProducts)
            {
                System.Console.WriteLine($"Name: {product.Name}, Number: {product.ProductNumber}");
            }
        }
    }
}