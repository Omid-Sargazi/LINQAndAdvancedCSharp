using AdventureWorksLINQ.Models;
using MediatR;

namespace AdventureWorksLINQ.AdventureWorks.Application.Features.Orders.Queries.GetOrdersByCustomer
{
    public class GetOrdersByCustomerQueryHandler : IRequestHandler<GetOrdersByCustomerQuery, List<CustomerOrderDto>>
    {
        private readonly AdventureWorks2019Context _context;
        public GetOrdersByCustomerQueryHandler(AdventureWorks2019Context context)
        {
            _context = context;
        }
        public async Task<List<CustomerOrderDto>> Handle(GetOrdersByCustomerQuery request, CancellationToken cancellationToken)
        {
            return await _context.GetOrdersByCustomerAsync(request.CustomerId);
        }
    }
}