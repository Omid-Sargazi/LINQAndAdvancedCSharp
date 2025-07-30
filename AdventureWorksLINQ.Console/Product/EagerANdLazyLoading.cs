using AdventureWorksLINQ.Models;
using Microsoft.EntityFrameworkCore;

namespace AdventureWorksLINQ.Console.Product
{
    public class LoadingProduct
    {
        private static readonly AdventureWorks2019Context _context = new AdventureWorks2019Context();
        public static void Run()
        {
            var person = _context.People.First(p => p.BusinessEntityId == 1);
            // System.Console.WriteLine($"Person:{person.FirstName} {person.LastName}");

            var person2 = _context.People
            .Include(p => p.EmailAddresses)
            .First(p => p.BusinessEntityId == 1);

            System.Console.WriteLine($"Person:{person2.FirstName} {person2.LastName}");

            var lazyOrder = _context.SalesOrderHeaders.First(o => o.SalesOrderId == 43659);
            System.Console.WriteLine($"Order:{lazyOrder.SalesOrderId},Total:{lazyOrder.TotalDue}");

            foreach (var detail in lazyOrder.SalesOrderDetails)
            {
                System.Console.WriteLine($"Detail:{detail.SalesOrderDetailId},QTY:{detail.OrderQty}");
            }


            var eagerOrder = _context.SalesOrderHeaders
            .Include(o => o.SalesOrderDetails)
            .First(o => o.SalesOrderId == 43659);
            System.Console.WriteLine($"Order:{eagerOrder.SalesOrderId},Total:{eagerOrder.TotalDue}");

            foreach (var detail in eagerOrder.SalesOrderDetails)
            {
                System.Console.WriteLine($"Detail:{detail.SalesOrderDetailId},QTY: {detail.OrderQty}");
            }


            foreach (var email in person2.EmailAddresses)
                {

                    System.Console.WriteLine("Hello, World!");

                    System.Console.WriteLine($"Email:{email.EmailAddress1}");
                }
        }
    }
}