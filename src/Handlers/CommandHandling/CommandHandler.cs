using Haiku.Bot.Handlers.CommandHandling;
using Telegram.Bot;
using Telegram.Bot.Types;

public class CommandHandler
{
    private readonly IEnumerable<ICommand> _commands;

    public CommandHandler(IEnumerable<ICommand> commands, ITelegramBotClient _telegramClient)
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

        if (commandName == "help")
        {
            commandParams.TextParams = _commands.Select(x => x.GetType().Name.ToLower()).ToArray();
        }    
            
        await command.HandleCommand(commandParams, token);
    }

    private (ICommand, string) Parse(string input)
    {
        var commandName = input[1..].ToLower();

        var command = _commands.Single(x => x.GetType().Name.ToLower() == commandName);

        return (command, commandName);
    }
}