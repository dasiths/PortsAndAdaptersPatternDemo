using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using PortsAndAdaptersPatternDemo.RequestProcessing.Features.GetProduct;
using Spectre.Console.Cli;

internal sealed class GetProductCommand : Command<GetProductCommand.Settings>
{
    public sealed class Settings : CommandSettings
    {
        [CommandArgument(0, "[ProductId]")]
        public int ProductId { get; set; }
    }

    private readonly GetProductRequestProcessor _getProductRequestProcessor;

    public GetProductCommand(GetProductRequestProcessor getProductRequestProcessor)
    {
        _getProductRequestProcessor = getProductRequestProcessor;
    }

    public override int Execute([NotNull] CommandContext context, [NotNull] Settings settings)
    {
        var result = _getProductRequestProcessor.HandleAsync(new GetProductRequest()
        {
            ProductId = settings.ProductId
        }, CancellationToken.None).ConfigureAwait(false).GetAwaiter().GetResult();

        Console.WriteLine(JsonSerializer.Serialize(result, new JsonSerializerOptions()
        {
            WriteIndented = true
        }));

        return 0;
    }
}