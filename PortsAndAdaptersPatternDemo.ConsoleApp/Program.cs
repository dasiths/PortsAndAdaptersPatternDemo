using System.ComponentModel;
using Spectre.Console;
using Spectre.Console.Cli;

var app = new CommandApp();
app.Configure(config =>
{
    config.AddCommand<GetOrderCommand>("GetOrder");
    config.AddCommand<GetProductCommand>("GetProduct");
});

return app.Run(args);