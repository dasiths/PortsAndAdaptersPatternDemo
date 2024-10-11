using Microsoft.AspNetCore.Mvc;
using PortsAndAdaptersPatternDemo.RequestProcessing.Features.GetOrder;

namespace PortsAndAdaptersPatternDemo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly GetOrderRequestProcessor _getOrderRequestProcessor;

        public OrdersController(GetOrderRequestProcessor getOrderRequestProcessor)
        {
            _getOrderRequestProcessor = getOrderRequestProcessor;
        }

        public async Task<ActionResult<GetOrderResponse>> GetOrder(int orderId, CancellationToken cancellationToken)
        {
            return await _getOrderRequestProcessor.HandleAsync(new GetOrderRequest()
            {
                OrderId = orderId                
            }, cancellationToken);
        }
    }
}
