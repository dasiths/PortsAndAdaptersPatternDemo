using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using PortsAndAdaptersPatternDemo.ConsoleApp;
using PortsAndAdaptersPatternDemo.Data;
using PortsAndAdaptersPatternDemo.RequestProcessing.Features.GetOrder;
using Spectre.Console.Cli;

var services = new ServiceCollection();
services.AddMediatR(m => m.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly(), typeof(GetOrderRequest).Assembly)
    .Lifetime = ServiceLifetime.Scoped);
services.AddDbContext<DemoDbContext>();

// Create a type registrar and register any dependencies.
// A type registrar is an adapter for a DI framework.
var registrar = new TypeRegistrar(services);

DemoDbContext.SeedData();

var app = new CommandApp();
app.Configure(config =>
{
    config.AddCommand<GetOrderCommand>("GetOrder");
    config.AddCommand<GetProductCommand>("GetProduct");
});

return app.Run(args);

