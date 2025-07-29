using AdventureWorksLINQ.Models;
using Microsoft.EntityFrameworkCore;

namespace AdventureWorksLINQ.Console.Product
{
    public class TwentyOrdersIn2013
    {
        private static AdventureWorks2019Context _context = new AdventureWorks2019Context();
        public static void Run()
        {
            int year = 2013;
            var orders = _context.SalesOrderHeaders
            .Where(o => o.OrderDate.Year == year)
            .Include(o => o.Customer)
            .ThenInclude(c => c.Person)
            .Select(o => new
            {
                o.SalesOrderId,
                o.OrderDate,
                CustomerName = o.Customer.Person != null ? o.Customer.Person.FirstName + " " + o.Customer.Person.LastName : "N/A",
            })
            .OrderByDescending(o => o.OrderDate)
            .Take(20).ToList();

            foreach (var order in orders)
            {
                System.Console.WriteLine($"Order ID: {order.SalesOrderId}, Date: {order.OrderDate}, Customer: {order.CustomerName}");
            }
        }
    }
}