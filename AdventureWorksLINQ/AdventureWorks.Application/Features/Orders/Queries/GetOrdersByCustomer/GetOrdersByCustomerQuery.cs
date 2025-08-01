using MediatR;

namespace AdventureWorksLINQ.AdventureWorks.Application.Features.Orders.Queries.GetOrdersByCustomer
{
     public class GetOrdersByCustomerQuery : IRequest<List<CustomerOrderDto>>
    {
        public int CustomerId { get; set; }
        public GetOrdersByCustomerQuery(int customerId)
        {
            CustomerId = customerId;
        }
    }
}