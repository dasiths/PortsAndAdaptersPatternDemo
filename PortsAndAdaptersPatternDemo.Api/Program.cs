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

//app.MapGet("/products/{productId}/", (int productId, GetProductRequestProcessor getProductRequestProcessor, CancellationToken cancellationToken) => getProductRequestProcessor.HandleAsync(new GetProductRequest()
//{
//    ProductId = productId
//}, cancellationToken));

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.MapControllers();
app.Run();
