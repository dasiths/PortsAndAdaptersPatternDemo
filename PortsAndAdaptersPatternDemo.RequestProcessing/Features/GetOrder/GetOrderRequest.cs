using MediatR;

namespace PortsAndAdaptersPatternDemo.RequestProcessing.Features.GetOrder
{
    public class GetOrderRequest: IRequest<GetOrderResponse>
    {
        public int OrderId { get; set; }
    }
}
