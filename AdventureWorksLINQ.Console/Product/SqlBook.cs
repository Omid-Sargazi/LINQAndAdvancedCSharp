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

            var query = (from e in db.Employees

                         join p in db.BusinessEntities
                         on e.BusinessEntityId equals p.BusinessEntityId
                         where EF.Functions.Like(e.JobTitle, "Sales Representative%")
                         select new
                         {
                             e.HireDate,
                             e.BusinessEntity.FirstName,
                             e.BusinessEntity.LastName
                         }
            );


            var countries = db.Employees
            .Include(e => e.BusinessEntity)
            .Include(e => e.BusinessEntity.BusinessEntity.BusinessEntityAddresses)
            .ThenInclude(bea => bea.Address)
            .ThenInclude(a => a.StateProvince)
            .Where(e => EF.Functions.Like(e.JobTitle, "Sales Representative%")
                && e.BusinessEntity.BusinessEntity.BusinessEntityAddresses.Any(bea => bea.Address.StateProvince.CountryRegionCode == "US")
            ).Select(e => new
            {
                e.BusinessEntity.FirstName,
                e.BusinessEntity.LastName,
                postalCode = e.BusinessEntity.BusinessEntity.BusinessEntityAddresses.First().Address.PostalCode,
                CountryRegionCode = e.BusinessEntity.BusinessEntity.BusinessEntityAddresses.First().Address.StateProvince.CountryRegionCode
            }).ToList();


            foreach (var item in countries)
            {
                System.Console.WriteLine(item+"Contries");
            }
            

            



            
        }
    }
}