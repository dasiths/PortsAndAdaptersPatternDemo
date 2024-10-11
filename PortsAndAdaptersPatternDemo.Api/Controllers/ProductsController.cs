using Microsoft.AspNetCore.Mvc;
using PortsAndAdaptersPatternDemo.RequestProcessing.Features.GetProduct;

namespace PortsAndAdaptersPatternDemo.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly GetProductRequestProcessor _getProductRequestProcessor;
    public ProductsController(GetProductRequestProcessor getProductRequestProcessor)
    {
        _getProductRequestProcessor = getProductRequestProcessor;
    }

    public async Task<ActionResult<GetProductResponse>> GetProduct(int productId, CancellationToken cancellationToken)
    {
        return await _getProductRequestProcessor.HandleAsync(new GetProductRequest()
        {
            ProductId = productId
        }, cancellationToken);
    }
}

