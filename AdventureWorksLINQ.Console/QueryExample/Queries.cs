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

        // foreach (var p in enumerablePeople)
        //     {

        //         System.Console.WriteLine(p.LastName);
        //     }
    }
    
   
}