using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using MediatR;
using PortsAndAdaptersPatternDemo.RequestProcessing.Features.GetProduct;
using Spectre.Console.Cli;

internal sealed class GetProductCommand : Command<GetProductCommand.Settings>
{
    public sealed class Settings : CommandSettings
    {
        [CommandArgument(0, "[ProductId]")]
        public int ProductId { get; set; }
    }

    private readonly IMediator _mediator;

    public GetProductCommand(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override int Execute([NotNull] CommandContext context, [NotNull] Settings settings)
    {
        var result = _mediator.Send(new GetProductRequest()
        {
            ProductId = settings.ProductId
        }).ConfigureAwait(false).GetAwaiter().GetResult();

        Console.WriteLine(JsonSerializer.Serialize(result, new JsonSerializerOptions()
        {
            WriteIndented = true
        }));

        return 0;
    }
}