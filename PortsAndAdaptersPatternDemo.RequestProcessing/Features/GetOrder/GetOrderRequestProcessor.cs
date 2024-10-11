using Microsoft.EntityFrameworkCore;
using PortsAndAdaptersPatternDemo.Data;
using PortsAndAdaptersPatternDemo.Models;

namespace PortsAndAdaptersPatternDemo.RequestProcessing.Features.GetOrder;

public class GetOrderRequestProcessor
{
    private readonly DemoDbContext _demoDbContext;

    public GetOrderRequestProcessor(DemoDbContext demoDbContext)
    {
        _demoDbContext = demoDbContext;
    }

    public async Task<GetOrderResponse> HandleAsync(GetOrderRequest request, CancellationToken cancellationToken)
    {
        var result = await _demoDbContext.Orders.Include(o => o.Products).Where(o => o.OrderId == request.OrderId).Select(o =>
            new OrderModel()
            {
                Customer = o.Customer,
                OrderId = o.OrderId,
                Products = o.Products.Select(p => new ProductModel()
                {
                    ProductId = p.ProductId,
                    Name = p.Name
                }).ToList()
            }).FirstOrDefaultAsync(cancellationToken);

        return new GetOrderResponse()
        {
            Result = result
        };
    }
}