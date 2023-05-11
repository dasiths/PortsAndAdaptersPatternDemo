using MediatR;
using Microsoft.AspNetCore.Mvc;
using PortsAndAdaptersPatternDemo.RequestProcessing.Features.GetProduct;

namespace PortsAndAdaptersPatternDemo.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<ActionResult<GetProductResponse>> GetProduct(int productId)
    {
        return await _mediator.Send(new GetProductRequest()
        {
            ProductId = productId
        });
    }
}