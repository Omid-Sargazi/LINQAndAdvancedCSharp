using AdventureWorksLINQ.Console.Models;
using Microsoft.EntityFrameworkCore;

namespace AdventureWorksLINQ.Console.QueryExample;
public class Queries
{

    private static AdventureWorks2019Context db = new AdventureWorks2019Context();
    public static void Run()
    {
        System.Console.WriteLine("hello");
        var people = db.People.ToList().Take(10);
        var p = db.People.ToList();
        var queryablePeople = db.People.Where(p => p.LastName.StartsWith("A"));
        var enumerablePeople = db.People.ToList().Where(p => p.LastName.StartsWith("C"));

        var result = db.People
        .Where(p => p.FirstName.StartsWith("A"))
        .Select(p => new { p.FirstName, p.LastName })
        .OrderBy(p => p.FirstName)
        .Take(5)
        .ToList();

        // var orders = db.SalesOrderHeaders.ToList().Take(10);
        var orders = db.SalesOrderHeaders
        .Include(o => o.Customer)
        .ToList().Take(10);
        foreach (var order in orders)
        {
            System.Console.WriteLine(order.Customer.Person.FirstName);
        }

        var query20 = from d in db.SalesOrderDetails
                      join h in db.SalesOrderHeaders on
        d.SalesOrderId equals h.SalesOrderId
                      join prod in db.Products on d.ProductId equals prod.ProductId
                      where h.Status == 5
                      group d by new { prod.ProductId, prod.Name } into g
                      orderby g.Sum(x => x.LineTotal) descending
                      select new
                      {
                          ProductId = g.Key.ProductId,
                          ProductName = g.Key.Name,
                          TotalSale = g.Sum(x => x.LineTotal)
                      };

        var employees = db.Employees.Where(e => e.HireDate >= DateOnly.FromDateTime(new DateTime(2010, 1, 1))).OrderByDescending(e => e.HireDate)
        .Select(e => new
        {
            e.BusinessEntityId,
            e.NationalIdnumber,
            e.HireDate,
            e.JobTitle,
            e.MaritalStatus,
            e.Gender,
        });
        

    }
    
   
}