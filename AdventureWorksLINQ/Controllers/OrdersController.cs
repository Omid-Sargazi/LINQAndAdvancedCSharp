using AdventureWorksLINQ.AdventureWorks.Application.Features.Orders.Queries.GetOrdersByCustomer;
using AdventureWorksLINQ.AdventureWorks.Application.Features.Orders.Queries.GetTopOrders;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace AdventureWorksLINQ.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IMediator _mediator;
        public OrdersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("top")]
        public async Task<IActionResult> GetTopOrders()
        {
            var result = await _mediator.Send(new GetTopOrdersQuery());
            return Ok(result);
        }

        [HttpGet("by-customer/{customerId}")]
        public async Task<IActionResult> GetOrdersByCustomer(int customerId)
        {
            var result = await _mediator.Send(new GetOrdersByCustomerQuery(customerId));
            return Ok(result);
        }
    }
}