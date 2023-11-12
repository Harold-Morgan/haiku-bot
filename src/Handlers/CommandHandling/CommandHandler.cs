using Haiku.Bot.Handlers.CommandHandling;
using Telegram.Bot;
using Telegram.Bot.Types;

public class CommandHandler
{
    private readonly IEnumerable<ICommand> _commands;

    public CommandHandler(IEnumerable<ICommand> commands)
    {
        _commands = commands;
    }

    public async Task ParseAndHandleCommand(Update update, string input, CancellationToken token)
    {
        var (command, commandName) = Parse(input);

        var commandParams = new CommandParameters
        {
            Update = update
        };

        if (command == null)
            return;

        if (commandName == "help")
            commandParams.TextParams = _commands.Select(x => x.GetType().Name.ToLower() + " - " + x.Description).ToArray();
        else
            commandParams.TextParams = input.Split(' ')[1..];

        await command.HandleCommand(commandParams, token);
    }

    private (ICommand?, string) Parse(string input)
    {
        var commandRaw = input[1..].ToLower();
        var commandName = commandRaw.Split(' ')[0];

        //remove /help@SamuraichBot postfix that tg adds
        if (commandName.Contains('@'))
            commandName = commandName.Split('@')[0];

        var command = _commands.SingleOrDefault(x => x.GetType().Name.ToLower() == commandName);

        return (command, commandName);
    }
}