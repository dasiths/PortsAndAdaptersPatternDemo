using MediatR;
using Microsoft.EntityFrameworkCore;
using PortsAndAdaptersPatternDemo.Data;
using PortsAndAdaptersPatternDemo.Models;

namespace PortsAndAdaptersPatternDemo.RequestProcessing.Features.GetProduct;

public class GetProductRequestProcessor : IRequestHandler<GetProductRequest, GetProductResponse>
{
    private readonly DemoDbContext _demoDbContext;

    public GetProductRequestProcessor(DemoDbContext demoDbContext)
    {
        _demoDbContext = demoDbContext;
    }

    public async Task<GetProductResponse> Handle(GetProductRequest request, CancellationToken cancellationToken)
    {
        var result = await _demoDbContext.Products.Where(p => p.ProductId == request.ProductId).Select(o =>
            new ProductModel()
            {
                Name = o.Name,
                ProductId = o.ProductId
            }).FirstOrDefaultAsync(cancellationToken);

        return new GetProductResponse()
        {
            Result = result
        };
    }
}