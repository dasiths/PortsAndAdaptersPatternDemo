using System.Text.Json;
using PortsAndAdaptersPatternDemo.RequestProcessing.Features.GetOrder;
using Spectre.Console.Cli;

internal class GetOrderCommand : Command<GetOrderCommand.Settings>
{
    private readonly GetOrderRequestProcessor _getOrderRequestProcessor;

    public class Settings : CommandSettings
    {
        [CommandArgument(0, "[OrderId]")]
        public int OrderId { get; set; }
    }
    
    public GetOrderCommand(GetOrderRequestProcessor getOrderRequestProcessor)
    {
        _getOrderRequestProcessor = getOrderRequestProcessor;
    }

    public override int Execute(CommandContext context, Settings settings)
    {
        var result = _getOrderRequestProcessor.HandleAsync(new GetOrderRequest()
        {
            OrderId = settings.OrderId
        }, CancellationToken.None).ConfigureAwait(false).GetAwaiter().GetResult();

        Console.WriteLine(JsonSerializer.Serialize(result, new JsonSerializerOptions()
        {
            WriteIndented = true
        }));

        return 0;
    }
}