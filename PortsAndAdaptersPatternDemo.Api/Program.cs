using System.Reflection;
using PortsAndAdaptersPatternDemo.Data;
using PortsAndAdaptersPatternDemo.RequestProcessing.Features.GetOrder;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddMediatR(m => m.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly(), typeof(GetOrderRequest).Assembly)
    .Lifetime = ServiceLifetime.Scoped);
builder.Services.AddDbContext<DemoDbContext>();

DemoDbContext.SeedData();

var app = builder.Build();

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
