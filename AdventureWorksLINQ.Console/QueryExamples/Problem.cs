using AdventureWorksLINQ.Console.Models;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using Xunit.Sdk;

namespace AdventureWorksLINQ.Console.QueryExamples
{
    public class Problem
    {
        private static AdventureWorks2019Context db = new AdventureWorks2019Context();
        public static void Run()
        {
            var q = from h in db.SalesOrderHeaders
                    join d in db.SalesOrderDetails on h.SalesOrderId equals d.SalesOrderId
                    join p in db.Products on d.ProductId equals p.ProductId
                    select new
                    {
                        h.SalesOrderId,
                        h.OrderDate,
                        d.SalesOrderDetailId,
                        Product = p.Name,
                        d.OrderQty,
                        d.UnitPrice,
                        d.LineTotal
                    };

            var list = q.AsNoTracking().Take(10).ToList();

            foreach (var order in list)
            {
                System.Console.WriteLine($"OrderID: {order.SalesOrderId}, Date: {order.OrderDate}, Product: {order.Product}, Qty: {order.OrderQty}, LineTotal: {order.LineTotal}");
            }

            var q2 = db.SalesOrderHeaders
            .Join(db.SalesOrderDetails,
                h => h.SalesOrderId,
                d => d.SalesOrderId,
                (h, d) => new { Header = h, Detail = d }
            )
            .Join(db.Products,
                temp => temp.Detail.ProductId,
                p => p.ProductId,
                (temp, p) => new
                {
                    temp.Header.SalesOrderId,
                    temp.Header.OrderDate,
                    temp.Detail.SalesOrderDetailId,
                    Product = p.Name,
                    temp.Detail.OrderQty,
                    temp.Detail.UnitPrice,
                    temp.Detail.LineTotal
                }
            );

            var list2 = q.AsNoTracking().Take(10).ToList();

            var qqq = db.SalesOrderHeaders
            .AsNoTracking()
            .Where(h => h.OrderDate.Year == 2014 && h.OnlineOrderFlag == true)
            .Join(db.SalesOrderDetails,
                h => h.SalesOrderId,
                d => d.SalesOrderId,
                (h, d) => new { h.SalesOrderId, h.OrderDate, d.ProductId, d.OrderQty, d.UnitPrice, d.LineTotal }
            )
            .Join(db.Products,
                temp => temp.ProductId,
                p => p.ProductId,
                (temp, p) => new
                {
                    temp.SalesOrderId,
                    temp.OrderDate,
                    ProductName = p.Name,
                    temp.OrderQty,
                    temp.UnitPrice,
                    temp.LineTotal
                }
            ).OrderByDescending(x => x.LineTotal)
            .Take(10);

            var sql = qqq.ToQueryString();
            var list3 = q.ToList();
            System.Console.WriteLine("Generated SQL:");
            System.Console.WriteLine(sql);
        }

    }

    public class Problem2
    {
        private static AdventureWorks2019Context db = new AdventureWorks2019Context();
        public static void Run()
        {
            var q = from h in db.SalesOrderHeaders
                    join d in db.SalesOrderDetails
            on h.SalesOrderId equals d.SalesOrderId into g
                    from d in g.DefaultIfEmpty()
                    select new
                    {
                        h.SalesOrderId,
                        h.OrderDate,
                        DetailId = (int?)d.SalesOrderDetailId,
                        ProductId = (int?)d.ProductId,
                        d.OrderQty,
                        d.LineTotal

                    };

            var qq = db.SalesOrderHeaders
        .AsNoTracking()
        .Select(h => new
        {
            h.SalesOrderId,
            h.OrderDate,
            h.CustomerId,
            h.TotalDue,

            Details = h.SalesOrderDetails.Select(d => new
            {
                d.SalesOrderDetailId,
                d.ProductId,
                d.OrderQty,
                d.LineTotal
            }).ToList(),

        });
        }


        public static void RunQuery()
        {
            var badQuery = db.Products.ToList();//Bad Query.
            var flteredBad = badQuery.Where(p => p.ListPrice > 500).ToList();//Bad Filter

            var goodQuery = db.Products
            .Where(p => p.ListPrice > 500)
            .Select(p => new { p.ProductId, p.Name, p.ListPrice })
            .AsNoTracking()
            .ToList();

            var badpaging = db.Products.ToList()
            .OrderByDescending(p => p.ModifiedDate)
            .Skip(0).Take(10);


            var goodPaging = db.Products
            .OrderByDescending(p => p.ModifiedDate)
            .Select(p => new { p.ProductId, p.Name, p.ModifiedDate })
            .Skip(0).Take(10)
            .AsNoTracking().ToList();

            var hasProducts = db.Products.Count() > 0;

            var hasProductsOptimized = db.Products.Any();


            var badJoin = db.SalesOrderHeaders
            .Join(db.Customers,
                o => o.CustomerId,
                c => c.CustomerId,
                (o, c) => new { Order = o, Customer = c }
            ).ToList();

            var goodJoin = db.SalesOrderHeaders
            .Join(db.Customers,
                o => o.CustomerId,
                c => c.CustomerId,
                (o, c) => new
                {
                    o.SalesOrderId,
                    o.OrderDate,
                    o.TotalDue,
                    CustomerName = c.Person.FirstName
                }
            ).AsNoTracking()
            .Take(100).ToList();


            var orders = db.SalesOrderHeaders.Take(10).ToList();
            foreach (var order in orders)
            {
                order.SalesOrderDetails = db.SalesOrderDetails
                .Where(d => d.SalesOrderId == order.SalesOrderId).ToList();//N+1 Query
            }

            var optimizedReport = db.SalesOrderHeaders
            .Take(10)
            .GroupJoin(db.SalesOrderDetails,
                o => o.SalesOrderId,
                d => d.SalesOrderId,
                (order, detail) => new
                {
                    Order = order,
                    DetailCount = detail.Count(),
                    TotalQuantity = detail.Sum(d => d.OrderQty)
                }
            ).AsNoTracking().ToList();
        }
    }
}