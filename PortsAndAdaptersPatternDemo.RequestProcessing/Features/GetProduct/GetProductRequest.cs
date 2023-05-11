using MediatR;

namespace PortsAndAdaptersPatternDemo.RequestProcessing.Features.GetProduct
{
    public class GetProductRequest: IRequest<GetProductResponse>
    {
        public int ProductId { get; set; }
    }
}
