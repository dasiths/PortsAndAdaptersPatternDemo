using Spectre.Console.Cli;

internal class GetOrderCommand : Command<GetOrderCommand.Settings>
{
    public class Settings : CommandSettings
    {
        [CommandArgument(0, "[OrderId]")]
        public string OrderId { get; set; }
    }


    public override int Execute(CommandContext context, Settings settings)
    {
        return 0;
    }
}