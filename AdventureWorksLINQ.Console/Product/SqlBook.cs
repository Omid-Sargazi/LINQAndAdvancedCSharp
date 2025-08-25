using AdventureWorksLINQ.Models;
using Microsoft.EntityFrameworkCore;

namespace AdventureWorksLINQ.Console.Product
{
    public class SqlBook
    {
        private static AdventureWorks2019Context db = new AdventureWorks2019Context();
        public static void Run()
        {
            var result = db.Employees
            .Include(e => e.BusinessEntity)
            .Where(e => EF.Functions.Like(e.JobTitle, "Sales Representative%"))
            .Select(e => new
            {
                e.BusinessEntity.FirstName,
                e.BusinessEntity.LastName,
                e.HireDate
            });

            foreach (var items in result)
            {
                System.Console.WriteLine(items);
            }


            var resultLazy = db.Employees
            .Where(e => EF.Functions.Like(e.JobTitle, "Sales Representative%"))
            .Select(e => new
            {
                e.BusinessEntity.FirstName,
                e.BusinessEntity.LastName,
                e.HireDate
            });

            foreach (var item in resultLazy)
            {
                System.Console.WriteLine(item);
            }

            
        }
    }
}