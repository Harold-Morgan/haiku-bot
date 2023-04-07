public class CommandHandler
{
    private readonly IEnumerable<ICommand> _commands;

    public CommandHandler(IEnumerable<ICommand> commands)
    {
        _commands = commands;
    }

    internal string? ParseCommand(string input)
    {
        var commandName = input[1..].ToLower();

        var command = _commands.Single(x => x.GetType().Name.ToLower() == commandName);

        if (commandName == "help")
            return command.HandleCommand(_commands.Select(x => x.GetType().Name.ToLower()).ToArray());

        var result = command.HandleCommand();

        return result;
    }
}