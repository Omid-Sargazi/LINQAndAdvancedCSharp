using AdventureWorksLINQ.Models;
using MediatR;

namespace AdventureWorksLINQ.AdventureWorks.Application.Features.Orders.Queries.GetTopOrders
{
    public class GetTopOrdersQuery : IRequest<List<TopOrderDto>>{}


    public class GetTopOrdersQueryHandler : IRequestHandler<GetTopOrdersQuery, List<TopOrderDto>>
    {
        private readonly AdventureWorks2019Context _context;
        public GetTopOrdersQueryHandler(AdventureWorks2019Context context)
        {
            _context = context;
        }
        public async Task<List<TopOrderDto>> Handle(GetTopOrdersQuery request, CancellationToken cancellationToken)
        {
            return await _context.GetTopOrdersFromSPAsync();
        }
    }
}