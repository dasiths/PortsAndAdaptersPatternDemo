using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PortsAndAdaptersPatternDemo.Data;
using PortsAndAdaptersPatternDemo.Models;

namespace PortsAndAdaptersPatternDemo.RequestProcessing.Features.GetOrder
{
    public class GetOrderRequest: IRequest<GetOrderResponse>
    {
        public int OrderId { get; set; }
    }

    public class GetOrderResponse
    {
        public OrderModel? Result { get; set; }
    }

    public class GetOrderRequestProcessor : IRequestHandler<GetOrderRequest, GetOrderResponse>
    {
        private readonly DemoDbContext _demoDbContext;

        public GetOrderRequestProcessor(DemoDbContext demoDbContext)
        {
            _demoDbContext = demoDbContext;
        }

        public async Task<GetOrderResponse> Handle(GetOrderRequest request, CancellationToken cancellationToken)
        {
            var result = await _demoDbContext.Orders.Where(o => o.OrderId == request.OrderId).Select(o =>
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

}
