using System.Diagnostics.CodeAnalysis;
using Spectre.Console.Cli;

internal sealed class GetProductCommand : Command<GetProductCommand.Settings>
{
    public sealed class Settings : CommandSettings
    {
        [CommandArgument(0, "[ProductId]")]
        public string ProductId { get; set; }
    }

    public override int Execute([NotNull] CommandContext context, [NotNull] Settings settings)
    {
        return 0;
    }
}