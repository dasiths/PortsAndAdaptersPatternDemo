using System.Text.Json;
using MediatR;
using PortsAndAdaptersPatternDemo.RequestProcessing.Features.GetOrder;
using Spectre.Console.Cli;

internal class GetOrderCommand : Command<GetOrderCommand.Settings>
{
    private readonly IMediator _mediator;

    public class Settings : CommandSettings
    {
        [CommandArgument(0, "[OrderId]")]
        public int OrderId { get; set; }
    }
    
    public GetOrderCommand(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override int Execute(CommandContext context, Settings settings)
    {
        var result = _mediator.Send(new GetOrderRequest()
        {
            OrderId = settings.OrderId
        }).ConfigureAwait(false).GetAwaiter().GetResult();

        Console.WriteLine(JsonSerializer.Serialize(result, new JsonSerializerOptions()
        {
            WriteIndented = true
        }));

        return 0;
    }
}