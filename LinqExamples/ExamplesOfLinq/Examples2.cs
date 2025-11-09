namespace LinqExamples.ExamplesOfLinq
{
    public class Examples2
    {
        public static void Execute()
        {
            var orders = new List<Order>
            {
                new Order { Id = 1, Customer = "Alice" },
                new Order { Id = 2, Customer = "Bob" }
            };

            var orderItems = new List<OrderItem>
            {
                new OrderItem { OrderId = 1, ProductId = 101, Qty = 2 },
                new OrderItem { OrderId = 1, ProductId = 102, Qty = 1 },
                new OrderItem { OrderId = 2, ProductId = 103, Qty = 3 }
            };

            var products = new List<Product>
            {
                new Product { Id = 101, Name = "Laptop", Price = 1000 },
                new Product { Id = 102, Name = "Mouse", Price = 25 },
                new Product { Id = 103, Name = "Keyboard", Price = 50 }
            };

            var result = orders.Join(orderItems,
            o => o.Id,

oi => oi.OrderId,
(o, oi) => new { CusName = o.Customer, orderId = o.Id, oi.Qty, oi.ProductId }

            ).Join(products, x => x.ProductId, p => p.Id, (x, p) => new { x.CusName, x.orderId, ProductName = p.Name, p.Price, x.Qty }).GroupBy(x => new { x.orderId, x.CusName }).Select(g => new
            {
                orderId = g.Key.orderId,
                Customer = g.Key.CusName,
                Items = g.Select(i => new { i.ProductName, i.Qty, LineTotal = i.Price * i.Qty }).ToList(),
                OrderQty = g.Sum(i => i.Qty * i.Price)
            }).ToList();
            
        }
    }



    public class Order
    {
        public int Id { get; set; }
        public string Customer { get; set; }
    }

    public class OrderItem
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Qty { get; set; }
    }

    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }


}