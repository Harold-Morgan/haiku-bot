using Haiku.Bot.Handlers.CommandHandling;

public interface ICommand
{
    public Task HandleCommand(CommandParameters @params, CancellationToken token = new());
}