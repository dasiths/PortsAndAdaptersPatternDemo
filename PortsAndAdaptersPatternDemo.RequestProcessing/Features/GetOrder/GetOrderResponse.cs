using PortsAndAdaptersPatternDemo.Models;

namespace PortsAndAdaptersPatternDemo.RequestProcessing.Features.GetOrder;

public class GetOrderResponse
{
    public OrderModel? Result { get; set; }
}