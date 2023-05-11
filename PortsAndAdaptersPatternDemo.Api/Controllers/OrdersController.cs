using MediatR;
using Microsoft.AspNetCore.Mvc;
using PortsAndAdaptersPatternDemo.RequestProcessing.Features.GetOrder;

namespace PortsAndAdaptersPatternDemo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrdersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<ActionResult<GetOrderResponse>> GetOrder(int orderId)
        {
            return await _mediator.Send(new GetOrderRequest()
            {
                OrderId = orderId
            });
        }
    }
}
