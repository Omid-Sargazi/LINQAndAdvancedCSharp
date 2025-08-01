namespace AdventureWorksLINQ.AdventureWorks.Application.Features.Orders.Queries.GetTopOrders
{
    public class TopOrderDto
    {
        public int SalesOrderID { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
    }
}