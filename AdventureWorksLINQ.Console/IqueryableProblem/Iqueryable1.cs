using AdventureWorksLINQ.Console.Models;
using Microsoft.EntityFrameworkCore;

namespace AdventureWorksLINQ.Console.IqueryableProblem
{
    public class Iqueryable1
    {
            private static AdventureWorks2019Context db = new AdventureWorks2019Context();

        public static void Run()
        {
            int page = 1;
            int pageSize = 10;
            bool desc = false;
            Execute(page, pageSize, false);
        }
        
        private static void Execute(int page, int pageSize,bool desc)
        {
            if (page < 1) page = 1;

            if (pageSize <= 0) pageSize = 10;

            var source = db.Products.AsNoTracking();

            var ordered = desc ? source.OrderByDescending(p => p.Name) : source.OrderBy(p => p.Name);

            var totalCount = ordered.Count();

            var pagedQuery = ordered.Skip((page - 1) * pageSize)
            .Take(pageSize);

            string sql = pagedQuery.ToQueryString();
            System.Console.WriteLine("===SQL===");
            System.Console.WriteLine(sql);

            var items = pagedQuery.Select(p => new { p.ProductId, p.Name }).ToList();
            System.Console.WriteLine($"=== Page {page} / Size {pageSize} | Total {totalCount} ===");
            foreach(var it in items)
            {
                System.Console.WriteLine($"{it.ProductId}--{it.Name}");
            }


            
        }
    }
}