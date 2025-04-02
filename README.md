## Project Structure

### 1. API Layer
**Project Name**: `PortsAndAdaptersPatternDemo.Api`

- **Purpose**: This project handles the HTTP requests and routes them to the appropriate request processors.
- **Dependencies**: 
  - `PortsAndAdaptersPatternDemo.Data` for database context.
  - `PortsAndAdaptersPatternDemo.RequestProcessing` for handling requests.

**Key File**: `Program.cs`
```csharp
using System.Reflection;
using PortsAndAdaptersPatternDemo.Data;
using PortsAndAdaptersPatternDemo.RequestProcessing.Features.GetOrder;
using PortsAndAdaptersPatternDemo.RequestProcessing.Features.GetProduct;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddTransient<GetOrderRequestProcessor>();
builder.Services.AddTransient<GetProductRequestProcessor>();
builder.Services.AddDbContext<DemoDbContext>();

DemoDbContext.SeedData();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapControllers();
app.Run();
```

**Key File**: `OrderController.cs`

```csharp
[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    private readonly GetOrderRequestProcessor _getOrderRequestProcessor;

    public OrderController(GetOrderRequestProcessor getOrderRequestProcessor)
    {
        _getOrderRequestProcessor = getOrderRequestProcessor;
    }

    [HttpGet("{orderId}")]
    public async Task<IActionResult> GetOrder(int orderId)
    {
        var result = await _getOrderRequestProcessor.HandleAsync(new GetOrderRequest { OrderId = orderId });
        return Ok(result);
    }
}
```

### 2. Data Layer
**Project Name**: `PortsAndAdaptersPatternDemo.Data`

- **Purpose**: This project manages the database context, domain entity models and data access logic.
- **Dependencies**: 
  - `Microsoft.EntityFrameworkCore` for database operations.

**Key File**: `DemoDbContext.cs`
```csharp
using Microsoft.EntityFrameworkCore;

namespace PortsAndAdaptersPatternDemo.Data
{
    public class DemoDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "DemoDb");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                .HasMany(e => e.Products)
                .WithMany(e => e.Orders);
        }

        public static void SeedData() // only required for dev and testing
        {
            using var context = new DemoDbContext();
            context.Database.EnsureCreated();

            var products = Enumerable.Range(1, 10).Select(i => new Product()
            {
                Name = $"Product {i}",
                ProductId = i
            });

            context.Products.AddRangeAsync(products);
            context.SaveChanges();

            var rnd = new Random();

            var orders = Enumerable.Range(1, 10).Select(i => new Order()
            {
                OrderId = i,
                Customer = $"Customer {i}",
                Products = context.Products.OrderBy(_ => rnd.Next()).Take(5).ToList()
            });

            context.Orders.AddRangeAsync(orders);
            context.SaveChanges();
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}
```

File `Order.cs`

```csharp
public class Order
{
    [Key]
    public int OrderId { get; set; }
    public string Customer { get; set; }
    public List<Product> Products { get; set; }
}
```

### 3. Request Processing Layer
**Project Name**: `PortsAndAdaptersPatternDemo.RequestProcessing`

- **Purpose**: This project implements the use cases and application services. It contains all business logic of the application, acting as the orchestrator between the API and Data layers.
- **Dependencies**: 
  - `PortsAndAdaptersPatternDemo.Data` for accessing the database context.
  - `PortsAndAdaptersPatternDemo.Dtos` for dto models.

**Key File**: `GetOrderRequestProcessor.cs`
```csharp
using Microsoft.EntityFrameworkCore;
using PortsAndAdaptersPatternDemo.Data;
using PortsAndAdaptersPatternDemo.Dtos;

namespace PortsAndAdaptersPatternDemo.RequestProcessing.Features.GetOrder
{
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
                new OrderDto()
                {
                    Customer = o.Customer,
                    OrderId = o.OrderId,
                    Products = o.Products.Select(p => new ProductDto()
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
```

### 4. Model Layer
**Project Name**: `PortsAndAdaptersPatternDemo.Dtos`

- **Purpose**: This project contains only DTO (Data Transfer Object) type objects with minimal logic. It primarily consists of data structures used for communication between layers. These are not domain models that contain business logic.
- **Dependencies**: None.

**Key File**: `OrderDto.cs`
```csharp
public class OrderDto
{
    public int OrderId { get; set; }
    public string Customer { get; set; }
    public List<Product> Products { get; set; }
}
```

## Summary of Dependencies
- `PortsAndAdaptersPatternDemo.Api` depends on `PortsAndAdaptersPatternDemo.Data` and `PortsAndAdaptersPatternDemo.RequestProcessing`.
- `PortsAndAdaptersPatternDemo.RequestProcessing` depends on `PortsAndAdaptersPatternDemo.Data` and `PortsAndAdaptersPatternDemo.Dtos`.

This layout ensures that each project has a clear responsibility and the dependencies are well-defined, promoting a clean and maintainable architecture.
