namespace AdventureWorksLINQ.AdventureWorks.Application.Features.Orders.Queries.GetOrdersByCustomer
{
    public class CustomerOrderDto
    {
        public int SalesOrderID { get; set; }
        public DateTime orderDate { get; set; }
        public decimal TotalDue { get; set; }
    }
}