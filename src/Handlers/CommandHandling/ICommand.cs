using Haiku.Bot.Handlers.CommandHandling;

public interface ICommand
{
    public string Description { get; }
    public Task HandleCommand(CommandParameters @params, CancellationToken token = new());
}